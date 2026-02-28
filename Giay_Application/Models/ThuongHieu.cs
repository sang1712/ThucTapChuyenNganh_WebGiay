using System;
using System.Collections.Generic;

namespace Giay_Application.Models
{
    public partial class ThuongHieu
    {
        public ThuongHieu()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string MaTh { get; set; } = null!;
        public string? TenTh { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
