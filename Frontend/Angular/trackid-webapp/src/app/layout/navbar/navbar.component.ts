import { Component, OnInit } from '@angular/core';
import { faBars, faSearch } from '@fortawesome/free-solid-svg-icons';
import { UserService } from 'src/app/user/user.service';
import { LayoutService } from '../layout.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  faSearch = faSearch;
  faBars = faBars;

  constructor(
    private layoutService: LayoutService,
    private userService: UserService) { }

  ngOnInit(): void {
  }

  toggleSidebar() : void {
    this.layoutService.toggle();
  }

  isLoggedIn() : boolean {
    return this.userService.isLoggedIn();
  }
}
