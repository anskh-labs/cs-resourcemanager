// Install and Enable Migration in VS Studio using PM Console
Install-Package EntityFrameworkCore -IncludePrerelease
enable-migrations -Project <MyProjectName>

// Add migration
Add-Migration Init