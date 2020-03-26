export interface IGroup {
  id: number;
  name: string;
}

export class Group implements IGroup {

  constructor(public id: number,
    public name: string) {
  }
}
