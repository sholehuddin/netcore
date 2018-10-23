import { Component, OnInit, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { FormGroup, FormBuilder, Validators, FormArray } from "@angular/forms";
import { RoleAction } from "../../../../shared/models/action-model";
import { SysParam } from "../../../../shared/models/sys-param";

@Component({
    selector: "app-action-role-form",
    templateUrl: "./action-role-form.component.html",
    styleUrls: ["./action-role-form.component.css"]
})
export class ActionRoleFormComponent implements OnInit {

    roleAction: RoleAction;
    actions: SysParam[];
    role: string;

    sysCtrl: SysParam[] = [];
    selected: SysParam[] = [];

    constructor(
        public dialogRef: MatDialogRef<ActionRoleFormComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any // IRoleAction
    ) { }

    ngOnInit() {
        this.role = this.data.role;
        this.actions = this.data.actions == null ? [] : this.data.actions;
        this.selected = [...this.actions];

        this.constructSysCtrl();
    }

    constructSysCtrl() {
        /**
         * todo:
         * - isi sys control dengan semua value
         * - isi selected dengan data.role.Actions
         */
        this.sysCtrl = this.data.ctrActions;
        this.checkSelectedAsterisk(this.selected);
        // this.selected = this.roleAction.Actions;
    }

    onOk() {

        // this.roleAction.Actions = this.selected;
        let roleAction = <RoleAction>{};
        roleAction.Role = this.role;
        roleAction.Actions = this.selected;

        this.restoreSysAction();
        this.dialogRef.close(roleAction);
    }

    onCancel() {
        this.restoreSysAction();
        this.dialogRef.close();
    }

    restoreSysAction() {
        this.sysCtrl = [];
        this.selected = [];
    }

    onSelect(event: any, row: any) {

        if (event.target.checked) {
            this.selected.push(row);
        } else {
            this.selected = this.selected.filter((item: SysParam) => !this.checkTwoSysParam(item, row));
        }

        this.checkSelectedAsterisk(this.selected);
    }

    checkTwoSysParam(first: SysParam, second: SysParam): boolean {
        return (first.ControllerName == second.ControllerName) &&
            (first.ActionName == second.ActionName);
    }

    checkSelectedAsterisk(selection: SysParam[]): void {
        // cek apakah ada yang bentuknya
        if (selection.length > 0) {
            this.sysCtrl = [...this.data.ctrActions];
            selection.forEach(sysParam => {
                if (sysParam.ActionName == "*") {
                    this.clearSelectedExceptAsterisk(sysParam.ControllerName);
                    this.sysCtrl = this.sysCtrl.filter(
                        item =>
                            (item.ControllerName == sysParam.ControllerName &&
                                item.ActionName == "*") ||
                            item.ControllerName != sysParam.ControllerName
                    );
                }
            });
        } else {
            this.sysCtrl = this.data.ctrActions;
        }
    }

    clearSelectedExceptAsterisk(controllerName: string) {
        let tempIterator = [...this.selected];
        tempIterator.forEach(item => {
            if (
                (item as SysParam).ControllerName == controllerName &&
                (item as SysParam).ActionName != "*"
            ) {
                this.selected.splice(this.selected.indexOf(item), 1);
            }
        });
    }

    isChecked(value: SysParam): boolean {
        let tempIterator = [...this.selected];
        let test = tempIterator.filter((item: SysParam) => (item.ControllerName == value.ControllerName && item.ActionName == value.ActionName))
        return test.length > 0;
    }

    onSelectAll(event: any) {
        console.log('event', event);
        if (event) {
            // all row selected
            // clear selected
            this.selected = [];
        } else {
            // check all row
            let tempSysRow = [...this.sysCtrl];
            this.selected = []; // kosongin
            tempSysRow.forEach((item: SysParam) => {
                // isi selected yang bintang
                if (item.ActionName == '*') {
                    this.selected.push(item);
                }
            });
        }

        // check selected *
        this.checkSelectedAsterisk(this.selected);
    }
}
