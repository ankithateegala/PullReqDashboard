import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class PullReqAPIService {

  constructor(private http: Http) { 
  }

  getPullRequests(){
    return this.http
                .get('http://localhost:17897/api/PullRequest/5')
                .map(res => res.json);
  }
}