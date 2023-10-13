import { Component, OnInit } from '@angular/core';
import { sideNavAuthenticatedLinks } from '../constants';
import { NavLink } from '../types';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  navLinks: NavLink[] = [];

  ngOnInit(): void {
    // TODO: Adjust links shown based on authentication when auth added
    this.navLinks = sideNavAuthenticatedLinks;
  }
}
