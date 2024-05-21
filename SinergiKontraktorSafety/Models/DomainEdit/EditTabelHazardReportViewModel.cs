using SinergiKontraktorSafety.Models.Domain;

namespace SinergiKontraktorSafety.Models.DomainEdit
{
    public class EditTabelHazardReportViewModel
    {
        public Guid Id { get; set; }
        public string? site { get; set; }
        public DateTime? tanggalLapor { get; set; }
        public string? catatan { get; set; }
        public string? perusahaan { get; set; }
        public string? lokasi { get; set; }
        public string? JenisBahaya { get; set; }
        public string? Tindakan { get; set; }
        public string? file { get; set; }
        public string? ActionLanjutan { get; set; }
        public string? email { get; set; }
        public string? userid { get; set; }
        public DateTime? created { get; set; }
        public List<User> list { get; internal set; }
    }
}
