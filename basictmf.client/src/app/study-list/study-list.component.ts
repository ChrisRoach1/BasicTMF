import { Component, OnInit } from '@angular/core';
import { Study } from '../../models/study';
import { StudyService } from '../../services/study.service';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-study-list',
  templateUrl: './study-list.component.html',
  styleUrl: './study-list.component.css'
})
export class StudyListComponent implements OnInit {

  public studies: BehaviorSubject<Study[] | null> = new BehaviorSubject<Study[] | null>(null);
  public studies$ = this.studies.asObservable();

  public constructor(private studyService: StudyService, private http: HttpClient){}

  ngOnInit(): void {
    this.studies$ = this.studyService.getAllStudies();

  }


}
