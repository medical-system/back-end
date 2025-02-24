
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.Extensions.FileProviders;
using Serilog;

namespace MedicalSystem.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Host.UseSerilog((context, configuration) =>
				configuration.ReadFrom.Configuration(context.Configuration)
			);

			builder.Services.AddDependencies(builder.Configuration);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			//app.UseStaticFiles();
			app.UseSerilogRequestLogging();
			app.UseHangfireDashboard("/jobs");

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCors();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
