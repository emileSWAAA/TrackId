import { Component, OnInit } from '@angular/core';
import { PaginatedList } from 'src/app/common/paginatedlist';
import { Track } from '../models/track.model';

@Component({
  selector: 'app-track-overview',
  templateUrl: './track-overview.component.html',
  styleUrls: ['./track-overview.component.scss']
})
export class TrackOverviewComponent implements OnInit {

  tracks: PaginatedList<Track> | undefined;

  constructor() { }

  ngOnInit(): void {
    this.tracks = {
      pageIndex: 0,
      totalPages: 1,
      hasNextPage: false,
      hasPreviousPage: false,
      totalCount: 4,
      items: [{ title: 'abc', artists: ['Eelke Kleijn'], id: '1'}]
    }
  }

}
