# Soulash 2 Explorer

![Static Badge](https://img.shields.io/badge/Language-C%23-blue?style=flat-square&logo=sharp)

![Static Badge](https://img.shields.io/badge/License-LGPLv3-orange?style=flat-square&logo=gnuemacs)

![S2E Logo](banner.png)

S2E is a work in progress full save game exploration tool.

## Current Features

- View the history of any Entity in the game
- View the world's combined history

## Planned Features

- Basic Entity viewer / Editor

## Maybe Features

- Change which entity is player controlled
- Map / Company / State / Etc editor

## Most Recent Changelog

### 0.2.1.0

- Added a new splash tab for showing general world info
- Renamed Entity History tab to Actor Viewer for clarity
  - Actor Viewer tab is now paginated and uses a listing pool to avoid allocations. This dramatically improves performance: loading a world with 800 years of history is now about the same speed as one with 25, for example
  - Actors now have properly colorized portraits, yay

Known Issues:

- The combined history viewer still reads the whole history in one chunk so it'll still be a little slower on big history worlds but otherwise S2E is so much more responsive now.

View the complete [changelog](CHANGELOG.md)

---

## See Also

- [Compiling](Compiling.md)
- [Contributing](Contributing.md)
