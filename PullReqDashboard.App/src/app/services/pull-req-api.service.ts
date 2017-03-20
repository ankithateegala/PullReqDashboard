import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class PullReqAPIService {

  constructor(private http: Http) { 
    console.log("a-service");
  }

  getPullRequests(){
    return this.http.get('http://pullreqdashboardapi-dev.us-east-2.elasticbeanstalk.com/api/values/2').map(res => res.json);
  }

}
