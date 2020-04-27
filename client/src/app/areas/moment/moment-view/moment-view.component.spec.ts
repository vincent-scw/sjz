import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MomentViewComponent } from './moment-view.component';

describe('MomentViewComponent', () => {
  let component: MomentViewComponent;
  let fixture: ComponentFixture<MomentViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [MomentViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MomentViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
