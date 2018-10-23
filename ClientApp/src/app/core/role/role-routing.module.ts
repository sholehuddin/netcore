import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RoleListComponent } from './components/role-list/role-list.component';
import { RoleFormComponent } from './components/role-form/role-form.component';
import { RoleDetailComponent } from './components/role-detail/role-detail.component';
import { ActionRoleListComponent } from './components/action-role-list/action-role-list.component';
import { ActionRoleFormComponent } from './components/action-role-form/action-role-form.component';


const routes: Routes = [
  {
    path: "action",
    children: [
      { path: "", component: ActionRoleListComponent },
      { path: "form", component: ActionRoleFormComponent }
    ],
    data: {
      // Uses static text (Home)
      breadcrumbs: "Access Manager"
    }
  },
  {
    path: "",
    children: [
      { path: "", component: RoleListComponent },
      { path: "form", component: RoleFormComponent },
      { path: ":id", component: RoleDetailComponent }
    ],
    data: {
      // Uses static text (Home)
      breadcrumbs: "Role Manager"
    }
  }  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RoleRoutingModule { }
