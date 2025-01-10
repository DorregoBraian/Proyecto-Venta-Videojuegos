import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SesionDetalleJuegoComponent } from './sesion-detalle-juego.component';

describe('SesionDetalleJuegoComponent', () => {
  let component: SesionDetalleJuegoComponent;
  let fixture: ComponentFixture<SesionDetalleJuegoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SesionDetalleJuegoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SesionDetalleJuegoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
