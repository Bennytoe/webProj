using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webdb.Database;

namespace webdb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<WebDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<WebDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            using (var serviceScope = app.Services.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetRequiredService<WebDbContext>();
                db.Database.Migrate();

                new ApplicationDbContextSeeder().SeedAsync(db, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}