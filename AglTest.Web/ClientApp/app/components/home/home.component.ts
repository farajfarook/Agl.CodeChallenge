import { Component, Inject, OnInit } from '@angular/core';
import { PetService } from '../../services/pet.service';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    providers: [PetService]
})
export class HomeComponent implements OnInit{

    private _petService: PetService;
    petCollections: any;

    constructor(petService: PetService) {
        this._petService = petService;
    }

    ngOnInit(): void {
        this._petService.GetPetCollection().subscribe((result) => {
            this.petCollections = result;
        });
    }
}
