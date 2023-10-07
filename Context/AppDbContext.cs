using APICatalogo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context;

public class AppDbContext : IdentityDbContext<UserModel>
{

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  { }

  public DbSet<Categoria> Categorias { get; set; }
  public DbSet<Produto> Produtos { get; set; }

  protected override void OnModelCreating(ModelBuilder model)
  {
    var roles = this.SeedRoles(model);
    var users = this.SeedUser(model);
    this.SeedUserRoles(model, users, roles);

    base.OnModelCreating(model);
  }

  protected List<IdentityRole> SeedRoles(ModelBuilder model)
  {

    List<IdentityRole> roles = new List<IdentityRole>(){
      new IdentityRole(){Name="Super Admin", NormalizedName="SUPER ADMIN"},
      new IdentityRole(){Name="Seller", NormalizedName="SELLER"},
      new IdentityRole(){Name="Client", NormalizedName="CLIENT"}
    };

    model.Entity<IdentityRole>().HasData(roles);

    return roles;
  }

  protected List<UserModel> SeedUser(ModelBuilder model)
  {

    List<UserModel> users = new List<UserModel>(){
      new UserModel(){UserName="Chique Pet", NormalizedUserName="CHIQUE PET", Email="kauedomingues98@gmail.com", NormalizedEmail="KAUEDOMINGUES98@GMAIL.COM",BirthDate = new DateTime(2003, 10, 09), EmailConfirmed=true, PhoneNumber="17996583206", PhoneNumberConfirmed=true}
    };

    model.Entity<UserModel>().HasData(users);
    var passwordHash = new PasswordHasher<UserModel>();
    users[0].PasswordHash = passwordHash.HashPassword(users[0], "Bilegata123");

    return users;
  }

  protected void SeedUserRoles(ModelBuilder model, List<UserModel> users, List<IdentityRole> roles)
  {
    List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

    foreach (var role in roles)
    {
      userRoles.Add(new IdentityUserRole<string>()
      {
        UserId = users[0].Id,
        RoleId = role.Id
      });
    }

    model.Entity<IdentityUserRole<string>>().HasData(userRoles);
  }

}