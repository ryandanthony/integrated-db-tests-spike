#!/bin/bash
echo "DB_USER: $DB_USER"
echo "DB_HOST: $DB_HOST"
echo "DB_PORT: $DB_PORT"
echo "DB_DBNAME: $DB_DBNAME"
echo "FILE: $FILE"
/opt/mssql-tools/bin/sqlcmd -S "$DB_HOST,$DB_PORT" -i $FILE -U $DB_USER -P $DB_PASSWORD