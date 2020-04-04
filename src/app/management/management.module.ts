import { NgModule } from "@angular/core";
import { ManagementRoutingModule } from "./management-routing.module";
import { SharedModule } from "../shared.module";
import { ImageManagerComponent } from "./image-manager.component";
import { ImageBoxComponent } from "./image-box.component";

@NgModule({
  imports: [
    SharedModule,
    ManagementRoutingModule
  ],
  declarations: [
    ImageManagerComponent,
    ImageBoxComponent
  ]
})
export class ManagementModule {

}