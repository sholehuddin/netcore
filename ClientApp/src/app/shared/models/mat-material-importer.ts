import {
  MatCardModule,
  MatInputModule,
  MatButtonModule,
  MatIconModule,
  MatTooltipModule,
  MatDialogModule
} from '@angular/material'
import { PusintekUiModule } from '../../shared/pusintek-ui/pusintek-ui.module'

import { animationModule, animation } from './animation'
import { WingModule } from '../wing/wing.module'
import { NgxDatatableModule } from '@swimlane/ngx-datatable'
import { FlexLayoutModule } from '@angular/flex-layout'
import { ReactiveFormsModule, FormsModule } from '@angular/forms'
import { DataTableModule } from 'primeng/primeng'

export const matButtonImporter = [
	MatCardModule,
  MatButtonModule,
  MatInputModule,
  MatIconModule,
  MatTooltipModule,
  MatDialogModule,
  PusintekUiModule,
]

export const DataTableImporter = [
  ...matButtonImporter,
  ...animationModule,
  WingModule,
  NgxDatatableModule,
  FlexLayoutModule,
  ReactiveFormsModule,
  FormsModule,
  DataTableModule
]
