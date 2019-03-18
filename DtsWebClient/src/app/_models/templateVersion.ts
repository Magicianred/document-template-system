import {Version} from './Version'

export class TemplateVersions {
  id: number;
  name: string;
  versions: Version[];
  owner: {
    id: string;
    name: string;
    surname: string;
    email: string;
  }
}
