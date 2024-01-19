﻿using System.Net;
using System.Text.Json;
using TechLanches.Application.DTOs;
using TechLanches.Core;

namespace TechLanches.Adapter.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string message = "Erro interno. Contate um administrador do sistema. \n";

            var exceptionType = exception.GetType();

            message += exception.Message;

            var status = exceptionType switch
            {
                Type t when t == typeof(DomainException) => HttpStatusCode.BadRequest,
                Type t when t == typeof(NotImplementedException) => HttpStatusCode.NotImplemented,
                _ => HttpStatusCode.InternalServerError,
            };

            var exceptionResult = JsonSerializer.Serialize(new ErrorResponseDTO { MensagemErro = message, StatusCode = status });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}