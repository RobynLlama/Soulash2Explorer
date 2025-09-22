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

### Version 0.3.0.0

Features:

- HistorySave keeps a chronological order of events
- Combined History Viewer now uses pagination and the new ordered history
- Entity History uses the new ordered history
- Unknown Entities use a default portrait
- Added portrait caching
  - Caching does not have eviction because the cost is so small, report any issues, please

View the complete [changelog](CHANGELOG.md)

---

## See Also

- [Compiling](Compiling.md)
- [Contributing](Contributing.md)
