## Detected Hints

### hint: multi-project-dependency-ordering
- **Status**: resolved
- **Priority**: MUST
- **Evidence**: Ten SDK-style projects share a library/test dependency chain and were retargeted atomically under the approved All-at-Once strategy.
- **Detected**: During `02-framework-package-upgrade`; all target edits were already applied as one buildable boundary before the hint was recovered.

### hint: test-project-lifecycle
- **Status**: resolved
- **Priority**: MUST
- **Evidence**: The primary test project and five imported platform test hosts consume the upgraded library and shared test core.
- **Detected**: During `02-framework-package-upgrade`; all test TFMs and automation paths were updated in the same atomic task.

## Breakdown Decisions

### task: 02-framework-package-upgrade
- Kept atomic because the approved scenario constraint explicitly requires an All-at-Once update with no temporary dependency-tier states; the detected project/test lifecycle work is complete within that boundary.
