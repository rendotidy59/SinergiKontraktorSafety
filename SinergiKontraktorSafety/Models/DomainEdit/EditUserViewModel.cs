using System.ComponentModel.DataAnnotations;

namespace SinergiKontraktorSafety.Models.DomainEdit
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }
        public string? nama { get; set; }
        public string? idcard { get; set; }
        public string? nohp { get; set; }
        public string? divisi { get; set; }
        public string? aplikasi { get; set; }
        public string? level { get; set; }
        public string? plant { get; set; }

        [Required, MaxLength(8)]
        public string? password { get; set; } = string.Empty;
        public byte[]? passwordsalt { get; set; } = new byte[32];
        public DateTime? log { get; set; }
        public string? email { get; set; }
        public string? VerifikasiToken { get; set; }
        public DateTime? VerifiAdd { get; set; }
        public string? PasswordResertToken { get; set; }
        public DateTime? ResetTokenExpired { get; set; }
        public int? suspend { get; set; }
        public string? status { get; set; }
        public string? tokenlogin { get; set; }
    }
}
