import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPricelistFormComponent } from './edit-pricelist-form.component';

describe('EditPricelistFormComponent', () => {
  let component: EditPricelistFormComponent;
  let fixture: ComponentFixture<EditPricelistFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditPricelistFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPricelistFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
