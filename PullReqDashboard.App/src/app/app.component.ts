import { Component, OnInit, Input  } from '@angular/core';
import { PullReq } from "./interfaces/pull-req";
import { PullReqAPIService } from './services/pull-req-api.service'
import { FeedService } from './services/feed.service'


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [PullReqAPIService, FeedService]
})
export class AppComponent implements OnInit {
    PullRequests:PullReq[];
    @Input() pullRequests: PullReq[];
    @Input() connection: string;
    
  constructor(private pullReqAPIService: PullReqAPIService, private FeedService: FeedService) {
    this.pullReqAPIService.getPullRequests().subscribe(a => {
      this.PullRequests = a;
    });
  }

  ngOnInit() {
    this.FeedService.updatePullRequests.subscribe(
        p => {
            console.log('received..');
            console.log(p);
    });
  }
}