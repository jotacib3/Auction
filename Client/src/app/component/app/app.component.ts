import { Component, OnInit } from '@angular/core';
import { JwtHelper } from '../../../../node_modules/angular2-jwt';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'frontend';
  jwtHelper: JwtHelper = new JwtHelper();

  constructor (private authService: AuthService ) {}
  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.authService.decodeToken = this.jwtHelper.decodeToken(token);
    }
  }

}
