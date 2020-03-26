import { Group } from './group'

export interface IUser {
  id: number;
  name: string;
  age: number;
  gender: number;
  groups: Group[];
}

export class User implements IUser {

  constructor(public id: number,
    public name: string,
    public age: number,
    public gender: number, public groups: Group[]) {
  }
}
