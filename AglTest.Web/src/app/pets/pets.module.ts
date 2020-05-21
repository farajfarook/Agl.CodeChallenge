import { NgModule } from '@angular/core';

import { PetsComponent } from './components/pets.component';
import { PetListComponent } from './components/pet-list.component';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { MatChipsModule } from '@angular/material/chips';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
    imports: [
        CommonModule,
        HttpClientModule,
        MatChipsModule,
        MatListModule,
        MatProgressSpinnerModule,
        FlexLayoutModule
    ],
    exports: [],
    declarations: [PetsComponent, PetListComponent],
    providers: [],
})
export class PetsModule { }
