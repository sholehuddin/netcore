import { Component, OnInit, ViewChild, TemplateRef } from "@angular/core";
import { UserService } from "../../services/user.service";
import { User } from "../../models/user";
import { AuthService } from "../../../../shared/services/auth.service";
import { DatatableComponent } from "@swimlane/ngx-datatable";
import { Router } from "@angular/router";
import { MatDialog, MatSnackBar } from "@angular/material";
import { UserFormComponent } from "../user-form/user-form.component";
import { PuiConfirmDialogService } from "../../../../shared/pusintek-ui/components/pui-confirm-dialog/pui-confirm-dialog.service";
import { PuiSnackbarService } from "../../../../shared/pusintek-ui/components/pui-snackbar/pui-snackbar.service";
import { UserNewFormComponent } from "../user-new-form/user-new-form.component";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "user-list",
  templateUrl: "./user-list.component.html",
  styleUrls: ["./user-list.component.css"],
  providers: [UserService]
})
/** user-list component*/
export class UserListComponent implements OnInit {
  /* properties */
  users: User[];
  temp: User[];
  selected: any[] = [];
  columns: any[];
  selectedUser: string;

  @ViewChild("gravatar") gravatarTpl: TemplateRef<any>;
  @ViewChild(DatatableComponent) table: DatatableComponent;

  /** user-list ctor */
  constructor(
    private userService: UserService,
    private authService: AuthService,
    private router: Router,
    private dialog: MatDialog,
    private dialogsService: PuiConfirmDialogService,
    private snackbarService: PuiSnackbarService
  ) {}

  /** Called by Angular after user-list component initialized */
  ngOnInit(): void {
    this.userService.getUsers().subscribe(result => {
      this.users = result;
      this.temp = [...result];
    });

    // specify column
    this.columns = [
      {
        cellTemplate: this.gravatarTpl,
        prop: "Gravatar",
        width: "40"
      },
      { prop: "Nama" },
      { prop: "Nip", width: "170" },
      { prop: "UserRoles" }
    ];
  }

  updateFilter(event: any) {
    const val = event.target.value.toLowerCase();

    // filter our data
    const temp = this.temp.filter(function(d) {
      return (
        d.Nama.toLowerCase().indexOf(val) !== -1 ||
        d.Nip.toLowerCase().indexOf(val) !== -1 ||
        !val
      );
    });

    // update the rows
    this.users = temp;
    // Whenever the filter changes, always go back to the first page
    this.table.offset = 0;
  }

  singleSelectCheck(row: any) {
    return this.selected.indexOf(row) === -1;
  }

  onAssignRoleClick() {
    // console.log('assign role untuk user', this.selected);
    let dialogRef = this.dialog.open(UserFormComponent, {
      data: {
        user: this.selected[0],
        roles: this.selected[0].UserRoles
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result != null) {
        this.userService.postUserRoles(result).subscribe((response: User) => {
          // search user
          this.users = this.users.map(x => {
            if (x.UserId == response.UserId) {
              x = Object.assign({}, x, response);
            }
            return x;
          });
          this.snackbarService.showSnackBar();
        });
      }
      //this.animal = result;
    });
  }

  onAddNewUserClick() {
    // open dialog
    let dialogRef = this.dialog.open(UserNewFormComponent, {
      data: {
        // list of roles
        roles: this.authService.allRoles
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result != null) {
        //console.log('result after closed', result);
        this.userService.postNewUser(result).subscribe(result => {
            this.snackbarService.showSnackBar(result.isSuccessful?'info':'error', result.message);
          this.userService.getUsers().subscribe(result => {
            this.users = result;
            this.temp = [...result];
          });
        });
      }
    });
  }

/*
  showSnackBar(type: string="success", msg:string="") {
    if (type == "success") {
      msg = "Simpan Berhasil";
    } else if (type == "error") {
      msg = "Simpan Gagal";
    }
    let snackBarRef = this.snackBar.openFromComponent(SnackbarComponent, {
      duration: 3000,
      data: { type: type, msg: msg }
    });
  }
  */
}
