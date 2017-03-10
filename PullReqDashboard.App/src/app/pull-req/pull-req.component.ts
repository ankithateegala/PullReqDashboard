import { Component, OnInit, Input } from '@angular/core';
import { PullReq } from "./pull-req";

@Component({
  selector: 'app-pull-req',
  templateUrl: './pull-req.component.html',
  styleUrls: ['./pull-req.component.css']
})
export class PullReqComponent implements OnInit {
  @Input() PullRequest: PullReq;

  constructor() {
   }

  ngOnInit() {
  }

}