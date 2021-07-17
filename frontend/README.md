# Introduction

SPA client for a Todo app, with an ASP.NET REST API. 

## Setting up the dev environment

### Installing dependencies

Install the angular CLI globally:

```console
npm install -g @angular/cli@11
```

You should be able to run the following command in a new console window:

```console
ng --version
```

After you can run the `ng --version` command, go to the project's directory and install it's dependencies:

```console
cd path/to/project
npm install
```

Finally, you should be able to run the dev server with:

```console
ng serve --open
```

Note: Don't forget to start the backend too or the app won't work!

### Configuring VSCode

VSCode can handle Angular projects by default, but there are some essential extensions for it:

- [EditorConfig](https://marketplace.visualstudio.com/items?itemName=EditorConfig.EditorConfig): for handling file indentation, quote style, charset (so it's the same for all of us)
- [Angular Language Service](https://marketplace.visualstudio.com/items?itemName=Angular.ng-template): autocomplete in template files, angular specific diagnostic messages
- [gitignore](https://marketplace.visualstudio.com/items?itemName=codezombiech.gitignore): to see which files are not under version control
- [markdownlint](https://marketplace.visualstudio.com/items?itemName=DavidAnson.vscode-markdownlint): to lint markdown files (obviously)
- [ESLint](https://marketplace.visualstudio.com/items?itemName=dbaeumer.vscode-eslint), [Prettier](https://marketplace.visualstudio.com/items?itemName=esbenp.prettier-vscode): see below

## Code quality

To automatically check as many code quality issues as we can, the project uses [ESLint](https://eslint.org/).

We recommend using it's [VSCode extension](https://marketplace.visualstudio.com/items?itemName=dbaeumer.vscode-eslint) to see warnings during development. The same checks will be run in a CI pipeline during every pull requests.

## Style guide

Use [Prettier](https://prettier.io/).
