# Sherpa

## Important ⚠️

This README.md is just a quick reference, but if you are onboarding, take a look at the [onboarding documentation](https://codurance.atlassian.net/wiki/spaces/RD/pages/754515969/Technical+Onboarding) to set up the project properly.

## Requirements 🗻

- .NET 6.0
- Docker
  - Used for running test containers while testing
  - Also used for running MongoDB [locally for dev environment](#running-local-db).
- Node.js LTS
  - Used for running the TailwindCSS compiler.

## .NET dependencies

After opening the project, install all needed dependencies (IDE should do it by itself*).

*If not, run the following command:
```bash
dotnet restore
```

## Important Authentication Related Notes!
- As Blazor 6 is outdated and we don't expect patching comming from the DotNet team, we've had to patch the Blazor Authentication library to support Cognito, you can see that in `index.html` we've switched the default `Authentication.js` file for the patched one, present in our js folder. To see the patches, look for `//PATCH: ...` in the code. Brief summary of the patches
  - Disable silent login using Iframe, as Cognito doesn't support it and Blazor will wait for **10 SECONDS** before trying the redirect login
  - Tweak the logout params as Blazor was not using the previous fix on `Authentication.razor` file and using the default parameters instead, being those not supported by Cognito.

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