using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TReponse>(ILogger<LoggingBehavior<TRequest, TReponse>> logger)
    : IPipelineBehavior<TRequest, TReponse>
    where TRequest : notnull, IRequest<TReponse>
    where TReponse : notnull
{
    public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request = {Request} - response = {Response} , - requestData = {RequestData}",
            typeof(TRequest).Name, typeof(TReponse).Name, request);

        Stopwatch timer = new();
        timer.Start();

        TReponse? response = await next();

        timer.Stop();

        TimeSpan timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > 3) 
        {
            logger.LogWarning("[PERFORMANCE] the request {Request} took {TimeTaken}",
                typeof(TRequest).Name, timeTaken);
        }

        logger.LogInformation("[END] Handle {Request} with response {Response}",
            typeof(TRequest).Name, typeof(TReponse).Name);

        return response;
    }
}
