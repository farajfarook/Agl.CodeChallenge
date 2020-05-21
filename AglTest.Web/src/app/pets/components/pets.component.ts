import { Component, OnInit, OnDestroy } from '@angular/core';
import { PersonGender } from '../models';
import { PetsService } from '../services/pets.service';
import { Subscription, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
    selector: 'app-pets',
    template: `
    <mat-card *ngFor="let gender of genders$ | async">
        <h1>{{gender}}</h1>
        <app-pet-list [gender]="gender"></app-pet-list>            
    </mat-card>

    `
})

export class PetsComponent implements OnInit, OnDestroy {

    genders$: Observable<PersonGender[]>
    subscription: Subscription

    constructor(private petService: PetsService) {
        this.genders$ = this.petService.petsByGender$.pipe(
            map(res => res.map(r => r.gender))
        )
    }

    ngOnInit() {
        this.subscription = this.petService.loadPetsByGender().subscribe()
    }

    ngOnDestroy() {
        this.subscription.unsubscribe()
    }
}