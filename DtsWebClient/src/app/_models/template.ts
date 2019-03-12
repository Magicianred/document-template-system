export class Template {
  id: number;
  name: string;
  templateState: string;
  versionCount: number;
  owner: {
    name: string;
    surname: string;
    email: string;
  }
}
