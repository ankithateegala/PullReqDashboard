import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class PullReqAPIService {

  constructor(private http: Http) { 
    console.log("service");
  }

  getPullRequests(){
    return this.http.get('https://ucikqskfm5.execute-api.us-east-1.amazonaws.com/Prod/api/values/3').map(res => res.json);
  }

}
