import { Component, Input } from '@angular/core';
import { PetsService } from '../services/pets.service';
import { Observable } from 'rxjs';
import { Pet } from '../models';
import { map } from 'rxjs/operators';
import { PersonGender } from '../models/person.model';

@Component({
    selector: 'app-pet-list',
    template: `    
    <div mat-subheader><h1>{{gender}}</h1></div>
    <mat-list-item *ngFor="let pet of pets$ | async">
        <div fxFlex="50%">{{pet.name}}</div>
        <mat-chip-list fxFlex="50%"><mat-chip>{{pet.type}}</mat-chip></mat-chip-list>        
    </mat-list-item>
    <mat-list-item *ngIf="!(pets$ | async).length">
        <span class="mat-caption">
            ... No pets owned by {{gender}} owners
        </span>
    </mat-list-item>
    `
})

export class PetListComponent {
    @Input() gender: PersonGender
    pets$: Observable<Pet[]>
    constructor(private petsService: PetsService) {
        this.pets$ = this.petsService.petsByGender$.pipe(
            map(p => p.find(p => p.gender == this.gender)),
            map(p => p && p.pets)
        )
    }
}