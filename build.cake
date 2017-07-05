 

var target = Argument("target" , "Default");
var configuration = Argument("configuration" , "Debug");
var solutionPath = "./src/ErrataExtensions.sln";

Task("NuGet-Package-Restore")
.Does(()=>{
NuGetRestore(solutionPath);
});

Task("Build")
.IsDependentOn ("NuGet-Package-Restore")
.Does(()=>{
MSBuild(solutionPath, new MSBuildSettings()
.WithProperty("Windows" , "True")
.WithProperty("Verbosity", "Minimal"));
});

Task("Test")
.IsDependentOn("Build")
.Does(()=> {
    var paths = new List<FilePath>() { "./src/Errata.Core.Tests/bin/Debug/Errata.Core.Tests.dll"  };
    MSTest(paths, new MSTestSettings() { NoIsolation = false });
});

Task("Nuget-Package-Create")
.IsDependentOn ("Test")
.Does(()=> {

var nuGetPackSettings = new NuGetPackSettings
	{
        Version                 = "1.0.0.5",
        ReleaseNotes            =new[] {"Added Cake Build"},
		OutputDirectory         = "./nuget",
        BasePath                = "./src/ErrataExtensions/bin/Debug",
        
		IncludeReferencedProjects = true,
        Symbols                 = true,
		Properties = new Dictionary<string, string>
		{  
			{ "Configuration", "Debug" }
		}
	};


     NuGetPack("./src/ErrataExtensions/ErrataExtensions.csproj", nuGetPackSettings);

});

Task("Default")
.IsDependentOn("Nuget-Package-Create");

RunTarget(target)