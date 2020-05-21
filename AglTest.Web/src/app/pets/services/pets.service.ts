import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { tap, finalize, catchError } from 'rxjs/operators';
import { PetGroup } from '../models';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class PetsService {

    private ApiBase: string = environment.api.host + environment.api.services.pets

    private petsByGender: BehaviorSubject<PetGroup[]> = new BehaviorSubject([])
    public petsByGender$ = this.petsByGender.asObservable()

    private loading: BehaviorSubject<boolean> = new BehaviorSubject(false)
    public loading$ = this.loading.asObservable()

    private error: BehaviorSubject<string> = new BehaviorSubject(null)
    public error$ = this.error.asObservable()

    constructor(private httpClient: HttpClient) { }

    public loadPetsByGender(): Observable<ListByGenderResponse> {
        this.loading.next(true)
        this.error.next(null)
        return this.httpClient.get<ListByGenderResponse>(`${this.ApiBase}/_by_gender`).pipe(
            tap(resp => this.petsByGender.next(resp.records)),
            finalize(() => this.loading.next(false)),
            catchError((err) => {
                this.error.next(err)
                return throwError(err)
            })
        )
    }
}

export interface ListByGenderResponse {
    records: PetGroup[]
}