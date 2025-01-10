import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SesionHomeComponent } from './sesion-home.component';

describe('SesionHomeComponent', () => {
  let component: SesionHomeComponent;
  let fixture: ComponentFixture<SesionHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SesionHomeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SesionHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
