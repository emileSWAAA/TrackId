﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TrackId.Application.Queries;
using TrackId.Business.Dto;
using TrackId.Business.Services;
using TrackId.Common.Enum;
using TrackId.Contracts.Track;

namespace TrackId.Application.Commands.Track.Post
{
    public class PostTrackCommand : IRequest<PostTrackCommandResult>
    {
        public string Title { get; set; }

        public TrackType Type { get; set; }

        public IEnumerable<Guid> Artists { get; set; }

        public Guid? GenreId { get; set; }
    }

    public class PostTrackCommandHandler : IRequestHandler<PostTrackCommand, PostTrackCommandResult>
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;

        public PostTrackCommandHandler(ITrackService trackService, IMapper mapper)
        {
            _trackService = trackService;
            _mapper = mapper;
        }

        public async Task<PostTrackCommandResult> Handle(PostTrackCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TrackDto>(request);
            if (dto is null)
            {
                return new PostTrackCommandResult(RequestErrorType.ValidationError, "Invalid parameters.");
            }

            var result = await _trackService.AddAsync(dto, cancellationToken);
            if (result is null)
            {
                return new PostTrackCommandResult(RequestErrorType.NotCreated, "Unable to create track.");
            }

            return new PostTrackCommandResult(_mapper.Map<PostTrackResponse>(result));
        }
    }

    public class PostTrackCommandResult : BaseQueryResponse<PostTrackResponse>
    {
        public PostTrackCommandResult(PostTrackResponse result) : base(result) { }

        public PostTrackCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage) { }

        public PostTrackCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage) { }
    }
}
