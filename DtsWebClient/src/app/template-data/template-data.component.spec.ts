import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TemplateDataComponent } from './template-data.component';

describe('TemplateDataComponent', () => {
  let component: TemplateDataComponent;
  let fixture: ComponentFixture<TemplateDataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TemplateDataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TemplateDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
