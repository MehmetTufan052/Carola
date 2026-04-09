using Carola.BusinessLayer.Abstract;
using Carola.BusinessLayer.Concrete;
using Carola.BusinessLayer.Mapping;
using Carola.BusinessLayer.ValidationRules;
using Carola.DataAccessLayer.Abstract;
using Carola.DataAccessLayer.Concrete;
using Carola.DataAccessLayer.EntityFramework;
using Carola.EntityLayer.Entities;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CarolaContext>();

builder.Services.AddScoped<IBrandService,BrandManager>();
builder.Services.AddScoped<IBrandDal,EfBrandDal>();

builder.Services.AddScoped<ICarService,CarManager>();
builder.Services.AddScoped<ICarDal,EfCarDal>();

builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<ICategoryDal,EfCategoryDal>();

builder.Services.AddScoped<ILocationService,LocationManager>();
builder.Services.AddScoped<ILocationDal,EfLocationDal>();

builder.Services.AddScoped<ICustomerService,CustomerManager>();
builder.Services.AddScoped<ICustomerDal,EfCustomerDal>();

builder.Services.AddAutoMapper(typeof(GeneralMapping));

builder.Services.AddScoped<IValidator<Brand>, BrandValidator>();



// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
