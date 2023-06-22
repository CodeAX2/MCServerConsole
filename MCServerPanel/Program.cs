var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options => {
	options.JsonSerializerOptions.IncludeFields = true;
});
builder.Services.AddScoped<
	MCServerPanel.Data.IMCServerProvider,
	MCServerPanel.Data.MineStatServerProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
