import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPricelistFormComponent } from './add-pricelist-form.component';

describe('AddPricelistFormComponent', () => {
  let component: AddPricelistFormComponent;
  let fixture: ComponentFixture<AddPricelistFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddPricelistFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddPricelistFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
