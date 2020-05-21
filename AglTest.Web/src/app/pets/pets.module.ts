import { NgModule } from '@angular/core';

import { PetsComponent } from './components/pets.component';
import { PetListComponent } from './components/pet-list.component';
import { PetDetailComponent } from './components/pet-detail.component';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';

@NgModule({
    imports: [
        CommonModule,
        HttpClientModule,
        MatCardModule,
        MatListModule
    ],
    exports: [],
    declarations: [PetsComponent, PetListComponent, PetDetailComponent],
    providers: [],
})
export class PetsModule { }
