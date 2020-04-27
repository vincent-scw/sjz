import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MomentEditorComponent } from './moment-editor.component';

describe('MomentEditorComponent', () => {
  let component: MomentEditorComponent;
  let fixture: ComponentFixture<MomentEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [MomentEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MomentEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
