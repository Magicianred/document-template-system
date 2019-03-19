export class Version {
  id: number;
  creationTime: string;
  content: string;
  versionState: string;
  creator: {
    id: string;
    name: string;
    surname: string;
    email: string;
  }
}
