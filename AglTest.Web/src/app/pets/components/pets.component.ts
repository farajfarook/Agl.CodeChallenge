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
    `,
    styles: [`
    mat-card {
        margin: 10px;
    }
    `]
})

export class PetsComponent implements OnInit, OnDestroy {

    loading$: Observable<boolean>
    genders$: Observable<PersonGender[]>
    subscription: Subscription

    constructor(private petService: PetsService) {
        this.genders$ = this.petService.petsByGender$.pipe(
            map(res => res.map(r => r.gender))
        )
        this.loading$ = this.petService.loading$
    }

    ngOnInit() {
        this.subscription = this.petService.loadPetsByGender().subscribe()
    }

    ngOnDestroy() {
        this.subscription.unsubscribe()
    }
}