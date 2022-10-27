using Microsoft.EntityFrameworkCore;
using RedArbor.RedArborTest.Domain.Repositories;
using RedArborDb;
using RedArborDb.Repositories;
using RedArborTest.Queries.CandidateExperiencesQueries;
using RedArborTest.Queries.CandidatesQueries;
using RedArborTest.Services.CandidateExperiencesService;
using RedArborTest.Services.CandidatesService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RedArborDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RedArborConnection")));

builder.Services.AddScoped<ICandidatesRepository, CandidatesRepository>();
builder.Services.AddScoped<ICandidateExperiencesRepository, CandidateExperiencesRepository>();
builder.Services.AddScoped<ICandidatesService, CandidatesService>();
builder.Services.AddScoped<ICandidateExperiencesService, CandidateExperiencesService>();
builder.Services.AddScoped<ICandidatesQueries, CandidatesQueries>();
builder.Services.AddScoped<ICandidateExperiencesQueries, CandidateExperiencesQueries>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RedArborDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
