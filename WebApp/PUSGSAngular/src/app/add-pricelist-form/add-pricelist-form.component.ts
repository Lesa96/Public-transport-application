import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { PricelistService } from '../pricelist.service';

@Component({
  selector: 'app-add-pricelist-form',
  templateUrl: './add-pricelist-form.component.html',
  styleUrls: ['./add-pricelist-form.component.css']
})
export class AddPricelistFormComponent implements OnInit {

  ticketType = ["OneHourTicket", "Daily", "Monthly", "Annual"];
  passengerType = ["Student", "Pensioner", "Regular"];
  pricelistItems : any[] = [];

  addForm = this.fb.group(
    {
      validFrom : ['', Validators.required],
      validUntil : ['', Validators.required],
      pricelistItems : []
    }
  )

  pricelistItemsForm = this.fb.group(
    {
      price0 : ['', Validators.required],
      price1 : ['', Validators.required],
      price2 : ['', Validators.required],
      price3 : ['', Validators.required],
      price4 : ['', Validators.required],
      price5 : ['', Validators.required],
      price6 : ['', Validators.required],
      price7 : ['', Validators.required],
      price8 : ['', Validators.required],
      price9 : ['', Validators.required],
      price10 : ['', Validators.required],
      price11 : ['', Validators.required]
    }
  )

  constructor(private pricelistService : PricelistService , private fb: FormBuilder) { }

  ngOnInit() {
    this.intializeList();
  }

  onSubmit() {
    this.addPricelist();
  }

  addPricelist()
  {
    let bindingModel = this.addForm.value;
      bindingModel['pricelistItems'] = this.pricelistItems;
    this.pricelistService.addPricelist(bindingModel).subscribe();
  }

  savePrices()
  {
    this.pricelistItems[0].price = this.pricelistItemsForm.value.price0;
    this.pricelistItems[1].price = this.pricelistItemsForm.value.price1;
    this.pricelistItems[2].price = this.pricelistItemsForm.value.price2;
    this.pricelistItems[3].price = this.pricelistItemsForm.value.price3;
    this.pricelistItems[4].price = this.pricelistItemsForm.value.price4;
    this.pricelistItems[5].price = this.pricelistItemsForm.value.price5;
    this.pricelistItems[6].price = this.pricelistItemsForm.value.price6;
    this.pricelistItems[7].price = this.pricelistItemsForm.value.price7;
    this.pricelistItems[8].price = this.pricelistItemsForm.value.price8;
    this.pricelistItems[9].price = this.pricelistItemsForm.value.price9;
    this.pricelistItems[10].price = this.pricelistItemsForm.value.price10;
    this.pricelistItems[11].price = this.pricelistItemsForm.value.price11;
    this.addForm.value.pricelistItems = this.pricelistItems;
  }

  intializeList()
  {
    this.passengerType.forEach( (pType, pIndex) => {
      this.ticketType.forEach( (tType, tIndex) => {
        this.pricelistItems.push(
          new Object({
          passengerType: this.passengerType[pIndex],
          ticketType: this.ticketType[tIndex],
          price: 0
        })); 
      });  
    });
  }
}
