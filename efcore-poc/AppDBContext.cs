using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

public class AppDBContext : DbContext
{
    public DbSet<Request> Requests { get; set; }
    public DbSet<User> Users { get; set; }

    public string DbPath { get; }

    public AppDBContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}")
            .LogTo(Console.WriteLine, LogLevel.Warning)
            .EnableSensitiveDataLogging();
}

public class Request
{
    public int Id { get; set; }
    public string Desc { get; set; }
    public int? CreatedById { get; set; }
    public virtual User CreatedBy { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}