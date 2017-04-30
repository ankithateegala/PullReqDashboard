import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { PullReq } from './../interfaces/pull-req';
import { Connection, HubConnection } from './../signalR';


@Injectable()
export class FeedService {
  private connection: HubConnection;
  url: string;
  queryString: string;
  
  constructor(private http: Http) {
    this.url = 'http://localhost:5000/api/pullrequest';
    this.connection = new HubConnection(new Connection(this.url, this.queryString));
    this.connection.on('updatePullRequests', pullRequests => this.updatePullRequests(pullRequests));
    this.connection.start();
  }

  private updatePullRequests(pullRequests: PullReq[]){
            console.log('received..', pullRequests);

  }

}
