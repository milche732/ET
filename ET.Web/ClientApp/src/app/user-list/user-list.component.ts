import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { User } from '../_models/user';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { AlertService } from '../_services/alert.service';
@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
    Error: any;
    UserList: User[]
  constructor(private userService: UserService,
    private alertService: AlertService,
    private router: Router,
    private activatedRoute: ActivatedRoute) {

    }

    deleteUserDialog(user: User) {
        let that = this;
        this.alertService.confirmThis("Are you sure you want to delete user?", function () {
            that.deleteUser(user);
        }, function () {
        })
    }

    deleteUser(user: User) {
        this.userService.delete(user).subscribe(x => {
            this.UserList = this.UserList.filter(x => x.id != user.id);
        }, error => {
            this.Error = error;
        });
    }
    editUser(user: User) {
        this.router.navigate(['/edit', user.id]);
    }
    ngOnInit()
    {
        this.activatedRoute.params.subscribe(params => {
          if (params.term)
            this.findAny(params.term);
          }
        );

        this.userService.getAll().subscribe(x => {
            this.UserList = x;
        }, error => {
            this.Error = error;
        });
    }

    findAny(term) {
      this.userService.findAny(term).subscribe(x => {
        this.UserList = x;
      }, error => {
        this.Error = error;
      });
    }

}
