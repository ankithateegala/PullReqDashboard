import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PullReqComponent } from './pull-req.component';

describe('PullReqComponent', () => {
  let component: PullReqComponent;
  let fixture: ComponentFixture<PullReqComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PullReqComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PullReqComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
