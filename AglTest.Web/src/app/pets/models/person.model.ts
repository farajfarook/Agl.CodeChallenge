export interface Person {
    gender: PersonGender
    name: string
    age: number
    pets: string[]
}

export enum PersonGender {
    Male, Female, Other
}