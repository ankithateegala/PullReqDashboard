import { Component } from '@angular/core';
import { PullReq } from "./Pull-Req/pull-req";
import { PullReqAPIService } from './services/pull-req-api.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [PullReqAPIService]
})
export class AppComponent {
    PullRequests: PullReq[];

  constructor(private pullReqAPIService: PullReqAPIService) {
    this.pullReqAPIService.getPullRequests().subscribe(gets => { 
        console.log(gets);
    });
    this.PullRequests = [
      {      
        title: "Title1",
        col1: "col1",
        col2: "col2",
        col3: "col3"
      },
      {
        title: "Title2",
        col1: "col1",
        col2: "col2",
        col3: "col3"
      },
      {      
        title: "Title3",
        col1: "col1",
        col2: "col2",
        col3: "col3"
      }
    ];

   }
}