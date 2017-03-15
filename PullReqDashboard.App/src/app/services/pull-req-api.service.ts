import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class PullReqAPIService {

  constructor(private http: Http) { 
    console.log("a-service");
  }

  getPullRequests(){
    return this.http.get('https://w3qcs22w8h.execute-api.us-east-1.amazonaws.com/Prod/api/values/1').map(res => res.json);
  }

}
