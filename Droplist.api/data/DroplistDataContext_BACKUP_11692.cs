<<<<<<< HEAD
﻿using Droplist.api.Models;
=======
﻿using System.Data.Entity;
using Droplist.api.Models;
>>>>>>> c6ab08ec2bbfcfb8ef35796d91eccea9fdf40a36
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Droplist.api.data
{
<<<<<<< HEAD
    public class DroplistDataContext : IdentityDbContext<User>
    {
        public DroplistDataContext() : base("droplist.api")
        {

        }

        public IDbSet<Building> Buildings { get; set; }
        public IDbSet<Department> Departments { get; set; }
        public IDbSet<Models.Droplist> Droplists { get; set; }
        public IDbSet<DroplistItem> DroplistItem { get; set; }
        public IDbSet<Employee> Employee { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Section> Sections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // building has many drop lists, drivers?, stockers?
            modelBuilder.Entity<Building>()
                .HasMany(building => building.Droplists)
                .WithRequired(droplist => droplist.Building)
                .HasForeignKey(droplist => droplist.BuildingId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Building>()
                .HasMany(Building => Building.Employees)
                .WithRequired(employee => employee.Building)
                .HasForeignKey(employee => employee.BuildingId);

            modelBuilder.Entity<Building>()
                .HasMany(Building => Building.Sections)
                .WithRequired(section => section.Building)
                .HasForeignKey(section => section.BuildingId);

            modelBuilder.Entity<Building>()
                .HasMany(building => building.Products)
                .WithRequired(product => product.Building)
                .HasForeignKey(product => product.BuildingId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(employee => employee.Droplists)
                .WithRequired(droplist => droplist.Employee)
                .HasForeignKey(droplist => droplist.EmployeeId);

            modelBuilder.Entity<Department>()
                .HasMany(department => department.Droplists)
                .WithRequired(droplist => droplist.Department)
                .HasForeignKey(droplist => droplist.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(department => department.Sections)
                .WithRequired(section => section.Department)
                .HasForeignKey(section => section.DepartmentId);

            modelBuilder.Entity<Models.Droplist>()
                .HasMany(droplist => droplist.DroplistItems)
                .WithRequired(droplistItem => droplistItem.Droplist)
                .HasForeignKey(droplistItem => droplistItem.DroplistId);

            modelBuilder.Entity<Product>()
                .HasMany(product => product.DroplistItems)
                .WithRequired(droplistItem => droplistItem.Product)
                .HasForeignKey(droplistItem => droplistItem.ProductId);

            modelBuilder.Entity<User>()
                .HasOptional(user => user.Employee)
                .WithOptionalDependent(employee => employee.User)
                .Map(m => m.MapKey("EmployeeId"));

            base.OnModelCreating(modelBuilder);
        }
    }
=======
	public class DroplistDataContext : IdentityDbContext<User>
	{
		public DroplistDataContext() : base("droplist.api")
		{

		}
		public IDbSet<Building> Buildings { get; set; }
		public IDbSet<Department> Departments { get; set; }
		public IDbSet<Models.Droplist> Droplists { get; set; }
		public IDbSet<DroplistItem> DroplistItems { get; set; }
		public IDbSet<Employee> Employees { get; set; }
		public IDbSet<Product> Products { get; set; }
		public IDbSet<Section> Sections { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{

			// building has many drop lists, drivers?, stockers?

			modelBuilder.Entity<Building>()
				.HasMany(Building => Building.Droplists)
				.WithRequired(droplist => droplist.Building)
				.HasForeignKey(droplist => droplist.BuildingId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Building>()
				.HasMany(Building => Building.Employees)
				.WithRequired(employee => employee.Building)
				.HasForeignKey(employee => employee.BuildingId);

			modelBuilder.Entity<Employee>()
				.HasMany(Employee => Employee.Droplists)
				.WithRequired(droplist => droplist.Employee)
				.HasForeignKey(droplist => droplist.EmployeeId);

			modelBuilder.Entity<Section>()
				.HasMany(Section => Section.Droplists)
				.WithRequired(droplist => droplist.Section)
				.HasForeignKey(droplist => droplist.SectionId);

			modelBuilder.Entity<Department>()
				.HasMany(Department => Department.Sections)
				.WithRequired(section => section.Department)
				.HasForeignKey(section => section.DepartmentId);

			modelBuilder.Entity<Models.Droplist>()
				.HasMany(droplist => droplist.DroplistItems)
				.WithRequired(DroplistItem => DroplistItem.Droplist)
				.HasForeignKey(DroplistItem => DroplistItem.DroplistId);

			modelBuilder.Entity<Product>()
				.HasMany(product => product.DroplistItems)
				.WithRequired(DroplistItem => DroplistItem.Product)
				.HasForeignKey(DroplistItem => DroplistItem.ProductId);

			modelBuilder.Entity<User>()
				.HasOptional(user => user.Employee)
				.WithOptionalDependent(employee => employee.User)
				.Map(m => m.MapKey("EmployeeId"));

			base.OnModelCreating(modelBuilder);
		}
	}
>>>>>>> c6ab08ec2bbfcfb8ef35796d91eccea9fdf40a36
}