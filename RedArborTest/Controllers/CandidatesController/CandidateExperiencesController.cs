using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RedArbor.RedArborTest.Domain.Entities;
using RedArborTest.Commands.CandidateExperiencesCommands;
using RedArborTest.Services.CandidateExperiencesService;
using RedArborTest.Services.CandidatesService;
using RedArborTest.Utils;

namespace RedArborTest.Controllers.CandidatesController
{
    public class CandidateExperiencesController : Controller
    {
        private readonly ICandidateExperiencesService candidateExperiencesService;
        private readonly ICandidatesService candidatesService;

        public CandidateExperiencesController(ICandidateExperiencesService candidateExperiencesService, ICandidatesService candidatesService)
        {
            this.candidateExperiencesService = candidateExperiencesService;
            this.candidatesService = candidatesService;
        }

        /// <summary>
        /// Main View - Shows all Candidates Experiences and Candidate information
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await this.candidateExperiencesService.GetExperiences());
        }

        /// <summary>
        /// Load Create view
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            await LoadCanDidateList();

            return View();
        }

        /// <summary>
        /// Create new Candidate Experience
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCandidateExperienceCommand candidateExperience)
        {
            await this.candidateExperiencesService.Create(candidateExperience);

            TempData["success"] = NotificationMessages.CandidateExpereincesUpdateSuccess;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Load candidates list
        /// </summary>
        /// <returns></returns>
        private async Task LoadCanDidateList()
        {
            var candidates = await this.candidatesService.GetCandidates();

            //Create a list with NAME and SURNAME together
            var candidatesList = (from c in candidates
                                  select new
                                  {
                                      Id = c.IdCandidate,
                                      FullName = c.Name.Value + " " + c.Surname.Value
                                  }).ToList();

            //Add adn index to show at the begining
            candidatesList.Insert(0, new { Id = 0, FullName = " Seleccionar..." });

            ViewData["Candidates"] = new SelectList(candidatesList, "Id", "FullName");
        }

        private async Task<CandidateExperiences> GetCandidateExperience(int id)
        {
            var candidateExperiences = await this.candidateExperiencesService.GetExperienceById(id);

            if (candidateExperiences == null)
            {
                TempData["error"] = NotificationMessages.CandidateExperiencesDataBaseError;
                return null;
            }

            return candidateExperiences;
        }

        /// <summary>
        /// Update Candidate Experience
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = NotificationMessages.CandidateExperiencesDataBaseError;
                return RedirectToAction("Index");
            }

            var candidateExperiences = await GetCandidateExperience(id.Value);

            if (candidateExperiences == null)
            {
                TempData["error"] = NotificationMessages.CandidateExperiencesDataBaseError;
                RedirectToAction("Index");
            }
                
            return View(new UpdateCandidateExperienceCommand(candidateExperiences.IdCandidateExperience, candidateExperiences.IdCandidate, candidateExperiences.Company.Value, candidateExperiences.Job.Value, 
                candidateExperiences.Description.Value, candidateExperiences.Salary.Value, candidateExperiences.BeginDate.Value, candidateExperiences.EndDate.Value, candidateExperiences.Candidate));
        }

        /// <summary>
        /// Update Candidate Experience 
        /// </summary>
        /// <param name="candidateExperience"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCandidateExperienceCommand candidateExperience)
        {
            await this.candidateExperiencesService.Update(candidateExperience);
            TempData["success"] = NotificationMessages.CandidateExpereincesUpdateSuccess;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete Candidate Experience
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            await this.candidateExperiencesService.Delete(id.Value);

            TempData["success"] = NotificationMessages.CandidateExpereincesDeleteSuccess;
            return RedirectToAction("Index");
        }
    }
}
