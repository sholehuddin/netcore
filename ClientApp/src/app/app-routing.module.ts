import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./main/home/components/home/home.component";
import { PageErrorComponent } from "./core/layout/components/page-error/page-error.component";
import { Page404Component } from "./core/layout/components/page-404/page-404.component";
import { Page401Component } from "./core/layout/components/page-401/page-401.component";

const routes: Routes = [
  {
    path: "",
    component: HomeComponent
  },
  {
    path: "user",
    loadChildren: './core/user/user.module#UserModule'
  },
  {
    path: "menu",
    loadChildren: './core/menu/menu.module#MenuModule'
  },
  {
    path: "role",
    loadChildren: './core/role/role.module#RoleModule'
  },
  {
    path: "generator-crud",
    loadChildren: './core/generator-crud/generator-crud.module#GeneratorCrudModule'
  },
  {
    path: "error",
    children: [
      { path: "", component: PageErrorComponent },
      { path: "404", component: Page404Component },
      { path: "401", component: Page401Component }
    ],
    data: {
      // Uses static text (Home)
      breadcrumbs: "Error"
    }
  },
  { path: "**", redirectTo: "error/404" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [],
  declarations: [
    Page404Component,
    PageErrorComponent,
    Page401Component
  ]
})
export class AppRoutingModule {}