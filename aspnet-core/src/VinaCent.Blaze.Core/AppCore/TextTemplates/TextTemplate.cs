using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinaCent.Blaze.AppCore.TextTemplates
{
    [Table(nameof(AppCore) + "." + nameof(TextTemplates))]
    public class TextTemplate : Entity<Guid>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsStatic { get; set; }

        public static readonly TextTemplate PasswordReset = new()
        {
            Id = Guid.Parse("f01007b5-2b85-478b-a36c-1dacb043df21"),
            Name = "Email::PasswordReset",
            Content = "{0}",
            IsStatic = true,
        };

        public static readonly TextTemplate EmailConfirmation = new()
        {
            Id = Guid.Parse("d2b46848-7996-4f68-9038-803fa49502e4"),
            Name = "Email::EmailConfirmation",
            Content = "{0}",
            IsStatic = true,
        };

        public static readonly TextTemplate SecurityCode = new()
        {
            Id = Guid.Parse("6ab11f58-3cf1-481a-83c8-e8a6f9b5a154"),
            Name = "Email::SecurityCode",
            Content = "{0}",
            IsStatic = true,
        };

        public static readonly TextTemplate WelcomeAfterJoinSystem = new()
        {
            Id = Guid.Parse("6ab11f58-3cf1-481a-83c8-e8a6f9b5a154"),
            Name = "Email::WelcomeAfterJoinSystem",
            Content = "{0}",
            IsStatic = true,
        };

        public static readonly TextTemplate GithubReleaseBuildCompleted = new()
        {
            Id = Guid.Parse("975260d1-0851-4346-871a-198e42d163b2"),
            Name = "Email::GithubReleaseBuildCompleted",
            Content = "{0}",
            IsStatic = true,
        };

        public static readonly List<TextTemplate> Defaults = new()
        {
            PasswordReset,
            EmailConfirmation,
            SecurityCode,
            WelcomeAfterJoinSystem,
            GithubReleaseBuildCompleted
        };
    }
}
