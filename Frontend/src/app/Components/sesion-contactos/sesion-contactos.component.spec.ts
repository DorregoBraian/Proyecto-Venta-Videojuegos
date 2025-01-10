import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SesionContactosComponent } from './sesion-contactos.component';

describe('SesionContactosComponent', () => {
  let component: SesionContactosComponent;
  let fixture: ComponentFixture<SesionContactosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SesionContactosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SesionContactosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
