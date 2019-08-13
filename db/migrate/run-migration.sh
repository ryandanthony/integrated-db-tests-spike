#!/bin/sh
echo "DB_USER: $DB_USER"
echo "DB_HOST: $DB_HOST"
echo "DB_PORT: $DB_PORT"
echo "DB_DBNAME: $DB_DBNAME"

flyway  -user=$DB_USER -password=$DB_PASSWORD -url="jdbc:sqlserver://$DB_HOST:$DB_PORT;databaseName=$DB_DBNAME" migrate