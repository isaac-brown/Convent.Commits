// <copyright file="ConventionalCommitMessageFactoryTests.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Commits.UnitTests
{
    using System;
    using AutoFixture;
    using Convent.Commits;
    using FluentAssertions;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ConventionalCommitMessageFactory"/> class.
    /// </summary>
    public class ConventionalCommitMessageFactoryTests
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented

        private const string Scope = @"\([a-z\d-]+\)";
        private const string Description = ".+";
        private const string Issue = @"Closes #\d+";
        private const string NewLine = @"(\r|\n|\r\n)";
        private const string BreakingChange = "BREAKING CHANGE: .+";

        private static readonly string Body = $"((?>.+){NewLine}?)";

        [Fact]
        public void Given_no_commit_message_options_When_CreateCommitMessage_is_called_Then_should_include_type_and_description()
        {
            // Arrange.
            IFixture fixture = new Fixture().Customize(new ConventCommitsCustomization());
            var commitType = fixture.Create<CommitType>();

            var sut = fixture.Create<ConventionalCommitMessageFactory>();

            // Act.
            var actual = sut.CreateCommitMessage(commitType);

            // Assert.
            actual.Should()
                  .MatchRegex($"^{commitType}: {Description}$");
        }

        [Fact]
        public void Given_commit_type_is_null_When_CreateCommitMessage_is_called_Then_should_throw_ArgumentNullException()
        {
            // Arrange.
            IFixture fixture = new Fixture();

            var sut = fixture.Create<ConventionalCommitMessageFactory>();

            // Act.
            Action createCommitMessageWithNullOptions = () => sut.CreateCommitMessage(commitType: null!);

            // Assert.
            createCommitMessageWithNullOptions.Should()
                                              .Throw<ArgumentNullException>()
                                              .WithMessage("Value cannot be null. (Parameter 'commitType')");
        }

        [Fact]
        public void Given_commit_message_options_is_null_When_CreateCommitMessage_is_called_Then_should_throw_ArgumentNullException()
        {
            // Arrange.
            IFixture fixture = new Fixture().Customize(new ConventCommitsCustomization());
            var commitType = fixture.Create<CommitType>();

            var sut = fixture.Create<ConventionalCommitMessageFactory>();

            // Act.
            Action createCommitMessageWithNullOptions = () => sut.CreateCommitMessage(commitType, options: null!);

            // Assert.
            createCommitMessageWithNullOptions.Should()
                                              .Throw<ArgumentNullException>()
                                              .WithMessage("Value cannot be null. (Parameter 'options')");
        }

        [Fact]
        public void Given_commit_message_options_has_scope_When_CreateCommitMessage_is_called_Then_result_should_include_type_scope_and_description()
        {
            // Arrange.
            IFixture fixture = new Fixture().Customize(new ConventCommitsCustomization());
            var commitType = fixture.Create<CommitType>();
            var options = new CommitMessageOptions
            {
                HasScope = true,
            };

            var sut = fixture.Create<ConventionalCommitMessageFactory>();

            // Act.
            var actual = sut.CreateCommitMessage(commitType, options);

            // Assert.
            actual.Should()
                  .MatchRegex($"^{commitType}{Scope}: {Description}$");
        }

        [Fact]
        public void Given_commit_message_options_has_body_When_CreateCommitMessage_is_called_Then_result_should_include_type_and_body()
        {
            // Arrange.
            IFixture fixture = new Fixture().Customize(new ConventCommitsCustomization());
            var commitType = fixture.Create<CommitType>();
            var options = new CommitMessageOptions
            {
                HasBody = true,
            };

            var sut = fixture.Create<ConventionalCommitMessageFactory>();

            // Act.
            var actual = sut.CreateCommitMessage(commitType, options);

            // Assert.
            actual.Should()
                  .MatchRegex($"^{commitType}: {Description}{NewLine}"
                            + $"{Body}+$");
        }

        [Fact]
        public void Given_commit_message_options_has_issue_When_CreateCommitMessage_is_called_Then_result_should_include_type_and_issue()
        {
            // Arrange.
            IFixture fixture = new Fixture().Customize(new ConventCommitsCustomization());
            var commitType = fixture.Create<CommitType>();
            var options = new CommitMessageOptions
            {
                HasIssue = true,
            };

            var sut = fixture.Create<ConventionalCommitMessageFactory>();

            // Act.
            var actual = sut.CreateCommitMessage(commitType, options);

            // Assert.
            actual.Should()
                  .MatchRegex($"^{commitType}: {Description}{NewLine}"
                            + $"{Issue}$");
        }

        [Fact]
        public void Given_commit_message_options_has_breaking_change_When_CreateCommitMessage_is_called_Then_result_should_include_type_and_breaking_change()
        {
            // Arrange.
            IFixture fixture = new Fixture().Customize(new ConventCommitsCustomization());
            var commitType = fixture.Create<CommitType>();
            var options = new CommitMessageOptions
            {
                HasBreakingChange = true,
            };

            var sut = fixture.Create<ConventionalCommitMessageFactory>();

            // Act.
            var actual = sut.CreateCommitMessage(commitType, options);

            // Assert.
            actual.Should()
                  .MatchRegex($"^{commitType}!: {Description}{NewLine}"
                            + $"{BreakingChange}$");
        }

        [Fact]
        public void Given_commit_message_options_has_breaking_change_and_scope_When_CreateCommitMessage_is_called_Then_exclamation_mark_should_be_added_after_scope()
        {
            // Arrange.
            IFixture fixture = new Fixture().Customize(new ConventCommitsCustomization());
            var commitType = fixture.Create<CommitType>();
            var options = new CommitMessageOptions
            {
                HasBreakingChange = true,
                HasScope = true,
            };

            var sut = fixture.Create<ConventionalCommitMessageFactory>();

            // Act.
            var actual = sut.CreateCommitMessage(commitType, options);

            // Assert.
            actual.Should()
                  .MatchRegex($"^{commitType}{Scope}!: {Description}{NewLine}"
                            + $"{BreakingChange}$");
        }

        [Fact]
        public void Given_commit_message_options_has_issue_and_breaking_change_When_CreateCommitMessage_is_called_Then_result_should_have_issue_followed_by_breaking_change()
        {
            // Arrange.
            IFixture fixture = new Fixture().Customize(new ConventCommitsCustomization());
            var commitType = fixture.Create<CommitType>();
            var options = new CommitMessageOptions
            {
                HasIssue = true,
                HasBreakingChange = true,
            };

            var sut = fixture.Create<ConventionalCommitMessageFactory>();

            // Act.
            var actual = sut.CreateCommitMessage(commitType, options);

            // Assert.
            actual.Should()
                  .MatchRegex($"{Issue}{NewLine}"
                            + $"{BreakingChange}$");
        }
    }
}
