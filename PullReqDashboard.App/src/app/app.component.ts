import { Component } from '@angular/core';
import { PullReq } from "./Pull-Req/pull-req";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
    PullRequests: PullReq[];

  constructor() {
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
