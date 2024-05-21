namespace SinergiKontraktorSafety.Models.Domain
{
    public class TabelSOT
    {
        public Guid Id { get; set; }
        public string? site { get; set; }
        public DateTime? tanggalobservasi { get; set; }
        public string? catatan { get; set; }
        public string? perusahaan { get; set; }
        public string? lokasi { get; set; }
        public string? namapeserta { get; set; }


       
        public string? userid { get; set; }
        public DateTime? created { get; set; }


        public string? identifikasibahaya { get; set; }
        public string? posturtubuhergonomosi { get; set; }

        public string? alatpelindungdiri { get; set; }
        public string? ketaatanterhadapaturan { get; set; }
        public string? kebersihanlingkungankerja { get; set; }
        public string? kelayakanperalatankerja { get; set; }

        public string? pengawasan { get; set; }
        public string? komunikasisafety { get; set; }

        public string? kesimpulan { get; set; }

        public string? pointpembelajaran { get; set; }
        public string? filedokument { get; set; }

        public string? action { get; set; }

        public string? kesimpulanObservasi { get; set; }

    }
}
 