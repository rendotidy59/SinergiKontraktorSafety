using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SinergiKontraktorSafety.Data;
using SinergiKontraktorSafety.Models.Domain;
using SinergiKontraktorSafety.Models.DomainAdd;
using SinergiKontraktorSafety.Models.DomainEdit;
using System.Data;
using System.Security.Claims;

namespace SinergiKontraktorSafety.Controllers
{
    [Authorize]
    public class CSMSController : Controller
    {
        private readonly SinergiDbContext sinergiDbContext;

        public CSMSController(SinergiDbContext sinergiDbContext)
        {
            this.sinergiDbContext = sinergiDbContext;
        }
        public IActionResult SOT()
        {

            var TambahData = TempData["TambahData"] as string;
            ViewData["TambahData"] = TambahData;


            ViewBag.menu = "Safety Observation Tour (SOT)";
            var events = sinergiDbContext.tabels.Select(x => new
            {
                title = x.catatan,
                start = x.tanggalobservasi.HasValue ? x.tanggalobservasi.Value.ToString("yyyy-MM-dd") : null,
                allDay = true
            }).ToList();

            var model = new AddTabelSOTViewModel
            {
                data = sinergiDbContext.tabels.ToList()
            };

            ViewBag.EventsJson = JsonConvert.SerializeObject(events);
            return View(model);
        }
         
        public IActionResult TambahSot()
        {
            ViewBag.menu = "Safety Observation Tour (SOT)";
            ViewBag.submenu = "Tambah Data";

            var events = sinergiDbContext.tabels.Select(x => new
            {
                title = x.catatan,
                start = x.tanggalobservasi.HasValue ? x.tanggalobservasi.Value.ToString("yyyy-MM-dd") : null,
                allDay = true 
            }).ToList();

            var model = new AddTabelSOTViewModel
            {
                data = sinergiDbContext.tabels.ToList()
            };

            ViewBag.EventsJson = JsonConvert.SerializeObject(events); 
            return View(model);
        }


        [HttpPost]
        public IActionResult AddSOT(AddTabelSOTViewModel addTabelSOTViewModel)
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            var data = new TabelSOT
            {
                Id = Guid.NewGuid(),
                site = addTabelSOTViewModel.site,
                tanggalobservasi = addTabelSOTViewModel.tanggalobservasi,
                catatan = addTabelSOTViewModel.catatan,
                perusahaan = addTabelSOTViewModel.perusahaan,
                lokasi = addTabelSOTViewModel.lokasi,
                namapeserta = addTabelSOTViewModel.namapeserta,
                userid = userId,
                created = DateTime.Now,
                identifikasibahaya = addTabelSOTViewModel.identifikasibahaya,
                posturtubuhergonomosi = addTabelSOTViewModel.posturtubuhergonomosi,
                alatpelindungdiri = addTabelSOTViewModel.alatpelindungdiri,
                ketaatanterhadapaturan = addTabelSOTViewModel.ketaatanterhadapaturan,
                kebersihanlingkungankerja = addTabelSOTViewModel.kebersihanlingkungankerja,
                kelayakanperalatankerja = addTabelSOTViewModel.kelayakanperalatankerja,
                pengawasan = addTabelSOTViewModel.pengawasan,

                komunikasisafety = addTabelSOTViewModel.komunikasisafety,
                kesimpulan = addTabelSOTViewModel.kesimpulan,
                pointpembelajaran = addTabelSOTViewModel.pointpembelajaran,
                action = addTabelSOTViewModel.action,
                kesimpulanObservasi = addTabelSOTViewModel.kesimpulanObservasi,
                

            };

            sinergiDbContext.tabels.Add(data);
            sinergiDbContext.SaveChanges();

            TempData["TambahData"] = "Berhasil , Tambah data berhasil ";

            return RedirectToAction("SOT", "CSMS");


        }

        public IActionResult ViewSot(Guid id)
        {
            ViewBag.menu = "Safety Observation Tour (SOT)";
            ViewBag.submenu = "View Data";
            var edit = sinergiDbContext.tabels.First(x => x.Id == id);
            if (edit != null)
            {
                var editmodel = new EditTabelSOTViewModel()
                {
                    Id = edit.Id,
                    site = edit.site,
                    tanggalobservasi = edit.tanggalobservasi,
                    catatan = edit.catatan,
                    perusahaan = edit.perusahaan,
                    lokasi = edit.lokasi,
                    namapeserta = edit.namapeserta,

                    identifikasibahaya = edit.identifikasibahaya,
                    posturtubuhergonomosi = edit.posturtubuhergonomosi,
                    alatpelindungdiri = edit.alatpelindungdiri,
                    ketaatanterhadapaturan = edit.ketaatanterhadapaturan,
                    kebersihanlingkungankerja = edit.kebersihanlingkungankerja,
                    kelayakanperalatankerja = edit.kelayakanperalatankerja,
                    pengawasan = edit.pengawasan,

                    komunikasisafety = edit.komunikasisafety,
                    kesimpulan = edit.kesimpulan,
                    pointpembelajaran = edit.pointpembelajaran,
                    action = edit.action,
                    kesimpulanObservasi = edit.kesimpulanObservasi,
                    userid = edit.userid,
                    list = sinergiDbContext.users.Where(x => x.nama == edit.userid).ToList()
                };

                var allRecords = sinergiDbContext.tabels.ToList();

                int countAman = allRecords.Count(x => x.identifikasibahaya == "Aman") +
                                allRecords.Count(x => x.posturtubuhergonomosi == "Aman") +
                                allRecords.Count(x => x.alatpelindungdiri == "Aman") +
                                allRecords.Count(x => x.ketaatanterhadapaturan == "Aman") +
                                allRecords.Count(x => x.kebersihanlingkungankerja == "Aman") +
                                allRecords.Count(x => x.kelayakanperalatankerja == "Aman") +
                                allRecords.Count(x => x.pengawasan == "Aman") +
                                allRecords.Count(x => x.komunikasisafety == "Aman") +
                                allRecords.Count(x => x.kesimpulan == "Aman") +
                                allRecords.Count(x => x.pointpembelajaran == "Aman") +
                                allRecords.Count(x => x.action == "Aman") +
                                allRecords.Count(x => x.kesimpulanObservasi == "Aman");

                int countTidakAman = allRecords.Count(x => x.identifikasibahaya == "Tidak Aman") +
                                     allRecords.Count(x => x.posturtubuhergonomosi == "Tidak Aman") +
                                     allRecords.Count(x => x.alatpelindungdiri == "Tidak Aman") +
                                     allRecords.Count(x => x.ketaatanterhadapaturan == "Tidak Aman") +
                                     allRecords.Count(x => x.kebersihanlingkungankerja == "Tidak Aman") +
                                     allRecords.Count(x => x.kelayakanperalatankerja == "Tidak Aman") +
                                     allRecords.Count(x => x.pengawasan == "Tidak Aman") +
                                     allRecords.Count(x => x.komunikasisafety == "Tidak Aman") +
                                     allRecords.Count(x => x.kesimpulan == "Tidak Aman") +
                                     allRecords.Count(x => x.pointpembelajaran == "Tidak Aman") +
                                     allRecords.Count(x => x.action == "Tidak Aman") +
                                     allRecords.Count(x => x.kesimpulanObservasi == "Tidak Aman");

                int countTidakRelevan = allRecords.Count(x => x.identifikasibahaya == "Tidak Relevan") +
                                        allRecords.Count(x => x.posturtubuhergonomosi == "Tidak Relevan") +
                                        allRecords.Count(x => x.alatpelindungdiri == "Tidak Relevan") +
                                        allRecords.Count(x => x.ketaatanterhadapaturan == "Tidak Relevan") +
                                        allRecords.Count(x => x.kebersihanlingkungankerja == "Tidak Relevan") +
                                        allRecords.Count(x => x.kelayakanperalatankerja == "Tidak Relevan") +
                                        allRecords.Count(x => x.pengawasan == "Tidak Relevan") +
                                        allRecords.Count(x => x.komunikasisafety == "Tidak Relevan") +
                                        allRecords.Count(x => x.kesimpulan == "Tidak Relevan") +
                                        allRecords.Count(x => x.pointpembelajaran == "Tidak Relevan") +
                                        allRecords.Count(x => x.action == "Tidak Relevan") +
                                        allRecords.Count(x => x.kesimpulanObservasi == "Tidak Relevan");

                ViewBag.CountAman = countAman;
                ViewBag.CountTidakAman = countTidakAman;
                ViewBag.CountTidakRelevan = countTidakRelevan;


                return View(editmodel);

            }
            return RedirectToAction("SOT");
        }


        public IActionResult EditDataSOT(Guid id)
        {
            ViewBag.menu = "Safety Observation Tour (SOT)";
            ViewBag.submenu = "Edit Data";
            var edit = sinergiDbContext.tabels.First(x => x.Id == id);
            if (edit != null)
            {
                var editmodel = new EditTabelSOTViewModel()
                {
                    Id = edit.Id,
                    site = edit.site,
                    tanggalobservasi = edit.tanggalobservasi,
                    catatan = edit.catatan,
                    perusahaan = edit.perusahaan,
                    lokasi = edit.lokasi,
                    namapeserta = edit.namapeserta,

                    identifikasibahaya = edit.identifikasibahaya,
                    posturtubuhergonomosi = edit.posturtubuhergonomosi,
                    alatpelindungdiri = edit.alatpelindungdiri,
                    ketaatanterhadapaturan = edit.ketaatanterhadapaturan,
                    kebersihanlingkungankerja = edit.kebersihanlingkungankerja,
                    kelayakanperalatankerja = edit.kelayakanperalatankerja,
                    pengawasan = edit.pengawasan,

                    komunikasisafety = edit.komunikasisafety,
                    kesimpulan = edit.kesimpulan,
                    pointpembelajaran = edit.pointpembelajaran,
                    action = edit.action,
                    kesimpulanObservasi = edit.kesimpulanObservasi,
                    userid = edit.userid,
                    list = sinergiDbContext.users.Where(x => x.nama == edit.userid).ToList()
                };


                return View(editmodel);

            }
            return RedirectToAction("SOT");
        }

        public IActionResult UpdateDataSOT(EditTabelSOTViewModel model)
        {
            var data = sinergiDbContext.tabels.Find(model.Id);
            if (data != null)
            {
                data.site = model.site;
                data.tanggalobservasi = model.tanggalobservasi;
                data.catatan = model.catatan;
                data.perusahaan = model.perusahaan;
                data.lokasi = model.lokasi;
                data.namapeserta = model.namapeserta;
                
                data.identifikasibahaya = model.identifikasibahaya;
                data.posturtubuhergonomosi = model.posturtubuhergonomosi;
                data.alatpelindungdiri = model.alatpelindungdiri;
                data.ketaatanterhadapaturan = model.ketaatanterhadapaturan;
                data.kebersihanlingkungankerja = model.kebersihanlingkungankerja;
                data.kelayakanperalatankerja = model.kelayakanperalatankerja;
                data.pengawasan = model.pengawasan;

                data.komunikasisafety = model.komunikasisafety;
                data.kesimpulan = model.kesimpulan;
                data.pointpembelajaran = model.pointpembelajaran;
                data.action = model.action;
                data.kesimpulanObservasi = model.kesimpulanObservasi;

                sinergiDbContext.SaveChanges();
                return RedirectToAction("SOT");
            };
            return RedirectToAction("SOT");
        }


        public IActionResult EditHasil(string id)
        {

            ViewBag.menu = "Safety Observation Tour (SOT)";
            ViewBag.BreadcrumbMenu = "Hasil Observasi";

            if (Guid.TryParse(id, out Guid guidId))
            {
                var data = sinergiDbContext.tabels.FirstOrDefault(s => s.Id == guidId);

                if (data != null)
                {
                    var editmodel = new EditTabelSOTViewModel
                    { 
                        Id = data.Id,

                    };
                    return View(editmodel);
                }
                else
                {
                    return RedirectToAction("SOT");
                }
            }


            return RedirectToAction("SOT");
        }


        public IActionResult HazardReport()
        {
            ViewBag.menu = "Hazard Report";
            var model = new AddTabelHazardReportViewModel
            {
                data = sinergiDbContext.tabelHazardReports.ToList()
            };

            return View(model);
        }

        public IActionResult TambahHazardReport()
        {
            ViewBag.menu = "Hazard Report";
            ViewBag.submenu = "Tambah Data";
            //var model = new AddTabelHazardReportViewModel
            //{
            //    data = sinergiDbContext.tabelHazardReports.ToList()
            //};
            return View();
        }

        public IActionResult AddDataHazardReport(AddTabelHazardReportViewModel addTabelHazardReportViewModel)
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            var data = new TabelHazardReport
            {
                Id = Guid.NewGuid(),
                site = addTabelHazardReportViewModel.site,
                tanggalLapor = addTabelHazardReportViewModel.tanggalLapor,
                catatan = addTabelHazardReportViewModel.catatan,
                perusahaan = addTabelHazardReportViewModel.perusahaan,
                lokasi = addTabelHazardReportViewModel.lokasi,
                JenisBahaya = addTabelHazardReportViewModel.JenisBahaya,
                Tindakan = userId,
                created = DateTime.Now,
                ActionLanjutan = addTabelHazardReportViewModel.ActionLanjutan,
              


            };

            sinergiDbContext.tabelHazardReports.Add(data);
            sinergiDbContext.SaveChanges();

            TempData["TambahData"] = "Berhasil , Tambah data berhasil ";

            return RedirectToAction("HazardReport", "CSMS");
        }

        public IActionResult ViewDataHazardreport(Guid id)
        {
            ViewBag.menu = "Hazard Report";
            ViewBag.submenu = "View Data";
            var edit = sinergiDbContext.tabelHazardReports.First(x => x.Id == id);
            if (edit != null)
            {
                var editmodel = new EditTabelHazardReportViewModel()
                {
                    Id = edit.Id,
                    site = edit.site,
                    tanggalLapor = edit.tanggalLapor,
                    catatan = edit.catatan,
                    perusahaan = edit.perusahaan,
                    lokasi = edit.lokasi,
                    JenisBahaya = edit.JenisBahaya,

                    Tindakan = edit.Tindakan,
                    ActionLanjutan = edit.ActionLanjutan,
                    
                    userid = edit.userid,
                    list = sinergiDbContext.users.Where(x => x.nama == edit.userid).ToList()
                };

              

                return View(editmodel);

            }
            return RedirectToAction("SOT");
        }


        public IActionResult UpdateDataHazardReport(EditTabelHazardReportViewModel model)
        {
            var data = sinergiDbContext.tabelHazardReports.Find(model.Id);
            if (data != null)
            {
                data.site = model.site;
                data.tanggalLapor = model.tanggalLapor;
                data.catatan = model.catatan;
                data.perusahaan = model.perusahaan;
                data.lokasi = model.lokasi;
                data.JenisBahaya = model.JenisBahaya;

                data.Tindakan = model.Tindakan;
                data.ActionLanjutan = model.ActionLanjutan;

                sinergiDbContext.SaveChanges();
                return RedirectToAction("SOT");
            };
            return RedirectToAction("SOT");
        }


        public IActionResult ToolboxMeeting()
        {
            ViewBag.menu = "Toolbox Meeting";
            return View();
        }


        public IActionResult TambahToolBoxMeeting()
        {
            ViewBag.menu = "Toolbox Meeting";
            ViewBag.submenu = "Tambah Data";
            //var model = new AddTabelHazardReportViewModel
            //{
            //    data = sinergiDbContext.tabelHazardReports.ToList()
            //};
            return View();
        }


        
        public IActionResult LSA()
        {
            ViewBag.menu = "Life Saving Audit";
            return View();
        }

        public IActionResult TambahLSA()
        {
            ViewBag.menu = "Life Saving Audit";
            ViewBag.submenu = "Tambah Data";
            return View();
        }

        
        public IActionResult TO()
        {
            ViewBag.menu = "Task Observation";
            return View();
        }

        public IActionResult eLearning()
        {
            ViewBag.menu = "e-Learning";
            return View();
        }
    }
}
