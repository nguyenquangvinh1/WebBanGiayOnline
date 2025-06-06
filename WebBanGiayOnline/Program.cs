﻿	using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
	using Microsoft.AspNetCore.Authentication.Cookies;
	using Microsoft.EntityFrameworkCore;
	using WebBanGiay.Data;
	using WebBanGiay.Models.ViewModel;
	using WebBanGiay.Service;


	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.
	builder.Services.AddControllersWithViews();

	builder.Services.AddSession(options =>
	{
		options.IdleTimeout = TimeSpan.FromMinutes(5000);
	});


//Authen và Autho
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        // Controller khách hàng
//        options.LoginPath = "/TaiKhoan/Login";       
//        options.LogoutPath = "/TaiKhoan/Logout";
//        options.AccessDeniedPath = "/TaiKhoan/AccessDenied";

//        options.Events = new CookieAuthenticationEvents
//        {
//            OnRedirectToLogin = context =>
//            {
//                var path = context.Request.Path;

//                if (path.StartsWithSegments("/admin", StringComparison.OrdinalIgnoreCase))
//                {
//                    // ✅ Đổi theo controller admin mới là AccountController
//                    context.Response.Redirect("/Admin/Account/LoginAdmin");
//                }
//                else
//                {
//                    context.Response.Redirect("/TaiKhoan/Login"); 
//                }

//                return Task.CompletedTask;
//            }
//        };
//    });

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = "CustomerScheme";
	options.DefaultChallengeScheme = "CustomerScheme";
})
.AddCookie("AdminScheme", options =>
{
options.LoginPath = "/Admin/Account/LoginAdmin";
options.AccessDeniedPath = "/Admin/Account/AccessDenied";
options.Cookie.Name = "AdminAuthCookie";

options.Events = new CookieAuthenticationEvents
{
	OnRedirectToLogin = context =>
	{
		context.Response.Redirect(options.LoginPath);
		return Task.CompletedTask;
	}
};
})
.AddCookie("CustomerScheme", options =>
{
options.LoginPath = "/TaiKhoan/Login";
options.AccessDeniedPath = "/TaiKhoan/AccessDenied";
options.Cookie.Name = "CustomerAuthCookie";

options.Events = new CookieAuthenticationEvents
{
	OnRedirectToLogin = context =>
	{
		context.Response.Redirect(options.LoginPath);
		return Task.CompletedTask;
	}
};
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.AuthenticationSchemes.Add("AdminScheme");
        policy.RequireRole("Admin");
    });

    options.AddPolicy("EmployeePolicy", policy =>
    {
        policy.AuthenticationSchemes.Add("AdminScheme");
        policy.RequireRole("Nhân Viên");
    });

    options.AddPolicy("AdminOrEmployeePolicy", policy =>
    {
        policy.AuthenticationSchemes.Add("AdminScheme");
        policy.RequireRole("Admin", "Nhân Viên");
    });

    options.AddPolicy("CustomerPolicy", policy =>
    {
        policy.AuthenticationSchemes.Add("CustomerScheme");
        policy.RequireRole("Khách hàng");
    });
});




//.AddCookie(options =>
//{
//    options.LoginPath = "/TaiKhoan/Login";
//    options.LogoutPath = "/TaiKhoan/Logout";
//    options.Cookie.Path = "/"; // <- Cookie hoạt động trên toàn bộ website

//    options.AccessDeniedPath = "/TaiKhoan/AccessDenied";
//});

//builder.Services.AddAuthorization(options =>
//	{
//		options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
//		options.AddPolicy("EmployeePolicy", policy => policy.RequireRole("Nhân Viên"));
//		options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Khách hàng"));
//		options.AddPolicy("AdminOrEmployeePolicy", policy => policy.RequireRole("Admin", "Nhân Viên"));
//	});


// Đăng ký dbcontext
builder.Services.AddDbContext<AppDbContext>(options => {
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
		options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

	});
	builder.Services.AddDistributedMemoryCache();

	builder.Services.AddSession(options =>
	{
		options.IdleTimeout = TimeSpan.FromMinutes(100);
		options.Cookie.HttpOnly = true;
		options.Cookie.IsEssential = true;
	});

	builder.Services.AddSingleton<IVnPayService, VnPayService>();
	builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
	builder.Services.AddScoped<IMomoService, MomoService>();

	builder.Services.AddScoped<EmailService>();
	builder.Services.AddScoped<IGhnService, GhnService>();

	builder.Services.AddHttpClient();



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
	app.UseSession();

	app.UseAuthentication();
	app.UseAuthorization();


	app.MapControllerRoute(
		 name: "areas",
		  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
			  );

	app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

	app.Run();