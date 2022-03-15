import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { AppRoutingModule } from "../app-routing.module";
import { TrackOverviewComponent } from "./track-overview/track-overview.component";
import { TrackDetailsComponent } from './track-details/track-details.component';
import { TrackUploadComponent } from "./track-upload/track-upload.component";
import { CommentTrackDetailsComponent } from "../comment/comment-track-details/comment-track-details.component";
import { CommentCreateComponent } from "../comment/comment-create/comment-create.component";

@NgModule({
    declarations: [
        TrackOverviewComponent,
        TrackDetailsComponent,
        TrackUploadComponent,
        CommentTrackDetailsComponent,
        CommentCreateComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgbModule,
        ReactiveFormsModule,
        HttpClientModule
    ]
})

export class TrackModule {}