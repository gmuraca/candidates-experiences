using RedArbor.RedArborTest.Domain.ValueObjects.Candidates;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedArbor.RedArborTest.Domain.Entities
{
    public class Candidates
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Candidato")]
        public int IdCandidate { get; init; }

        [Required(ErrorMessage = "Your message here")]
        [Display(Name = "Nombre")]
        public CandidateName Name { get; private set; }

        [Required(ErrorMessage = "Your message here")]
        [Display(Name = "Apellido")]
        public CandidateSurname Surname { get; private set; }

        [Required(ErrorMessage = "Your message here")]
        [Display(Name = "Fecha de Nacimiento")]
        public CandidateBirthdate Birthdate { get; private set; }

        [Required(ErrorMessage = "Your message here")]
        [Display(Name = "Email")]
        public CandidateEmail Email { get; private set; }

        public CandidateInsertDate InsertDate { get; private set; }

        public CandidateModifyDate ModifyDate { get; private set; }

        public virtual IList<CandidateExperiences> Experiences { get; private set; }


        public void SetName(CandidateName name)
        {
            Name = name;
        }
        public void SetSurname(CandidateSurname surname)
        {
            Surname = surname;
        }
        public void SetBirthdate(CandidateBirthdate birthdate)
        {
            Birthdate = birthdate;
        }
        public void SetEmail(CandidateEmail email)
        {
            Email = email;
        }
        public void SetInsertDate(CandidateInsertDate insertDate)
        {
            InsertDate = insertDate;
        }
        public void SetModifyDate(CandidateModifyDate modifyDate)
        {
            ModifyDate = modifyDate;
        }
    }
}
