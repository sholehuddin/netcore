import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ModelAttribute } from '../models/ModelAttribute';
import { EntityService } from '../../services/entity.service';

@Component({
  selector: 'app-attribute-list',
  templateUrl: './attribute-list.component.html',
  styleUrls: ['./attribute-list.component.css']
})
export class AttributeListComponent implements OnInit {
  public model: string
  public attributes: ModelAttribute[] = []
  public selectedAttr: ModelAttribute[] = []
  public primaryKey: string
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<AttributeListComponent>,
    public service: EntityService
  ) { }

  ngOnInit() {
    if(this.data.model === "" || this.data.model === null)
    {
      this.dialogRef.close()
    }
    this.model = this.data.model
    this.service.getById(this.model).subscribe((result ) => {
      this.attributes = result
    })
  }

  onNoClick(){
    this.dialogRef.close()
  }

  onOkClick(){
    const result = { attr: this.selectedAttr, primaryKey:  this.primaryKey}
    this.dialogRef.close(result)
  }

  attrSelected(attr: ModelAttribute): boolean
  {
    return this.selectedAttr.indexOf(attr) != -1
  }

  selectChange(modelSelected: any)
  {
    this.selectedAttr = modelSelected.selectedOptions.selected.map(x => x.value)
  }
}
