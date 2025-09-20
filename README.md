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

### Version 0.2.4.0

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
- The inspector will still fail to resolve history, portraits and possibly other items if they originate from saves

View the complete [changelog](CHANGELOG.md)

---

## See Also

- [Compiling](Compiling.md)
- [Contributing](Contributing.md)
