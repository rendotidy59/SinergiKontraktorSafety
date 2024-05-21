namespace SinergiKontraktorSafety.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string? nama { get; set; }
        public string? idcard { get; set; }
        public string? nohp { get; set; }
        public string? divisi { get; set; }
        public string? aplikasi { get; set; }
        public string? level { get; set; }
        public string? plant { get; set; }
        public byte[]? password { get; set; } = new byte[64];
        public byte[]? passwordsalt { get; set; } = new byte[64];
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
