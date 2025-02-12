import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs';
@Injectable({
  providedIn: 'root',
})

export class AuthService {
  
  private apiUrl = 'http://localhost:5003/api'; // Update this with your backend URL
  
  constructor(private http: HttpClient) {}

  // Register a new user
  register(user: { userName: string; userId: string; email: string; userPassword: string;  isActive: boolean }): Observable<any> {
    const token = localStorage.getItem('jwt');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': token ? `Bearer ${token}` : ''
    });
    
    console.log(user);
    return this.http.post(`${this.apiUrl}/register`, user, {headers})
    .pipe(
      catchError(error => {
        throw error; 
      })
    );
  }
  
  // Login a user
  login(user: { email: string; userPassword: string; isActive: true; }): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, user, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }).pipe(
      catchError(error => {
        console.error('Login error:', error);
        throw error;
      })
    );
  }
  

  // Store JWT token
  saveToken(token: string): void {
    localStorage.setItem('jwtToken', token);
  }

  // Retrieve JWT token
  getToken(): string | null {
    return localStorage.getItem('jwtToken');
  }

  // Remove JWT token
  logout(): void {
    localStorage.removeItem('jwtToken');
  }
}
