using System;
using System.Collections.Generic;

namespace Giay_Application.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        public string MaSp { get; set; } = null!;
        public string? TenSp { get; set; }
        public decimal? Gia { get; set; }
        public int? SoLuongTon { get; set; }
        public string? HinhAnh { get; set; }
        public string? MaLoai { get; set; }
        public string? MaNcc { get; set; }
        public string? MaKm { get; set; }
        public string? MaSize { get; set; }
        public string? MaMau { get; set; }
        public string? MaTh { get; set; }
        public string? MoTa { get; set; }




        public virtual KhuyenMai? MaKmNavigation { get; set; }
        public virtual LoaiSanPham? MaLoaiNavigation { get; set; }
        public virtual MauSac? MaMauNavigation { get; set; }
        public virtual NhaCungCap? MaNccNavigation { get; set; }
        public virtual SizeGiay? MaSizeNavigation { get; set; }
        public virtual ThuongHieu? MaThNavigation { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
