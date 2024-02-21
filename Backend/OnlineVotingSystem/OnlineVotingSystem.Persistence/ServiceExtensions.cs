﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.Helpers.EmailContent;
using OnlineVotingSystem.Persistence.Helpers.GenerateTokens;
using OnlineVotingSystem.Persistence.Helpers.MapperProfile;
using OnlineVotingSystem.Persistence.MainFeatures.AdminFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.AdminFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.SearchFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.SearchFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.Services;
using System.Text;

namespace OnlineVotingSystem.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("default");

        services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICandidateService, CandidateService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ISearchService, SearchService>();

        services.AddScoped<Tokens>();
        services.AddScoped<EmailContentProvider>();

        services.AddAutoMapper(typeof(AutoMapperConfig));

        services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })

            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });
        }
}
