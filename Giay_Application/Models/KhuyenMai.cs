using System;
using System.Collections.Generic;

namespace Giay_Application.Models
{
    public partial class KhuyenMai
    {
        public KhuyenMai()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string MaKm { get; set; } = null!;
        public string? TenKm { get; set; }
        public decimal? PhanTramGiam { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
