import { PersonGender } from './person.model';

export interface Pet {
    type: string
    name: string
}

export interface PetGroup {
    gender: PersonGender
    pets: Pet[]
}