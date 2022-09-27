import { Component, OnInit, Input } from '@angular/core';
import { AlertMessage } from 'src/app/models/alert-message';
import { AlertMessageModule } from 'src/app/modules/alert-message.module';
import { AlertType } from "../../../models/enum/alert-type.enum";

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent implements OnInit {
  enum: typeof AlertType = AlertType;

  constructor(private alertMessageService: AlertMessageModule) { 
  }

  model: AlertMessage = new AlertMessage('', '');

  ngOnInit() {
    this.alertMessageService.getMessage().subscribe((alertMessage: AlertMessage) => {
      this.model = alertMessage;
    })
  }
}
