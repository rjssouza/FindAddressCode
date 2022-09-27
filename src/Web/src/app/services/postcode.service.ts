import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

import { postCodeUrl } from "src/app/config/api";
import { PostcodeLocation } from "../models/postcode-location";
import { PostcodeLocationViewModel } from "../models/postcode-location.viewmodel";
import { CustomHttpClientModule } from "../modules/custom-http-client.module";

@Injectable({
  providedIn: "root",
})
export class PostcodeService {
  constructor(
    private http: CustomHttpClientModule
  ) {}

  getPostCodeViewModel(): Observable<PostcodeLocationViewModel> {
    var result = this.http.get<PostcodeLocationViewModel>(postCodeUrl);

    return result;
  }

  getPostCodeLocation(postCode: String): Observable<PostcodeLocation> {
    var result = this.http.get<PostcodeLocation>(postCodeUrl + "/" + postCode);

    return result;
  }
}
