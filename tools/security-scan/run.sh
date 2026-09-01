#!/usr/bin/env bash
# Security scan: static analysis with the security rules promoted to errors, then a dependency audit.
# Exit code 0 = no findings. Any finding = 1.
root="$(cd "$(dirname "$0")/../.." && pwd)"
sln="$root/AiEngineeringFundamentals.sln"
rules="CA5350,CA5351,CA5394,CA2100"
findings=0

echo "== Static analysis ($rules as errors)"
build_out="$(dotnet build "$sln" --nologo --no-incremental -clp:NoSummary "-p:WarningsAsErrors=$rules" 2>&1)"; build_rc=$?
echo "$build_out" | grep -E "error|warning CA|Build succeeded"
if [ "$build_rc" -ne 0 ]; then
  findings=$((findings+1)); echo "FINDING: static analysis reported security rule violations"
else
  echo "clean"
fi

echo
echo "== Dependency audit"
if audit="$(dotnet list "$sln" package --vulnerable --include-transitive 2>&1)"; then
  echo "$audit"
  if echo "$audit" | grep -q "has the following vulnerable packages"; then
    findings=$((findings+1)); echo "FINDING: vulnerable packages"
  else
    echo "clean"
  fi
else
  echo "$audit"
  echo "WARNING: dependency audit could not run (no access to the package source?). Treating as a finding."
  findings=$((findings+1))
fi

echo
if [ "$findings" -eq 0 ]; then echo "PASS: no findings"; exit 0; fi
echo "FAIL: $findings finding group(s)"
exit 1
