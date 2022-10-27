using Microsoft.EntityFrameworkCore;
using RedArbor.RedArborTest.Domain.Entities;
using RedArbor.RedArborTest.Domain.Repositories;
using RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences;
using RedArbor.RedArborTest.Domain.ValueObjects.Candidates;
using RedArborDb;
using RedArborDb.Repositories;
using RedArborTest.Commands.CandidateExperiencesCommands;
using RedArborTest.Queries.CandidateExperiencesQueries;
using RedArborTest.Services.CandidateExperiencesService;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTestsRedArbor
{
    public class CandidateExperiencesServiceTests
    {
        private readonly ICandidateExperiencesService candidatesService;
        private readonly ICandidateExperiencesRepository candidatesRepository;
        private readonly ICandidateExperiencesQueries candidatesQueries;
        RedArborDbContext context;

        public CandidateExperiencesServiceTests()
        {
            context = new RedArborDbContext(new DbContextOptionsBuilder<RedArborDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            candidatesRepository = new CandidateExperiencesRepository(context);
            candidatesQueries = new CandidateExperiencesQueries(candidatesRepository);
            candidatesService = new CandidateExperiencesService(candidatesRepository, candidatesQueries);

            AddCandidates(2);
        }

        [Fact]
        public async void GetAll_NotEmptyDataBase_Ok()
        {
            AddExperiences(2);

            var response = await candidatesService.GetExperiences();

            Assert.NotNull(response);
            Assert.Equal(context.CandidateExperiences.ToList().Count, response.Count);
            foreach (var candidateExperience in context.CandidateExperiences.ToList())
            {
                Assert.Contains(response.ToList(), x => x.IdCandidateExperience.Equals(candidateExperience.IdCandidateExperience)
                                                    && x.IdCandidate.Equals(candidateExperience.IdCandidate)
                                                    && x.Company.Equals(candidateExperience.Company)
                                                    && x.Job.Equals(candidateExperience.Job)
                                                    && x.Description.Equals(candidateExperience.Description)
                                                    && x.BeginDate.Equals(candidateExperience.BeginDate)
                                                    && x.EndDate.Equals(candidateExperience.EndDate));
            }
        }



        [Fact]
        public async void GetAll_EmptyDataBase_NotThrows()
        {
            var error = Record.ExceptionAsync(async () => await candidatesService.GetExperiences());
            Assert.Null(error.Exception);
        }

        [Fact]
        public async void GetById_Found_Ok()
        {
            AddExperiences(1);
            var experience = context.CandidateExperiences.First();
            var response = await candidatesService.GetExperienceById(experience.IdCandidateExperience);

            Assert.NotNull(response);

            Assert.True(experience.IdCandidate.Equals(response.IdCandidate));
            Assert.True(experience.Company.Equals(response.Company));
            Assert.True(experience.Job.Equals(response.Job));
            Assert.True(experience.Description.Equals(response.Description));
            Assert.True(experience.BeginDate.Equals(response.BeginDate));
            Assert.True(experience.EndDate.Equals(response.EndDate));
        }

        [Fact]
        public async void GetById_NotFound_NotThrows()
        {
            AddExperiences(1);
            var error = Record.ExceptionAsync(async () => await candidatesService.GetExperienceById(context.CandidateExperiences.Last().IdCandidateExperience + 10));

            Assert.Null(error.Exception);

            var response = await candidatesService.GetExperienceById(context.CandidateExperiences.Last().IdCandidateExperience + 10);

            Assert.Null(response);
        }

        [Fact]
        public async void Create_NeeExperience_Ok()
        {
            var experience = CreateExperience(context.Candidates.First(), 0, 1);

            var error = Record.ExceptionAsync(async () => await candidatesService.Create(CreateCandidateExperienceCommand(experience)));
            Assert.Null(error.Exception);

            var experienceDb = context.CandidateExperiences.FirstOrDefault();


            Assert.NotNull(experienceDb);
            Assert.True(experienceDb.IdCandidate.Equals(experience.IdCandidate));
            Assert.True(experienceDb.Company.Equals(experience.Company));
            Assert.True(experienceDb.Job.Equals(experience.Job));
            Assert.True(experienceDb.Description.Equals(experience.Description));
            Assert.True(experienceDb.Salary.Equals(experience.Salary));
            Assert.True(experienceDb.BeginDate.Equals(experience.BeginDate));
            Assert.True(experienceDb.EndDate.Equals(experience.EndDate));
            Assert.NotNull(experienceDb.InsertDate);
        }

        [Fact]
        public async void Update_UpdateCandidate_Ok()
        {
            AddExperiences(1);

            var updateExperience = context.CandidateExperiences.First();

            updateExperience.SetCompany(CandidateExperiencesCompany.Create("Updated" + "Company" + "Candidate" + updateExperience.IdCandidate));
            updateExperience.SetJob(CandidateExperiencesJob.Create("Updated" + "Job" + "Candidate" + updateExperience.IdCandidate));
            updateExperience.SetDescription(CandidateExperiencesDescription.Create("Updated" + "Description" + "Candidate" + updateExperience.IdCandidate));
            updateExperience.SetSalary(CandidateExperiencesSalary.Create(updateExperience.Salary.Value + 1));
            updateExperience.SetBeginDate(CandidateExperiencesBeginDate.Create(DateTime.Today.AddYears(-2)));

            var error = Record.ExceptionAsync(async () => await candidatesService.Update(UpdateCandidateExperienceCommand(updateExperience)));
            Assert.Null(error.Exception);

            var candidateDb = context.CandidateExperiences.First(x => x.IdCandidateExperience.Equals(updateExperience.IdCandidateExperience));

            Assert.True(candidateDb.IdCandidate.Equals(updateExperience.IdCandidate));
            Assert.True(candidateDb.Company.Equals(updateExperience.Company));
            Assert.True(candidateDb.Job.Equals(updateExperience.Job));
            Assert.True(candidateDb.Description.Equals(updateExperience.Description));
            Assert.True(candidateDb.Salary.Equals(updateExperience.Salary));
            Assert.True(candidateDb.BeginDate.Equals(updateExperience.BeginDate));
            Assert.True(candidateDb.EndDate.Equals(updateExperience.EndDate));
            Assert.NotNull(candidateDb.ModifyDate);
        }

        [Fact]
        public async void Delete_DeleteCandidate_Ok()
        {
            AddExperiences(1);

            var deletedExperience = context.CandidateExperiences.First();

            var error = Record.ExceptionAsync(async () => await candidatesService.Delete(deletedExperience.IdCandidateExperience));
            Assert.Null(error.Exception);

            var candidateDb = context.CandidateExperiences.Where(x => x.IdCandidateExperience.Equals(deletedExperience.IdCandidateExperience));

            Assert.Empty(candidateDb);
        }

        private UpdateCandidateExperienceCommand UpdateCandidateExperienceCommand(CandidateExperiences updatedExperience)
            => new UpdateCandidateExperienceCommand(updatedExperience.IdCandidateExperience, updatedExperience.IdCandidate, updatedExperience.Company.Value, updatedExperience.Job.Value, 
                                                    updatedExperience.Description.Value, updatedExperience.Salary.Value, updatedExperience.BeginDate.Value, updatedExperience.EndDate.Value, updatedExperience.Candidate);

        private CreateCandidateExperienceCommand CreateCandidateExperienceCommand(CandidateExperiences updatedExperience)
            => new CreateCandidateExperienceCommand(updatedExperience.IdCandidate, updatedExperience.Company.Value, updatedExperience.Job.Value, updatedExperience.Description.Value, updatedExperience.Salary.Value, updatedExperience.BeginDate.Value, updatedExperience.EndDate.Value);

        private void AddExperiences(int num)
        {
            var candidatesExperiences = new List<CandidateExperiences>();
            var candidatesDb = context.Candidates.ToList();

            for (int i = 0; i < candidatesDb.Count(); i++)
            {
                for (int j = 0; j < num; j++)
                {
                    candidatesExperiences.Add(CreateExperience(candidatesDb[i], j, num));
                }
            }

            context.AddRange(candidatesExperiences);
            context.SaveChanges();
        }

        private CandidateExperiences CreateExperience(Candidates candidate, int j, int num)
        {
            var candidateExperience = new CandidateExperiences();
            candidateExperience.SetIdCandidate(candidate.IdCandidate);
            candidateExperience.SetCompany(CandidateExperiencesCompany.Create("Company" + j + "Candidate" + candidate.IdCandidate));
            candidateExperience.SetJob(CandidateExperiencesJob.Create("Job" + j + "Candidate" + candidate.IdCandidate));
            candidateExperience.SetDescription(CandidateExperiencesDescription.Create("Description" + j + "Candidate" + candidate.IdCandidate));
            candidateExperience.SetSalary(CandidateExperiencesSalary.Create(100));
            candidateExperience.SetBeginDate(CandidateExperiencesBeginDate.Create(DateTime.Today.AddYears(-2)));
            candidateExperience.SetEndDate(CandidateExperiencesEndDate.Create(num - 1 == j ? null : DateTime.Today.AddYears(-2)));

            return candidateExperience;
        }

        private List<Candidates> AddCandidates(int num)
        {
            List<Candidates> response = CreateCandidates(num);
            context.Candidates.AddRange(response);
            context.SaveChanges();

            return response;
        }

        private static List<Candidates> CreateCandidates(int num)
        {
            var response = new List<Candidates>();
            for (int i = 0; i < num; i++)
            {
                var candidate = new Candidates();
                candidate.SetName(CandidateName.Create("CandidateName" + i.ToString()));
                candidate.SetSurname(CandidateSurname.Create("CandidateSurname" + i.ToString()));
                candidate.SetEmail(CandidateEmail.Create("Candidate" + i.ToString() + "@email.com"));
                candidate.SetBirthdate(CandidateBirthdate.Create(DateTime.Today.AddYears(-21)));
                candidate.SetInsertDate(CandidateInsertDate.Create(DateTime.Now.AddDays(-30)));

                response.Add(candidate);
            }

            return response;
        }
    }
}
