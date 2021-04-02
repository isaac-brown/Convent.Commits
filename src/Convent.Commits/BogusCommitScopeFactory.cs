// <copyright file="BogusCommitScopeFactory.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Commits
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Bogus;

    /// <summary>
    /// Implementation of <see cref="IFactory{T}"/> for <see cref="CommitScope"/> using <see cref="Bogus"/>.
    /// </summary>
    internal class BogusCommitScopeFactory : IFactory<CommitScope>
    {
        private const string ReplaceableSymbolsPattern = @"[^\w\d]";
        private readonly Faker<CommitScope> faker;

        /// <summary>
        /// Initializes a new instance of the <see cref="BogusCommitScopeFactory"/> class.
        /// </summary>
        public BogusCommitScopeFactory()
        {
            this.faker = new Faker<CommitScope>();
            this.faker.CustomInstantiator(f =>
            {
                var words = Enumerable.Range(start: 1, f.Random.Number(min: 1, max: 3))
                                      .Select(_ => f.Random.Word().Split(' ').First())
                                      .Select(word => Regex.Replace(word, ReplaceableSymbolsPattern, string.Empty))
                                      .Select(word => word.ToLower());

                string scope = string.Join("-", words);

                return new CommitScope(scope);
            });
        }

        /// <inheritdoc/>
        public CommitScope Create() => this.faker.Generate(count: 1).Single();
    }
}