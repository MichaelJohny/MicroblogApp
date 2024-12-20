﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicrblogApp.Persistence.Configurations;


public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        // Table name (optional)
        builder.ToTable("Posts");

        // Primary Key
        builder.HasKey(p => p.Id);

        // Properties
        builder.Property(p => p.Content)
            .IsRequired() // The content is mandatory
            .HasMaxLength(140); // Maximum of 140 characters
        
        builder.Property(p => p.OriginalImageUrl)
            .HasMaxLength(2048) // Reasonable URL length
            .IsRequired(false); // Image is optional
        
        builder.Property(p => p.Latitude)
            .IsRequired();

        builder.Property(p => p.Longitude)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();
        
        builder.HasMany(e => e.Images)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId);
        builder.HasOne(e => e.User)
            .WithMany(e => e.Posts)
            .HasForeignKey(e => e.UserId);
        
        builder.HasQueryFilter(p => !p.IsDeleted);
        
        // Indexes (optional but recommended for better query performance)
        builder.HasIndex(p => p.CreatedAt);
        
    }
}

