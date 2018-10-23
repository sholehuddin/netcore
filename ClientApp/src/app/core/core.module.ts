import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { LayoutModule } from "./layout/layout.module";
import { MatSnackBarModule } from "@angular/material";;


@NgModule({
  imports: [
    CommonModule,
    LayoutModule,
    MatSnackBarModule
  ]
})
export class CoreModule {}
