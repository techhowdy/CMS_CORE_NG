import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { ForgotPasswordComponent } from "./forgot-password/forgot-password.component";
import { ValidateCodeComponent } from "./validate-code/validate-code.component";
import { SendCodeComponent } from "./send-code/send-code.component";
import { AboutUsComponent } from "./about-us/about-us.component";
import { TermsComponent } from "./terms/terms.component";
import { ContactUsComponent } from "./contact-us/contact-us.component";


const routes: Routes = [
  { path: "home", component: HomeComponent },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'validate-code', component: ValidateCodeComponent },
  { path: 'myaccount', loadChildren: () => import(`./user/user.module`).then(m => m.UserModule) },
  { path: 'send-code', component: SendCodeComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'terms', component: TermsComponent },
  { path: 'contact-us', component: ContactUsComponent },
  { path: '**', component: HomeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
