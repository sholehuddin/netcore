import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler, Injectable } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { McBreadcrumbsModule } from 'ngx-breadcrumbs';
import { AuthService } from './shared/services/auth.service';
import { CoreModule } from './core/core.module';
import { MainModule } from './main/main.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutMainComponent } from './core/layout/components/layout-main/layout-main.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { NgHttpLoaderModule } from 'ng-http-loader';
import { AppRoutingModule } from './app-routing.module';
import { MatIconModule, MatTabsModule } from '@angular/material';

@NgModule({
  declarations: [],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgHttpLoaderModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatTabsModule,
    FormsModule,
    CoreModule,
    MainModule,
    FlexLayoutModule,
    AppRoutingModule,
    McBreadcrumbsModule.forRoot()
  ],
  providers: [AuthService],
  bootstrap: [LayoutMainComponent]
})
export class AppModule { }
