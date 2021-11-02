import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  @Input() title: string = '';
  name: string = '';

  constructor(private authService: AuthService,
              private router: Router) {
    
  }


  authenticatedUser$  = this.authService.authenticatedUser$;
  isUserAuthenticated$ = this.authService.isUserAuthenticated$;
            
  
  ngOnInit(): void {
    this.authenticatedUser$.subscribe(user => {
      this.name = user?.Name;
    });
    
  }
  
  logout(){
    this.authService.logout();

    this.authService.isUserAuthenticated.next(false);

    this.router.navigate(['/login']);
  }

}
