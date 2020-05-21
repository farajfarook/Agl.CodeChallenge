import { Component, Input } from '@angular/core';
import { PetsService } from '../services/pets.service';
import { Observable } from 'rxjs';
import { Pet } from '../models';
import { map } from 'rxjs/operators';
import { PersonGender } from '../models/person.model';

@Component({
    selector: 'app-pet-list',
    template: `
    <mat-list>
        <mat-list-item *ngFor="let pet of pets$ | async">
            <app-pet-detail [pet]="pet"></app-pet-detail>
        </mat-list-item>
    </mat-list>
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