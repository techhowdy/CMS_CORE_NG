import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SendCodeComponent } from './send-code/send-code.component';
import { TermsComponent } from './terms/terms.component';
import { ValidateCodeComponent } from './validate-code/validate-code.component';
import { UserComponent } from './user/user.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthGuardService } from './services/auth-guard.service';

@NgModule({
    declarations: [
        AppComponent,
        AboutUsComponent,
        ContactUsComponent,
        ForgotPasswordComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,
        SendCodeComponent,
        TermsComponent,
        ValidateCodeComponent,
        UserComponent,
        NavMenuComponent
    ],
    imports: [BrowserModule, AppRoutingModule, HttpClientModule, BrowserAnimationsModule, ReactiveFormsModule, ToastrModule.forRoot()],
    providers: [AuthGuardService, CookieService],
    bootstrap: [AppComponent]
})
export class AppModule {}
