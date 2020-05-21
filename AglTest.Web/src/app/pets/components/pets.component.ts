import { Component, OnInit, OnDestroy } from '@angular/core';
import { PersonGender } from '../models';
import { PetsService } from '../services/pets.service';
import { Subscription, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
    selector: 'app-pets',
    template: `    
    <mat-spinner *ngIf="loading$ | async"></mat-spinner>
    <mat-list *ngFor="let gender of genders$ | async">        
        <app-pet-list [gender]="gender"></app-pet-list>            
    </mat-list>
    <div class="error mat-body-strong" *ngIf="error$ | async">{{error$ | async}}</div>
    `,
    styles: [`
    mat-card {
        margin: 10px;
    }
    mat-spinner {
        margin:10px auto;
    }
    .error {
        margin: 16px;
        color: darkred;
    }
    `]
})

export class PetsComponent implements OnInit, OnDestroy {

    loading$: Observable<boolean>
    error$: Observable<string>
    genders$: Observable<PersonGender[]>
    subscription: Subscription

    constructor(private petService: PetsService) {
        this.genders$ = this.petService.petsByGender$.pipe(
            map(res => res.map(r => r.gender))
        )
        this.loading$ = this.petService.loading$
        this.error$ = this.petService.error$.pipe(
            map(err => err && err.message)
        )
    }

    ngOnInit() {
        this.subscription = this.petService.loadPetsByGender().subscribe()
    }

    ngOnDestroy() {
        this.subscription.unsubscribe()
    }
}