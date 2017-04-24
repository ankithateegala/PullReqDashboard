
import{Approver} from "./approver";

export interface PullReq {
    title:string;
    url:string;
    createdBy:string;
    approver:Approver[];
}
