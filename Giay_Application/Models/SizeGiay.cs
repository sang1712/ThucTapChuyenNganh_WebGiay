using System;
using System.Collections.Generic;

namespace Giay_Application.Models
{
    public partial class SizeGiay
    {
        public SizeGiay()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string MaSize { get; set; } = null!;
        public string? KichCo { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
