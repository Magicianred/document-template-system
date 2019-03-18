export class Version {
  id: number;
  creationTime: string;
  templateVersion: string;
  versionState: string;
  creator: {
    id: string;
    name: string;
    surname: string;
    email: string;
  }
}
