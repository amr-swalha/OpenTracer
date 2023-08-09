namespace WebAPI.Middlewares
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; img-src *; media-src *; script-src *");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
            context.Response.Headers.Add("Server", "unknown");
            context.Response.Headers.Add("Permissions-Policy", "microphone=(), geolocation=()");

            await _next(context);
        }
    }
}
