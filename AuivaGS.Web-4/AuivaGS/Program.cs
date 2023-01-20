using AuivaGS.DbModel.Models;
using AuviaGS.Core.Mangers.MagersInterfaces;
using AuviaGS.Core.Mangers;
using AuviaGS.DbModel.Mangers.MangerInterfaces;
using AuviaGS.Notifications.Implementation;
using AuviaGS.Notifications;
using ExceptionsMid.Extenstions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using Twilio.Clients;
using AuviaGS.SMSServies;
using AutoMapper;
using AuivaGS.Core.Mangers.MangerInterfaces;
using AuivaGS.Core.Mangers;
using System.Text.Json.Serialization;
using AuviaGS.EmailService;
using AuviaGS.Common.Helper;
using AuivaGS.AWSServies.Servies;
using AuivaGS.DbModel.MangerSP;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var configer = builder.Configuration;

var emailConfig = configer.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuivaGS", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please insert Bearer JWT token, Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = configer.GetValue<string>("Jwt:Issuer"),
              ValidAudience = configer.GetValue<string>("Jwt:Issuer"),
              ClockSkew = TimeSpan.Zero,
              IssuerSigningKey = new SymmetricSecurityKey(
                                     Encoding.UTF8.GetBytes(configer.GetValue<string>("Jwt:Key"))
                                     )
          };
      });

builder.Services.AddCors(o => o.AddPolicy("yousef", policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
}));


builder.Services.AddDbContext<AuivaGSDbContext>();
builder.Services.AddDbContext<SPContext>();
builder.Services.AddLogging();

builder.Services.AddScoped<IStoredP, StoredPManger>();
builder.Services.AddScoped<IUserManger, UserManger>();
builder.Services.AddScoped<IProjectsManger, ProjectsManger>();
builder.Services.AddScoped<IArtManger, ArtManger>();
builder.Services.AddScoped<ICommonManger, CommonManger>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddHttpClient<ITwilioRestClient, TwilioClient>();
builder.Services.AddSingleton<IStorageServies, StorageServies>();

var app = builder.Build();

AppSettingsHelper.AppSettingsConfigure(app.Services.GetRequiredService<IConfiguration>(), env);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Log.Logger = new LoggerConfiguration()
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Minute)
            .CreateLogger();



app.ConfigureExceptionHandler(Log.Logger, env);

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseCors("yousef");

app.UseAuthorization();

app.MapControllers();

app.Run();
