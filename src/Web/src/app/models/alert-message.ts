import * as internal from "assert";
import { AppComponent } from '../app.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AlertType } from './enum/alert-type.enum'

export class AlertMessage {
  alertType: AlertType;
  message: string;

  constructor(messageType, message) {
    this.setMessageType(messageType);
    this.setMessage(message);
  }

  getMessage(): string{
    return this.message;
  }

  setMessage(message: string){
    this.message = message;
  }

  setMessageType(alertType: AlertType){
    this.alertType = alertType;
  }

  getAlertType(): AlertType{
    return this.alertType;
  }
}
