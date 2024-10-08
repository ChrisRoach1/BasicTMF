import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {



  private notification: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);


  private notified: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public notified$: Observable<boolean> = this.notified.asObservable();

  private notificationLevel: BehaviorSubject<string> = new BehaviorSubject<string>("info");
  public notificationLevel$: Observable<string> = this.notificationLevel.asObservable();

  constructor() { }

  public setNotification(message: string | null, alertLevel: "info" | "success" | "warning" | "error"){
    this.notification.next(message);
    this.notificationLevel.next(alertLevel);
    if(message !== null){
      this.notified.next(true);

      setTimeout(() =>{
        return this.notified.next(false);
      }, 2500);
    }
  }

  public getNotification(){
    return this.notification.getValue();
  }



}
