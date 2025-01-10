import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardFavoritosComponent } from './card-favoritos.component';

describe('CardFavoritosComponent', () => {
  let component: CardFavoritosComponent;
  let fixture: ComponentFixture<CardFavoritosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CardFavoritosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardFavoritosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
