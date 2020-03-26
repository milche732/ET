import { Component, OnInit } from '@angular/core';
import { Output, EventEmitter } from '@angular/core';
import { UserService } from '../_services/user.service';
import { GroupService } from '../_services/group.service';
import { User } from '../_models/user';
import { IGroup } from '../_models/group';
import { ActivatedRoute, Router } from "@angular/router";
import { catchError, map, } from 'rxjs/operators';
import { Observable, of, from } from 'rxjs';
import { AlertService } from '../_services/alert.service';

import {
  FormGroup,
  FormControl,
  Validators,
  AbstractControl
} from "@angular/forms";
@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {
  userForm: FormGroup;
  private id: FormControl;
  private name: FormControl;
  private user: User;
  private allGroups: IGroup[];
  private success: boolean;
  @Output() userCreated = new EventEmitter<User>();
  @Output() userUpdated = new EventEmitter<User>();
  constructor(private activatedRoute: ActivatedRoute,
    private alertService: AlertService,
    private userService: UserService,
    private groupService: GroupService) {

  }
  ngOnInit() {
    this.createFormControls();
    this.createForm();
    this.activatedRoute.params.subscribe(params => {
      if (params.id)
        this.loadUser(params.id)
    }
    );
    this.groupService.getAll().subscribe(x => {
      this.allGroups = x;
    });
  }
  loadUser(id: number) {
    this.userService.get(id).subscribe(x => this.loadForm(x));
  }
  loadForm(x: User) {
    this.user = x;
    this.id.setValue(x.id);
    this.name.setValue(x.name);
  }
  createFormControls() {
    this.id = new FormControl('');
    this.name = new FormControl('', Validators.required, this.validateUserNameTaken.bind(this));
  }
  validateUserNameTaken(control: AbstractControl) {
    if (this.user != null)
      return of(null);
    return this.userService.check(control.value).pipe(
      map(res => {
        return res ? null : { idTaken: true };
      }),
      catchError(() => null)
    );
  }

  removeGroup(group: IGroup) {

    let that = this;
    this.alertService.confirmThis("Are you sure you want remove from group?", function () {
      that.groupService.removeGroup(that.user, group).subscribe(x => {
        that.user.groups = that.user.groups.filter(item => item.id !== group.id);        
      })
    }, function () {
    })


    
  }

  addGroup(group: IGroup) {
    if (group) {
      if (this.user.groups.filter(item => item.id === group.id).length == 0) {
        this.groupService.addGroup(this.user, group).subscribe(x => {
          this.user.groups.push(group);
        })
      }
    }
  }

  createForm() {
    this.userForm = new FormGroup({
      id: this.id,
      name: this.name,
    });

    this.userForm.valueChanges.subscribe(val => {
      this.success = false;
    });
  }

  onSubmitForm() {
    if (this.userForm.valid) {
      if (this.user == null) {
        this.userService.create(this.userForm.getRawValue()).subscribe(x => {
          this.userForm.reset();
          this.loadForm(x);
          this.userCreated.emit(x);
          this.success = true;
        });
      } else {
        this.userService.update(this.userForm.getRawValue()).subscribe(x => {
          this.userForm.reset();
          this.loadForm(x);
          this.userUpdated.emit(x)
          this.success = true;
        });
      }
    }
  }
}
