[CmdletBinding(SupportsShouldProcess)]
param (
    [Parameter()]
    [string]
    $ApiKey
)

# Publishes a NuGet package.
Push-Location

Set-Location $PSScriptRoot

try {
    Set-Location ..\src\Convent.Commits\bin\Release\
    dotnet nuget push "*.nupkg" --source https://api.nuget.org/v3/index.json --api-key $ApiKey
    dotnet nuget push "*.snupkg" --source https://api.nuget.org/v3/index.json --api-key $ApiKey
}
finally {
    Pop-Location
}
