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

## Important Authentication Related Notes!
- As Blazor 6 is outdated and we don't expect patching comming from the DotNet team, we've had to patch the Blazor Authentication library to support Cognito, you can see that in `index.html` we've switched the default `Authentication.js` file for the patched one, present in our js folder. To see the patches, look for `//PATCH: ...` in the code. Brief summary of the patches
  - Disable silent login using Iframe, as Cognito doesn't support it and Blazor will wait for **10 SECONDS** before trying the redirect login
  - Tweak the logout params as Blazor was not using the previous fix on `Authentication.razor` file and using the default parameters instead, being those not supported by Cognito. **Please have in mind that one of the changed parameter is the hardcoded `client_id`, if that changes, a manual replacement is necessary**

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
