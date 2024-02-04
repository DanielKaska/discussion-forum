using Forum.DB;
using Forum.Models;
using Forum.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------- JWT ---------------------------------------------  //

//get jwt options from appsettings.json
var jwtOptions = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

//configure authenticaction
builder.Services.AddAuthentication(
    //set auth scheme to bearer
    option =>
    {
        option.DefaultAuthenticateScheme = "Bearer";
        option.DefaultScheme = "Bearer";
        option.DefaultChallengeScheme = "Bearer";
    })
 
    .AddJwtBearer(options =>
    {
        //set bearer options 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
        };
    }
);

//----------------------------------------------------------------------------------------------------//



// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddControllersWithViews().AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddSingleton<PostService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ForumDbContext>();//db context
builder.Services.AddSingleton(jwtOptions); //add jwt options as dependency

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
