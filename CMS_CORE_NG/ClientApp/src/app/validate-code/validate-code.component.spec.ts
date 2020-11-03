import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidateCodeComponent } from './validate-code.component';

describe('ValidateCodeComponent', () => {
  let component: ValidateCodeComponent;
  let fixture: ComponentFixture<ValidateCodeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ValidateCodeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ValidateCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
