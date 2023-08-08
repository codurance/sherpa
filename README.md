# Sherpa

## Important ⚠️
After opening the project, install all needed dependencies (IDE should do it by itself).

After that, go to SherpaBackend, Properties folder and create a `launchSettings.json` based on `launchSettings.json.sample`. Ask for AWS credentials or search them in Confluence.

## To run 🚀
1. Execute the following command to compile the styles while coding (you will need Node installed)
```bash
cd SherpaFrontend && npx tailwindcss -i ./Styles/tailwind.css -o ./wwwroot/css/tailwind.css --watch
```

2. Execute the project
```bash
cd SherpaBackend && dotnet run
``` 
or in watch mode
```bash
cd SherpaBackend && dotnet watch
```