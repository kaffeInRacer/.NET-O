sln-add-service:
	@projects=$$(find Services -name "*.csproj"); \
	if [ -n "$$projects" ]; then \
		dotnet sln root_directory.slnx add $$projects; \
	fi

sln-remove-service:
	@projects=$$(find Services -name "*.csproj"); \
	if [ -n "$$projects" ]; then \
		dotnet sln root_directory.slnx remove $$projects; \
	fi

sln-add-all:
	@projects=$$(find . -name "*.csproj"); \
	if [ -n "$$projects" ]; then \
		dotnet sln root_directory.slnx add $$projects; \
	fi

sln-remove-all:
	@projects=$$(find . -name "*.csproj"); \
	if [ -n "$$projects" ]; then \
		dotnet sln root_directory.slnx remove $$projects; \
	fi

# Docker Commands
docker-up:
	docker-compose up -d

docker-down:
	docker-compose down

docker-logs:
	docker-compose logs -f

docker-restart:
	docker-compose restart

docker-clean:
	docker-compose down -v

# User Service Migrations
USER_INFRA 	:= Services/User/User.Infrastructure/User.Infrastructure.csproj
USER_API	:= Services/User/User.Api/User.Api.csproj
USER_CTX 	:= AppDbContext

user-db-add:
	@read -p "Migration name: " name; \
	dotnet ef migrations add $$name \
		--project $(USER_INFRA) \
		--startup-project $(USER_API) \
		--context $(USER_CTX) \
		--output-dir Persistence/Migrations

user-db-remove:
	dotnet ef migrations remove \
		--project $(USER_INFRA) \
		--startup-project $(USER_API) \
		--context $(USER_CTX)

user-sql-up:
	dotnet ef database update \
		--project $(USER_INFRA) \
		--startup-project $(USER_API) \
		--context $(USER_CTX)

user-sql-down:
	@read -p "Migration Id: " id; \
	dotnet ef database update $$id \
		--project $(USER_INFRA) \
		--startup-project $(USER_API) \
		--context $(USER_CTX)

