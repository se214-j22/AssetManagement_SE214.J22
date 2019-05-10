import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-mini-chart-item',
  templateUrl: './mini-chart-item.component.html',
  styleUrls: ['./mini-chart-item.component.css']
})
export class MiniChartItemComponent implements OnInit {
  @Input() link: string;
  @Input() title: string;
  @Input() count: number;
  @Input() color: string;
  constructor() { }

  ngOnInit() {
  }

}
