import { Injectable } from '@angular/core';
import { AuthService, User } from '@auth0/auth0-angular';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class LocalAuthService {


  private user: BehaviorSubject<User | null | undefined> = new BehaviorSubject<User | null | undefined>(null);
  public user$: Observable<User | null | undefined> = this.user.asObservable();

  private permissions: BehaviorSubject<String[] | null> = new BehaviorSubject<String[] | null>(null);
  public permissions$: Observable<String[] | null> = this.permissions.asObservable();

  constructor(private  auth: AuthService)
  {

    this.auth.user$.pipe(tap(x =>{
      this.setUser(x);
      this.auth.getAccessTokenSilently().subscribe(x =>{
        const decodedToken = this.getDecodedAccessToken(x);
        this.setPermissions(decodedToken["permissions"]);
      });

    })).subscribe();
  }

  public setUser(user: User | null | undefined){
    this.user.next(user);
  }

  public setPermissions(permissions: String[] | null){
    this.permissions.next(permissions);
  }

  public getPermissions(){
    return this.permissions.getValue();
  }

  getDecodedAccessToken(token: string): any {
    const helper = new JwtHelperService();
    try {
      return helper.decodeToken(token);
    } catch(Error) {
      return null;
    }
  }

}
