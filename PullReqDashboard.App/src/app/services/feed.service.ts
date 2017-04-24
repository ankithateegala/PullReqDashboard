import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from "rxjs/Subject";
import { PullReq } from "../interfaces/pull-req";
import { FeedSignalR, FeedProxy, FeedClient, SignalRConnectionStatus} from '../interfaces/signalr';
/*import * as $ from 'jquery';*/

@Injectable()
export class FeedService {
  currentState = SignalRConnectionStatus.Disconnected;
  connectionState: Observable<SignalRConnectionStatus>;
  updatePullRequests:Observable<PullReq[]>;
  setConnectionId: Observable<string>;

  private updatePullRequestsSubject = new Subject<PullReq[]>();
  private connectionStateSubject = new Subject<SignalRConnectionStatus>();
  private setConnectionIdSubject = new Subject<string>();
  
  constructor(private http: Http) { 
    this.connectionState = this.connectionStateSubject.asObservable();
    this.updatePullRequests = this.updatePullRequestsSubject.asObservable();
    this.setConnectionId = this.setConnectionIdSubject.asObservable();
    this.start(true);
  }

  start(debug: boolean): Observable<SignalRConnectionStatus> {

    $.connection.hub.logging = debug;
    // Configure the proxy
    let connection = <FeedSignalR>$.connection;
    // reference signalR hub named 'broadcaster'
    let feedHub = connection.broadcaster;

    // Methods called by server
    feedHub.client.setConnectionId = id => this.onSetConnectionId(id);
    feedHub.client.updatePullRequests = pullRequests => this.onUpdatePullRequests(pullRequests);
  
    // start the connection
    console.log("start...");
    
    $.connection.hub.start()
        .done(response => this.setConnectionState(SignalRConnectionStatus.Connected))
        .fail(error => this.updatePullRequestsSubject.error(error));
  
    return this.connectionState;
  }

  private setConnectionState(connectionState: SignalRConnectionStatus) {
      console.log('connection state changed to: ' + connectionState);
      this.currentState = connectionState;
      this.connectionStateSubject.next(connectionState);
  }

  private onUpdatePullRequests(pullRequests: PullReq[]){
    this.updatePullRequestsSubject.next(pullRequests);
  }

  private onSetConnectionId(id: string) {
      this.setConnectionIdSubject.next(id);
  }

}
