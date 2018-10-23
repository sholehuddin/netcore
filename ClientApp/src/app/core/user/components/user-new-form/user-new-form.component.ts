import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UserService } from '../../services/user.service';
import { PuiSnackbarService } from '../../../../shared/pusintek-ui/components/pui-snackbar/pui-snackbar.service';
import { Role } from '../../models/role';
import { Pegawai } from '../../models/pegawai';

@Component({
  selector: 'app-user-new-form',
  templateUrl: './user-new-form.component.html',
  styleUrls: ['./user-new-form.component.css'],
  providers: [UserService]
})
export class UserNewFormComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<UserNewFormComponent>,
    private uService: UserService,
    private snackbar: PuiSnackbarService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  model: Pegawai;
  roles: Role[];
  nipStr: string;

  ngOnInit() {
    // cek data : list roles
    this.roles = this.data.roles;
    // reset model
    this.model = null;
  }

  onFindPegawaiClick() {
    //console.log('find pegawai', this.nipStr);
    if (this.nipStr != null) {
      this.uService.getPegawaiByNip(this.nipStr).subscribe(result => {
        //console.log('result find', result);
        if (result != null) {
          let isSuccess: boolean = result.isSuccessful;
          //console.log('is success', isSuccess);

          if (isSuccess) {
            this.model = result.data;
          } else {
            this.model = {} as Pegawai;
            this.snackbar.showSnackBar('info', 'Pegawai tidak ditemukan');
          }
        } else {
          // show message not find

        }
      });
    }
  }

  onAddUserClick() {
    // isi model, lempar ke list
    this.dialogRef.close(this.model);
  }

}
