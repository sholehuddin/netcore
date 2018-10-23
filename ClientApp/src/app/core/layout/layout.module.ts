import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutMainComponent, LayoutContentComponent, LayoutSearchBarComponent } from './components/layout-main/layout-main.component';
import { LayoutNavComponent } from './components/layout-nav/layout-nav.component';
import { LoaderComponent } from './components/loader/loader.component';
import { RouterModule } from '@angular/router';
import { MatToolbarModule, MatSidenavModule, MatButtonModule, MatListModule, MatMenuModule, MatCardModule, MatProgressBarModule, MatIconModule, MatInputModule } from '@angular/material';
import { PanelMenuModule, BreadcrumbModule, ScrollPanelModule } from 'primeng/primeng';
import { NavMenuModule } from "./components/layout-nav/nav-menu.component";
import { FlexLayoutModule } from "@angular/flex-layout";
import { PerfectScrollbarModule, PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { FormsModule } from '@angular/forms';
import { McBreadcrumbsModule } from "ngx-breadcrumbs";
import { NgHttpLoaderModule } from 'ng-http-loader';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FlexLayoutModule,
    PerfectScrollbarModule,
    FormsModule,
    MatToolbarModule,
    MatSidenavModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatMenuModule,
    MatCardModule,
    MatProgressBarModule,
    PanelMenuModule,
    BreadcrumbModule,
    NavMenuModule,
    MatInputModule,
    CommonModule,
    McBreadcrumbsModule,
    NgHttpLoaderModule,
    ScrollPanelModule
  ],
  declarations: [
    LayoutMainComponent,
    LayoutContentComponent,
    LayoutSearchBarComponent,
    LayoutNavComponent,
    LoaderComponent,
  ],
  exports: [LayoutMainComponent, LoaderComponent, LayoutSearchBarComponent],
  providers: [
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ]
})
export class LayoutModule {}
