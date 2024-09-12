﻿using MedicalSystem.API.Authentication;
using MedicalSystem.API.Entities;
using MedicalSystem.API.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MedicalSystem.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerServices();
			services.AddAuthConfig(configuration);

			var connectionStrings = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection String DefaultConnection not found.");
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionStrings));

			return services;
		}

		private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			return services;
		}

		private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddIdentity<ApplicationUser,ApplicationRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.AddSingleton<IJwtProvider, JwtProvider>();

			services.AddOptions<JwtOptions>().BindConfiguration(JwtOptions.SectionName).ValidateDataAnnotations().ValidateOnStart();

			var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(o =>
				{
					o.SaveToken = true;
					o.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
						ValidIssuer = jwtSettings?.Issuer,
						ValidAudience = jwtSettings?.Audience
					};
				});

			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequiredLength = 6;
				options.SignIn.RequireConfirmedEmail = true;
				options.User.RequireUniqueEmail = true;
			});

			return services;
		}
	}
}
