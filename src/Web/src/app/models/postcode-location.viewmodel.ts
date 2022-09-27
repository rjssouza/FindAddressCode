import * as internal from "assert";
import { PostcodeLocation } from "src/app/models/postcode-location";

export class PostcodeLocationViewModel {
  loading: boolean;
  postCode: string;
  postCodeList: PostcodeLocation[] = [];
  destinationPlaceName: string;
  
  constructor(loading: boolean) {
    this.loading = loading;
  }

  hasList(): boolean {
    var result = this.postCodeList.length > 0;
    return result;
  }
}
