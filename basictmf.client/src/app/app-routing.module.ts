import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeroesListComponent } from './heroes-list/heroes-list.component';
import { StudyListComponent } from './study-list/study-list.component';

const routes: Routes = [{path: 'heroes-list', component: HeroesListComponent}, {path: 'study-list', component: StudyListComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
