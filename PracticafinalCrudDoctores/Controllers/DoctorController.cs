using Microsoft.AspNetCore.Mvc;
using PracticafinalCrudDoctores.Rpositories;
using PracticafinalCrudDoctores.Models;

namespace PracticafinalCrudDoctores.Controllers
{
    public class DoctorController : Controller
    {
        RepositoryDoctor repo;
        RepositoryHospital repoHos;
        public DoctorController()
        {
            this.repo = new RepositoryDoctor();
            this.repoHos = new RepositoryHospital();
        }
        public IActionResult Index()
        {
            List<Doctor> listaDoctores = this.repo.GetDoctores();
            return View(listaDoctores);
        }
        public IActionResult DetailsDoctor(int id)
        {
            Doctor doctor = this.repo.FindDoctor(id);
            return View(doctor);
        }
        public IActionResult CreateDoctor()
        {
            ViewData["ESPECIALIDADES"] = this.repo.GetEspecialidadesDoctor();
            List<Hospital> listaHospitales = this.repoHos.GetHospitales();
            return View(listaHospitales);
        }
        [HttpPost]
        public async Task<IActionResult>CreateDoctor(int idHospital, string apellido, string especialidad, int salario)
        {
            ViewData["ESPECIALIDADES"] = this.repo.GetEspecialidadesDoctor();
            await this.repo.InsertDoctorAsync(idHospital,apellido, especialidad, salario);
            return RedirectToAction("Index");
        }

    }
}
