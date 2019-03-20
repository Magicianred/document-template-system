export class Token {
  exp: number;
  iat: number;
  nbf: number;
  role: string;
  unique_name: string;
}

export class TokenValue {
  content: string;
}
