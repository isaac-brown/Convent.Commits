Push-Location

Set-Location $PSScriptRoot

try {
    dotnet pack ..\src\Convent.Commits\Convent.Commits.csproj `
    -c Release `
    -p:IncludeSymbols=true `
    -p:SymbolPackageFormat=snupkg
}
finally {
    Pop-Location
}
