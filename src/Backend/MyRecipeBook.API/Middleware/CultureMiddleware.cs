using MyRecipeBook.Domain.Extensions;
using System.Globalization;

namespace MyRecipeBook.API.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;


        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            var supportedLaguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            var requestCulture = context.Request.Headers["Accept-Language"].FirstOrDefault();

            var cultureInfo = new CultureInfo("en");

            if (requestCulture.NotEmpty()
                && supportedLaguages.Exists(c => c.Name.Equals(requestCulture)))
            {
                cultureInfo = new CultureInfo(requestCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
