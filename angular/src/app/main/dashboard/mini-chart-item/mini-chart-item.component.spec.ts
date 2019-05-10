import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MiniChartItemComponent } from './mini-chart-item.component';

describe('MiniChartItemComponent', () => {
  let component: MiniChartItemComponent;
  let fixture: ComponentFixture<MiniChartItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MiniChartItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MiniChartItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
