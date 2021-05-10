# pw SQL Cwi123456@

# docker-compose up --force-recreate  -d

# Apos container up rodar comando ou fazer chamada no postman

curl --location --request POST 'http://0.0.0.0:8083/connectors' \
--header 'Content-Type: application/json' \
--data-raw '{
  "name": "inventory-connector", 
  "config": {
    "connector.class": "io.debezium.connector.sqlserver.SqlServerConnector", 
    "database.hostname": "sqlserver", 
    "database.port": "1433", 
    "database.user": "sa", 
    "database.password": "Cwi123456@", 
    "database.dbname": "Origem", 
    "database.server.name": "origem", 
    "table.include.list": "dbo.Lojista,dbo.sku,dbo.skulojista,dbo.produto", 
    "database.history.kafka.bootstrap.servers": "kafka:9092", 
    "database.history.kafka.topic": "dbhistory.origem" 
  }
}'

## Para ver o topicos acesse http://localhost:8080
