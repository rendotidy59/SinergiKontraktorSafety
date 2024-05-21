using Microsoft.EntityFrameworkCore;
using SinergiKontraktorSafety.Models.Domain;

namespace SinergiKontraktorSafety.Data
{
    public class SinergiDbContext : DbContext
    {
        public SinergiDbContext(DbContextOptions<SinergiDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<TabelSOT> tabels { get; set; }
        public DbSet<TabelHazardReport> tabelHazardReports { get; set; }
        public DbSet<TabelToolBoxMeeting> tabelToolBoxMeetings { get; set; }

    }
}
