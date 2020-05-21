import { Component, Input } from '@angular/core';
import { Pet } from '../models';

@Component({
    selector: 'app-pet-detail',
    template: `
        <div>
            {{pet.name}} <span class="mat-small">({{pet.type}})</span>
        </div>
    `
})

export class PetDetailComponent {
    @Input() pet: Pet
    constructor() { }
}