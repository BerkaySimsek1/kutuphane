using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Sadece tek bir hesap olacak: Kullanıcı adı "admin" ve şifre "admin123"
        if (username == "admin" && password == "admin123")
        {
            // Başarılı giriş - kullanıcıyı anasayfaya yönlendir
            return RedirectToAction("Index", "Home");
        }
        else
        {
            // Hatalı giriş
            ViewBag.Error = "Geçersiz kullanıcı adı veya şifre";
            return View();
        }
    }
}