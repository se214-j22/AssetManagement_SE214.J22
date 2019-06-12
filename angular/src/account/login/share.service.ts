import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
@Injectable({
    providedIn: 'root'
})
export class SharedService {
    private subject = new Subject<any>();
    sendMessage(change: any) {
        this.subject.next(change);
    }
    clearMessage() {
        this.subject.next();
    }
    getMessage(): Observable<any> {
        return this.subject.asObservable();
    }
}
