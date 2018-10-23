import { ViewChild } from '@angular/core'
import { DatatableComponent } from '@swimlane/ngx-datatable'
import { CrudAction } from './crud-action'

export class PrimeNgTableBase extends CrudAction {
  columns: any[] = []
  selected: any = {}
	showSearch = false
	serverSearch = false
  @ViewChild(DatatableComponent) table: DatatableComponent

  onSelect($event: any) {
    this.selected = $event.data
		this.showSearch = false
  }
  onRowUnselect($event: any) {
    this.selected = {}
  }

	state(): string {
		return Object.keys(this.selected).length === 0 ? 'inactive' : 'active'
	}

  updateFilter($event: any) {
    this.datas = $event
	}

	onSearch($event: any) {
		let keyword = ''
		try {
			keyword = $event.target.value.toLowerCase()
			this.serverSearch ? this.getData({keyword: keyword}) : this.searchData(keyword)
		} catch (e) {
		}
	}

	getData(params: { [key: string]: any} = {}): any {
		return new Promise((resolve, reject) => {
			super.getData(params).then((result: any) => {
				this.selected = {}
				resolve(result)
			}).catch((err: any) => reject(err))
		})
	}

	onClickSearch() {
		this.showSearch = true
	}

	searchData(keyword: string) {
		this.datas = this.dataTemp.filter(x => JSON.stringify(x).toLowerCase().indexOf(keyword) !== -1)
		this.table.offset = 0
	}
}
