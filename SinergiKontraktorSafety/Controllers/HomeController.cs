using MailKit.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SinergiKontraktorSafety.Data;
using SinergiKontraktorSafety.Models;
using SinergiKontraktorSafety.Models.Domain;
using SinergiKontraktorSafety.Models.DomainAdd;
using SinergiKontraktorSafety.Models.DomainEdit;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using MailKit.Net.Smtp;

namespace SinergiKontraktorSafety.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SinergiDbContext sinergiDbContext;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, SinergiDbContext sinergiDbContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this.sinergiDbContext = sinergiDbContext;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var successRegister = TempData["successRegister"] as string;
            ViewData["successRegister"] = successRegister;

            var LoginCekEmail = TempData["LoginCekEmail"] as string;
            ViewData["LoginCekEmail"] = LoginCekEmail;

            var islogincokies = TempData["islogincokies"] as string;
            ViewData["islogincokies"] = islogincokies;

            var EmailBlokir = TempData["EmailBlokir"] as string;
            ViewData["EmailBlokir"] = EmailBlokir;


            var ChangePasswordAlert = TempData["ChangePasswordAlert"] as string;
            ViewData["ChangePasswordAlert"] = ChangePasswordAlert;

            var passcorrect = TempData["passcorrect"] as string;
            ViewData["passcorrect"] = passcorrect;

            var email = TempData["email"] as string;
            ViewData["email"] = email;



            var model = new AddUserViewModel
            {
                usernew = sinergiDbContext.users.ToList()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        [HttpPost]
        public async Task<IActionResult> LoginUser(AddUserViewModel addUserViewModel, EditUserViewModel model)
        {
            using (var transaction = sinergiDbContext.Database.BeginTransaction())
            {
                try
                {
                    var user = sinergiDbContext.users
                        .FirstOrDefault(u => u.email == addUserViewModel.email);

                    if (user == null)
                    {
                        TempData["LoginCekEmail"] = "Email Tidak Terdaftar";
                        return RedirectToAction("Index", "Home");
                    }

                    //var existingUser = HttpContext.User.Identity.Name;
                    //if (!string.IsNullOrEmpty(existingUser))
                    //{
                    //    TempData["islogincokies"] = "Anda sudah masuk dari perangkat lain. Silakan logout dari perangkat tersebut sebelum mencoba masuk lagi.";
                    //    return RedirectToAction("Index", "Home");
                    //}


                    if (!verifikasipasswordhash(addUserViewModel.password, user.password, user.passwordsalt))
                    {

                        user.suspend++;
                        Console.WriteLine($"Failed login attempt for user {user.email}, suspend count = {user.suspend}");

                        if (user.suspend >= 3)
                        {
                            user.status = "Blocked";
                            user.PasswordResertToken = CreatedRandomToken();
                            user.ResetTokenExpired = DateTime.Now.AddDays(1);

                            sinergiDbContext.SaveChanges();
                            transaction.Commit();
                            Console.WriteLine($"Changes committed, suspend count saved as {user.suspend}");
                            SendNotifResetPasswordEmail(user.email, user.PasswordResertToken, user.nama);

                            TempData["EmailBlokir"] = "Akun Anda telah diblokir karena 3x percobaan login gagal. Silahkan Cek Email/Hub Administator";
                            return RedirectToAction("Index", "Home");
                        }
                        else if (user.suspend == 2)
                        {
                            user.status = "Ganti Password";
                            TempData["ChangePasswordAlert"] = "Peringatan: Anda telah Telah Request Ganti Password. Harap Cek Email.";
                        }


                        sinergiDbContext.SaveChanges();
                        transaction.Commit();
                        TempData["passcorrect"] = "Password salah silahkan coba lagi";

                        return RedirectToAction("Index", "Home");
                    }


                    user.suspend = 0;
                    user.status = "Aktif";


                    //var sessionToken = GenerateSessionToken();
                    //user.tokenlogin = sessionToken;

                    sinergiDbContext.SaveChanges();
                    transaction.Commit();
                    if (user.VerifiAdd == null)
                    {
                        TempData["email"] = "Silahkan Cek Email Untuk Verifikasi";
                        return RedirectToAction("Index", "Home");
                    }

                    await Authenticate(user);

                    SendNotifLoginEmail(user.email, user.nama);
                    TempData["successLogin"] = "Selamat Datang di App Sinergi";
                    return RedirectToAction("Index", "Dashboard");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                    TempData["error"] = "Terjadi kesalahan dalam proses login";
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        private bool verifikasipasswordhash(string password, byte[] passwordhash, byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512(passwordsalt))
            {

                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordhash);
            }
        }


        private string CreatedRandomToken()
        {
            byte[] randomBytes = RandomNumberGenerator.GetBytes(64);
            return BitConverter.ToString(randomBytes).Replace("-", "").Substring(0, 64);
        }

        private void SendNotifResetPasswordEmail(string email, string PasswordResertToken, string nama)
        {

            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var username = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];


            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sinergi", _configuration["EmailSettings:Username"]));
            message.To.Add(new MailboxAddress("Notifikasi Reset Password", email));

            message.Subject = "Reset Password";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>hallo {nama}, Silahkan Reset Password atau hub Administrator atau IT. Klik <a href='https://sinergikontraktorsafety.com/Home/ResetPassword?token={PasswordResertToken}&nama={nama}'>di sini</a> Untuk Reset password Anda.</p>";

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(smtpServer, smtpPort, SecureSocketOptions.Auto);
                client.Authenticate(username, password);
                Console.WriteLine("Sending email...");
                client.Send(message);
                client.Disconnect(true);
            }



        }


        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, user.nama),
                    new Claim(ClaimTypes.NameIdentifier, user.idcard),
                    new Claim(ClaimTypes.Email, user.email), // Tambahkan klaim untuk email
                    new Claim("Level", user.level),
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }




        private void SendNotifLoginEmail(string email, string nama)
        {

            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var username = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];


            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sinergi", _configuration["EmailSettings:Username"]));
            message.To.Add(new MailboxAddress("Notifikasi Login Email", email));

            message.Subject = "Anda Telah Login";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>hallo {nama} Anda telah Login aplikasi Sinergi Kontraktor Safety,jika bukan anda di sini silahkan untuk hub Administator atau IT.</p>";

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(smtpServer, smtpPort, SecureSocketOptions.Auto);
                client.Authenticate(username, password);
                Console.WriteLine("Sending email...");
                client.Send(message);
                client.Disconnect(true);
            }



        }


        [HttpPost]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Logout(string email)
        {

            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = sinergiDbContext.users.FirstOrDefault(u => u.email == email);

            if (user != null)
            {
                user.status = "Off";
                user.tokenlogin = null;
                sinergiDbContext.SaveChanges();
            }


            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }










    }
}
