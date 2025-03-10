using Cliente.Aplication.Generics;
using System.Net;

namespace Cliente.Application.Common.Response;

public class Response
{
    public async static Task<Responses<T>> Ok<T>(HttpStatusCode StatusCode, string message = "", T Tresponse = default, bool success = true)
    {
        var response = new Responses<T>();
        response.Success = success;
        response.Response = Tresponse;
        response.Messages.Add(message);
        response.StatusCode = StatusCode;

        return response;
    }

    public async static Task<Responses<T>> Error<T>(HttpStatusCode StatusCode, string message = null!, T Tresponse = default, bool success = false)
    {
        var response = new Responses<T>();
        response.Success = success;
        response.Response = Tresponse;
        response.Messages.Add(message);
        response.StatusCode = StatusCode;

        return response;
    }

    public async static Task<Responses<T>> ErrorsList<T>(HttpStatusCode StatusCode, List<string> messages = null!, T Tresponse = default, bool success = false)
    {
        var response = new Responses<T>();
        response.Success = success;
        response.Response = Tresponse;
        response.Messages = messages;
        response.StatusCode = StatusCode;

        return response;
    }
}
