# Clean output directory
rm -rf out

# Back up the current TuxEngine.csproj
mv TuxEngine.csproj TuxEngine.csproj.bak

# Windows
cp "./build_platforms/TuxEngine.windows.csproj" "TuxEngine.csproj"
dotnet restore
dotnet publish -c Release -o "out/windows"
rm TuxEngine.csproj

# OSX
cp "./build_platforms/TuxEngine.osx.csproj" "TuxEngine.csproj"
dotnet restore
dotnet publish -c Release -o "out/osx"
rm TuxEngine.csproj

# Linux
cp "./build_platforms/TuxEngine.linux.csproj" "TuxEngine.csproj"
dotnet restore
dotnet publish -c Release -o "out/linux"
rm TuxEngine.csproj

# Restore backup
mv TuxEngine.csproj.bak TuxEngine.csproj
dotnet restore