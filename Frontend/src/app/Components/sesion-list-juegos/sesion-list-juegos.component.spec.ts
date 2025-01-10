import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SesionListJuegosComponent } from './sesion-list-juegos.component';

describe('SesionListJuegosComponent', () => {
  let component: SesionListJuegosComponent;
  let fixture: ComponentFixture<SesionListJuegosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SesionListJuegosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SesionListJuegosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
