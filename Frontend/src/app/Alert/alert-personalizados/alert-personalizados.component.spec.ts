import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlertPersonalizadosComponent } from './alert-personalizados.component';

describe('AlertPersonalizadosComponent', () => {
  let component: AlertPersonalizadosComponent;
  let fixture: ComponentFixture<AlertPersonalizadosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AlertPersonalizadosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AlertPersonalizadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
