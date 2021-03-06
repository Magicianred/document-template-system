export class User
{
  role: string;
  id: string;
  token: string;
}

export class UserData {
  id: number;
  name: string;
  surname: string;
  role: string;
  email: string;
  status: string;
  type: string;
  newType: string;
}

export class UserChangingData {
  id: string;
  name: string;
  surname: string;
  email: string;
}
