using System;
using System.Collections.Generic;

namespace Giay_Application.Models
{
    public partial class ChiTietDonHang
    {
        public string MaCtdh { get; set; } = null!;
        public string? MaDh { get; set; }
        public string? MaSp { get; set; }
        public int? SoLuong { get; set; }
        public decimal? DonGia { get; set; }

        public virtual DonHang? MaDhNavigation { get; set; }
        public virtual SanPham? MaSpNavigation { get; set; }
    }
}
