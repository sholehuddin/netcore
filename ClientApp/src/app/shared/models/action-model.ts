import { SysParam } from "./sys-param";

export interface RoleAction {
  Role: string;
  Action: string;
  Actions: SysParam[];
}
