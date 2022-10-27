using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedArbor.RedArborTest.Domain.Entities;

namespace RedArborDb
{
    public class RedArborDbContext : DbContext
    {
        public RedArborDbContext(DbContextOptions<RedArborDbContext> options) : base(options)
        {

        }

        public DbSet<Candidates> Candidates { get; set; }
        public DbSet<CandidateExperiences> CandidateExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CandidatesEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new CandidateExperiencesEntityTypeConfiguration());
        }
    }

    public class CandidatesEntityTypeConfiguration : IEntityTypeConfiguration<Candidates>
    {
        public void Configure(EntityTypeBuilder<Candidates> builder)
        {
            builder.HasKey(x => x.IdCandidate);
            builder.OwnsOne(x => x.Name).Property(x => x.Value).HasColumnName("Name").IsRequired();
            builder.OwnsOne(x => x.Surname).Property(x => x.Value).HasColumnName("Surname").IsRequired();
            builder.OwnsOne(x => x.Birthdate).Property(x => x.Value).HasColumnName("Birthdate").IsRequired();
            builder.OwnsOne(x => x.Email).Property(x => x.Value).HasColumnName("Email").IsRequired();
            builder.OwnsOne(x => x.InsertDate).Property(x => x.Value).HasColumnName("InsertDate").IsRequired();
            builder.OwnsOne(x => x.ModifyDate).Property(x => x.Value).HasColumnName("ModifyDate");

            builder.Navigation(b => b.Experiences)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.ToTable("Candidates");
        }
    }

    public class CandidateExperiencesEntityTypeConfiguration : IEntityTypeConfiguration<CandidateExperiences>
    {
        public void Configure(EntityTypeBuilder<CandidateExperiences> builder)
        {
            builder.HasKey(x => x.IdCandidateExperience);
            builder.HasOne(o => o.Candidate).WithMany(x => x.Experiences).HasForeignKey("IdCandidate")
                        .OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasIndex("IdCandidate");
            builder.Property(x => x.IdCandidate).HasColumnName("IdCandidate").IsRequired();
            builder.OwnsOne(x => x.Company).Property(x => x.Value).HasColumnName("Company").IsRequired();
            builder.OwnsOne(x => x.Job).Property(x => x.Value).HasColumnName("Job").IsRequired();
            builder.OwnsOne(x => x.Description).Property(x => x.Value).HasColumnName("Description").IsRequired();
            builder.OwnsOne(x => x.Salary).Property(x => x.Value).HasColumnName("Salary").IsRequired();
            builder.OwnsOne(x => x.BeginDate).Property(x => x.Value).HasColumnName("BeginDate").IsRequired();
            builder.OwnsOne(x => x.EndDate).Property(x => x.Value).HasColumnName("EndDate");
            builder.OwnsOne(x => x.InsertDate).Property(x => x.Value).HasColumnName("InsertDate").IsRequired();
            builder.OwnsOne(x => x.ModifyDate).Property(x => x.Value).HasColumnName("ModifyDate");

            builder.ToTable("CandidateExperiences");
        }
    }
}