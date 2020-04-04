export interface User {
  id: string;
  username: string;
  displayName: string;
}

export interface UserWithToken {
  user: User;
  accessToken: string;
}