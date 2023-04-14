import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { contract } from '../managment.service';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';

@Component({
  selector: 'contract',
  templateUrl: './contract.component.html',
  styleUrls: ['./contract.component.css'],
})
export class ContractComponent implements OnInit {
  @Input() contracts: contract[];
  @Output() selectedContractId = new EventEmitter<number>();
  public dataSource = new MatTableDataSource<contract>();
  public displayedColumns = ['contractNumber', 'name', 'typeName'];

  ngOnInit(): void {
    this.dataSource.data.push({
      id: 0,
      contractNumber: '',
      name: '',
      typeId: 0,
      customerId: 0,
      typeName: '',
    });
  }

  ngDoCheck(): void {
    if (this.contracts != null) {
      this.dataSource.data = this.contracts;
    }
  }
  onClickRow(id: number) {
    // console.log(id);
    this.selectedContractId.emit(id);
  }
}
