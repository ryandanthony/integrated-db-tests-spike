.\build.ps1
$dbhost="sqlserver"
$username="sa"
$password="c3kgC5#Adfl*"
$dbname = "spike-db"

docker run --rm --network devtools_default -e "DB_DBNAME=master" -e "DB_USER=$username" -e "DB_PASSWORD=$password" -e "DB_HOST=$dbhost" ${dbname}-dropdb:integration
docker run --rm --network devtools_default -e "DB_DBNAME=master" -e "DB_USER=$username" -e "DB_PASSWORD=$password" -e "DB_HOST=$dbhost" ${dbname}-createdb:integration
docker run --rm --network devtools_default -e "DB_DBNAME=${dbname}" -e "DB_USER=$username" -e "DB_PASSWORD=$password" -e "DB_HOST=$dbhost" ${dbname}-migrate:integration 