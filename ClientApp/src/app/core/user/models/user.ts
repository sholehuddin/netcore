export class User {
    UserId: string;
    PegawaiId: string;
    Nama: string;
    Nip: string;
    IsLocked: boolean;
    Gravatar: string;
    Roles: string;
    UserRoles: string[];
}

export function CurrentUser(): User{
  return JSON.parse(sessionStorage.getItem('CurrentUser'))
}


export interface UserInfo {
  Nama: string;
  Nip: string;
  Gravatar: string;
  Roles: string;
}
