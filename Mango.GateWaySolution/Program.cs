using Mango.GateWaySolution.Extension;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Mango.GateWaySolution
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddOcelot();
            builder.AddAppAuthentication();
            var app = builder.Build();


            app.MapGet("/", () => "Hello World!");
            app.UseOcelot();
            app.Run();
        }
    }
}
