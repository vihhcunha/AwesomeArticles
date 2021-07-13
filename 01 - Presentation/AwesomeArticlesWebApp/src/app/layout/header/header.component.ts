import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {

  mostrarMenu: boolean = false;

  constructor(public userService: UserService) { }

  ngOnInit(): void {
  }

  toggleMenu(){
    this.mostrarMenu = !this.mostrarMenu
  }

  sair(){
    this.userService.logout();
  }

}
