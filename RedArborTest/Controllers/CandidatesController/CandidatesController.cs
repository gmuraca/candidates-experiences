using Microsoft.AspNetCore.Mvc;
using RedArborTest.Commands.CandidatesCommands;
using RedArborTest.Services.CandidatesService;
using RedArborTest.Utils;

namespace RedArborTest.Controllers.CandidatesController
{
    public class CandidatesController : Controller
    {
        private readonly ICandidatesService candidatesService;

        public CandidatesController(ICandidatesService candidatesService)
        {
            this.candidatesService = candidatesService;
        }

        /// <summary>
        /// Main View - Shows all Candidates and their data
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await this.candidatesService.GetCandidates());
        }

        /// <summary>
        /// Load Create View
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            return View();
        }

        /// <summary>
        /// Create new Candidate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCandidateCommand model)
        {
            await this.candidatesService.Create(model);

            TempData["success"] = NotificationMessages.CandidateAddSuccess;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Load Update view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = NotificationMessages.CandidateDataBaseError;
                return RedirectToAction("Index");
            }

            var candidate = await this.candidatesService.GetCandidate(id.Value);

            if (candidate == null)
            {
                TempData["error"] = NotificationMessages.CandidateDataBaseError;
                return RedirectToAction("Index");
            }

            return View(new UpdateCandidateCommand(candidate.IdCandidate, candidate.Name.Value, candidate.Surname.Value, candidate.Birthdate.Value, candidate.Email.Value));
        }

        /// <summary>
        /// Update indicated Candidate
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCandidateCommand candidate)
        {
            await this.candidatesService.Update(candidate);
            TempData["success"] = NotificationMessages.CandidateUpdateSuccess;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete indicated Candidate and its experiences
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            await this.candidatesService.Delete(id.Value);
            TempData["success"] = NotificationMessages.CandidateDeleteSuccess;

            return RedirectToAction("Index");
        }
    }
}
