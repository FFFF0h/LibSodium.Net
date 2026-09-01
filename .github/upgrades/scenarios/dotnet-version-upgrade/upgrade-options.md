# Upgrade Options — LibSodium.Net

Assessment: 5 SDK-style projects targeting .NET 8, with a mechanical .NET 10 target-framework update and no high-risk migrations detected.

## Strategy

### Upgrade Strategy
The small modern-.NET solution is suited to one atomic framework upgrade across all projects.

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Upgrade all projects simultaneously in one atomic pass. |
| Top-Down | Upgrade entry-point applications first and temporarily multi-target shared libraries. |
