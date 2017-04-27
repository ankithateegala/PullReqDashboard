import { Component, OnInit, Input } from '@angular/core';
import {Approver} from '../interfaces/approver';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {
  @Input() createdBy: String;
  @Input() Approvers: Approver[];

  constructor() {
   }

  ngOnInit() {
    console.log(this.Approvers.length);
  }

}
