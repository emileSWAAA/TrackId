import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class LayoutService {
    private sidebarState$ : BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);

    getSidebarState(): Observable<boolean> {
        return this.sidebarState$.asObservable();
    }

    toggle(): void {
        this.sidebarState$.next(!this.sidebarState$.value);
    }
}
