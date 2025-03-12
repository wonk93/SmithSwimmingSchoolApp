using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using SmithSwimmingSchoolApp.Models;

namespace SmithSwimmingSchoolApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Swimmer> Swimmers { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Grouping> Groupings { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Coach)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.CoachId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Swimmer)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.SwimmerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Grouping)
                .WithMany(g => g.Enrollments)
                .HasForeignKey(e => e.GroupingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Enrollment)
                .WithMany(e => e.Reports)
                .HasForeignKey(r => r.EnrollmentId);
           
            modelBuilder.Entity<Coach>()
                .HasOne(c => c.IdentityUser)
                .WithOne()
                .HasForeignKey<Coach>(c => c.IdentityUserId)
                .OnDelete(DeleteBehavior.Cascade);
           
            modelBuilder.Entity<Swimmer>()
                .HasOne(s => s.IdentityUser)
                .WithOne()
                .HasForeignKey<Swimmer>(s => s.IdentityUserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
                new IdentityRole { Id = "2", Name = "Coach", NormalizedName = "COACH" },
                new IdentityRole { Id = "3", Name = "Swimmer", NormalizedName = "SWIMMER" },
                new IdentityRole { Id = "4", Name = "Visitor", NormalizedName = "VISITOR" }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
           
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "100", UserName = "admin@3s.com", NormalizedUserName = "ADMIN@3S.COM",EmailConfirmed = true,
                    Email = "admin@3s.com", NormalizedEmail = "ADMIN@3S.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "RoPe75916213"),
                    FullName = "Administrator"
                },
                new ApplicationUser
                {
                    Id = "101", UserName = "johnsmith@3s.com", NormalizedUserName = "JOHNSMITH@3S.COM",EmailConfirmed = true,
                    Email = "johnsmith@3s.com", NormalizedEmail = "JOHNSMITH@3S.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "RoPe75916213"),
                    FullName = "John Smith"
                },
                new ApplicationUser
                {
                    Id = "102", UserName = "aliceaohnson@3s.com", NormalizedUserName = "ALICEJOHNSON@3S.COM",EmailConfirmed = true,
                    Email = "aliceaohnson@3s.com", NormalizedEmail = "ALICEJOHNSON@3S.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "RoPe75916213"),
                    FullName = "Alice Johnson"
                },
                 new ApplicationUser
                {
                    Id = "103", UserName = "bobbrown@3s.com", NormalizedUserName = "BOBBROWN@3S.COM",EmailConfirmed = true,
                    Email = "bobbrown@3s.com", NormalizedEmail = "BOBBROWN@3S.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "RoPe75916213"),
                    FullName = "Bob Brown"
                },
                 new ApplicationUser
                {
                    Id = "104", UserName = "charlieblack@3s.com", NormalizedUserName = "CHARLIEBLACK@3S.COM",EmailConfirmed = true,
                    Email = "charlieblack@3s.com", NormalizedEmail = "CHARLIEBLACK@3S.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "RoPe75916213"),
                    FullName = "Charlie Black"
                },
                 new ApplicationUser
                {
                    Id = "105", UserName = "dianawhite@3s.com", NormalizedUserName = "DIANAWHITE@3S.COM",EmailConfirmed = true,
                    Email = "dianawhite@3s.com", NormalizedEmail = "DIANAWHITE@3S.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "RoPe75916213"),
                    FullName = "Diana White"
                },
                 new ApplicationUser
                {
                    Id = "106", UserName = "edwardgreen@3s.com", NormalizedUserName = "EDWARDGREEN@3S.COM",EmailConfirmed = true,
                    Email = "edwardgreen@3s.com", NormalizedEmail = "EDWARDGREEN@3S.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "RoPe75916213"),
                    FullName = "Edward Green"
                },
                 new ApplicationUser
                {
                    Id = "107", UserName = "janedoe@3s.com", NormalizedUserName = "JANEDOE@3S.COM",EmailConfirmed = true,
                    Email = "janedoe@3s.com", NormalizedEmail = "JANEDOE@3S.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "RoPe75916213"),
                    FullName = "Jane Doe"
                },
                 new ApplicationUser
                {
                    Id = "108", UserName = "michaeljordan@3s.com", NormalizedUserName = "MICHAELJORDAN@3S.COM", EmailConfirmed = true,
                    Email = "michaeljordan@3s.com", NormalizedEmail = "MICHAELJORDAN@3S.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "RoPe75916213"),
                    FullName = "Michael Jordan",
                    
                },
                 new ApplicationUser
                {
                    Id = "109", UserName = "serenawilliams@3s.com", NormalizedUserName = "SERENAWILLIAMS@3S.COM",EmailConfirmed = true,
                    Email = "serenawilliams@3s.com", NormalizedEmail = "SERENAWILLIAMS@3S.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "RoPe75916213"),
                    FullName = "Serena Williams"
                },
                 new ApplicationUser
                {
                    Id = "110", UserName = "rogerfederer@3s.com", NormalizedUserName = "ROGERFEDERER@3S.COM",EmailConfirmed = true,
                    Email = "rogerfederer@3s.com", NormalizedEmail = "ROGERFEDERER@3S.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "RoPe75916213"),
                    FullName = "Roger Federer"
                },

            };
            modelBuilder.Entity<ApplicationUser>().HasData(users);

            // Seed User Roles
            var userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> { UserId = "100", RoleId = "1" }, // Admin
                new IdentityUserRole<string> { UserId = "101", RoleId = "2" }, // Coach
                new IdentityUserRole<string> { UserId = "102", RoleId = "3" },  // Swimmer
                new IdentityUserRole<string> { UserId = "107", RoleId = "2" },  // Coach
                new IdentityUserRole<string> { UserId = "108", RoleId = "2" },  // Coach
                new IdentityUserRole<string> { UserId = "109", RoleId = "2" },  // Coach
                new IdentityUserRole<string> { UserId = "110", RoleId = "2" },  // Coach
                new IdentityUserRole<string> { UserId = "103", RoleId = "3" },  // Swimmer
                new IdentityUserRole<string> { UserId = "104", RoleId = "3" },  // Swimmer
                new IdentityUserRole<string> { UserId = "105", RoleId = "3" },  // Swimmer
                new IdentityUserRole<string> { UserId = "106", RoleId = "3" }  // Swimmer
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            
            modelBuilder.Entity<Coach>().HasData(
                new Coach { Id = 1, Name = "John Smith", PhoneNumber = "123-456-7890", IdentityUserId = "101", CoachSex = CoachSex.Male },
                new Coach { Id = 2, Name = "Jane Doe", PhoneNumber = "987-654-3210", IdentityUserId = "107", CoachSex = CoachSex.Female },
                new Coach { Id = 3, Name = "Michael Jordan", PhoneNumber = "333-444-5555", IdentityUserId = "108", CoachSex = CoachSex.Male },
                new Coach { Id = 4, Name = "Serena Williams", PhoneNumber = "777-888-9999", IdentityUserId = "109", CoachSex = CoachSex.Female },
                new Coach { Id = 5, Name = "Roger Federer", PhoneNumber = "555-666-7777", IdentityUserId = "110", CoachSex = CoachSex.Female }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Beginner Swimming", CoachId = 1, LevelCourse = LevelCourse.Junior },
                new Course { Id = 2, Title = "Advanced Swimming", CoachId = 2, LevelCourse = LevelCourse.Gilder },
                new Course { Id = 3, Title = "Intermediate Swimming", CoachId = 3, LevelCourse = LevelCourse.Gilder },
                new Course { Id = 4, Title = "Expert Swimming", CoachId = 4, LevelCourse = LevelCourse.Senior },
                new Course { Id = 5, Title = "Master Swimming", CoachId = 5, LevelCourse = LevelCourse.Pro }
            );           
           
            modelBuilder.Entity<Swimmer>().HasData(
                new Swimmer
                {
                    Id = 1,
                    Name = "Alice Johnson",
                    PhoneNumber = "111-222-3333",                  
                    BirthDate = new DateTime(2005, 5, 15),
                    IdentityUserId = "102",
                    SwimmerSex = SwimmerSex.Female
                },
                new Swimmer
                {
                    Id = 2,
                    Name = "Bob Brown",
                    PhoneNumber = "444-555-6666",                   
                    BirthDate = new DateTime(2008, 8, 20),
                    IdentityUserId = "103",
                    SwimmerSex = SwimmerSex.Male
                },
                new Swimmer
                {
                    Id = 3,
                    Name = "Charlie Black",
                    PhoneNumber = "888-999-0000",                    
                    BirthDate = new DateTime(2010, 12, 25),
                    IdentityUserId = "104",
                    SwimmerSex = SwimmerSex.Male
                },
                new Swimmer
                {
                    Id = 4,
                    Name = "Diana White",
                    PhoneNumber = "222-333-4444",                    
                    BirthDate = new DateTime(2006, 3, 10),
                    IdentityUserId = "105",
                    SwimmerSex = SwimmerSex.Female
                },
                new Swimmer
                {
                    Id = 5,
                    Name = "Edward Green",
                    PhoneNumber = "555-777-8888",                    
                    BirthDate = new DateTime(2007, 7, 5),
                    IdentityUserId = "106",
                    SwimmerSex = SwimmerSex.Male
                }
            );
            
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Id = 1, CourseId = 1, SwimmerId = 1, GroupingId = 1 },
                new Enrollment { Id = 2, CourseId = 2, SwimmerId = 2, GroupingId = 2 },
                new Enrollment { Id = 3, CourseId = 3, SwimmerId = 3, GroupingId = 3 },
                new Enrollment { Id = 4, CourseId = 4, SwimmerId = 4, GroupingId = 4 },
                new Enrollment { Id = 5, CourseId = 5, SwimmerId = 5, GroupingId = 5 }
            );
           
            modelBuilder.Entity<Grouping>().HasData(
                new Grouping { Id = 1, Level = "Beginner", StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 4, 1), Places = 15 },
                new Grouping { Id = 2, Level = "Advanced", StartDate = new DateTime(2024, 1, 10), EndDate = new DateTime(2024, 4, 10), Places = 10 },
                new Grouping { Id = 3, Level = "Intermediate", StartDate = new DateTime(2024, 2, 1), EndDate = new DateTime(2024, 5, 1), Places = 12 },
                new Grouping { Id = 4, Level = "Expert", StartDate = new DateTime(2024, 3, 1), EndDate = new DateTime(2024, 6, 1), Places = 8 },
                new Grouping { Id = 5, Level = "Master", StartDate = new DateTime(2024, 4, 1), EndDate = new DateTime(2024, 7, 1), Places = 6 }
            );
          
            modelBuilder.Entity<Report>().HasData(
                new Report { Id = 1, Content = "Good progress", EnrollmentId = 1 },
                new Report { Id = 2, Content = "Excellent performance", EnrollmentId = 2 },
                new Report { Id = 3, Content = "Needs improvement", EnrollmentId = 3 },
                new Report { Id = 4, Content = "Outstanding swimmer", EnrollmentId = 4 },
                new Report { Id = 5, Content = "Beginner with potential", EnrollmentId = 5 }
            );
        }
    }
}
