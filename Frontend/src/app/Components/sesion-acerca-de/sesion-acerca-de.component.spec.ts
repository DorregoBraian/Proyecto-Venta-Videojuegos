import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SesionAcercaDeComponent } from './sesion-acerca-de.component';

describe('SesionAcercaDeComponent', () => {
  let component: SesionAcercaDeComponent;
  let fixture: ComponentFixture<SesionAcercaDeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SesionAcercaDeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SesionAcercaDeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
