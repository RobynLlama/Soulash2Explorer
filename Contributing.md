# Contributing

S2E is under active development and pull requests are likely to quickly fall behind the main branch. If you still want to brave the waters and make a contribution please follow these guidelines to keep things running smoothly.

## Style

- Always have trailing line endings on files. This is a setting you can enable in most IDEs
- For markdown obey the style guides of a quality linter such as `DavidAnson.vscode-markdownlint`
- This project uses `2 space indents` and `Allman style braces`, you can configure your IDE to respect an open file's existing style so it won't bother your other projects, so please do this
- For Source files, try to avoid nesting by using early returns under negative checks instead of branches under positive checks. This is known as `guarding` and it reduces indentation clutter dramatically.

Example:

  ```cs
  //Do not
  If (thing)
  {
    //do stuff here
  }

  //Do
  {
    If (!thing)
      return;

    //do stuff here
  }
  ```

## Commits / Pull requests

- Be descriptive of what your commits do.
- Follow [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) to help keep track of what you are changing both for yourself and others that wish to read your code
- **Avoid** committing changes to .gitignore since this will effect everyone's instance if merged. Use `git rm <file>` instead to ask git to stop tracking a file locally
- **Do not** make style only pull requests, maintainers can handle style issues themselves
