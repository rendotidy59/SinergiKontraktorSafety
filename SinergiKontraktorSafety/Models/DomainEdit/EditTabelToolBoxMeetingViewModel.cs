namespace SinergiKontraktorSafety.Models.DomainEdit
{
    public class EditTabelToolBoxMeetingViewModel
    {
        public Guid Id { get; set; }
        public string? site { get; set; }
        public DateTime? TanggalTbm { get; set; }
        public string? perusahaan { get; set; }
        public string? judultbm { get; set; }
        public string? namapemateri { get; set; }
        public string? jumlahpeserta { get; set; }
        public string? file { get; set; }
        public string? hasildiskusiTbm { get; set; }
        public string? email { get; set; }
        public string? userid { get; set; }
        public DateTime? created { get; set; }
    }
}
