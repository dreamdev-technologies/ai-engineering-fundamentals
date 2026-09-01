# security-scan

Two checks, both of which fail the run on any finding:

1. **Static analysis.** Builds the solution with the security analyser rules listed in `run.ps1` promoted to errors (weak cryptography, insecure randomness, SQL built from strings). The rules are warnings in a normal build; see the repository `.editorconfig`.
2. **Dependency audit.** `dotnet list package --vulnerable --include-transitive`, which needs access to the package source's vulnerability data. If the audit cannot run it counts as a finding rather than a pass; a scan that silently skips a check is worse than one that fails.

Run it with `tools/security-scan/run.ps1` (Windows) or `tools/security-scan/run.sh`. The scaffold has findings on purpose; activity 06 hands them to an agent to fix.
