import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PanelGcpComponent } from './panel-gcp.component';

describe('PanelGcpComponent', () => {
  let component: PanelGcpComponent;
  let fixture: ComponentFixture<PanelGcpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PanelGcpComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PanelGcpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
