using Giay_Application.Models;
using Giay_Application.MyModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;

namespace Giay_Application.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly QuanliBanGiayContext db;

        public IActionResult Index()
        { 
            var data=db.SanPhams.ToList();
            return View(data);
        }
        public IActionResult Detail(string id)
        {
            if (id == null) return NotFound();
            var sp = db.SanPhams
                .Include(s => s.MaSizeNavigation)
                .Include(s => s.MaMauNavigation)
                .Include(s => s.MaThNavigation)
                .FirstOrDefault(s => s.MaSp == id);
            if (sp == null) return NotFound();
            // Load tất cả size & màu trong database
            ViewBag.ListSize = db.SizeGiays.ToList();
            ViewBag.ListColor = db.MauSacs.ToList();
            return View(sp);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult AddToCart(string MaSp, int quantity, string size, string mau, string promoCode)
        {
            var sp = db.SanPhams.FirstOrDefault(x => x.MaSp == MaSp);
            if (sp == null) return NotFound();

            if (string.IsNullOrEmpty(size))
            {
                TempData["error"] = "Bạn phải chọn size!";
                return RedirectToAction("Detail", new { id = MaSp });
            }

            if (string.IsNullOrEmpty(mau))
            {
                TempData["error"] = "Bạn phải chọn màu sắc!";
                return RedirectToAction("Detail", new { id = MaSp });
            }

            List<CartItem> cart = new();
            var session = HttpContext.Session.GetString("cart");
            if (!string.IsNullOrEmpty(session))
                cart = JsonConvert.DeserializeObject<List<CartItem>>(session) ?? new List<CartItem>();

            decimal giaGoc = sp.Gia ?? 0m;
            decimal giaSauGiamMotSp = giaGoc;    // giá 1 sp sau giảm
            decimal thanhTien = 0;               // tổng tiền dòng

            // Áp mã khuyến mãi (nếu có)
            if (!string.IsNullOrEmpty(promoCode))
            {
                var km = db.KhuyenMais.FirstOrDefault(x => x.MaKm == promoCode);
                if (km != null)
                {
                    decimal phanTram = km.PhanTramGiam ?? 0m;
                    if (phanTram < 0) phanTram = 0;
                    if (phanTram > 100) phanTram = 100;

                    giaSauGiamMotSp = Math.Round(giaGoc * (1m - phanTram / 100m), 0);

                    TempData["promo"] = $"Đã áp dụng mã giảm {phanTram}%!";
                }
                else
                {
                    TempData["error"] = "Mã khuyến mãi không hợp lệ!";
                    return RedirectToAction("Detail", new { id = MaSp });
                }
            }

            // ---- TÍNH THÀNH TIỀN THEO SỐ LƯỢNG ----
            thanhTien = giaSauGiamMotSp * quantity;

            // Nếu đã có trong giỏ thì cộng dồn
            var existing = cart.FirstOrDefault(c => c.MaSp == MaSp && c.Size == size && c.Mau == mau);
            if (existing != null)
            {
                existing.Quantity += quantity;
                existing.Gia = giaSauGiamMotSp * existing.Quantity; // cập nhật lại thành tiền
            }
            else
            {
                cart.Add(new CartItem
                {
                    MaSp = MaSp,
                    TenSp = sp.TenSp,
                    HinhAnh = sp.HinhAnh,
                    Gia = thanhTien,        // ⚠️ Thành tiền
                    Size = size,
                    Mau = mau,
                    Quantity = quantity
                });
            }

            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));

            TempData["msg"] = "Đã thêm vào giỏ hàng!";
            return RedirectToAction("Detail", new { id = MaSp });
        }
      


        public class CartItem
        {
            public string MaSp { get; set; }
            public string TenSp { get; set; }
            public string HinhAnh { get; set; }
            public decimal Gia { get; set; }
            public string Size { get; set; }
            public string Mau { get; set; }
            public int Quantity { get; set; }
        }

        //Thêm sản phẩm
        public IActionResult FormThemSanpham()
        {
            ViewBag.LoaiSanPhams = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "TenLoai");
            ViewBag.NhaCungCaps = new SelectList(db.NhaCungCaps.ToList(), "MaNcc", "TenNcc");
            ViewBag.KhuyenMais = new SelectList(db.KhuyenMais.ToList(), "MaKm", "TenKm");
            ViewBag.SizeGiays = new SelectList(db.SizeGiays.ToList(), "MaSize", "KichCo");
            ViewBag.MauSacs = new SelectList(db.MauSacs.ToList(), "MaMau", "TenMau");
            ViewBag.ThuongHieus = new SelectList(db.ThuongHieus.ToList(), "MaTh", "TenTh");
            return View();
        }
        private readonly IWebHostEnvironment _env;

        public HangHoaController(QuanliBanGiayContext context, IWebHostEnvironment env)
        {
            db = context;
            _env = env;

        }

        [HttpPost]
        public IActionResult ThemSanPham(CSanPham x)
        {
            // Kiểm tra ModelState để đảm bảo các trường hợp không hợp lệ sẽ quay lại form và hiển thị lỗi.
            if (!ModelState.IsValid)
            {
                LoadDropdownThem();  // Load lại dropdown nếu có lỗi
                return View("FormThemSanpham", x);
            }

            // Tạo một đối tượng SanPham từ dữ liệu trong CSanPham
            var sp = new SanPham
            {
                MaSp = x.MaSp,
                TenSp = x.TenSp,
                Gia = (decimal?)x.Gia,
                SoLuongTon = x.SoLuongTon,
                MaLoai = x.MaLoai,
                MaNcc = x.MaNcc,
                MaKm = x.MaKm,
                MaSize = x.MaSize,
                MaMau = x.MaMau,
                MaTh = x.MaTh,
                MoTa = x.MoTa
            };

            // Kiểm tra và xử lý tệp hình ảnh nếu có
            if (x.FileHinh != null && x.FileHinh.Length > 0)
            {
                string folder = Path.Combine(_env.WebRootPath, "images");  // Đường dẫn thư mục images
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);  // Tạo thư mục nếu chưa tồn tại

                string fileName = x.MaSp + Path.GetExtension(x.FileHinh.FileName);  // Lấy tên tệp ảnh từ mã sản phẩm và phần mở rộng
                string filePath = Path.Combine(folder, fileName);  // Tạo đường dẫn đầy đủ tới thư mục

                try
                {
                    // Lưu tệp hình ảnh vào thư mục images
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        x.FileHinh.CopyTo(stream);  // Lưu hình ảnh vào server
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Lỗi khi tải ảnh lên: {ex.Message}";
                    return View("FormThemSanpham", x);
                }

                sp.HinhAnh = fileName;  // Gán tên file cho thuộc tính HinhAnh trong đối tượng SanPham
            }
            else
            {
                sp.HinhAnh = "no-image.png";  // Nếu không có ảnh, gán ảnh mặc định
            }

            // Thêm sản phẩm vào cơ sở dữ liệu và lưu thay đổi
            db.SanPhams.Add(sp);
            db.SaveChanges();

            // Quay lại trang danh sách sản phẩm sau khi thêm thành công
            TempData["msg"] = "Sản phẩm đã được thêm thành công!";
            return RedirectToAction("Index");
        }








        private void LoadDropdownThem()
        {
            ViewBag.LoaiSanPhams = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "TenLoai");
            ViewBag.NhaCungCaps = new SelectList(db.NhaCungCaps.ToList(), "MaNcc", "TenNcc");
            ViewBag.KhuyenMais = new SelectList(db.KhuyenMais.ToList(), "MaKm", "TenKm");
            ViewBag.SizeGiays = new SelectList(db.SizeGiays.ToList(), "MaSize", "KichCo");
            ViewBag.MauSacs = new SelectList(db.MauSacs.ToList(), "MaMau", "TenMau");
            ViewBag.ThuongHieus = new SelectList(db.ThuongHieus.ToList(), "MaTh", "TenTh");
        }







        //Sửa sản phẩm


        public IActionResult FormSuaSanPham(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var sp = db.SanPhams.Find(id);
            if (sp == null) return NotFound();

            // Load dropdowns
            ViewBag.LoaiSanPhams = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai", sp.MaLoai);
            ViewBag.NhaCungCaps = new SelectList(db.NhaCungCaps, "MaNcc", "TenNcc", sp.MaNcc);
            ViewBag.KhuyenMais = new SelectList(db.KhuyenMais, "MaKm", "TenKm", sp.MaKm);
            ViewBag.SizeGiays = new SelectList(db.SizeGiays, "MaSize", "KichCo", sp.MaSize);
            ViewBag.MauSacs = new SelectList(db.MauSacs, "MaMau", "TenMau", sp.MaMau);
            ViewBag.ThuongHieus = new SelectList(db.ThuongHieus, "MaTh", "TenTh", sp.MaTh);

            return View(sp);
        }




        [HttpPost]
        public IActionResult SuaSanPham(SanPham model, IFormFile? fileHinh)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns(model);
                return View("FormSuaSanPham", model);
            }

            var sp = db.SanPhams.Find(model.MaSp);
            if (sp == null) return NotFound();

            // Cập nhật thông tin
            sp.TenSp = model.TenSp;
            sp.Gia = model.Gia;
            sp.SoLuongTon = model.SoLuongTon;
            sp.MaLoai = model.MaLoai;
            sp.MaNcc = model.MaNcc;
            sp.MaKm = model.MaKm;
            sp.MaSize = model.MaSize;
            sp.MaMau = model.MaMau;
            sp.MaTh = model.MaTh;
            sp.MoTa = model.MoTa;

            // 1️⃣ Giữ lại ảnh cũ
            string oldImage = sp.HinhAnh;

            // 2️⃣ Nếu có chọn ảnh mới → thay ảnh
            if (fileHinh != null && fileHinh.Length > 0)
            {
                string folder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(fileHinh.FileName);
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    fileHinh.CopyTo(stream);
                }

                sp.HinhAnh = fileName;
            }
            else
            {
                // ✔ Không chọn ảnh mới → giữ lại ảnh cũ
                sp.HinhAnh = oldImage;
            }

            db.SaveChanges();

            return RedirectToAction("FormChitietSanpham", new { id = sp.MaSp });
        }


        private void LoadDropdowns(SanPham sp)
        {
            ViewBag.LoaiSanPhams = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai", sp.MaLoai);
            ViewBag.NhaCungCaps = new SelectList(db.NhaCungCaps, "MaNcc", "TenNcc", sp.MaNcc);
            ViewBag.KhuyenMais = new SelectList(db.KhuyenMais, "MaKm", "TenKm", sp.MaKm);
            ViewBag.SizeGiays = new SelectList(db.SizeGiays, "MaSize", "KichCo", sp.MaSize);
            ViewBag.MauSacs = new SelectList(db.MauSacs, "MaMau", "TenMau", sp.MaMau);
            ViewBag.ThuongHieus = new SelectList(db.ThuongHieus, "MaTh", "TenTh", sp.MaTh);
        }






        //Xóa sản phẩm
        public IActionResult FormXoaSanPham(string id)
        {
            var sp = db.SanPhams.Find(id);
            if (sp == null) return NotFound();
            return View(sp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XoaSanPham(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var sp = db.SanPhams
                       .Include(s => s.ChiTietDonHangs)   // cần đúng tên navigation
                       .FirstOrDefault(s => s.MaSp == id);

            if (sp == null) return NotFound();

            // Xóa chi tiết đơn hàng liên quan
            var chitiet = db.ChiTietDonHangs.Where(c => c.MaSp == id).ToList();
            if (chitiet.Any())
                db.ChiTietDonHangs.RemoveRange(chitiet);

            // Xóa ảnh
            try
            {
                if (!string.IsNullOrEmpty(sp.HinhAnh))
                {
                    string img = Path.Combine(_env.WebRootPath, "images", sp.HinhAnh);
                    if (System.IO.File.Exists(img))
                    {
                        System.IO.File.SetAttributes(img, FileAttributes.Normal);
                        System.IO.File.Delete(img);
                    }
                }
            }
            catch { }

            // Xóa SP
            db.SanPhams.Remove(sp);
            db.SaveChanges();

            return RedirectToAction("Index");
        }





        public IActionResult FormChitietSanpham(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var sp = db.SanPhams
                .Include(s => s.MaSizeNavigation)
                .Include(s => s.MaMauNavigation)
                .Include(s => s.MaThNavigation)
                .FirstOrDefault(s => s.MaSp == id);

            if (sp == null)
                return NotFound();

            return View(sp);
        }

    }
}
