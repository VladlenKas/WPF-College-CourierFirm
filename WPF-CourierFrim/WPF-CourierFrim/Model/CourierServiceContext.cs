using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace WPF_CourierFrim.Model;

public partial class CourierServiceContext : DbContext
{
    public CourierServiceContext()
    {
    }

    public CourierServiceContext(DbContextOptions<CourierServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<ContentType> ContentTypes { get; set; }

    public virtual DbSet<Delivery> AllDeliveries { get; set; }

    public virtual DbSet<Employee> AllEmployees { get; set; }

    public virtual DbSet<EmployeeDelivery> EmployeeDeliveries { get; set; }

    public virtual DbSet<Order> AllOrders { get; set; }

    public virtual DbSet<Organisation> AllOrganisations { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Rate> AllRates { get; set; }

    public virtual DbSet<StatusDelivery> StatusDeliveries { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    #region Обновленные или существующие записи
    public IQueryable<Employee> Employees => AllEmployees.Where(r => r.IsDeleted != 1)
        .Include(r => r.Post)
        .Include(r => r.Transport);
    public IQueryable<Organisation> Organisations => AllOrganisations.Where(r => r.IsDeleted != 1);
    public IQueryable<Rate> Rates => AllRates.Where(r => r.IsDeleted != 1);
    public IQueryable<Delivery> Deliveries => AllDeliveries
        .Include(r => r.StatusDelivery)
        .Include(r => r.Order)
            .ThenInclude(r => r.Content)
                .ThenInclude(r => r.ContentType)
        .Include(r => r.Order)
            .ThenInclude(r => r.Organisation)
        .Include(r => r.Order)
            .ThenInclude(r => r.Rate)
        .Include(r => r.EmployeeDeliveries)
            .ThenInclude(e => e.Employee)
                .ThenInclude(t => t.Transport);
    public IQueryable<Order> Orders => AllOrders
        .Include(r => r.Rate)
        .Include(r => r.Organisation)
        .Include(r => r.Content)
            .ThenInclude(r => r.ContentType)
        .Include(o => o.Deliveries)
            .ThenInclude(d => d.EmployeeDeliveries);

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=courier_service", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.39-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PRIMARY");

            entity.ToTable("content");

            entity.HasIndex(e => e.ContentTypeId, "content_type_id_idx");

            entity.Property(e => e.ContentId).HasColumnName("content_id");
            entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.Weight)
                .HasPrecision(10, 2)
                .HasColumnName("weight");

            entity.HasOne(d => d.ContentType).WithMany(p => p.Contents)
                .HasForeignKey(d => d.ContentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("content_type_id");
        });

        modelBuilder.Entity<ContentType>(entity =>
        {
            entity.HasKey(e => e.ContentTypeId).HasName("PRIMARY");

            entity.ToTable("content_type");

            entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryId).HasName("PRIMARY");

            entity.ToTable("delivery");

            entity.HasIndex(e => e.OrderId, "order_id_idx");

            entity.HasIndex(e => e.StatusDeliveryId, "status_delivery_id_idx");

            entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");
            entity.Property(e => e.DatetimePresentation)
                .HasColumnType("datetime")
                .HasColumnName("datetime_presentation");
            entity.Property(e => e.DatetimeReceiving)
                .HasColumnType("datetime")
                .HasColumnName("datetime_receiving");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
            entity.Property(e => e.StatusDeliveryId).HasColumnName("status_delivery_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_id");

            entity.HasOne(d => d.StatusDelivery).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.StatusDeliveryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("status_delivery_id");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.HasIndex(e => e.PostId, "post_id_idx");

            entity.HasIndex(e => e.TransportId, "transport_id_idx");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Firstname)
                .HasMaxLength(45)
                .HasColumnName("firstname");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Lastname)
                .HasMaxLength(45)
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasMaxLength(45)
                .HasColumnName("login");
            entity.Property(e => e.Passport)
                .HasMaxLength(10)
                .HasColumnName("passport");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(45)
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .HasColumnName("phone");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.TransportId).HasColumnName("transport_id");

            entity.HasOne(d => d.Post).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_id");

            entity.HasOne(d => d.Transport).WithMany(p => p.Employees)
                .HasForeignKey(d => d.TransportId)
                .HasConstraintName("transport_id");
        });

        modelBuilder.Entity<EmployeeDelivery>(entity =>
        {
            entity.HasKey(e => e.EmployeeDeliveryId).HasName("PRIMARY");

            entity.ToTable("employee_delivery");

            entity.HasIndex(e => e.DeliveryId, "delivery_id_idx");

            entity.HasIndex(e => e.EmployeeId, "employee_id_idx");

            entity.Property(e => e.EmployeeDeliveryId).HasColumnName("employee_delivery_id");
            entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.HasOne(d => d.Delivery).WithMany(p => p.EmployeeDeliveries)
                .HasForeignKey(d => d.DeliveryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("delivery_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeDeliveries)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_id");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.ContentId, "content_id_idx");

            entity.HasIndex(e => e.OrganisationId, "organisation_id_idx");

            entity.HasIndex(e => e.RateId, "rate_id_idx");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ContentId).HasColumnName("content_id");
            entity.Property(e => e.DatetimeCompletion)
                .HasColumnType("datetime")
                .HasColumnName("datetime_completion");
            entity.Property(e => e.DatetimeCreation)
                .HasColumnType("datetime")
                .HasColumnName("datetime_creation");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(120)
                .HasColumnName("delivery_address");
            entity.Property(e => e.FullnameClient)
                .HasMaxLength(120)
                .HasColumnName("fullname_client");
            entity.Property(e => e.OrganisationId).HasColumnName("organisation_id");
            entity.Property(e => e.PhoneClient)
                .HasMaxLength(11)
                .HasColumnName("phone_client");
            entity.Property(e => e.RateId).HasColumnName("rate_id");
            entity.Property(e => e.ReceivingAddress)
                .HasMaxLength(120)
                .HasColumnName("receiving_address");

            entity.HasOne(d => d.Content).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("content_id");

            entity.HasOne(d => d.Organisation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("organisation_id");

            entity.HasOne(d => d.Rate).WithMany(p => p.Orders)
                .HasForeignKey(d => d.RateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rate_id");
        });

        modelBuilder.Entity<Organisation>(entity =>
        {
            entity.HasKey(e => e.OrganisationId).HasName("PRIMARY");

            entity.ToTable("organisation");

            entity.Property(e => e.OrganisationId).HasColumnName("organisation_id");
            entity.Property(e => e.Address)
                .HasMaxLength(120)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(90)
                .HasColumnName("email");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PRIMARY");

            entity.ToTable("post");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasKey(e => e.RateId).HasName("PRIMARY");

            entity.ToTable("rate");

            entity.Property(e => e.RateId)
                .ValueGeneratedNever()
                .HasColumnName("rate_id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Description)
                .HasMaxLength(120)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<StatusDelivery>(entity =>
        {
            entity.HasKey(e => e.StatusDeliveryId).HasName("PRIMARY");

            entity.ToTable("status_delivery");

            entity.Property(e => e.StatusDeliveryId).HasColumnName("status_delivery_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.TransportId).HasName("PRIMARY");

            entity.ToTable("transport");

            entity.Property(e => e.TransportId).HasColumnName("transport_id");
            entity.Property(e => e.Brand)
                .HasMaxLength(45)
                .HasColumnName("brand");
            entity.Property(e => e.Color)
                .HasMaxLength(45)
                .HasColumnName("color");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(6)
                .HasColumnName("license_plate");
            entity.Property(e => e.Model)
                .HasMaxLength(45)
                .HasColumnName("model");
            entity.Property(e => e.Year)
                .HasColumnType("year")
                .HasColumnName("year");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
