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
    .Property(d => d.parentId)
    .IsRequired()
    .HasMaxLength(LengthConstants.LENGTH50)
    .HasColumnName("parent_id");

    builder.OwnsOne(d => d.Name, nameBuilder =>
    {
        nameBuilder.Property(n => n.Value)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH50)
            .HasColumnName("name"); 
    });

    builder
    .Property(d => d.slug)
    .IsRequired()
    .HasMaxLength(LengthConstants.LENGTH500)
    .HasColumnName("slug");

    builder
    .Property(d => d.path)
    .IsRequired()
    .HasMaxLength(LengthConstants.LENGTH500)
    .HasColumnName("path");

    builder
    .Property(d => d.createdAt)
    .IsRequired()
    .HasColumnName("created");

     
    builder
    .Property(d => d.updatedAt)
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

      builder.OwnsOne(l => l.address, addressBuilder =>
        {
            addressBuilder.Property(a => a.Value)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH500)
                .HasColumnName("address");
        });
    
    builder
    .Property(l => l.createdAt)
    .IsRequired()
    .HasColumnName("created");

     
    builder
    .Property( l => l.updatedAt)
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
    .Property(p => p.createdAt)
    .IsRequired()
    .HasColumnName("created");

     
    builder
    .Property(p => p.updatedAt)
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
    
    
    
     builder.Property(dl => dl.departmentId)
            .IsRequired()
            .HasColumnName("department_id");

     
    builder.Property(dl => dl.locationId)
            .IsRequired()
            .HasColumnName("location_id");
     
      builder.Property(dl => dl.isPrimary)
            .IsRequired()
            .HasColumnName("is_primary");
     
      builder.HasOne<Department>()
         .WithMany()
         .HasForeignKey(dl => dl.departmentId)
         .OnDelete(DeleteBehavior.Cascade);

     
      builder.HasOne<Location>()                                      
          .WithMany()                                                 
          .HasForeignKey(dl => dl.locationId)                         
          .OnDelete(DeleteBehavior.Cascade);

      builder.HasIndex(dl => new { dl.departmentId, dl.locationId }).IsUnique();       
}
}

public class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
{
    builder.ToTable("department_positions");

     builder.HasKey(dp => dp.Id);
    builder.Property(dp => dp.Id).HasColumnName("id");
    
    
     builder.Property(dp => dp.departmentId)
            .IsRequired()
            .HasColumnName("department_id");

     
    builder.Property(dp => dp.positionId)
            .IsRequired()
            .HasColumnName("position_id");
     
    builder.HasOne<Department>()
         .WithMany()
         .HasForeignKey(dl => dl.departmentId)
         .OnDelete(DeleteBehavior.Cascade);

     
      builder.HasOne<Position>()                                      
          .WithMany()                                                 
          .HasForeignKey(dl => dl.positionId)                         
          .OnDelete(DeleteBehavior.Cascade);

      builder.HasIndex(dl => new { dl.departmentId, dl.positionId }).IsUnique();

}
}
