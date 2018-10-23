import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WingButtonIconComponent } from './components/wing-button-icon/wing-button-icon.component';
import {
  MatCardModule,
  MatInputModule,
  MatButtonModule,
  MatIconModule,
  MatTooltipModule,
  MatDialogModule,
  MatTableModule,
  MatFormFieldModule
} from "@angular/material";
import { WingDividerVerticalComponent } from './components/wing-divider-vertical/wing-divider-vertical.component';
import { WingSearchComponent } from './components/wing-search/wing-search.component';
import { FlexLayoutModule } from "@angular/flex-layout";
import { WingButtonCrudComponent } from './components/wing-button-crud/wing-button-crud.component';
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    FormsModule,
    RouterModule,
    //Import Material Module
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatDialogModule,
    MatTableModule,
    MatFormFieldModule,
  ],
  declarations: [
    WingButtonIconComponent, 
    WingDividerVerticalComponent, 
    WingSearchComponent, 
    WingButtonCrudComponent
  ],
  exports: [
    WingButtonIconComponent, 
    WingDividerVerticalComponent,
    WingSearchComponent,
    WingButtonCrudComponent
  ],
})
export class WingModule { }
