using Microsoft.EntityFrameworkCore;
using ShoppingStore.Model;
using System;
using System.Reflection.Metadata;

namespace ShoppingStore.API.DbContexts
{
	public class ShoppingStoreContext : DbContext
	{
		public DbSet<BrandModel> Brands { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<CategoryModel> Categories { get; set; }
		public DbSet<RatingModel> Ratings { get; set; }
		public DbSet<OrderModel> Orders { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }

		public ShoppingStoreContext(DbContextOptions<ShoppingStoreContext> options) : base(options) { } // to use ConnectionString register in Container(Program.js)

		protected override void OnModelCreating(ModelBuilder modelBuilder) // initial data (seed)
		{
			CategoryModel macbook = new() { Id = Guid.NewGuid(), Name = "Macbook", Slug = "macbook", Description = "Macbook is large Product in the world", Status = 1 };
			CategoryModel pc = new() { Id = Guid.NewGuid(), Name = "Pc", Slug = "pc", Description = "Pc is large Product in the world", Status = 1 };

			BrandModel apple = new() { Id = Guid.NewGuid(),  Name = "Apple", Slug = "apple", Description = "Apple is large brand in the world", Status = 1 };
			BrandModel samsung = new() { Id = Guid.NewGuid(),  Name = "Samsung", Slug = "samsung", Description = "Samsung is large brand in the world", Status = 1 };

			modelBuilder.Entity<ProductModel>()
				.HasData(
				new ProductModel()
				{
					Id = Guid.NewGuid(),
					Name = "Macbook",
					Slug = "macbook",
					Description = "Macbook is the Best",
					Image = "1.jpg",
					CategoryId = macbook.Id,
					BrandId = apple.Id,
					Price = 1233
				},
				new ProductModel()
				{
					Id = Guid.NewGuid(),
					Name = "Pc",
					Slug = "pc",
					Description = "Pc is the Best",
					Image = "1.jpg",
					CategoryId = pc.Id,
					BrandId = samsung.Id,
					Price = 1233
				});

			modelBuilder.Entity<CategoryModel>().HasData(macbook, pc);
			modelBuilder.Entity<BrandModel>().HasData(apple, samsung);

            // Set not delete cascade - reference Migrations\ShoppingStoreMigrations\20240826235204_UpdateProductRatingConstraint.Designer.cs
            modelBuilder
                .Entity<ProductModel>()
				.HasOne(p => p.Brand) // navigation property/collection in product table
                .WithMany() // navigation property/collection in brand table if not use can left empty
                .OnDelete(DeleteBehavior.Restrict); //default DeleteBehavior.Cascade

            modelBuilder // 1 Category with many Product
				.Entity<ProductModel>()
				.HasOne(e => e.Category) //navigation property: Category in ProductModel table
				.WithMany()
				.OnDelete(DeleteBehavior.Restrict);

            modelBuilder 
				.Entity<OrderDetails>()
				.HasOne(o => o.Product)
				.WithMany()
				.OnDelete(DeleteBehavior.Restrict);

    //        modelBuilder // prevent delete cascade rating review when delete product (should delete cascade rating when delete product)
				//.Entity<RatingModel>()
				//.HasOne(o => o.Product)
				//.WithMany(p => p.Ratings)
				//.OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
		}


		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	//optionsBuilder.UseSqlite("connectionstring"); // another way to use ConnectionString register
		//	base.OnConfiguring(optionsBuilder);
		//}
	}
}
