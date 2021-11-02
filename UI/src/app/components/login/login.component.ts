import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginData = {
    userName: '',
    password: ''
  };

  constructor(private authService: AuthService,
    private router:Router) { }

  ngOnInit(): void {
  }

  login(){
    this.authService.login(this.loginData).subscribe((data:any)=>{
      console.log(data);
      
      this.authService.setLocalStorage(data);

      this.authService.authenticatedUser.next(data);
      this.authService.isUserAuthenticated.next(true);
      this.router.navigate(['/']);
    },
    (err) => {
      console.log(err.error);
    });
  }

}
