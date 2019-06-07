import { Component, OnInit } from '@angular/core';
import { PricelistService } from 'src/app/pricelist.service';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-delete-pricelist-form',
  templateUrl: './delete-pricelist-form.component.html',
  styleUrls: ['./delete-pricelist-form.component.css']
})
export class DeletePricelistFormComponent implements OnInit {

  pricelists : any[] = [];
  
    deleteForm = this.fb.group(
      {
        id : ['', Validators.required]
      }
    )
  
    constructor(private pricelistService : PricelistService , private fb: FormBuilder) { }

  ngOnInit() {
    this.getPricelists();
  }

  onSubmit() {
    this.deletePricelist();
  }

  getPricelists()
  {
    this.pricelistService.getPricelists().subscribe(pricelists => this.pricelists = pricelists);
  }

  deletePricelist()
  {
    this.pricelistService.deletePricelist(this.deleteForm.value).subscribe();
  }

}
