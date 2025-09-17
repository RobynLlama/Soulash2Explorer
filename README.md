# SoulashExplorer

A CLI App to read and display various information from Soulash 2 save files

## Using This Project

All paths are currently hard-coded, change the save directory and base game path in Program.cs → Main to point to a proper save directory and base data path.

When run, the tool will output a lot of text to the console, including every loaded Humanoid entity, Portrait data (unused right now) and every Historical Record.

**New Feature**: Will now output all historical entities and their history to `output` in the same directory as it was run in. Its kinda ugly but it is functional.

## Historical Record Support

Historical Records that are supported currently:

- Birth
- Death (Partial, cause of death and location missing)
- Joined Family
- Became Family Leader
- New Job
- Married (Partial, spouse missing)
- Joined Company (Recognized, no special output yet)

This actually encompasses a surprising number of common events so you'll get a pretty good picture of the world's history with just these events.
