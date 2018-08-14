import { Component, OnInit } from '@angular/core';
import { MenuService } from '../menu.service';
import { Menu } from '../menu';
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  constructor(private menuService: MenuService) { }
  menues: Menu[];
  ngOnInit() {
    this.getMenues();
  }

  getMenues(): void {
    this.menuService.getMenues().subscribe(menues => this.menues = menues);
  }

}
