using Microsoft.AspNetCore.Mvc;
using PracticafinalCrudDoctores.Rpositories;
using PracticafinalCrudDoctores.Models;

namespace PracticafinalCrudDoctores.Controllers
{
    public class DoctorController : Controller
    {
        RepositoryDoctor repo;
        public DoctorController()
        {
            this.repo = new RepositoryDoctor();
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
        public async Task< IActionResult> CreateDoctor()
        {
            List<string> listaEspecialidades = await this.repo.GetEspecialidadesDoctorAsync();
            return View(listaEspecialidades);
        }
        [HttpPost]
        public async Task<IActionResult>CreateDoctor(int idHospital, string apellido, string especialidad, int salario)
        {
            await this.repo.InsertDoctorAsync(idHospital,apellido, especialidad, salario);
            return RedirectToAction("Index");
        }

    }
}
