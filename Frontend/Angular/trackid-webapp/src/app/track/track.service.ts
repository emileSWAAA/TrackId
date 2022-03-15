import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { PaginatedList } from "../common/paginatedlist";
import { Track } from "./models/track.model";

@Injectable({ providedIn: 'root' })
export class TrackService {
    private baseUrl: string = `${environment.apiUrl}/track`;

    constructor(private httpClient: HttpClient) {}

    getById(id: string) : Observable<Track> {
        return this.httpClient.get<Track>(`${this.baseUrl}/${id}`);
    }

    getPaginated(pageSize: number, pageIndex: number) : Observable<PaginatedList<Track>> {
        return this.httpClient.get<PaginatedList<Track>>(`${this.baseUrl}?pageIndex=${pageIndex}&pageSize=${pageSize}`);
    }
    
}