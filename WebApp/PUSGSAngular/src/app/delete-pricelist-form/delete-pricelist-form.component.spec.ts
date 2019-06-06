import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletePricelistFormComponent } from './delete-pricelist-form.component';

describe('DeletePricelistFormComponent', () => {
  let component: DeletePricelistFormComponent;
  let fixture: ComponentFixture<DeletePricelistFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeletePricelistFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeletePricelistFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
