using System;
using System.Collections.Generic;

namespace Giay_Application.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DonHangs = new HashSet<DonHang>();
        }

        public string MaKh { get; set; } = null!;
        public string? HoTen { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }

        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
