# Changelog

## Version 0.3.0.0

Features:

- HistorySave keeps a chronological order of events
- Combined History Viewer now uses pagination and the new ordered history
- Entity History uses the new ordered history
- Unknown Entities use a default portrait
- Added portrait caching
  - Caching does not have eviction because the cost is so small, report any issues, please

## Version 0.2.4.0

Fixes:

- Scroll Bar in Actor Viewer now resets when changing pages

Features:

- Added parsing for the `general.json` save file
- Modified Save Info tab to a split display in order to display new elements:
  - Saved Game Version
  - World Seed
  - Required Mods
- Cleaned up the project and removed old test files

Known Issues:

- The combined history viewer was not addressed in this update
- The inspector will still fail to resolve history, portraits and possibly other items if those items originate from modded content

## Version 0.2.1.0

- Added a new splash tab for showing general world info
- Renamed Entity History tab to Actor Viewer for clarity
  - Actor Viewer tab is now paginated and uses a listing pool to avoid allocations. This dramatically improves performance: loading a world with 800 years of history is now about the same speed as one with 25, for example
  - Actors now have properly colorized portraits

Known Issues:

- The combined history viewer still reads the whole history in one chunk so it'll still be a little slower on big history worlds but otherwise S2E is so much more responsive now.

## Godot Pre-release 1

- Added Entity History
- Added Combined History
- Added Portraits to Entity History

Known Issues:

- Portraits are not colorized
- Loading is not optimized very well
