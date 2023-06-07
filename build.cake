var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var versionSuffix = Argument("versionSuffix", "");
var myGetAccessToken = Argument("myGetAccessToken", "");
var myGetFeedUrl = Argument("myGetFeedUrl", "");
var symbolAccessToken = Argument("symbolAccessToken", "");
var symbolFeedUrl = Argument("symbolFeedUrl", "");

string versionInfo = null;

var artifactDirectory = Directory("./artifacts");

Task("Clean")
	.Does(() => {
		CleanDirectory(artifactDirectory);
		DotNetCoreClean("./");
	});

Task("SetVersionInfo")
	.IsDependentOn("Clean")
	.Does(() => {
		versionInfo = Context.FileSystem.GetFile("./GitVersion.txt").ReadLines(Encoding.UTF8).First();
		Information($"Version = {versionInfo}");
});

Task("Restore")
	.IsDependentOn("SetVersionInfo")
	.Does(() => {
		DotNetCoreRestore("./");
	});

Task("Build")
	.IsDependentOn("Restore")
	.Does(() => {
		var settings = new DotNetCoreBuildSettings{
			Configuration = configuration,
			NoIncremental = true,
			NoRestore = true
		};
		DotNetCoreBuild("./", settings);
	});

Task("Package")
	.IsDependentOn("Build")
	.Does(() => {
		var settings = new DotNetCorePackSettings {
			Configuration = configuration,
			OutputDirectory = artifactDirectory,
			NoBuild = true,
			NoRestore = true,
			IncludeSource = true,
			IncludeSymbols = true,
			VersionSuffix = versionSuffix,
			ArgumentCustomization = args => {
				args.Append("/p:SemVer=" + versionInfo);
				//args.Append("/p:SymbolPackageFormat=symbols.nupkg");
				return args;
			}
		};
		DotNetCorePack("./CommonLax.sln", settings);
	});

Task("Publish")
	.IsDependentOn("Package")
	.Does(() => {
		var settings = new DotNetCoreNuGetPushSettings {
			Source = myGetFeedUrl,
			ApiKey = myGetAccessToken,
			SymbolSource = symbolFeedUrl,
			SymbolApiKey = symbolAccessToken,
			WorkingDirectory = artifactDirectory
		};
		DotNetCoreNuGetPush("*.nupkg", settings);
	});

Task("Default")
	.IsDependentOn("Package");

RunTarget(target);