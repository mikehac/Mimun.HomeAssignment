import { Component } from '@angular/core';
import { ManagmentService, contract, customer } from './managment.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  idNumber: string = '';
  custumer: customer;
  contracts: contract[];
  constructor(private service: ManagmentService) {
    this.custumer = {
      id: 0,
      idNumber: '',
      firstName: '',
      lastName: '',
      address: {
        customerId: 0,
        city: '',
        street: '',
        houseNumber: '',
        postalCode: '',
      },
    };
  }
  onClick() {
    this.service.getCustomer(this.idNumber).subscribe((g) => {
      this.custumer = g.customer;
      this.contracts = g.contracts;
      console.log(this.custumer);
      console.log(this.contracts);
    });
  }
  custumerIsValid() {
    //If didn't find customer by Id Number, hide "Details" and "Address" sections
    if (
      this.custumer != null &&
      this.custumer.firstName !== '' &&
      this.custumer.lastName !== ''
    )
      return true;
    return false;
  }
}
