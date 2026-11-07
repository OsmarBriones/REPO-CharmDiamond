# Changelog

## [Unreleased]

### Fixed
- Runtime hygiene: the wear records are cleared when a level ends, and debug log lines in the hit patch are only built in debug mode.

### Added
- Unbreakable: while the Diamond is pocketed, valuables you hold in your hands take no damage. Each prevented hit wears the Diamond (8% by default).
- `WearCooldownSeconds` setting (0 to 10, default 0.5): minimum time between wear charges for one held valuable.
- `Debug.DebugMode` (off by default): puts the Diamond on the floor at the start of each level and writes detailed logs.
