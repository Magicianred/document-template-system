import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminTemplatePanelComponent } from './admin-template-panel.component';

describe('AdminTemplatePanelComponent', () => {
  let component: AdminTemplatePanelComponent;
  let fixture: ComponentFixture<AdminTemplatePanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminTemplatePanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminTemplatePanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
