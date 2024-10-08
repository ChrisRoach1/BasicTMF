import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Study } from '../models/study';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudyService {

  constructor(private http: HttpClient) { }

  getAllStudies(): Observable<Study[]> {

    return this.http.get<Study[]>('api/study');

  }
}
