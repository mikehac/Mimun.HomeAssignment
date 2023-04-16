import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ManagmentService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = environment.apiUrl;
  }

  public getCustomer(idNumber: string) {
    let queryUrl = this.baseUrl + 'Customer?idNumber=' + idNumber;
    return this.http.get<response>(queryUrl);
  }

  public getPackage(packageId: number) {
    let queryUrl = this.baseUrl + 'Package?packageId=' + packageId;
    return this.http.get<Package[]>(queryUrl);
  }

  public updateAddress(newAddress: address) {
    let queryUrl = this.baseUrl + 'Customer';
    return this.http.put<address>(queryUrl, newAddress).pipe(
      map((response) => {
        return response;
      })
    );
  }
}

export interface response {
  customer: customer;
  contracts: contract[];
}
export interface customer {
  id: number;
  idNumber: string;
  firstName: string;
  lastName: string;
  address: address;
}
export interface address {
  [x: string]: any;
  customerId: number;
  city: string;
  street: string;
  houseNumber: string;
  postalCode: string;
}
export interface contract {
  id: number;
  contractNumber: string;
  name: string;
  typeId: number;
  customerId: number;
  typeName: string;
}
export interface Package {
  id: number;
  packageName: string;
  packageTypeId: number;
  amount: number;
  totalUsed: number;
  contractId: number;
}
