using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProjectService.Domain;

namespace MyProjectService.Infrastructure.Postgres;
public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
{
    builder.ToTable("departments");

    builder.HasKey(d => d.Id);
    builder.Property(d => d.Id).HasColumnName("id");

    builder
    .Property(d => d.ParentId)
    .HasColumnName("parent_id");

    builder.OwnsOne(d => d.Name, nameBuilder =>
    {
        nameBuilder.Property(n => n.Value)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH50)
            .HasColumnName("name"); 
    });

    builder
    .Property(d => d.Slug)
    .IsRequired()
    .HasMaxLength(LengthConstants.LENGTH500)
    .HasColumnName("slug");

    builder
    .Property(d => d.Path)
    .IsRequired()
    .HasMaxLength(LengthConstants.LENGTH500)
    .HasColumnName("path");

    builder
    .Property(d => d.CreatedAt)
    .IsRequired()
    .HasColumnName("created");

     
    builder
    .Property(d => d.UpdatedAt)
    .IsRequired()
    .HasColumnName("updated");
     

}
}

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
{
    builder.ToTable("locations");

    builder.HasKey(l => l.Id);
    builder.Property(l => l.Id).HasColumnName("id");
    
    builder.OwnsOne(l => l.Name, nameBuilder =>
    {
        nameBuilder.Property(n => n.Value)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH50)
            .HasColumnName("name"); 
    });

      builder.OwnsOne(l => l.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.Value)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH500)
                .HasColumnName("address");
        });
    
    builder
    .Property(l => l.CreatedAt)
    .IsRequired()
    .HasColumnName("created");

     
    builder
    .Property( l => l.UpdatedAt)
    .IsRequired()
    .HasColumnName("updated");
     
     
}
}

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
{
    builder.ToTable("positions");

    builder.HasKey(p => p.Id);
    builder.Property(p => p.Id).HasColumnName("id");
    
    builder.OwnsOne(p => p.Name, nameBuilder =>
    {
        nameBuilder.Property(n => n.Value)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH50)
            .HasColumnName("name"); 
    });

    builder
    .Property(p => p.CreatedAt)
    .IsRequired()
    .HasColumnName("created");

     
    builder
    .Property(p => p.UpdatedAt)
    .IsRequired()
    .HasColumnName("updated");
     
     
}
}

public class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
{
    builder.ToTable("department_locations");

    builder.HasKey(dl => dl.Id);
    builder.Property(dl => dl.Id).HasColumnName("id");
    
    
    
     builder.Property(dl => dl.DepartmentId)
            .IsRequired()
            .HasColumnName("department_id");

     
    builder.Property(dl => dl.LocationId)
            .IsRequired()
            .HasColumnName("location_id");
     
      builder.Property(dl => dl.IsPrimary)
            .IsRequired()
            .HasColumnName("is_primary");
     
      builder.HasOne<Department>()
         .WithMany(d => d.DepartmentLocations)
         .HasForeignKey(dl => dl.DepartmentId)
         .OnDelete(DeleteBehavior.Cascade);

     
      builder.HasOne<Location>()                                      
          .WithMany()                                                 
          .HasForeignKey(dl => dl.LocationId)                         
          .OnDelete(DeleteBehavior.Cascade);

      builder.HasIndex(dl => new { dl.DepartmentId, dl.LocationId }).IsUnique();       
}
}

public class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
{
    builder.ToTable("department_positions");

     builder.HasKey(dp => dp.Id);
    builder.Property(dp => dp.Id).HasColumnName("id");
    
    
     builder.Property(dp => dp.DepartmentId)
            .IsRequired()
            .HasColumnName("department_id");

     
    builder.Property(dp => dp.PositionId)
            .IsRequired()
            .HasColumnName("position_id");
     
    builder.HasOne<Department>()
         .WithMany(d => d.DepartmentPositions)
         .HasForeignKey(dp => dp.DepartmentId)
         .OnDelete(DeleteBehavior.Cascade);

     
      builder.HasOne<Position>()                                      
          .WithMany()                                                
          .HasForeignKey(dp => dp.PositionId)                         
          .OnDelete(DeleteBehavior.Cascade);

      builder.HasIndex(dp => new { dp.DepartmentId, dp.PositionId }).IsUnique();

}
}
