import { Component, OnInit } from "@angular/core";
import { PostcodeService } from "src/app/services/postcode.service";
import { PostcodeLocationViewModel } from "src/app/models/postcode-location.viewmodel";
import { AlertMessageModule } from 'src/app/modules/alert-message.module';
import { AlertType } from "src/app/models/enum/alert-type.enum";

@Component({
  selector: "list-post-code",
  templateUrl: "./list-postcode.component.html",
  styleUrls: ["./list-postcode.component.css"],
})
export class ListPostCodeComponent implements OnInit {

  model: PostcodeLocationViewModel;

  constructor(private postCodeService: PostcodeService, private alertMessageModule: AlertMessageModule) {}

  ngOnInit(): void {
    this.model = new PostcodeLocationViewModel(true);
    this.getPostCodeViewModel();
  }

  getPostCodeViewModel() {
    this.postCodeService
      .getPostCodeViewModel()
      .subscribe((result) => {
        this.model = result;
      });
  }

  getPostCodeLocation() {
    this.model.loading = true;
    this.postCodeService
      .getPostCodeLocation(this.model.postCode)
      .subscribe((result) => {
        this.alertMessageModule.clearMessage();
        this.model.postCodeList.push(result);
        this.model.loading = false;
      }, (res) => {
        this.model.loading = false;
      });
  }
}
