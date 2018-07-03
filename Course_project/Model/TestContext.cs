namespace Course_project
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestContext : DbContext
    {
        public TestContext()
            : base("name=TestContext")
        {
        }

        private static TestContext context;

        public static TestContext getContext()
        {
            if (context == null)
                context = new TestContext();
            return context;
        }

        public virtual DbSet<Answer> Answers { get; set; }

        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<User_System> User_System { get; set; }
        public virtual DbSet<UserInfo> UserUnfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .Property(e => e.Text_Answer)
                .IsUnicode(false);

            modelBuilder.Entity<Answer>()
                .HasMany(e => e.Questions)
                .WithMany(e => e.Answers)
                .Map(m => m.ToTable("Question_Answer").MapLeftKey("ID_Answer").MapRightKey("ID_Question"));

            modelBuilder.Entity<Property>()
                .Property(e => e.Description_Property)
                .IsUnicode(false);

            modelBuilder.Entity<Property>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Property)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Property>()
                .HasMany(e => e.Tests)
                .WithRequired(e => e.Property)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Text_Question)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Tests)
                .WithMany(e => e.Questions)
                .Map(m => m.ToTable("Test_Question").MapLeftKey("ID_Question").MapRightKey("ID_Test"));

            modelBuilder.Entity<Theme>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Theme>()
                .HasMany(e => e.Properties)
                .WithRequired(e => e.Theme)
                .WillCascadeOnDelete(false);
        }

     
    }
}
