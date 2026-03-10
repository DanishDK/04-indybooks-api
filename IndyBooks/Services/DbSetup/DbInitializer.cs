using IndyBooks.Models;
namespace IndyBooks.Services;

    internal static class DbInitializerExtension
    {
        public static IApplicationBuilder InitializeDb(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<IndyBooksDataContext>();
                SeedData.Initialize(context);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Failed to initialize database", e);
            }

            return app;
        }
    }

