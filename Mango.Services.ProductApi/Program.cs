
using AutoMapper;
using Mango.Services.ProductApi.Data;
using Mango.Services.ProductApi.Extension;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddDbContext<AppDbContext>(option =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("productconnection"));
			});
			IMapper mapper = MappingConfig.RegisterMap().CreateMapper();
			builder.Services.AddSingleton(mapper);
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			builder.AddTokenConfiguration();

			builder.Services.AddAuthentication();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();
			ApplyMigration();

			app.Run();

			void ApplyMigration()
			{
				using (var scope = app.Services.CreateScope())
				{
					var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
					if (_db.Database.GetPendingMigrations().Count() > 0)
					{
						_db.Database.Migrate();
					}
				}
			}
		}
	}
}