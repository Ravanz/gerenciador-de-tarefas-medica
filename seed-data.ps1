# Script para popular o banco de dados com dados de teste
Write-Host "Populando banco de dados vSaude..." -ForegroundColor Green

# Copiar arquivo SQL para o container
docker cp seed-data.sql vsaude-sqlserver:/seed-data.sql

# Executar o script SQL
docker exec vsaude-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P Senha@Forte123 -C -i /seed-data.sql

Write-Host "`nDados inseridos com sucesso!" -ForegroundColor Green
Write-Host "Acesse http://localhost:5000/swagger para testar a API" -ForegroundColor Cyan

