import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuListComponent } from './components/menu-list/menu-list.component';
import { TreeModule, ContextMenuModule, PanelMenuModule, TreeTableModule } from 'primeng/primeng';
import { MatExpansionModule, MatListModule, MatChipsModule,MatRadioModule, MatSlideToggleModule, MatSelectModule, MatCheckboxModule} from '@angular/material';
import { MenuFormComponent } from './components/menu-form/menu-form.component';
import { MenuDetailComponent } from './components/menu-detail/menu-detail.component';
import { LayoutModule } from '../layout/layout.module';
import { MenuRoutingModule } from './menu-routing.module';
import { DataTableImporter } from '../../shared/models/mat-material-importer';

@NgModule({
    imports: [
        CommonModule,
        LayoutModule,
        TreeModule,
        ContextMenuModule,
        PanelMenuModule,
        MatExpansionModule,
        TreeTableModule,
        MatListModule,
        MatChipsModule,
        MatRadioModule,
        MatSlideToggleModule,
        MatSelectModule,
        MatCheckboxModule,
        MenuRoutingModule,
        ...DataTableImporter
    ],
    declarations: [
        MenuListComponent,
        MenuFormComponent,
        MenuDetailComponent]
})
export class MenuModule { }
