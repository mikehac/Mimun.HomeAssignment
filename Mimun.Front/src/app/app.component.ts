import { Component, OnInit } from '@angular/core';
import {
  ManagmentService,
  Package,
  address,
  contract,
  customer,
} from './managment.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  idNumber: string = '303456891';
  custumer: customer;
  contracts: contract[];
  packages: Package[];
  form: FormGroup;
  oldAddress: address;
  addressIsDirty: boolean;

  constructor(
    private service: ManagmentService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar
  ) {
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
  ngOnInit(): void {
    this.form = this.formBuilder.group({
      city: this.formBuilder.control(''),
      street: this.formBuilder.control(''),
      houseNumber: this.formBuilder.control(''),
      postalCode: this.formBuilder.control(''),
    });
    this.onChanges();
  }

  onChanges(): void {
    this.form.valueChanges.subscribe((val: address) => {
      if (
        this.oldAddress === undefined &&
        val.city !== '' &&
        val.street !== '' &&
        val.houseNumber !== '' &&
        val.postalCode !== ''
      ) {
        this.oldAddress = val;
        console.log(this.oldAddress);
      } else if (this.oldAddress !== undefined) {
        if (this.addressesAreSame(val)) {
          this.addressIsDirty = false;
          // console.log('not changed');
        } else {
          this.addressIsDirty = true;
          // console.log('changed');
        }
      }
    });
  }
  addressesAreSame(newAddress: address): boolean {
    if (
      newAddress.city === this.oldAddress.city &&
      newAddress.street === this.oldAddress.street &&
      newAddress.houseNumber === this.oldAddress.houseNumber &&
      newAddress.postalCode === this.oldAddress.postalCode
    ) {
      return true;
    }
    return false;
  }

  private loadAddress(address: address) {
    this.form.controls['city'].setValue(address.city);
    this.form.controls['street'].setValue(address.street);
    this.form.controls['houseNumber'].setValue(address.houseNumber);
    this.form.controls['postalCode'].setValue(address.postalCode);
  }

  onClick() {
    this.service.getCustomer(this.idNumber).subscribe((g) => {
      this.custumer = g.customer;
      this.contracts = g.contracts;
      this.loadAddress(this.custumer.address);
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

  onSelectContractId(selectedContractId: number) {
    this.service.getPackage(selectedContractId).subscribe((g) => {
      console.log(g);
      this.packages = g;
    });
  }

  onSubmit(newAddress: address) {
    // console.log(newAddress);
    newAddress.customerId = this.custumer.id;
    this.service.updateAddress(newAddress).subscribe((response) => {
      console.log(response);
      if (response['updated']) {
        this.openSnackBar('The address updated successfully');
      } else {
        this.openSnackBar("The address wasn't updated");
      }
    });
  }

  openSnackBar(message: string) {
    this.snackBar.open(message, '', {
      duration: 2000,
    });
  }
}
