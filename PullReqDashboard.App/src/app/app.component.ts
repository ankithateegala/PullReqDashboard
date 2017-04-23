import { Component } from '@angular/core';
import { PullReq } from "./pull-req/pull-req";
import { PullReqAPIService } from './services/pull-req-api.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [PullReqAPIService]
})
export class AppComponent {
    PullRequests:PullReq[];

  constructor(private pullReqAPIService: PullReqAPIService) {
    this.pullReqAPIService.getPullRequests().subscribe(a => {
      this.PullRequests = a;
    });
  }
}