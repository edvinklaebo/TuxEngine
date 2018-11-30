:: Clean output directory
rmdir /s /q out

:: Back up the current csproj
ren TuxEngine.csproj TuxEngine.csproj.bak

:: Windows
copy "build_platforms\TuxEngine.windows.csproj" "TuxEngine.csproj"
dotnet restore
dotnet publish -c Release -o "out\windows"
del TuxEngine.csproj

:: OSX
copy "build_platforms\TuxEngine.osx.csproj" "TuxEngine.csproj"
dotnet restore
dotnet publish -c Release -o "out\osx"
del TuxEngine.csproj

:: Linux
copy "build_platforms\TuxEngine.linux.csproj" "TuxEngine.csproj"
dotnet restore
dotnet publish -c Release -o "out\linux"
del TuxEngine.csproj

:: Restore backup
ren TuxEngine.csproj.bak TuxEngine.csproj
dotnet restore
