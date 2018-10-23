import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeModule } from './home/home.module';
import { SampleModule } from "./sample/sample.module";

@NgModule({
  imports: [
    CommonModule,
    HomeModule,
    SampleModule
  ],
  declarations: []
})
export class MainModule { }
