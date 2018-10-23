import { PuiConfirmDialogService } from '../pusintek-ui/components/pui-confirm-dialog/pui-confirm-dialog.service'
import { MatDialog } from '@angular/material/dialog'
import { PuiSnackbarService } from '../pusintek-ui/components/pui-snackbar/pui-snackbar.service'
import { IbaseCrudService } from '../services/ibase-crud.service'
import { TemplateRef, ComponentFactory } from '@angular/core';

export class CrudAction {
  datas: any[] = []
  dataTemp: any[] = []
  pagination: false
  limit = 20
  public service: IbaseCrudService
	public snackBar: PuiSnackbarService
	public dialog: MatDialog
  public dialogService: PuiConfirmDialogService

  getData(params: { [key: string]: any} = {}): any {
    if (this.service === undefined) {
      throw new Error(('service not defined'))
    }
		if (!this.pagination) {
			delete params['offset']
			delete params['limit']
		} else {
			params['offset'] = this.datas.length
			params['limit'] = this.limit
		}
		return new Promise((resolve, reject) =>  {
			this.service.getAll(params).subscribe(result => {
				if (this.pagination) {
					this.datas = [...this.datas, ...result]
					this.dataTemp = this.datas
				} else {
					this.datas = result
					this.dataTemp = [...result]
				}
				resolve(result)
			},
			(err) => reject(err))
		})
	}

  createData(result: any): any {
		return new Promise((resolve, reject) =>  {
			if (result != null) {
				this.service.create(result).subscribe(async () => {
					resolve(result)
					this.snackBar.showSnackBar('success', 'Data Berhasil Ditambahkan')
				},
				(err) => {
					this.snackBar.showSnackBar('error', 'Data Gagal Ditambahkan')
					reject(err)
				}
			)}
		})
	}

	editData(data: any, id: string): any {
		return new Promise((resolve, reject) =>  {
			if (data != null) {
				this.service.update(data, id).subscribe(async(result) => {
					resolve(result)
					this.snackBar.showSnackBar('success', 'Data Berhasil Diubah')
				},
				(err) => {
          this.snackBar.showSnackBar('error', 'Data Gagal Diubah')
          reject(err)
          }
				)
			}
		})
	}

	deleteData(id: string): any {
    return new Promise((resolve, reject) => {
      this.service.delete(id).subscribe(async(result) => {
        resolve(result)
        this.snackBar.showSnackBar('success', 'Data berhasil dihapus')

      },
      (err) => {
        this.snackBar.showSnackBar('error', 'Data gagal dihapus')
        reject(err)
      }
      )
    })

	}
}
