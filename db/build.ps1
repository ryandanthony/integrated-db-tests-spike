$dbname = "spike-db"

Push-Location sqlcmd
#This should prob be an image that is put into a repo instead of built everytime
docker build . -f Dockerfile -t sqlcmd:latest
Pop-Location

Push-Location create-db
docker build . -f Dockerfile -t ${dbname}-createdb:integration
Pop-Location

Push-Location drop-db
docker build . -f Dockerfile -t ${dbname}-dropdb:integration
Pop-Location

Push-Location migrate
docker build . -f Dockerfile -t ${dbname}-migrate:integration
Pop-Location