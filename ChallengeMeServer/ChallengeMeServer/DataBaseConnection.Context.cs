﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChallengeMeServer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ChallengeMeEntities : DbContext
    {
        public ChallengeMeEntities()
            : base("name=ChallengeMeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<challenge> challenges { get; set; }
        public virtual DbSet<challenges_and_users> challenges_and_users { get; set; }
        public virtual DbSet<notification> notifications { get; set; }
        public virtual DbSet<post_comments> post_comments { get; set; }
        public virtual DbSet<post> posts { get; set; }
        public virtual DbSet<profile_info> profile_info { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}