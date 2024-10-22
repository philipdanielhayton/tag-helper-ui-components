using TailwindMerge.Extensions;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

builder.Services.AddControllersWithViews();
builder.Services.AddTailwindMerge();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "root",
    pattern: "/",
    defaults: new { controller = "Home", action = "Index" });

if (env.IsDevelopment())
{
    app.MapWhen(
        context => context.Request.Path.StartsWithSegments("/@vite") ||
                   context.Request.Path.StartsWithSegments("/assets") ||
                   context.Request.Path.StartsWithSegments("/node_modules"),
        spaApp =>
        {
            spaApp.UseSpa(spa =>
            {
                spa.UseProxyToSpaDevelopmentServer("https://localhost:5173");
            });
        }
    );
}



app.Run();