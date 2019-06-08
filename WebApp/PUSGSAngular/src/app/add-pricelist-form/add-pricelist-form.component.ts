import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { PricelistService } from '../pricelist.service';

@Component({
  selector: 'app-add-pricelist-form',
  templateUrl: './add-pricelist-form.component.html',
  styleUrls: ['./add-pricelist-form.component.css']
})
export class AddPricelistFormComponent implements OnInit {

  addForm = this.fb.group(
    {
      validFrom : ['', Validators.required],
      validUntil : ['', Validators.required]
    }
  )

  constructor(private pricelistService : PricelistService , private fb: FormBuilder) { }

  ngOnInit() {
  }

  onSubmit() {
    this.addPricelist();
  }

  addPricelist()
  {
    this.pricelistService.addPricelist(this.addForm.value).subscribe();
  }
}
