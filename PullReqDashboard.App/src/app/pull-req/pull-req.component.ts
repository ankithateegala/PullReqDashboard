import { Component, OnInit, Input } from '@angular/core';
import { PullReq } from "./pull-req";
import { Approver } from "./approver";

@Component({
  selector: 'app-pull-req',
  templateUrl: './pull-req.component.html',
  styleUrls: ['./pull-req.component.css']
})
export class PullReqComponent implements OnInit {
  @Input() PullRequest: PullReq;
  ApproverCount:number;
  hideFirst:boolean;
  hideSecond:boolean;
  hideThird:boolean;
  constructor() {
  }

  ngOnInit() {
    this.ApproverCount = this.PullRequest.approver.length;

    if(this.ApproverCount == 1){
      this.hideFirst = false
      this.hideSecond = true
      this.hideThird = true
    }
    else if(this.ApproverCount > 1){
      this.hideFirst = true
      this.hideSecond = false
      this.hideThird = true
    }
    else if(this.ApproverCount == 0){
      this.hideFirst = true
      this.hideSecond = true
      this.hideThird = true
    }
  }
}