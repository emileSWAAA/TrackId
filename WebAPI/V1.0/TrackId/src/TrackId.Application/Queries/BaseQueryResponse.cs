using System.Collections.Generic;
using TrackId.Contracts;

namespace TrackId.Application.Queries
{
    public abstract class BaseQueryResponse<T> where T : IResponseContract
    {
        protected BaseQueryResponse(T result)
        {
            if (result is null)
            {
                Success = false;
                if (Errors is null)
                {
                    Errors = new List<RequestError>() {
                        new RequestError { ErrorMessage = "Not found", Type = RequestErrorType.NotFound } };
                }
            }
            else
            {
                Result = result;
                Success = true;
            }
        }

        protected BaseQueryResponse(RequestErrorType errorType, string errorMessage) : this(success: false, errorType, errorMessage) { }

        protected BaseQueryResponse(bool success, RequestErrorType errorType, string errorMessage)
        {
            if (!success)
            {
                if (Errors is null)
                {
                    Errors = new List<RequestError>() { new RequestError() { Type = errorType, ErrorMessage = errorMessage } };
                }
            }
        }

        public bool Success { get; private set; }

        public T Result { get; private set; }

        public IEnumerable<RequestError> Errors { get; private set; }

        public void AddError(string message, RequestErrorType type)
        {
            if (Errors is null)
            {
                Errors = new List<RequestError>() { new RequestError() { ErrorMessage = message, Type = type } };
            }
        }
    }
}
