import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardGenerosComponent } from './card-generos.component';

describe('CardGenerosComponent', () => {
  let component: CardGenerosComponent;
  let fixture: ComponentFixture<CardGenerosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CardGenerosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardGenerosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
