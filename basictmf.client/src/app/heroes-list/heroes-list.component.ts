import { DOCUMENT } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { LocalAuthService } from '../../services/local-auth.service';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-heroes-list',
  templateUrl: './heroes-list.component.html',
  styleUrl: './heroes-list.component.css'
})


export class HeroesListComponent {
  public forecasts: WeatherForecast[] = [];

  constructor(private http: HttpClient, private local: LocalAuthService) {
    this.getForecasts();
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(result =>{
      this.forecasts = result;
    }
    );
  }
}
