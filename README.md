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

## Known Issues

- Portraits are not color blended
- Loading is single threaded (large worlds will make it choke a bit)
  - Furthermore, loading uses a lot of wasted memory splitting strings instead of using Spans or some other logic

---

## See Also

- [Compiling](Compiling.md)
- [Contributing](Contributing.md)
