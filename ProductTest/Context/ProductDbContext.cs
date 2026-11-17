using Microsoft.EntityFrameworkCore;
using ProductTest.Models;

namespace ProductTest.Context;

public partial class ProductDbContext : DbContext
{
    public ProductDbContext()
    {
    }

    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    public virtual DbSet<Production> Productions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<StockItem> StockItems { get; set; }

    public virtual DbSet<Subcategory> Subcategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ProductDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_100_CI_AI_KS_SC_UTF8");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK_Category$");

            entity.ToTable("Category");

            entity.Property(e => e.IdCategory)
                .ValueGeneratedNever()
                .HasColumnName("ID_Category");
            entity.Property(e => e.Category1)
                .HasMaxLength(255)
                .HasColumnName("Category");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK_Order$");

            entity.ToTable("Order");

            entity.Property(e => e.IdOrder)
                .ValueGeneratedNever()
                .HasColumnName("ID_Order");
            entity.Property(e => e.DataOrder).HasColumnName("Data_Order");
            entity.Property(e => e.IdProduction).HasColumnName("ID_Production");
            entity.Property(e => e.IdSize).HasColumnName("ID_Size");
            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.NumberOrder).HasColumnName("Number_Order");

            entity.HasOne(d => d.IdProductionNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdProduction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order$_Production$");

            entity.HasOne(d => d.IdSizeNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdSize)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order$_Size$");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order$_User$");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK_Product$");

            entity.ToTable("Product");

            entity.Property(e => e.IdProduct)
                .ValueGeneratedNever()
                .HasColumnName("ID_Product");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IdCategory).HasColumnName("ID_Category");
            entity.Property(e => e.IdProduction).HasColumnName("ID_Production");
            entity.Property(e => e.IdSubcategory).HasColumnName("ID_Subcategory");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PathImg)
                .HasMaxLength(255)
                .HasColumnName("Path_IMG");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Structure).HasMaxLength(255);

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK_Product$_Category$");

            entity.HasOne(d => d.IdProductionNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdProduction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product$_Production$");

            entity.HasOne(d => d.IdSubcategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdSubcategory)
                .HasConstraintName("FK_Product$_Subcategory$");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => e.IdProductOrder).HasName("PK_Product_Order$");

            entity.ToTable("Product_Order");

            entity.Property(e => e.IdProductOrder)
                .ValueGeneratedNever()
                .HasColumnName("ID_Product_Order");
            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.IdProduct).HasColumnName("ID_Product");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Order$_Order$");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Order$_Product$");
        });

        modelBuilder.Entity<Production>(entity =>
        {
            entity.HasKey(e => e.IdProduction).HasName("PK_Production$");

            entity.ToTable("Production");

            entity.Property(e => e.IdProduction)
                .ValueGeneratedNever()
                .HasColumnName("ID_Production");
            entity.Property(e => e.Prodaction).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK_Role$");

            entity.ToTable("Role");

            entity.Property(e => e.IdRole)
                .ValueGeneratedNever()
                .HasColumnName("ID_Role");
            entity.Property(e => e.Role1)
                .HasMaxLength(255)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.IdSize).HasName("PK_Size$");

            entity.ToTable("Size");

            entity.Property(e => e.IdSize)
                .ValueGeneratedNever()
                .HasColumnName("ID_Size");
            entity.Property(e => e.Size1)
                .HasColumnType("decimal(2, 0)")
                .HasColumnName("Size");
        });

        modelBuilder.Entity<StockItem>(entity =>
        {
            entity.HasKey(e => e.IdStockItem).HasName("PK_Stock_Item$");

            entity.ToTable("Stock_Item");

            entity.Property(e => e.IdStockItem)
                .ValueGeneratedNever()
                .HasColumnName("ID_Stock_Item");
            entity.Property(e => e.CountForOrder).HasColumnName("Count_for_order");
            entity.Property(e => e.IdProduct).HasColumnName("ID_Product");
            entity.Property(e => e.IdProduction).HasColumnName("ID_Production");
            entity.Property(e => e.IdSize).HasColumnName("ID_Size");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.StockItems)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Item$_Product$");

            entity.HasOne(d => d.IdProductionNavigation).WithMany(p => p.StockItems)
                .HasForeignKey(d => d.IdProduction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Item$_Production$");

            entity.HasOne(d => d.IdSizeNavigation).WithMany(p => p.StockItems)
                .HasForeignKey(d => d.IdSize)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Item$_Size$");
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.HasKey(e => e.IdSubcategory).HasName("PK_Subcategory$");

            entity.ToTable("Subcategory");

            entity.Property(e => e.IdSubcategory)
                .ValueGeneratedNever()
                .HasColumnName("ID_Subcategory");
            entity.Property(e => e.Subcategory1)
                .HasMaxLength(255)
                .HasColumnName("Subcategory");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK_User$");

            entity.ToTable("User");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("ID_User");
            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Surname).HasMaxLength(255);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User$_Role$");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
