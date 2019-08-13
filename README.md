# integrated-db-tests-spike
Spike for how to do integrated db testing with docker


## How to use:

```powershell
cd db
./build.ps1
```
This will build the docker images:
- spike-db-createdb:integration (creates the dataase)
- spike-db-migrate:integration (loads the schema using flyway)
- spike-db-dropdb:integration (drops the database, used in ./Reset.ps1)

The ```spike-db-createdb``` and ```spike-db-migrate``` images are used in ```SqlServerFixture``` to create the database and apply the schema setup under /db/migrate/sql

Additional images could be built to populate the database with base data used in testing.


## Things left to do:
- Updates to support CI/CD
  - Make the image names configurable at run time in order. (i.e. pass in full docker image names with version numbers as environment variables)
  - Support password protected docker regsitries
  - Ensure that execution of db/create/migrate images happens on on a seperate docker server
