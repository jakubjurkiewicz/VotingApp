import { Component, OnInit } from '@angular/core';
import { VotingService } from '../../services/voting.service';
import { Candidate } from '../../models/candidate.model';
import { Voter } from '../../models/voter.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-voting',
  templateUrl: './voting.component.html',
  imports: [CommonModule, FormsModule],
  standalone: true,
  styleUrls: ['./voting.component.scss']
})
export class VotingComponent implements OnInit {
  candidates: Candidate[] = [];
  voters: Voter[] = [];
  availableVoters: Voter[] = [];
  newCandidate = { name: '' };
  newVoter = { name: '' };
  selectedVoterId: number | null = null;
  selectedCandidateId: number | null = null;

  constructor(private votingService: VotingService) { }

  ngOnInit(): void {
    this.loadCandidates();
    this.loadVoters();
  }

  loadCandidates(): void {
    this.votingService.getCandidates().subscribe({
      next: (data) => {
        this.candidates = data;
      },
      error: (error) => {
        console.error('Error loading candidates:', error);
      }
    });
  }

  loadVoters(): void {
    this.votingService.getVoters().subscribe({
      next: (data) => {
        this.voters = data;
        this.availableVoters = this.voters.filter(v => !v.hasVoted);
      },
      error: (error) => {
        console.error('Error loading voters:', error);
      }
    });
  }

  addVoter(): void {
    if (this.newVoter.name) {
      this.votingService.addVoter(this.newVoter).subscribe({
        next: () => {
          this.loadVoters();
          this.newVoter.name = '';
        },
        error: (error) => {
          console.error('Error adding voter:', error);
        }
      });
    }
  }

  addCandidate(): void {
    if (this.newCandidate.name) {
      this.votingService.addCandidate(this.newCandidate).subscribe({
        next: () => {
          this.loadCandidates();
          this.newCandidate.name = '';
        },
        error: (error) => {
          console.error('Error adding candidate:', error);
        }
      });
    }
  }


  castVote(): void {
    if (this.selectedVoterId && this.selectedCandidateId) {
      this.votingService.vote(this.selectedVoterId, this.selectedCandidateId).subscribe({
        next: (response) => {
          console.log(response.message);
          this.loadCandidates();
          this.loadVoters();
          this.selectedVoterId = null;
          this.selectedCandidateId = null;
        },
        error: (error) => {
          console.error('Error casting vote:', error);
        }
      });
    }
  }

}
