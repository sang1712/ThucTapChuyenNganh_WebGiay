using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Giay_Application.Models
{
    public partial class QuanliBanGiayContext : DbContext
    {
        public QuanliBanGiayContext()
        {
        }

        public QuanliBanGiayContext(DbContextOptions<QuanliBanGiayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; } = null!;
        public virtual DbSet<DonHang> DonHangs { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; } = null!;
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; } = null!;
        public virtual DbSet<MauSac> MauSacs { get; set; } = null!;
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; } = null!;
        public virtual DbSet<SanPham> SanPhams { get; set; } = null!;
        public virtual DbSet<SizeGiay> SizeGiays { get; set; } = null!;
        public virtual DbSet<ThuongHieu> ThuongHieus { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-9QKJPQ36\\SQLEXPRESS;Initial Catalog=QuanLiBanGiay;Integrated Security=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.HasKey(e => e.MaCtdh)
                    .HasName("PK__ChiTietD__1E4E40F036207891");

                entity.ToTable("ChiTietDonHang");

                entity.Property(e => e.MaCtdh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaCTDH");

                entity.Property(e => e.DonGia).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MaDh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaDH");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaSP");

                entity.HasOne(d => d.MaDhNavigation)
                    .WithMany(p => p.ChiTietDonHangs)
                    .HasForeignKey(d => d.MaDh)
                    .HasConstraintName("FK__ChiTietDon__MaDH__619B8048");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietDonHangs)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK__ChiTietDon__MaSP__628FA481");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasKey(e => e.MaDh)
                    .HasName("PK__DonHang__2725866107996974");

                entity.ToTable("DonHang");

                entity.Property(e => e.MaDh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaDH");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaKH");

                entity.Property(e => e.NgayDat).HasColumnType("date");

                entity.Property(e => e.TongTien).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK__DonHang__MaKH__5EBF139D");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh)
                    .HasName("PK__KhachHan__2725CF1E29131D37");

                entity.ToTable("KhachHang");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaKH");

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen).HasMaxLength(100);

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KhuyenMai>(entity =>
            {
                entity.HasKey(e => e.MaKm)
                    .HasName("PK__KhuyenMa__2725CF151D21389D");

                entity.ToTable("KhuyenMai");

                entity.Property(e => e.MaKm)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaKM");

                entity.Property(e => e.NgayBatDau).HasColumnType("date");

                entity.Property(e => e.NgayKetThuc).HasColumnType("date");

                entity.Property(e => e.PhanTramGiam).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.TenKm)
                    .HasMaxLength(100)
                    .HasColumnName("TenKM");
            });

            modelBuilder.Entity<LoaiSanPham>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiSanP__730A57592993B0C5");

                entity.ToTable("LoaiSanPham");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenLoai).HasMaxLength(100);
            });

            modelBuilder.Entity<MauSac>(entity =>
            {
                entity.HasKey(e => e.MaMau)
                    .HasName("PK__MauSac__3A5BBB7D4B0DB356");

                entity.ToTable("MauSac");

                entity.Property(e => e.MaMau)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenMau).HasMaxLength(50);
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNcc)
                    .HasName("PK__NhaCungC__3A185DEB0F494438");

                entity.ToTable("NhaCungCap");

                entity.Property(e => e.MaNcc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaNCC");

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TenNcc)
                    .HasMaxLength(100)
                    .HasColumnName("TenNCC");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK__SanPham__2725081C5D8203FD");

                entity.ToTable("SanPham");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaSP");

                entity.Property(e => e.Gia).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MaKm)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaKM");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaMau)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaNcc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaNCC");

                entity.Property(e => e.MaSize)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaTh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaTH");

                entity.Property(e => e.MoTa).HasMaxLength(300);

                entity.Property(e => e.TenSp)
                    .HasMaxLength(150)
                    .HasColumnName("TenSP");

                entity.HasOne(d => d.MaKmNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaKm)
                    .HasConstraintName("FK__SanPham__MaKM__59063A47");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK__SanPham__MaLoai__571DF1D5");

                entity.HasOne(d => d.MaMauNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaMau)
                    .HasConstraintName("FK__SanPham__MaMau__5AEE82B9");

                entity.HasOne(d => d.MaNccNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaNcc)
                    .HasConstraintName("FK__SanPham__MaNCC__5812160E");

                entity.HasOne(d => d.MaSizeNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaSize)
                    .HasConstraintName("FK__SanPham__MaSize__59FA5E80");

                entity.HasOne(d => d.MaThNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaTh)
                    .HasConstraintName("FK__SanPham__MaTH__5BE2A6F2");
            });

            modelBuilder.Entity<SizeGiay>(entity =>
            {
                entity.HasKey(e => e.MaSize)
                    .HasName("PK__SizeGiay__A787E7ED6F087056");

                entity.ToTable("SizeGiay");

                entity.Property(e => e.MaSize)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.KichCo)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ThuongHieu>(entity =>
            {
                entity.HasKey(e => e.MaTh)
                    .HasName("PK__ThuongHi__2725007585627B60");

                entity.ToTable("ThuongHieu");

                entity.Property(e => e.MaTh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaTH");

                entity.Property(e => e.TenTh)
                    .HasMaxLength(100)
                    .HasColumnName("TenTH");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
