// <copyright file="ConventionalCommitMessageFactory.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Commits
{
    using System;
    using System.Text;
    using Bogus;

    /// <summary>
    /// Responsible for creating commit messages which conform to the conventional commit specification.
    /// </summary>
    public class ConventionalCommitMessageFactory
    {
        private readonly IFactory<CommitDescription> commitDescriptionFactory;
        private readonly IFactory<CommitScope> commitScopeFactory;
        private readonly Faker faker = new Faker();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConventionalCommitMessageFactory"/> class.
        /// </summary>
        public ConventionalCommitMessageFactory()
        {
            this.commitDescriptionFactory = new BogusCommitDescriptionFactory();
            this.commitScopeFactory = new BogusCommitScopeFactory();
        }

        /// <summary>
        /// Creates a new commit message with the given <paramref name="commitType"/>.
        /// </summary>
        /// <param name="commitType">The <see cref="CommitType"/> to use.</param>
        /// <returns>A <see cref="string"/> representing a commit message.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="commitType"/> is null.
        /// </exception>
        public string CreateCommitMessage(CommitType commitType)
        {
            return this.CreateCommitMessage(commitType, CommitMessageOptions.Default);
        }

        /// <summary>
        /// Creates a new commit message with the given <paramref name="commitType"/> and <paramref name="options"/>.
        /// </summary>
        /// <param name="commitType">The <see cref="CommitType"/> to use.</param>
        /// <param name="options">Specifies the options to use when creating the commit message.</param>
        /// <returns>A <see cref="string"/> representing a commit message.</returns>
        /// <exception cref="ArgumentNullException">
        /// If either <paramref name="commitType"/> or <paramref name="options"/> are null.
        /// </exception>
        public string CreateCommitMessage(CommitType commitType, CommitMessageOptions options)
        {
            if (commitType is null)
            {
                throw new ArgumentNullException(nameof(commitType));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(commitType);

            this.MaybeAppendScope(options, stringBuilder);

            this.MaybeAppendExclamationMark(options, stringBuilder);

            this.AppendDescription(stringBuilder);

            this.MaybeAppendBody(options, stringBuilder);

            this.MaybeAppendTrailer(options, stringBuilder);

            return stringBuilder.ToString();
        }

        private void MaybeAppendExclamationMark(CommitMessageOptions options, StringBuilder stringBuilder)
        {
            if (options.HasBreakingChange)
            {
                stringBuilder.Append("!");
            }
        }

        private void MaybeAppendTrailer(CommitMessageOptions options, StringBuilder stringBuilder)
        {
            if (options.HasFooter)
            {
                stringBuilder.Append(Environment.NewLine);
                this.MaybeAppendIssue(options, stringBuilder);

                if (options.HasIssue && options.HasBreakingChange)
                {
                    stringBuilder.AppendLine();
                }

                this.MaybeAppendBreakingChange(options, stringBuilder);
            }
        }

        private void MaybeAppendBreakingChange(CommitMessageOptions options, StringBuilder stringBuilder)
        {
            if (options.HasBreakingChange)
            {
                var description = this.faker.Random.Words();
                stringBuilder.Append($"BREAKING CHANGE: {description}");
            }
        }

        private void MaybeAppendIssue(CommitMessageOptions options, StringBuilder stringBuilder)
        {
            if (options.HasIssue)
            {
                var issueNumber = this.faker.Random.Number(max: 10_000);
                stringBuilder.Append($"Closes #{issueNumber}");
            }
        }

        private void MaybeAppendScope(CommitMessageOptions options, StringBuilder stringBuilder)
        {
            if (options.HasScope)
            {
                CommitScope commitScope = this.commitScopeFactory.Create();
                stringBuilder.Append($"({commitScope})");
            }
        }

        private void AppendDescription(StringBuilder stringBuilder)
        {
            CommitDescription commitDescription = this.commitDescriptionFactory.Create();
            stringBuilder.Append($": {commitDescription}");
        }

        private void MaybeAppendBody(CommitMessageOptions options, StringBuilder stringBuilder)
        {
            if (options.HasBody)
            {
                string commitBody = this.faker.Random.Words();
                stringBuilder.Append($"{Environment.NewLine}{commitBody}");
            }
        }
    }
}