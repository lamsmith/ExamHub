﻿using ExamHub.Entity;
using ExamHub.Enum;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;

namespace ExamHub.Context
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Principal> Principals { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<ClassTeacher> ClassTeachers { get; set; }
        public DbSet<ClassStudent> ClassStudents { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }
        public DbSet<SubjectTeacher> SubjectTeachers { get; set; }
        public DbSet<SubjectStudent> SubjectStudents { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<GeneralExamResult> GeneralExamResults { get; set; }
        public DbSet<ListAnswer> ListAnswers { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Teacher>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Student>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Class>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Subject>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Exam>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Principal>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Option>().ToTable("Options");
            modelBuilder.Entity<StudentAnswer>().ToTable("StudentAnswers");



            modelBuilder.Entity<Principal>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<ExamQuestion>()
               .HasMany(eq => eq.Options)
               .WithOne(o => o.ExamQuestion)
               .HasForeignKey(o => o.ExamQuestionId);



            modelBuilder.Entity<ExamResult>()
                .HasOne(er => er.Student)
                .WithMany(s => s.ExamResults)
                .HasForeignKey(er => er.StudentId);

            modelBuilder.Entity<ExamResult>()
                .HasOne(er => er.Exam)
                .WithMany(e => e.ExamResults)
                .HasForeignKey(er => er.ExamId);

            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.ExamQuestion)
                .WithMany(eq => eq.StudentAnswers)
                .HasForeignKey(sa => sa.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);  // Or your desired delete behavior

            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.Student)
                .WithMany(s => s.StudentAnswers)
                .HasForeignKey(sa => sa.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GeneralExamResult>()
              .HasMany(ger => ger.ExamResults)
              .WithOne(er => er.GeneralExamResult)
              .HasForeignKey(er => er.GeneralExamResultId);

            modelBuilder.Entity<GeneralExamResult>()
                .HasOne(ger => ger.Student)
                .WithMany(s => s.GeneralExamResults)
                .HasForeignKey(ger => ger.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Class>()
                .Property(c => c.CreatedByPrincipalId)
                .IsRequired();





            modelBuilder.Entity<Role>().HasData(
              new Role { Id = 1, CreatedBy = "admin", CreatedAt = DateTime.Now, Name = "Principal" },
              new Role { Id = 2, CreatedBy = "admin", CreatedAt = DateTime.Now, Name = "Teacher" },
              new Role { Id = 3, CreatedBy = "admin", CreatedAt = DateTime.Now, Name = "Student", });

            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = 1,
                   CreatedAt = DateTime.Now,
                   Username = "admin",
                   Password = "admin",
                   FirstName = "ib",
                   LastName = "ibbb",
                   DateOfBirth = new DateTime(2000, 3, 19),
                   Gender = Gender.Male,
                   RoleId = 1,
                   CreatedBy = "1",
               });

            modelBuilder.Entity<User>().HasData(
              new User
              {
                  Id = 2,
                  CreatedAt = DateTime.Now,
                  Username = "teacher",
                  Password = "teacher",
                  FirstName = "Mko",
                  LastName = "axc",
                  DateOfBirth = new DateTime(1999, 3, 19),
                  Gender = Gender.Male,
                  RoleId = 2,
                  CreatedBy = "1",
              });

            modelBuilder.Entity<User>().HasData(
              new User
              {
                  Id = 3,
                  CreatedAt = DateTime.Now,
                  Username = "student",
                  Password = "student",
                  FirstName = "jnr",
                  LastName = "ib",
                  DateOfBirth = new DateTime(2005, 3, 19),
                  Gender = Gender.Male,
                  RoleId = 3,
                  CreatedBy = "1",
              });

            modelBuilder.Entity<Student>().HasData(
               new Student { Id = 1, UserId = 3, CreatedAt = DateTime.Now, CreatedBy = "1", }
                );

            modelBuilder.Entity<ClassStudent>().HasData(
                new ClassStudent { Id = 1, StudentId = 1, ClassId = 1, }
                );
            modelBuilder.Entity<ClassSubject>().HasData(
                new ClassSubject { Id = 1, SubjectId = 1, ClassId = 1, }
                );
            modelBuilder.Entity<ClassTeacher>().HasData(
                new ClassTeacher { Id = 1, TeacherId = 1, ClassId = 1, }
                );
            modelBuilder.Entity<StudentExam>().HasData(
                new StudentExam { Id = 1, ExamId = 1, StudentId = 1, }
                );
            modelBuilder.Entity<SubjectStudent>().HasData(
                new SubjectStudent { Id = 1, StudentId = 1, SubjectId = 1, }
                );
            modelBuilder.Entity<SubjectTeacher>().HasData(
                new SubjectTeacher { Id = 1, TeacherId = 1, SubjectId = 1, }
                );


            modelBuilder.Entity<Principal>().HasData(
             new Principal { Id = 1, UserId = 1, CreatedBy = "1", CreatedAt = DateTime.Now }
               );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, UserId = 2, CreatedBy = "1", CreatedAt = DateTime.Now }
                 );

            modelBuilder.Entity<Class>().HasData(
               new Class { Id = 1, ClassName = "JSS 1", CreatedBy = "1", CreatedAt = DateTime.Now },
               new Class { Id = 2, ClassName = "JSS 2", CreatedBy = "1", CreatedAt = DateTime.Now },
               new Class { Id = 3, ClassName = "JSS 3", CreatedBy = "1", CreatedAt = DateTime.Now },
               new Class { Id = 4, ClassName = "SSS 1", CreatedBy = "1", CreatedAt = DateTime.Now },
               new Class { Id = 5, ClassName = "SSS 2", CreatedBy = "1", CreatedAt = DateTime.Now }

               );

            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, SubjectName = "Math", CreatedBy = "1", CreatedAt = DateTime.Now, PrincipalId = 1 }
                );

            modelBuilder.Entity<Exam>().HasData(
                new Exam
                {
                    Id = 1,
                    ClassId = 1,
                    CreatedByTeacherId = 1,
                    ExamName = "frist term",
                    SubjectId = 1,
                    CreatedBy = "1",
                    CreatedAt = DateTime.Now,
                    StartTime = DateTime.Parse("08/04/2024 15:30"),
                    EndTime = DateTime.Parse("08/04/2024 17:30")
                }
                 );

           







        }
    }
}