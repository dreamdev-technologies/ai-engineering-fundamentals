# Security scan: static analysis with the security rules promoted to errors, then a dependency audit.
# Exit code 0 = no findings. Any finding = 1.
$root = (Resolve-Path "$PSScriptRoot/../..").Path
$sln = Join-Path $root "AiEngineeringFundamentals.sln"
$rules = "CA5350,CA5351,CA5394,CA2100"
# MSBuild splits a -p: value on commas and reads each part after the first as a switch
# of its own (MSB1006), so the rules are escaped before being passed through.
$rulesArg = $rules -replace ",", "%2c"
$findings = 0

Write-Host "== Static analysis ($rules as errors)"
dotnet build $sln --nologo --no-incremental -clp:NoSummary "-p:WarningsAsErrors=$rulesArg" | Where-Object { $_ -match "error|warning CA|Build succeeded" }
if ($LASTEXITCODE -ne 0) { $findings++; Write-Host "FINDING: static analysis reported security rule violations" } else { Write-Host "clean" }

Write-Host ""
Write-Host "== Dependency audit"
$audit = dotnet list $sln package --vulnerable --include-transitive
$audit | Write-Host
if ($LASTEXITCODE -ne 0) {
    Write-Host "WARNING: dependency audit could not run (no access to the package source?). Treating as a finding."
    $findings++
} elseif (($audit | Out-String) -match "has the following vulnerable packages") {
    $findings++; Write-Host "FINDING: vulnerable packages"
} else {
    Write-Host "clean"
}

Write-Host ""
if ($findings -eq 0) { Write-Host "PASS: no findings"; exit 0 }
Write-Host "FAIL: $findings finding group(s)"
exit 1
