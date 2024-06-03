using Adanom.Ecommerce.API;
using Adanom.Ecommerce.API.Site;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

// Add services come from other projects
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: SiteConstants.CORSPolicyNames.Default,
                      policy =>
                      {
                          policy.WithOrigins(
                              UIClientConstants.Admin.BaseURL,
                              UIClientConstants.Auth.BaseURL,
                              UIClientConstants.Store.BaseURL);
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                      });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(SiteConstants.CORSPolicyNames.Default);

app.UseAuthentication();
app.UseAuthorization();

// Use applciaition builders come from other projects
app.UseApplication();

app.MapControllers();

app.Run();
