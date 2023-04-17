import { Component, Input, OnInit } from '@angular/core';
import { Package, contract } from '../managment.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'package',
  templateUrl: './package.component.html',
  styleUrls: ['./package.component.css'],
})
export class PackageComponent implements OnInit {
  @Input() packages: Package[];
  public dataSource = new MatTableDataSource<Package>();
  public displayedColumns = [
    'packageName',
    'packageTypeName',
    'amount',
    'totalUsed',
  ];
  ngOnInit(): void {
    this.dataSource.data.push({
      id: 0,
      packageName: '',
      packageTypeId: 0,
      amount: 0,
      totalUsed: 0,
      contractId: 0,
      packageTypeName: '',
    });
  }

  ngDoCheck(): void {
    if (this.packages != null) {
      this.dataSource.data = this.packages;
    }
  }
}
