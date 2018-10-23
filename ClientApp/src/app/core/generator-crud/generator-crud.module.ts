import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GeneratorCrudRoutingModule } from './generator-crud-routing.module';
import { MatSelectModule, MatCheckboxModule, MatListModule, MatButtonModule, MatDialogModule, MatIconModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule } from '@angular/forms';
import { GeneratorComponent } from './components/generator/generator.component';
import { AttributeListComponent } from './components/attribute-list/attribute-list.component';

@NgModule({
  imports: [
    CommonModule,
    GeneratorCrudRoutingModule,
    MatSelectModule,
    MatCheckboxModule,
    MatListModule,
    MatButtonModule,
    FlexLayoutModule,
    LayoutModule,
    MatDialogModule,
    MatIconModule,
    FormsModule
  ],
  declarations: [GeneratorComponent, AttributeListComponent],
  entryComponents: [AttributeListComponent]
})
export class GeneratorCrudModule { }
