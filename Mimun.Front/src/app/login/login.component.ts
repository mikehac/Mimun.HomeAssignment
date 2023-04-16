import { Component } from '@angular/core';
import { ManagmentService } from '../managment.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  idNumber: string = '';
  showError: boolean = false;
  constructor(private service: ManagmentService, private router: Router) {}
  onClick() {
    this.service.CustomerExists(this.idNumber).subscribe((g) => {
      if (g) {
        this.router.navigate(['/customerDetails', this.idNumber]);
      } else {
        this.showError = true;
      }
    });
  }
}
