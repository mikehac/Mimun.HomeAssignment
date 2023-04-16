import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CustomerDetailsComponent } from './customer-details/customer-details.component';

const appRoutes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'customerDetails/:idNumber', component: CustomerDetailsComponent },
];

export const routing = RouterModule.forRoot(appRoutes);
