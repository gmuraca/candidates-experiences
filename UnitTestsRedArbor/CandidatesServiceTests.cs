using Microsoft.EntityFrameworkCore;
using RedArbor.RedArborTest.Domain.Entities;
using RedArbor.RedArborTest.Domain.Repositories;
using RedArbor.RedArborTest.Domain.ValueObjects.Candidates;
using RedArborDb;
using RedArborDb.Repositories;
using RedArborTest.Commands.CandidatesCommands;
using RedArborTest.Queries.CandidatesQueries;
using RedArborTest.Services.CandidatesService;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTestsRedArbor
{
    public class CandidatesServiceTests
    {
        private readonly ICandidatesService candidatesService;
        private readonly ICandidatesRepository candidatesRepository;
        private readonly ICandidatesQueries candidatesQueries;
        RedArborDbContext context;

        public CandidatesServiceTests()
        {
            context = new RedArborDbContext(new DbContextOptionsBuilder<RedArborDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            candidatesRepository = new CandidatesRepository(context);
            candidatesQueries = new CandidatesQueries(candidatesRepository);
            candidatesService = new CandidatesService(candidatesRepository, candidatesQueries);
        }

        [Fact]
        public async void GetAll_NotEmptyDataBase_Ok()
        {
            var candidatesDb = AddCandidates(2);

            var response = await candidatesService.GetCandidates();

            Assert.NotNull(response);
            Assert.Equal(candidatesDb.Count, response.Count);
            foreach (var candidate in candidatesDb)
            {
                Assert.True(response.ToList().Any(x => x.IdCandidate.Equals(candidate.IdCandidate)));
                Assert.True(response.ToList().Any(x => x.Name.Equals(candidate.Name)));
                Assert.True(response.ToList().Any(x => x.Surname.Equals(candidate.Surname)));
                Assert.True(response.ToList().Any(x => x.Birthdate.Equals(candidate.Birthdate)));
                Assert.True(response.ToList().Any(x => x.Email.Equals(candidate.Email)));
            }
        }

        [Fact]
        public async void GetAll_EmptyDataBase_NotThrows()
        {
            var error = Record.ExceptionAsync(async () => await candidatesService.GetCandidates());
            Assert.Null(error.Exception);
        }

        [Fact]
        public async void GetById_Found_Ok()
        {
            var candidatesDb = AddCandidates(1);

            var response = await candidatesService.GetCandidate(context.Candidates.First().IdCandidate);

            Assert.NotNull(response);

            Assert.True(candidatesDb.First().IdCandidate.Equals(response.IdCandidate));
            Assert.True(candidatesDb.First().Name.Equals(response.Name));
            Assert.True(candidatesDb.First().Surname.Equals(response.Surname));
            Assert.True(candidatesDb.First().Birthdate.Equals(response.Birthdate));
            Assert.True(candidatesDb.First().Email.Equals(response.Email));
        }

        [Fact]
        public async void GetById_NotFound_NotThrows()
        {
            var candidatesDb = AddCandidates(1);
            var error = Record.ExceptionAsync(async () => await candidatesService.GetCandidate(context.Candidates.First().IdCandidate + 1));
            
            Assert.Null(error.Exception);

            var response = await candidatesService.GetCandidate(context.Candidates.First().IdCandidate + 1);

            Assert.Null(response);  
        }

        [Fact]
        public async void Create_NewCandidate_Ok()
        {
            var newCandidate = CreateCandidates(1).FirstOrDefault();

            var error = Record.ExceptionAsync(async () => await candidatesService.Create(CreateCandidateCommand(newCandidate)));
            Assert.Null(error.Exception);

            Assert.True(context.Candidates.Any(x => x.Name.Value.Equals(newCandidate.Name.Value) 
                                        && x.Surname.Value.Equals(newCandidate.Surname.Value)
                                        && x.Birthdate.Value.Equals(newCandidate.Birthdate.Value)
                                        && x.Email.Value.Equals(newCandidate.Email.Value)));
        }

        [Fact]
        public async void Update_UpdateCandidate_Ok()
        {
            var updatedCandidate = AddCandidates(1).FirstOrDefault();

            updatedCandidate.SetName(CandidateName.Create("Updated" + updatedCandidate.Name.Value));
            updatedCandidate.SetSurname(CandidateSurname.Create("Updated" + updatedCandidate.Surname.Value));
            updatedCandidate.SetBirthdate(CandidateBirthdate.Create(DateTime.Today.AddYears(-10)));
            updatedCandidate.SetEmail(CandidateEmail.Create("Updated" + updatedCandidate.Email.Value));

            var error = Record.ExceptionAsync(async () => await candidatesService.Update(UpdateCandidateCommand(updatedCandidate)));
            Assert.Null(error.Exception);

            Assert.True(context.Candidates.Any(x => x.Name.Value.Equals(updatedCandidate.Name.Value)
                                        && x.Surname.Value.Equals(updatedCandidate.Surname.Value)
                                        && x.Birthdate.Value.Equals(updatedCandidate.Birthdate.Value)
                                        && x.Email.Value.Equals(updatedCandidate.Email.Value)));
            Assert.True(context.Candidates.Where(x => x.Name.Value.Equals(updatedCandidate.Name.Value)
                                        && x.Surname.Value.Equals(updatedCandidate.Surname.Value)
                                        && x.Birthdate.Value.Equals(updatedCandidate.Birthdate.Value)
                                        && x.Email.Value.Equals(updatedCandidate.Email.Value)).Count() == 1);
        }

        [Fact]
        public async void Delete_DeleteCandidate_Ok()
        {
            var deleteCandidate = AddCandidates(1).FirstOrDefault();

            var error = Record.ExceptionAsync(async () => await candidatesService.Delete(deleteCandidate.IdCandidate));
            Assert.Null(error.Exception);

            Assert.True(!context.Candidates.Any(x => x.Name.Value.Equals(deleteCandidate.Name.Value)
                                        && x.Surname.Value.Equals(deleteCandidate.Surname.Value)
                                        && x.Birthdate.Value.Equals(deleteCandidate.Birthdate.Value)
                                        && x.Email.Value.Equals(deleteCandidate.Email.Value)));
            Assert.True(context.Candidates.Where(x => x.Name.Value.Equals(deleteCandidate.Name.Value)
                                        && x.Surname.Value.Equals(deleteCandidate.Surname.Value)
                                        && x.Birthdate.Value.Equals(deleteCandidate.Birthdate.Value)
                                        && x.Email.Value.Equals(deleteCandidate.Email.Value)).Count() == 0);
        }

        private UpdateCandidateCommand UpdateCandidateCommand(Candidates? updatedCandidate)
            => new UpdateCandidateCommand(updatedCandidate.IdCandidate, updatedCandidate.Name.Value, updatedCandidate.Surname.Value, updatedCandidate.Birthdate.Value, updatedCandidate.Email.Value);

        private CreateCandidateCommand CreateCandidateCommand(Candidates candidate)
            => new CreateCandidateCommand(candidate.Name.Value, candidate.Surname.Value, candidate.Birthdate.Value, candidate.Email.Value);

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