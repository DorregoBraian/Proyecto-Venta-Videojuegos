import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SesionRecuperarPasswordComponent } from './sesion-recuperar-password.component';

describe('SesionRecuperarPasswordComponent', () => {
  let component: SesionRecuperarPasswordComponent;
  let fixture: ComponentFixture<SesionRecuperarPasswordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SesionRecuperarPasswordComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SesionRecuperarPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
