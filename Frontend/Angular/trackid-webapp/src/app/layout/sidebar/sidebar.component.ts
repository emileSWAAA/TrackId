import { Component, OnInit } from '@angular/core';
import { LayoutService } from '../layout.service';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  animations: [
    trigger('slideInOut', [
      state('show', style({
        transform: 'translate3d(0,0,0)'
      })),
      state('hide', style({
        transform: 'translate3d(-100%, 0, 0)'
      })),
      transition('show => hide', animate('400ms ease-in-out')),
      transition('hide => show', animate('400ms ease-in-out'))
    ]),
  ]
})
export class SidebarComponent implements OnInit {

  sidebarState: string = 'show';

  constructor(private layoutService : LayoutService) {
  }

  ngOnInit() : void {
    this.layoutService.getSidebarState().subscribe(data => {
      this.sidebarState = data === true ? 'show' : 'hide';
    });    
  }

}
