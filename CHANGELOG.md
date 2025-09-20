# Changelog

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
