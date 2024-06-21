# Sherpa

## Important ⚠️
After opening the project, install all needed dependencies (IDE should do it by itself*).

*If not, run the following command:
```bash
dotnet restore
```

After that, go to SherpaBackend, Properties folder and create a `launchSettings.json` based on `launchSettings.json.sample`. 

### Credentials
Obtain AWS credentials and MongoDB Atlas connection string in Bitwarden.

- AWS: Bitwarden
- MongoDB connection string: Bitwarden or [local MongoDB](#running-local-db)

## Requirements 🗻

- .NET 6.0
- Docker
  - Used for running test containers while testing
  - Also used for running MongoDB [locally for dev environment](#running-local-db).
    
- Node.js LTS
  - Used for running the TailwindCSS compiler.

## To run 🚀
Execute the project
```bash
cd SherpaBackend && dotnet run
``` 
or in watch mode
```bash
cd SherpaBackend && dotnet watch
```

### <p id="running-local-db">Working with local database</p>

You can use the following command:
  ```bash
  docker run --name mongo -p 27017:27017 -d mongodb/mongodb-community-server:latest
  ```

For this DB, the connection string would be: `mongodb://localhost:27017/`

## To test 🧪

Run the following command:
```bash
dotnet test
```

### Note

While executing the SherpaBackend tests some docker containers will open and close. This is normal, check (https://dotnet.testcontainers.org/) for more info.
