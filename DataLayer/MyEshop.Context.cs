﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace DataLayer
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class MyEShop_DBEntities : DbContext
{
    public MyEShop_DBEntities()
        : base("name=MyEShop_DBEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Features> Features { get; set; }

    public virtual DbSet<Product_Featuers> Product_Featuers { get; set; }

    public virtual DbSet<Product_Galleries> Product_Galleries { get; set; }

    public virtual DbSet<Product_Groups> Product_Groups { get; set; }

    public virtual DbSet<Product_Select_Groups> Product_Select_Groups { get; set; }

    public virtual DbSet<Product_Tag> Product_Tag { get; set; }

    public virtual DbSet<Products> Products { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<Product_Comment> Product_Comment { get; set; }

}

}
