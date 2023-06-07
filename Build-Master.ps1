dotnet tool restore

dotnet cake build.cake --target=Publish `
					   --myGetAccessToken="$($ENV:MYGET_ACCESS_TOKEN)" `
					   --myGetFeedUrl="$($ENV:MYGET_FEED_URL)" `
					   --symbolAccessToken="$($ENV:MYGET_ACCESS_TOKEN)" `
					   --symbolFeedUrl="$($ENV:SYMBOL_FEED_URL)"

dotnet nuget add source "$($ENV:CI_SERVER_URL)/api/v4/projects/$($ENV:CI_PROJECT_ID)/packages/nuget/index.json" --name gitlab --username gitlab-ci-token --password $($ENV:CI_JOB_TOKEN) --store-password-in-clear-text
dotnet nuget push "artifacts\*.nupkg" --source gitlab