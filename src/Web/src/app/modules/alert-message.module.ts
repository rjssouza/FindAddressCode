import * as internal from "assert";
import { NgModule } from "@angular/core";
import { AlertMessage } from "../models/alert-message";
import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { AlertType } from "../models/enum/alert-type.enum";
import { HttpStatusCode } from "../models/enum/http-code.enum";


@Injectable({
  providedIn: "root",
})
export class AlertMessageModule {
  public static alertMessage: AlertMessage;
  subject = new Subject();

  constructor() {}

  getMessage() {
    return this.subject.asObservable();
  }

  clearMessage(){
    var alert = new AlertMessage('', '');
    this.subject.next(alert);
  }

  showAlert(type, message) {
    var alert = new AlertMessage(type, message);
    this.subject.next(alert);
  }

  showAlertFromHttpResponse(result) {
    var alert: AlertMessage; // = new AlertMessage(type, message);

    switch (result.error.statusCode) {
      case HttpStatusCode.PreconditionFailed:
        alert = new AlertMessage(AlertType.Warning, result.error.message);
        break;
      case HttpStatusCode.Ok:
        alert = new AlertMessage(AlertType.Success, result.error.message);
        break;
      default:
        alert = new AlertMessage(AlertType.Error, result.error.message);
        break;
    }

    this.subject.next(alert);
  }
}
