import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AuthHttpInterceptor, authHttpInterceptorFn, provideAuth0 } from '@auth0/auth0-angular';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeroesListComponent } from './heroes-list/heroes-list.component';
import { IndexFileComponent } from './index-file/index-file.component';
import { StudyListComponent } from './study-list/study-list.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    HeroesListComponent,
    IndexFileComponent,
    StudyListComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [
    provideAnimationsAsync(),
    AuthHttpInterceptor,
    provideHttpClient(withInterceptors([authHttpInterceptorFn])),
    provideAuth0({
      domain: 'dev-tjsbzsbfnaw8tlzb.us.auth0.com',
      clientId: 'sBlBuCSjEVFecapT7WY62BJYE3gMgVt5',
      authorizationParams: {
        redirect_uri: window.location.origin,

        // Request this audience at user authentication time
        audience: 'https://tmf-server.com',

        // // Request this scope at user authentication time
        // scope: 'read:weather, read:current_user',
      },

      // Specify configuration for the interceptor
      httpInterceptor: {
        allowedList: [
          {
            // Match any request that starts 'https://dev-tjsbzsbfnaw8tlzb.us.auth0.com/api/v2/' (note the asterisk)
            uri: '*',
            tokenOptions: {
              authorizationParams: {
                // The attached token should target this audience
                audience: 'https://tmf-server.com',

                // The attached token should have these scopes
                scope: 'read:weather'
              }
            }
          },
          {
            // Match any request that starts 'https://dev-tjsbzsbfnaw8tlzb.us.auth0.com/api/v2/' (note the asterisk)
            uri: '/api/study',
            tokenOptions: {
              authorizationParams: {
                // The attached token should target this audience
                audience: 'https://tmf-server.com',

                // The attached token should have these scopes
                scope: 'study'
              }
            }
          },
          {
            // Match any request that starts 'https://{yourDomain}/api/v2/' (note the asterisk)
            uri: 'https://https://dev-tjsbzsbfnaw8tlzb.us.auth0.com/api/v2/*',
            tokenOptions: {
              authorizationParams: {
                // The attached token should target this audience
                audience: 'https://https://dev-tjsbzsbfnaw8tlzb.us.auth0.com/api/v2/',

                // The attached token should have these scopes
                scope: 'read:current_user, read:roles'
              }
            }
          }
        ]
      }
    }),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
