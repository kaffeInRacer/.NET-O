"""
Migrate backup_wilayah_indonesia.sql (PostgreSQL COPY format) → MongoDB
Collections: provinces, cities, districts, villages
"""

import re
from pymongo import MongoClient, InsertOne

SQL_FILE   = "backup_wilayah_indonesia.sql"
MONGO_URI  = "mongodb://admin:admin123@localhost:27017"
DB_NAME    = "administrative_db"

TABLES = {
    "provinces": ["id", "name", "created_at", "updated_at"],
    "cities":    ["id", "name", "province_id", "created_at", "updated_at"],
    "districts": ["id", "name", "city_id", "created_at", "updated_at"],
    "villages":  ["id", "name", "postal_code", "district_id", "created_at", "updated_at"],
}


def parse_copy_blocks(sql_file: str) -> dict[str, list[dict]]:
    results = {table: [] for table in TABLES}

    with open(sql_file, "r", encoding="utf-8") as f:
        content = f.read()

    for table, columns in TABLES.items():
        pattern = rf"COPY public\.{table} \([^)]+\) FROM stdin;\n(.*?)\\\."
        match = re.search(pattern, content, re.DOTALL)
        if not match:
            print(f"[WARN] No data found for table: {table}")
            continue

        rows_raw = match.group(1).strip().splitlines()
        for line in rows_raw:
            if not line.strip():
                continue
            values = line.split("\t")
            if len(values) != len(columns):
                continue
            row = {}
            for col, val in zip(columns, values):
                row[col] = None if val == "\\N" else val
            # use original id as _id for easy cross-referencing
            row["_id"] = row.pop("id")
            results[table].append(row)

        print(f"[OK] {table}: {len(results[table])} rows parsed")

    return results


def insert_to_mongo(data: dict[str, list[dict]]):
    client = MongoClient(MONGO_URI)
    db = client[DB_NAME]

    for table, docs in data.items():
        if not docs:
            continue

        collection = db[table]
        collection.drop()

        batch_size = 1000
        total = 0
        for i in range(0, len(docs), batch_size):
            batch = [InsertOne(doc) for doc in docs[i:i + batch_size]]
            collection.bulk_write(batch)
            total += len(batch)

        print(f"[OK] {table}: {total} documents inserted")

    # indexes for fast lookup
    db["cities"].create_index("province_id")
    db["districts"].create_index("city_id")
    db["villages"].create_index("district_id")
    db["villages"].create_index("postal_code")
    print("[OK] Indexes created")

    client.close()


if __name__ == "__main__":
    print("Parsing SQL...")
    data = parse_copy_blocks(SQL_FILE)

    print("\nInserting to MongoDB...")
    insert_to_mongo(data)

    print("\nDone.")
