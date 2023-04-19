import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import {
  ManagmentService,
  Package,
  address,
  contract,
  customer,
} from '../managment.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.css'],
})
export class CustomerDetailsComponent implements OnInit {
  idNumber: string = '';
  token: string = '';
  custumer: customer;
  contracts: contract[];
  packages: Package[];
  form: FormGroup;
  oldAddress: address;
  addressIsDirty: boolean;

  constructor(
    private service: ManagmentService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router
  ) {}
  ngOnInit(): void {
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
    this.initForm();

    this.route.params.subscribe((params: Params) => {
      this.idNumber = params['idNumber'];
      this.token = params['token'];
      this.service.getCustomer(this.idNumber, this.token).subscribe(
        (g) => {
          this.custumer = g.customer;
          this.contracts = g.contracts;
          this.loadAddress(this.custumer.address);
        },
        (err) => {
          console.log(err);
          if (err.status === 401) {
            this.router.navigate(['']);
          }
        }
      );
    });
  }
  initForm(): void {
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
      } else if (this.oldAddress !== undefined) {
        if (this.addressesAreSame(val)) {
          this.addressIsDirty = false;
        } else {
          this.addressIsDirty = true;
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
    this.service.getPackage(selectedContractId, this.token).subscribe((g) => {
      this.packages = g;
    });
  }

  onSubmit(newAddress: address) {
    newAddress.customerId = this.custumer.id;
    this.service.updateAddress(newAddress, this.token).subscribe((response) => {
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

  navigateToMain() {
    this.router.navigate(['']);
  }
}
