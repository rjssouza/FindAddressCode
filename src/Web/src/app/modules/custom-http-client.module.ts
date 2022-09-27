import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

import { AlertMessageModule } from '../modules/alert-message.module';

@Injectable({
  providedIn: "root",
})
export class CustomHttpClientModule {
  constructor(private http: HttpClient, private alertMessageService: AlertMessageModule) {}

  get<T>(url: string): Observable<T> {
    var result = this.http.get<T>(url);
    result.subscribe({
      error: (res) => {
        this.alertMessageService.showAlertFromHttpResponse(res);
      },
    });

    return result;
  }

  delete(url: string): Observable<Object> {
    var result = this.http.delete<Object>(url);
    result.subscribe({
      error: (res) => {
        this.alertMessageService.showAlertFromHttpResponse(res);
      },
    });

    return result;
  }

  post<T>(url: string, body: any | null): Observable<T> {
    var result = this.http.post<T>(url, body);
    result.subscribe({
      error: (res) => {
        this.alertMessageService.showAlertFromHttpResponse(res);
      },
    });

    return result;
  };

  put(url: string, body: any | null): Observable<Object> {
    var result = this.http.put(url, body);
    result.subscribe({
      error: (res) => {
        this.alertMessageService.showAlertFromHttpResponse(res);
      },
    });

    return result;
  };
}
