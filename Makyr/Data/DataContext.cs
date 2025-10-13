using System;
using System.Net.Mail;
using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : IdentityDbContext<AppUser>
{
      public DataContext(DbContextOptions<DataContext> options) : base(options)
      {
      }

      public DbSet<Photo> Photos { get; set; } = null!;
      public DbSet<Question> Questions { get; set; } = null!;
      public DbSet<Answer> Answers { get; set; } = null!;
      public DbSet<QuestionAttachment> QuestionAttachments { get; set; } = null!;
      public DbSet<AnswerAttachment> AnswerAttachments { get; set; } = null!;

      protected override void OnModelCreating(ModelBuilder builder)
      {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(entity =>
            {
                  entity.HasMany(user => user.Photos)
                    .WithOne(photo => photo.AppUser)
                    .HasForeignKey(photo => photo.AppUserId)
                    .OnDelete(DeleteBehavior.Cascade);

                  entity.HasMany(user => user.Questions)
                    .WithOne(question => question.AppUser)
                    .HasForeignKey(question => question.AppUserId)
                    .OnDelete(DeleteBehavior.Cascade);

                  entity.HasMany(user => user.Answers)
                    .WithOne(answer => answer.AppUser)
                    .HasForeignKey(answer => answer.AppUserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Photo>(entity =>
            {
                  entity.Property(p => p.Url)
                    .IsRequired();
            });

            builder.Entity<QuestionAttachment>(entity =>
            {
                  entity.Property(attachment => attachment.Url)
                    .IsRequired();

                  entity.HasOne(attachment => attachment.Question)
                        .WithMany(question => question.Attachments)
                        .HasForeignKey(attachment => attachment.QuestionId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AnswerAttachment>(entity =>
            {
                  entity.Property(attachment => attachment.Url)
                    .IsRequired();

                  entity.HasOne(attachment => attachment.Answer)
                        .WithMany(answer => answer.Attachments)
                        .HasForeignKey(attachment => attachment.AnswerId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Question>(entity =>
            {
                  entity.Property(question => question.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                  entity.Property(question => question.Description)
                    .IsRequired();

                  entity.HasMany(question => question.Answers)
                    .WithOne(answer => answer.Question)
                    .HasForeignKey(answer => answer.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Answer>(entity =>
            {
                  entity.Property(answer => answer.Description)
                    .IsRequired();
            });
      }

}
