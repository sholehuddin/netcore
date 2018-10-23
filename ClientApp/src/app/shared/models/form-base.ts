import { FormGroup, FormControl, Validators } from '@angular/forms'

export class FormBase {
  formGroup: FormGroup
  formGroupPpk: FormGroup
  isEdit = false
  public data: any
  dataResult: any
  public formAttribute: IFormControl[]
  dialogRef: any
  state = 'Tambah'
  constructor(formAttribute: IFormControl[]) {
    this.formAttribute = formAttribute
  }

  prepareForm() {
    this.createForm()
    if (this.data != null) {
      this.isEdit = true
      this.state = 'Edit'
      this.setFormValue()
    }
  }

	setFormValue() {
    const tempValueData: { [key: string]: any } = {}
    for (const x of this.formAttribute) {
      tempValueData[x.name] = this.data[x.name]
    }
		this.formGroup.setValue(tempValueData)
	}

  createForm() {
    const tempFormGroupObj: { [key: string]: any } = {}
		for (const attr of this.formAttribute) {
      let attrValue = ''
      if (this.isEdit) { attrValue = this.data[attr.name] }
      if (attr.required === false) {
        tempFormGroupObj[attr.name] = new FormControl(attrValue)
      } else {
        tempFormGroupObj[attr.name] = new FormControl(attrValue, Validators.required)
      }
		}
		this.formGroup = new FormGroup(tempFormGroupObj)
  }

  onOkClick() {
    this.dataResult = this.formGroup.value
    this.dialogRef.close(this.dataResult)
  }

  onNoClick() {
    this.dialogRef.close()
  }
}


export interface IFormControl {
  name: string
  required: boolean
  placeholder?: string
}
