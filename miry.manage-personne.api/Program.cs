using Microsoft.EntityFrameworkCore;
using miry.manage_personne.api.PrepData;
using miry.manage_personne.business.AsyncServices;
using miry.manage_personne.business.DataContext;
using miry.manage_personne.business.IServices;
using miry.manage_personne.business.Repositories;
using miry.manage_personne.business.Services;
using miry.manage_personne.domain.IRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ManagePersonneDbContext>(opt => opt.UseSqlite("Data Source=./personne_api.db"));

builder.Services.AddTransient<IPersonneRepository, PersonneRepository>();
builder.Services.AddScoped<IPersonneService, PersonneService>();
builder.Services.AddSingleton<IPersonneAsyncService, PersonneAsyncService>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //Initialize data base on Development
    InitializeDatabase.PopulateDatabase(app);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

