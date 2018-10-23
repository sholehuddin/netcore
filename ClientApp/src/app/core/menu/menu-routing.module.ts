import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MenuListComponent } from './components/menu-list/menu-list.component';
import { MenuFormComponent } from './components/menu-form/menu-form.component';
import { MenuDetailComponent } from './components/menu-detail/menu-detail.component';

const routes: Routes = [
  {
    path: "",
    children: [
      { path: "", component: MenuListComponent },
      { path: "form", component: MenuFormComponent },
      { path: ":id", component: MenuDetailComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MenuRoutingModule { }
