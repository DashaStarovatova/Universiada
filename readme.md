docker compose -f 'src\docker-compose.yml' up -d --build 'postgres';

docker exec -it postgres bash;

psql -U admin -d postgres;

create database keycloak;

exit;

exit;

docker compose -f 'src\docker-compose.yml' up -d --build 'keycloak';

dotnet ef database update --startup-project .\src\Web\ --project .\src\Infrastructure\ --connection "Host=localhost;Port=5433;Database=UniversiadaDb;Username=admin;Password=123"
