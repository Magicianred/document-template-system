import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditorsTemplatesComponent } from './editors-templates.component';

describe('EditorsTemplatesComponent', () => {
  let component: EditorsTemplatesComponent;
  let fixture: ComponentFixture<EditorsTemplatesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditorsTemplatesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditorsTemplatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
