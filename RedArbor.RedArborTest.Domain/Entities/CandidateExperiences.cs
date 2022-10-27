using RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences;
using RedArbor.RedArborTest.Domain.ValueObjects.Candidates;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedArbor.RedArborTest.Domain.Entities
{
    public class CandidateExperiences
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCandidateExperience { get; private set; }

        [Required(ErrorMessage = "La relacion con el candidato es requerida")]
        public int IdCandidate { get; private set; }

        [Required(ErrorMessage = "La compania es requerida")]
        [Display(Name = "Compania")]
        public CandidateExperiencesCompany Company { get; private set; }

        [Required(ErrorMessage = "El empleo es requerido")]
        [Display(Name = "Empleo")]
        public CandidateExperiencesJob Job { get; private set; }

        [Required(ErrorMessage = "La descripcion del empleo es requerida")]
        [Display(Name = "Descripcion")]
        public CandidateExperiencesDescription Description { get; private set; }

        [Required(ErrorMessage = "El salario es requrido")]
        [Display(Name = "Salario")]
        public CandidateExperiencesSalary Salary { get; private set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        [Display(Name = "Fecha de Inicio")]
        public CandidateExperiencesBeginDate BeginDate { get; private set; }

        public CandidateExperiencesEndDate EndDate { get; private set; }

        public CandidateExperiencesInsertDate InsertDate { get; private set; }

        public CandidateExperiencesModifyDate ModifyDate { get; private set; }

        [ForeignKey("IdCandidate")]
        public virtual Candidates Candidate { get; private set; }

        public void SetIdCandidate(int idCandidate)
        {
            IdCandidate = idCandidate;
        }

        public void SetCompany(CandidateExperiencesCompany company)
        {
            Company = company;
        }
        public void SetJob(CandidateExperiencesJob job)
        {
            Job = job;
        }
        public void SetDescription(CandidateExperiencesDescription description)
        {
            Description = description;
        }
        public void SetSalary(CandidateExperiencesSalary salary)
        {
            Salary = salary;
        }
        public void SetBeginDate(CandidateExperiencesBeginDate beginDate)
        {
            BeginDate = beginDate;
        }
        public void SetEndDate(CandidateExperiencesEndDate endDate)
        {
            EndDate = endDate;
        }
        public void SetInsertDate(CandidateExperiencesInsertDate insertDate)
        {
            InsertDate = insertDate;
        }
        public void SetModifyDate(CandidateExperiencesModifyDate modifyDate)
        {
            ModifyDate = modifyDate;
        }
    }
}
