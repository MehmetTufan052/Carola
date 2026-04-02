using Carola.BusinessLayer.Abstract;
using Carola.BusinessLayer.Concrete;
using Carola.DataAccessLayer.Abstract;
using Carola.DataAccessLayer.Concrete;
using Carola.DataAccessLayer.EntityFramework;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CarolaContext>();

builder.Services.AddScoped<IBrandService,BrandManager>();
builder.Services.AddScoped<IBrandDal,EfBrandDal>();

builder.Services.AddScoped<ICarService,CarManager>();
builder.Services.AddScoped<ICarDal,EfCarDal>();

builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<ICategoryDal,EfCategoryDal>();

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
