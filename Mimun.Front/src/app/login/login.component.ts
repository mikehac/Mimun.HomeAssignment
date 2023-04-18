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
  showCustomerNotExistError: boolean = false;
  idNumberIsValid: boolean = true;
  constructor(private service: ManagmentService, private router: Router) {}
  onClick() {
    if (!this.isIsraeliIdNumber(this.idNumber)) {
      this.idNumberIsValid = false;
      return;
    }
    this.idNumberIsValid = true;
    this.service.CustomerExists(this.idNumber).subscribe(
      (response) => {
        if (response.customerExist) {
          this.router.navigate([
            '/customerDetails',
            this.idNumber,
            response.token,
          ]);
        } else {
          this.showCustomerNotExistError = true;
        }
      },
      (err) => {
        console.log(err);
        if (err.status === 404) {
          this.showCustomerNotExistError = true;
        }
      }
    );
  }
  isIsraeliIdNumber(id: string) {
    id = String(id).trim();
    if (id.length > 9) return false;
    id = id.length < 9 ? ('00000000' + id).slice(-9) : id;
    return (
      Array.from(id, Number).reduce((counter, digit, i) => {
        const step = digit * ((i % 2) + 1);
        return counter + (step > 9 ? step - 9 : step);
      }) %
        10 ===
      0
    );
  }
}
