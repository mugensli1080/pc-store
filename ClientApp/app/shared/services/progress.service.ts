import { BrowserXhr } from '@angular/http';
import { Subject } from 'rxjs/Rx';
import { Injectable } from '@angular/core';

export interface Progress
{
    total: number;
    percentage: number;
}

@Injectable()
export class ProgressService
{

    private uploadProgress: Subject<Progress>;

    startTracking()
    {
        this.uploadProgress = new Subject();
        return this.uploadProgress;
    }

    notify(progress: Progress)
    {
        if (this.uploadProgress)
            this.uploadProgress.next(progress);
    }
    endTracking()
    {
        if (this.uploadProgress)
            this.uploadProgress.complete();
    }
}

@Injectable()
export class BrowserXhrWithProgress extends BrowserXhr
{
    constructor(private _progressService: ProgressService) { super(); }

    build(): XMLHttpRequest
    {
        let xhr: XMLHttpRequest = super.build();
        xhr.upload.onprogress = (event) => { this._progressService.notify(this.createProgress(event)); }
        xhr.upload.onloadend = () => { this._progressService.endTracking(); }

        return xhr;
    }

    createProgress(event: ProgressEvent): Progress
    {
        return { total: event.total, percentage: Math.round(event.loaded / event.total * 100) };
    }
}