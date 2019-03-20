import {Version} from './Version'

export class TemplateVersions {
  id: number;
  name: string;
  templateState: string;
  templateVersions: Version[];
  owner: {
    id: string;
    name: string;
    surname: string;
    email: string;
  }
}
