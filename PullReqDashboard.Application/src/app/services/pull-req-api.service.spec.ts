import { TestBed, inject } from '@angular/core/testing';

import { PullReqAPIService } from './pull-req-api.service';

describe('PullReqAPIService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PullReqAPIService]
    });
  });

  it('should ...', inject([PullReqAPIService], (service: PullReqAPIService) => {
    expect(service).toBeTruthy();
  }));
});
