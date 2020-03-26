import { Injectable, Inject, Pipe } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { config } from '../app.config';
import { catchError, map } from 'rxjs/operators';
import { IGroup, Group } from '../_models/group'
import { IUser, User } from '../_models/user'
@Injectable({
  providedIn: 'root'
})
export class GroupService {
  readonly baseUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl) {
    this.baseUrl = `${baseUrl}api/v1/groups`;
  }
  getAll(): Observable<IGroup[]> {
    //return this.http.get<IGroup[]>(`${this.baseUrl}`);
    var result = new Array<Group>();
    result.push(new Group(1, "Group A"));
    result.push(new Group(2, "Group B"));
    return of(result);
  }
  removeGroup(User: IUser, Group: IGroup): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${User.id}/${Group.id}`);
  }

  addGroup(User: IUser, Group: IGroup): Observable<boolean> {
    return this.http.put<boolean>(`${this.baseUrl}`, { userId: User.id, groupId: Group.id });
  }
}
