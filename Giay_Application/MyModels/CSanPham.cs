using Giay_Application.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Giay_Application.MyModels
{
    public class CSanPham
    {
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Bạn chưa nhập Mã sản phẩm")]
        public string MaSp { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Bạn chưa nhập Tên sản phẩm")]
        public string TenSp { get; set; }

        [Display(Name = "Giá sản phẩm")]
        [Required(ErrorMessage = "Bạn chưa nhập Giá sản phẩm")]
        [Range(1, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public double? Gia { get; set; }

        [Display(Name = "Số lượng tồn")]
        [Required(ErrorMessage = "Bạn chưa nhập Số lượng tồn")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn không hợp lệ")]
        public int? SoLuongTon { get; set; }

        [Display(Name = "Hình ảnh sản phẩm")]
        public string HinhAnh { get; set; }

        // renamed & Pascal-cased for clarity; use this in the view and controller
        [Display(Name = "Tệp hình")]
        public IFormFile FileHinh { get; set; }

        [Display(Name = "Mã loại")]
        public string MaLoai { get; set; }

        [Display(Name = "Mã nhà cung cấp")]
        public string MaNcc { get; set; }

        [Display(Name = "Mã khuyến mãi")]
        public string MaKm { get; set; }

        [Display(Name = "Mã size")]
        public string MaSize { get; set; }

        [Display(Name = "Mã màu")]
        public string MaMau { get; set; }

        [Display(Name = "Mã thương hiệu")]
        public string MaTh { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        public string MoTa { get; set; }

        // ============= Navigation =============
        public virtual LoaiSanPham MaLoaiNavigation { get; set; }
        public virtual NhaCungCap MaNccNavigation { get; set; }
        public virtual KhuyenMai MaKmNavigation { get; set; }
        public virtual SizeGiay MaSizeNavigation { get; set; }
        public virtual MauSac MaMauNavigation { get; set; }
        public virtual ThuongHieu MaThNavigation { get; set; }

        //public static CSanPham chuyendoi(SanPham x)
        //{
        //    return new CSanPham
        //    {
        //        MaSp = x.MaSp,
        //        TenSp = x.TenSp,
        //        Gia = (double?)x.Gia,
        //        SoLuongTon = x.SoLuongTon,
        //        HinhAnh = x.HinhAnh,
        //        MaLoai = x.MaLoai,
        //        MaNcc = x.MaNcc,
        //        MaKm = x.MaKm,
        //        MaSize = x.MaSize,
        //        MaMau = x.MaMau,
        //        MaTh = x.MaTh,
        //        MoTa = x.MoTa,

        //        MaLoaiNavigation = x.MaLoaiNavigation,
        //        MaNccNavigation = x.MaNccNavigation,
        //        MaKmNavigation = x.MaKmNavigation,
        //        MaSizeNavigation = x.MaSizeNavigation,
        //        MaMauNavigation = x.MaMauNavigation,
        //        MaThNavigation = x.MaThNavigation,
        //    };
        //}

        //public static SanPham chuyendoi(CSanPham x)
        //{
        //    return new SanPham
        //    {
        //        MaSp = x.MaSp,
        //        TenSp = x.TenSp,
        //        Gia = (decimal?)x.Gia,
        //        SoLuongTon = x.SoLuongTon,
        //        HinhAnh = x.HinhAnh,
        //        MaLoai = x.MaLoai,
        //        MaNcc = x.MaNcc,
        //        MaKm = x.MaKm,
        //        MaSize = x.MaSize,
        //        MaMau = x.MaMau,
        //        MaTh = x.MaTh,
        //        MoTa = x.MoTa ?? x.MoTa, // preserve mapping (if model names differ)
        //    };
        //}
    }
}