// <copyright file="BogusCommitDescriptionFactory.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Commits
{
    using System.Linq;
    using Bogus;

    /// <summary>
    /// Implementation of <see cref="IFactory{T}"/> for <see cref="CommitDescription"/> using <see cref="Bogus"/>.
    /// </summary>
    internal class BogusCommitDescriptionFactory : IFactory<CommitDescription>
    {
        private readonly Faker<CommitDescription> faker;

        /// <summary>
        /// Initializes a new instance of the <see cref="BogusCommitDescriptionFactory"/> class.
        /// </summary>
        public BogusCommitDescriptionFactory()
        {
            this.faker = new Faker<CommitDescription>();
            this.faker.CustomInstantiator(f => new CommitDescription(f.Random.Words()));
        }

        /// <inheritdoc/>
        public CommitDescription Create() => this.faker.Generate(count: 1).Single();
    }
}