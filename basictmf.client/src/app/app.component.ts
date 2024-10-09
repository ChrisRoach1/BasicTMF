import { DOCUMENT } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { AuthService, User } from '@auth0/auth0-angular';
import { LocalAuthService } from '../services/local-auth.service';
import { BehaviorSubject, Observable, take, tap } from 'rxjs';
import { animate, AnimationTriggerMetadata, state, style, transition, trigger } from '@angular/animations';
import { NotificationService } from '../services/notification.service';
import { StudyService } from '../services/study.service';
import { Study } from '../models/study';

export function FadeInOut(timingIn: number, timingOut: number, height: boolean = false): AnimationTriggerMetadata  {
  return trigger('fadeInOut', [
    transition(':enter', [
      style(height ? { opacity: 0 , height: 0, } : { opacity: 0, }),
      animate(timingIn, style(height ? { opacity: 1, height: 'fit-content' } : { opacity: 1, })),
    ]),
    transition(':leave', [
      animate( timingOut, style(height ? { opacity: 0, height: 0, } : { opacity: 0, })),
    ])
  ]);
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  animations: [FadeInOut(200, 300, true)]
})
export class AppComponent implements OnInit {
  public user: User | null | undefined;

  title = 'TMF';
  public isTextVisible: boolean = false;
  public showNotification$: Observable<boolean> = new Observable<boolean>();
  public studies: BehaviorSubject<Study[] | null> = new BehaviorSubject<Study[] | null>(null);
  public studies$ = this.studies.asObservable();
  public selectedStudyID: number | undefined = undefined;

  constructor(private http: HttpClient,public auth: AuthService,
    @Inject(DOCUMENT) public document: Document, private localAuth: LocalAuthService,
    public notification: NotificationService, private studyService: StudyService)
  {
    this.showNotification$ = this.notification.notified$;
    this.studies$ = this.studyService.getAllStudies();

  }

  ngOnInit() {
    this.localAuth.user$.subscribe(x =>{
      this.user = x;
    });

    this.studyService.selectedStudy$.pipe(take(1), tap(study =>{
      this.selectedStudyID = study?.id;
    })).subscribe();
  }

  login() {
    this.auth.loginWithRedirect({
    });

    this.auth.user$.pipe(tap(x =>{
      this.localAuth.setUser(x);
    })).subscribe();
  }

  studyChanged(){
    console.log(this.selectedStudyID);
    this.studies$.subscribe(studies => {
      var study  = studies?.filter(x => x.id == this.selectedStudyID)[0] ?? null;
      this.studyService.setSelectedStudy(study);
      this.notification.setNotification('Study Switched', 'success');
      this.isTextVisible = true;
    });

  }

  logout() {
    this.auth.logout({
      logoutParams: {
        returnTo: this.document.location.origin
      }
    });
  }

}


