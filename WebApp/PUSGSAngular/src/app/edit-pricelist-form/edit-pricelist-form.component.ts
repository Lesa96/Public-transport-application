import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { PricelistService } from '../pricelist.service';

@Component({
  selector: 'app-edit-pricelist-form',
  templateUrl: './edit-pricelist-form.component.html',
  styleUrls: ['./edit-pricelist-form.component.css']
})
export class EditPricelistFormComponent implements OnInit {

  ticketType = ["OneHourTicket", "Daily", "Monthly", "Annual"];
  passengerType = ["Student", "Pensioner", "Regular"];

  pricelists: any[] = [];
  pricelist: any;
  pricelistItems: any[] = [];
  pricelistItemsLoaded = false;

  selectForm = this.fb.group(
    {
      id: ['', Validators.required]
    }
  )

  editForm = this.fb.group(
    {
      id: ['', Validators.required],
      validFrom: ['', Validators.required],
      validUntil: ['', Validators.required],
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

  constructor(private pricelistService: PricelistService, private fb: FormBuilder) { }

  ngOnInit() {
    this.intializeList();
    this.getPricelists();
  }

  getPricelists() {
    this.pricelistService.getPricelists().subscribe(pricelists => this.pricelists = pricelists);
  }

  onSelectSubmit() {
    this.getPricelistItems();
    this.getPricelist();
  }

  onEditSubmit() {
    this.savePricelist();
  }

  getPricelist() {
    this.pricelistService.getPricelist(this.selectForm.value).subscribe(pricelist => {
      this.pricelist = pricelist;
      this.editForm.controls['id'].patchValue(pricelist.PricelistId);
      this.editForm.controls['validFrom'].patchValue(pricelist.ValidFrom.split("T")[0]);
      this.editForm.controls['validUntil'].patchValue(pricelist.ValidUntil.split("T")[0]);

      this.pricelistItemsLoaded = true;
    });
  }


  getPricelistItems() {
    this.pricelistService.getPricelistItems(this.selectForm.value).subscribe(items => {
      items.forEach((item, index) => {
        this.pricelistItems[index].price = item.Price;
        this.pricelistItems[index].pricelistItemId = item.PricelistItemId;
      });

      this.pricelistItemsForm.controls['price0'].patchValue(items[0].Price);
      this.pricelistItemsForm.controls['price1'].patchValue(items[1].Price);
      this.pricelistItemsForm.controls['price2'].patchValue(items[2].Price);
      this.pricelistItemsForm.controls['price3'].patchValue(items[3].Price);
      this.pricelistItemsForm.controls['price4'].patchValue(items[4].Price);
      this.pricelistItemsForm.controls['price5'].patchValue(items[5].Price);
      this.pricelistItemsForm.controls['price6'].patchValue(items[6].Price);
      this.pricelistItemsForm.controls['price7'].patchValue(items[7].Price);
      this.pricelistItemsForm.controls['price8'].patchValue(items[8].Price);
      this.pricelistItemsForm.controls['price9'].patchValue(items[9].Price);
      this.pricelistItemsForm.controls['price10'].patchValue(items[10].Price);
      this.pricelistItemsForm.controls['price11'].patchValue(items[11].Price);
    }
    );
  }

  savePricelist() {
    let bindingModel = this.editForm.value;
      bindingModel['pricelistItems'] = this.pricelistItems;
    this.pricelistService.savePricelist(bindingModel).subscribe();
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
    
    this.editForm.value.pricelistItems = this.pricelistItems;
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
