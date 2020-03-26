import { Injectable,Inject, Pipe } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { config } from '../app.config';
import { catchError, map } from 'rxjs/operators';
import { IUser } from '../_models/user'
import { IGroup } from '../_models/group';

class Gender{
    constructor(public name: string, public value: number) {

    }
}

export const Genders: Gender[] = [{ name: "Male", value: 0 }, { name: "Female", value: 1 }, ]; 

@Injectable({
  providedIn: 'root'
})
export class UserService {
    readonly baseUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl) {
        this.baseUrl = `${baseUrl}api/v1/users`;
    }
    getAll(): Observable<IUser[]>  {
        return this.http.get<IUser[]>(`${this.baseUrl}`);
    }

    findAny(term:string): Observable<IUser[]> {
      return this.http.get<IUser[]>(`${this.baseUrl}/findany/${term}`);
    }

    get(id: number): Observable<IUser> {
        return this.http.get<IUser>(`${this.baseUrl}/${id}`);
    }

    check(name: string): Observable<boolean> {
        return this.http.get<boolean>(`${this.baseUrl}/check/${name}`);
    }

    create(User: IUser): Observable<IUser>  {
        return this.http.post<IUser>(`${this.baseUrl}`, User);
    }

    update(User: IUser): Observable<IUser> {
        return this.http.put<IUser>(`${this.baseUrl}`, User);
    }

    delete(User: IUser): Observable<boolean> {
        return this.http.delete<boolean>(`${this.baseUrl}/${User.id}`);
    }

   
}
