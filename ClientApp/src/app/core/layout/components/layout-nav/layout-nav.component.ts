import { Component, OnInit, Input } from '@angular/core';
import { ViewChild, HostListener, QueryList, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../../../shared/services/auth.service';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { MenuItemModel } from "../../../../shared/models/menu-item";
import { MenuService } from '../../../menu/services/menu.service';
import { MenuItem } from "primeng/primeng";

@Component({
    // tslint:disable-next-line:component-selector
    selector: 'layout-nav',
    templateUrl: './layout-nav.component.html',
    styleUrls: ['./layout-nav.component.css'],
    providers: [MenuService]
})
export class LayoutNavComponent implements OnInit {

    constructor(
        private menuService: MenuService,
        private authService: AuthService
    ) { }

    panelMenus: MenuItem[] = [];
    navMenus: MenuItemModel[] = [];


    ngOnInit() {
        this.menuService.getMenus().subscribe(result => {
            this.panelMenus = this.getChildPanelItems(result);
            this.navMenus = result;
        });
    }


    @Output() childEvent = new EventEmitter();
    @Output() onMenuClick = new EventEmitter();
    @Input() items: MenuItemModel[];

    openSidenav() {
        this.childEvent.emit();
    }

    handleMenuClick() {
        this.onMenuClick.emit();
    }


    getChildPanelItems(items: MenuItemModel[]): any {
        var result: MenuItem[] = [];
        if (items != null && items.length > 0) {
            for (let child of items) {
                let node: MenuItem = {
                    icon: "fa-" + child.Icon,
                    label: child.Name,
                    routerLink: child.Link,
                    styleClass: "mat-list-item mat-ripple",
                    items: this.getChildPanelItems(child.Items)
                }
                result.push(node);
            }
            return result;
        } else {
            return null;
        }

    }
}
