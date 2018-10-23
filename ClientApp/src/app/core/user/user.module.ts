import { NgModule } from '@angular/core';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserFormComponent } from './components/user-form/user-form.component';
import { UserDetailComponent } from './components/user-detail/user-detail.component';
import { MatListModule, MatChipsModule,  MatExpansionModule, MatSlideToggleModule, MatDialogModule, MatSelectModule } from '@angular/material';
import { DataListModule } from 'primeng/primeng';
import { LayoutModule } from "../layout/layout.module";
import { UserNewFormComponent } from './components/user-new-form/user-new-form.component';
import { DataTableImporter } from '../../shared/models/mat-material-importer';
import { UserRoutingModule } from './user-routing.module';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [
    MatListModule,
    LayoutModule,
    // material modules
    MatDialogModule,
    MatChipsModule,
    MatExpansionModule,
    MatSlideToggleModule,
    DataListModule,
    MatSelectModule,
    ...DataTableImporter,
    UserRoutingModule,
    CommonModule
  ],
  declarations: [
    UserListComponent,
    UserFormComponent,
    UserDetailComponent,
    UserNewFormComponent
  ],
  providers: [
  ],
  entryComponents: [UserNewFormComponent, UserFormComponent]
})
export class UserModule { }
