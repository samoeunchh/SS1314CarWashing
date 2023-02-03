﻿using Microsoft.EntityFrameworkCore;

namespace SS1314CarWashing.Models;

public class AppDbContext:DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		:base(options)
	{

	}
	public DbSet<Customer> Customer { get; set; }
	public DbSet<Brand> Brand { get; set; }
	public DbSet<Model> Models { get; set; }
	public DbSet<OilType> OilType { get; set; }
	public DbSet<ItemType> ItemType { get; set; }
}
