import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcessDetailsComponent } from './processdetails.component';

describe('ProcessdetailsComponent', () => {
  let component: ProcessDetailsComponent;
  let fixture: ComponentFixture<ProcessDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ProcessDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcessDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
