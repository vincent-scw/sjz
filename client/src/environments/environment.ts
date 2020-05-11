// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  timelineSvcUrl: 'http://localhost:5030',
  authSvcUrl: 'http://localhost:5010',
  imageSvcUrl: 'http://localhost:5020',
  selfUrl: 'http://localhost:4200'
};
