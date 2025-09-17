# SoulashExplorer

A CLI App to read and display various information from Soulash 2 save files

## Using This Project

All paths are currently hard-coded, change the save directory and base game path in Program.cs → Main to point to a proper save directory and base data path.

When run, the tool will output a lot of text to the console, including every loaded Humanoid entity, Portrait data (unused right now) and every Historical Record.

## Historical Record Support

Historical Records that are supported currently:

- Birth
- Death
- Joined Family
- Became Family Leader
- New Job
- Married
- Joined Company (Partial)

This actually encompasses a surprising number of common events so you'll get a pretty good picture of the world's history with just these events.
