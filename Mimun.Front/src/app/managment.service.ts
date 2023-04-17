import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  private getHttpOptions(token: string): object {
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + token,
      }),
    };
    return httpOptions;
  }

  public CustomerExists(idNumber: string) {
    let queryUrl = this.baseUrl + 'Customer/Login?idNumber=' + idNumber;
    return this.http.get<loginResponse>(queryUrl);
  }

  public getCustomer(idNumber: string, token: string) {
    let queryUrl = this.baseUrl + 'Customer?idNumber=' + idNumber;
    return this.http.get<response>(queryUrl, this.getHttpOptions(token));
  }

  public getPackage(packageId: number, token: string) {
    let queryUrl = this.baseUrl + 'Package?packageId=' + packageId;
    return this.http.get<Package[]>(queryUrl, this.getHttpOptions(token));
  }

  public updateAddress(newAddress: address, token: string) {
    let queryUrl = this.baseUrl + 'Customer';

    return this.http
      .put<address>(queryUrl, newAddress, this.getHttpOptions(token))
      .pipe(
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
  packageTypeName: string;
}

export interface loginResponse {
  customerExist: boolean;
  token: string;
}
