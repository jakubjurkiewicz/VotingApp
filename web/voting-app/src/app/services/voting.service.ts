import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Candidate } from '../models/candidate.model';
import { Voter } from '../models/voter.model';


@Injectable({
  providedIn: 'root'
})
export class VotingService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getCandidates(): Observable<Candidate[]> {
    return this.http.get<Candidate[]>(`${this.apiUrl}/candidate`);
  }

  getVoters(): Observable<Voter[]> {
    return this.http.get<Voter[]>(`${this.apiUrl}/voter`);
  }

  addVoter(voter: { name: string }): Observable<{ Id: number }> {
    return this.http.post<{ Id: number }>(`${this.apiUrl}/voter`, voter);
  }

  addCandidate(candidate: { name: string }): Observable<{ Id: number }> {
    return this.http.post<{ Id: number }>(`${this.apiUrl}/candidate`, candidate);
  }

  vote(voterId: number, candidateId: number): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.apiUrl}/vote`, { voterId, candidateId });
  }
}
