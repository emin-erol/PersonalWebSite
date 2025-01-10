using Microsoft.EntityFrameworkCore;
using PersonalWebSite.DAL.Core;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Service.Interfaces;
using PersonalWebSite.Service.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PersonalWebSiteDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<PersonalWebSiteDbContext>();

builder.Services.AddScoped<IAboutDal, AboutRepository>();
builder.Services.AddScoped<IBannerDal, BannerRepository>();
builder.Services.AddScoped<IBlogDal, BlogRepository>();
builder.Services.AddScoped<IContactInfoDal, ContactInfoRepository>();
builder.Services.AddScoped<IContactMailDal, ContactMailRepository>();
builder.Services.AddScoped<IFooterDal, FooterRepository>();
builder.Services.AddScoped<IResumeCategoryDal, ResumeCategoryRepository>();
builder.Services.AddScoped<IResumeCategoryItemDal, ResumeCategoryItemRpository>();
builder.Services.AddScoped<IItemTechDal, ItemTechRepository>();
builder.Services.AddScoped<ISkillDal, SkillRepository>();
builder.Services.AddScoped<ISocialMediaDal, SocialMediaRepository>();
builder.Services.AddScoped<ITechIUsedDal, TechIUsedRepository>();
builder.Services.AddScoped<IManagementDal, ManagementRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
