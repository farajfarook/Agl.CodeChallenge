import { Http } from "@angular/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Response } from "@angular/http/src/static_response";
import 'rxjs/add/operator/map';

@Injectable()
export class PetService {
    
    private _http:Http;
    private _apiBase:string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this._http = http;
        this._apiBase = baseUrl + 'api/Pets';
    }

    GetPetCollection(): Observable<Response> {
        return this._http.get(this._apiBase).map((res:Response) => res.json());
    }
}