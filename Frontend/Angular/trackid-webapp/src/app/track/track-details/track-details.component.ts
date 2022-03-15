import { Component, OnInit } from '@angular/core';
import { Track } from '../models/track.model';

@Component({
  selector: 'app-track-details',
  templateUrl: './track-details.component.html',
  styleUrls: ['./track-details.component.scss'],
})
export class TrackDetailsComponent implements OnInit {
  track!: Track;

  constructor() {}

  ngOnInit(): void {
    this.track = {
      id: '1',
      title: 'abc',
      artists: ['Eelke Kleijn', 'Reinier Zonneveld', 'Marc Arcadicane'],
    };
  }

  getSanitizedArtists(): string {
    let sanitizedString = 'ID';
    if (!this.track.artists) {
      return sanitizedString;
    }

    for (let artist of this.track.artists) {
      if (sanitizedString === 'ID') {
        sanitizedString = artist;
      } else {
        sanitizedString = sanitizedString + ' & ' + artist;
      }
    }

    return sanitizedString;
  }
}
