import { DataTableBase } from "./datatable-base";
import { BaseCrudService } from "../services/base-crud.service";

export class Pagination extends DataTableBase {
  public service: BaseCrudService;
  public limit = 10;
  public offset = 0;
  public totalData = 0;
  public currentPage = 1;
  public datas: any = [];
  public keywordSearch = '';

  requestParams(): { [key: string]: any } {
    const params: { [key: string]: any } = { limit: this.limit, offset: this.offset }
    if (this.keywordSearch !== '') {
      params['search'] = this.keywordSearch
    }
    return params
  }

  getData(params: { [key: string]: any } = {}) {
    params = Object.assign(params, this.requestParams())
    this.service.getAllByPaging(params).subscribe(result => {
      this.datas = result.listData;
      this.totalData = result.totalItems;
    })
  }

  primeNgOnPaginationClick(event: any) {
    this.offset = event.first
    this.currentPage = this.offset / this.limit + 1
    this.getData()
  }

  onPaginationClick(page: any) {
    this.currentPage = page
    this.offset = (page - 1) * this.limit
    this.getData()
  }

  searchData() {
    this.currentPage = 1
    this.offset = 0
    this.getData()
  }

  setPage(event: any) {
    this.offset = event.offset * event.pageSize
    this.getData()
  }

  onSearch($event: any) {
    let keyword = ''
    try {
      keyword = $event.target.value.toLocaleLowerCase()
      this.getData({ keyword: keyword })
    } catch (e) {
    }
  }
}
