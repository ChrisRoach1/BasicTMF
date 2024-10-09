import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Study } from '../models/study';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudyService {

  private selectedStudy: BehaviorSubject<Study | null> = new BehaviorSubject<Study | null>(null);
  public selectedStudy$ = this.selectedStudy.asObservable();

  constructor(private http: HttpClient) {
    if(localStorage.getItem("selectedStudy")){
      var study = JSON.parse(localStorage.getItem("selectedStudy") ?? "");
      this.selectedStudy.next(study);
    }
   }

  getAllStudies(): Observable<Study[]> {
    return this.http.get<Study[]>('api/study');
  }

  setSelectedStudy(study: Study | null){
    localStorage.setItem("selectedStudy", JSON.stringify(study));
    this.selectedStudy.next(study);
  }



}
