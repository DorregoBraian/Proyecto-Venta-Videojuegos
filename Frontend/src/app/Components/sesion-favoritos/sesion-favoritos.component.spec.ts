import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SesionFavoritosComponent } from './sesion-favoritos.component';

describe('SesionFavoritosComponent', () => {
  let component: SesionFavoritosComponent;
  let fixture: ComponentFixture<SesionFavoritosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SesionFavoritosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SesionFavoritosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
