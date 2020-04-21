// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  apiServerUrl: 'https://sjz.azurewebsites.net',
  authUrl: 'http://localhost:5011',
  state: 'BCEeFWf45A53sdfaef434',
  linkedIn: {
    clientId: '81x1oimxi0mlzy',
    scope: 'r_basicprofile'
  },
};
