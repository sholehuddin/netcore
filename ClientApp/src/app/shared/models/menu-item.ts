export interface MenuItemModel {
    MenuId: number;
    Link: string;
    Exact: boolean;
    Name: string;
    Icon: string;
    Type: string;
    ParentName: string;
    Disabled: boolean;
    Expanded: boolean;
    Roles: string;
    Order: number;
    RoleList: string[];
    Items: MenuItemModel[];
}
