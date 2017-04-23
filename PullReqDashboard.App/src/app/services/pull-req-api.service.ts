import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { PullReq } from "../pull-req/pull-req";

@Injectable()
export class PullReqAPIService 
{
  constructor(private http: Http) {}

  getPullRequests(): Observable<PullReq[]>
  {
    return this.http.get('http://localhost:5000/api/pullrequest')
                    .map(res => res.json());
  }
}