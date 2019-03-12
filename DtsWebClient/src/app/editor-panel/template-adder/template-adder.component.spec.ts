import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TemplateAdderComponent } from './template-adder.component';

describe('TemplateAdderComponent', () => {
  let component: TemplateAdderComponent;
  let fixture: ComponentFixture<TemplateAdderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TemplateAdderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TemplateAdderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
