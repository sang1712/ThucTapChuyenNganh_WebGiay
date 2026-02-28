using Giay_Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Giay_Application.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = GetCartItems();
            return View(cart);
        }

        public IActionResult GetCartCount()
        {
            var cart = GetCartItems();
            int total = cart.Sum(x => x.Quantity);
            return Json(total);
        }

        // Nếu bạn muốn an toàn hơn hãy đổi sang [HttpPost] và submit form trong view.
        public IActionResult DeleteItem(string MaSp, string size, string mau)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(x =>
                string.Equals(x.MaSp, MaSp, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.Size, size, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.Mau, mau, StringComparison.OrdinalIgnoreCase)
            );
            if (item != null)
            {
                cart.Remove(item);
                SaveCartSession(cart);
                TempData["msg"] = "Đã xóa sản phẩm khỏi giỏ hàng.";
            }
            else
            {
                TempData["error"] = "Không tìm thấy sản phẩm trong giỏ hàng.";
            }
            return RedirectToAction("Index");
        }
        // Lấy cart từ session (an toàn)
        private List<HangHoaController.CartItem> GetCartItems()
        {
            var session = HttpContext.Session.GetString("cart");
            if (string.IsNullOrEmpty(session))
                return new List<HangHoaController.CartItem>();
            try
            {
                var cart = JsonConvert.DeserializeObject<List<HangHoaController.CartItem>>(session)
                           ?? new List<HangHoaController.CartItem>();
                return cart;
            }
            catch
            {
                return new List<HangHoaController.CartItem>();
            }
        }

        // Lưu cart vào session
        private void SaveCartSession(List<HangHoaController.CartItem> cart)
        {
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
        }


    }
}
