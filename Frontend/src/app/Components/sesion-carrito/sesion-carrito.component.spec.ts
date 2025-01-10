import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SesionCarritoComponent } from './sesion-carrito.component';

describe('SesionCarritoComponent', () => {
  let component: SesionCarritoComponent;
  let fixture: ComponentFixture<SesionCarritoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SesionCarritoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SesionCarritoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
