 

var target = Argument("target" , "Default");
var configuration = Argument("configuration" , "Debug");


Task("NuGet-Package-Restore")
.Does(()=>{
NuGetRestore("./ErrataExtensions.sln");
});

Task("Build")
.IsDependentOn ("NuGet-Package-Restore")
.Does(()=>{
MSBuild("./ErrataExtensions.sln", new MSBuildSettings()
.WithProperty("Windows" , "True")
.WithProperty("Verbosity", "Minimal"));
});

Task("Test")
.IsDependentOn("Build")
.Does(()=> {
    var paths = new List<FilePath>() { "./Errata.Core.Tests/bin/Debug/Errata.Core.Tests.dll"  };
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
        BasePath                = "./ErrataExtensions/bin/Debug",
        
		IncludeReferencedProjects = true,
        Symbols                 = true,
		Properties = new Dictionary<string, string>
		{  
			{ "Configuration", "Debug" }
		}
	};


     NuGetPack("./ErrataExtensions/ErrataExtensions.csproj", nuGetPackSettings);

});

Task("Default")
.IsDependentOn("Nuget-Package-Create");

RunTarget(target)