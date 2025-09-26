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

### Version 0.3.25.1

Features:

- Entities have been switched to a component model that makes loading them much easier
- The player entity is now loaded at the top of the Actor Listing
- The Combined History viewer's visuals were updated for readability
- All logging was moved to the log window
- New Recognized Components:
  - Humanoid: Race ID, Subrace ID and Gender
  - Persona: Date of Birth, Date of Death, Cause of Death and Responsible Entity
- Actor Listing now has a gender icon
- SaveCollection now reads and parses the company.sav file for company names and IDs
- Historical Entries Changed:
  - Marriage: Now outputs the spouse and spouse's maiden name
  - Death: Now outputs cause of death and for "Killed by" types who killed the entity
  - JoinedCompany: Now outputs the company that was joined
- Theme was updated across the entire application to more pleasant colors
- Fonts were applied more uniformly across the application for readability
- A Version Warning will appear when attempting to load saves created in a game version that has not been tested by the maintainer team for correctness. It can be ignored but exercise caution when doing so!

Fixes:

- Disable separator in menu because some windows users can click on it
- Components are properly cleared from the shared entity builder (likely had little effect, but it _was_ a bug)

Known Issues:

- Menu items may behave differently on Windows than Linux, needs investigating

View the complete [changelog](CHANGELOG.md)

---

## See Also

- [Compiling](Compiling.md)
- [Contributing](Contributing.md)
