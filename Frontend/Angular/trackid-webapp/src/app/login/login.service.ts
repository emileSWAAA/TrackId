import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

import { AuthenticationRequest } from './authentication.request';
import { environment } from 'src/environments/environment';
import { AuthenticationResponse } from './authentication.response';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {

    constructor(
        private http: HttpClient
    ) { }

    login(authRequest: AuthenticationRequest) {
        return this.http.post<AuthenticationResponse>(`${environment.apiUrl}auth/login`, authRequest)
            .pipe(map(authResp => {
                localStorage.setItem('jwt', JSON.stringify(authResp.token));
                return authResp;
            }));
    }
}