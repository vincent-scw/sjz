import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { AuthModule, OidcSecurityService } from 'angular-auth-oidc-client';

import { SharedModule } from './shared.module';

import { AppRoutingModule } from './app-routing.module';
import { TimelineModule } from './areas/timeline/timeline.module';
import { AccountModule } from './areas/account/account.module';
import { MomentModule } from './areas/moment/moment.module';
import { ManagementModule } from './management/management.module';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar.component';
import { MainComponent } from './main.component';
import { AccessKeyDialogComponent } from './areas/timeline/access-key-dialog/access-key-dialog.component';
import { AuthGuard } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';
import { TimelineService } from './services/timeline.service';
import { AuthInterceptor } from './services/auth.interceptor';
import { ImageService } from './services/image.service';
import { FooterComponent } from './footer.component';
import { PageNotFoundComponent } from './page-not-found.component';
import { MomentService } from './services/moment.service';

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
    AuthModule.forRoot(),
    SharedModule,
    TimelineModule,
    MomentModule,
    AccountModule,
    ManagementModule,
    AppRoutingModule
  ],
  providers: [
    AuthService,
    AuthGuard,
    OidcSecurityService,
    TimelineService,
    MomentService,
    ImageService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  entryComponents: [
    AccessKeyDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
