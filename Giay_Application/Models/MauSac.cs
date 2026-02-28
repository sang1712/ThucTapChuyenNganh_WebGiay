using System;
using System.Collections.Generic;

namespace Giay_Application.Models
{
    public partial class MauSac
    {
        public MauSac()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string MaMau { get; set; } = null!;
        public string? TenMau { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
