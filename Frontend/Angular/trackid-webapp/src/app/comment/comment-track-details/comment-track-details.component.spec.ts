import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommentTrackDetailsComponent } from './comment-track-details.component';

describe('CommentsTrackSingleComponent', () => {
  let component: CommentTrackDetailsComponent;
  let fixture: ComponentFixture<CommentTrackDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CommentTrackDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CommentTrackDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
