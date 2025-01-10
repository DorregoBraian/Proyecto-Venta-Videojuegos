import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SesionRegistrarComponent } from './sesion-registrar.component';

describe('SesionRegistrarComponent', () => {
  let component: SesionRegistrarComponent;
  let fixture: ComponentFixture<SesionRegistrarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SesionRegistrarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SesionRegistrarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
