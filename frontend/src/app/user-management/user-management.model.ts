export interface User {
  id: string;
  username: string;
  roles: string[];
  isActive: boolean;
}

export interface Role {
  name: string;
}
