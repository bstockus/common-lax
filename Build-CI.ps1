dotnet tool restore

dotnet cake build.cake --target=Default `
					   --versionSuffix="$($ENV:PACKAGE_SUFFIX)"