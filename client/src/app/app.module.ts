import { NgModule } from '@angular/core';

import { SharedModule } from './shared.module';

import { AppRoutingModule } from './app-routing.module';
import { TimelineModule } from './timeline/timeline.module';
import { AccountModule } from './account/account.module';
import { RecordModule } from './record/record.module';
import { ManagementModule } from './management/management.module';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar.component';
import { MainComponent } from './main.component';
import { AccessKeyDialogComponent } from './timeline/access-key-dialog/access-key-dialog.component';
import { AuthGuard } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';
import { TimelineService } from './services/timeline.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './services/auth.interceptor';
import { ImageService } from './services/image.service';
import { FooterComponent } from './footer.component';
import { PageNotFoundComponent } from './page-not-found.component';
import { RecordService } from './services/record.service';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    NavbarComponent,
    FooterComponent,
    AccessKeyDialogComponent,
    PageNotFoundComponent
  ],
  imports: [
    SharedModule,
    TimelineModule,
    RecordModule,
    AccountModule,
    ManagementModule,
    AppRoutingModule
  ],
  providers: [
    AuthService,
    AuthGuard,
    TimelineService,
    RecordService,
    ImageService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  entryComponents: [
    AccessKeyDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
