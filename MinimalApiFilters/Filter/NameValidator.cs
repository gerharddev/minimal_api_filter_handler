namespace MinimalApiFilters.Filter
{
    public class NameValidator : IRouteHandlerFilter
    {
        private ILogger _logger;
        public NameValidator(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<NameValidator>();
        }

        public async ValueTask<object?> InvokeAsync(RouteHandlerInvocationContext context, RouteHandlerFilterDelegate next)
        {
            var name = (string)context.Parameters[0];
            if (name == "Bill")
            {
                _logger.LogInformation("Bill is not allowed");
                return Results.Problem("Sorry, Bill is not allowed to my API");
            }
            _logger.LogInformation($"{name} is allowed");
            return await next(context);
        }
    }
}