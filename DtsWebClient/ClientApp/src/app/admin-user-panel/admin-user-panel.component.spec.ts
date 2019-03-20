import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminUserPanelComponent } from './admin-user-panel.component';

describe('AdminUserPanelComponent', () => {
  let component: AdminUserPanelComponent;
  let fixture: ComponentFixture<AdminUserPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminUserPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminUserPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
