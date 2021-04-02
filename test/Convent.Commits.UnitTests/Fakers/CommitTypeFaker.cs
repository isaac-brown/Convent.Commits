// <copyright file="CommitTypeFaker.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Commits.UnitTests.Fakers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;
    using Convent.Commits;

    /// <summary>
    /// Provides test data creation for the <see cref="CommitType"/> class.
    /// </summary>
    internal static class CommitTypeFaker
    {
        // TODO: Look at scanning an assembly for all static members which return CommitType where the class also
        // extends CommitType instead of this.
        private static readonly IEnumerable<CommitType> CommitTypes = new CommitType[]
        {
             CommitType.Feature,
             CommitType.Fix,
             CommitType.Chore,
        };

        private static readonly Faker<CommitType> Faker;

        static CommitTypeFaker()
        {
            Faker = new Faker<CommitType>();
            Faker.CustomInstantiator(f =>
            {
                return f.PickRandom(CommitTypes);
            });
        }

        /// <summary>
        /// Creates one new well-formed <see cref="CommitType"/> instance.
        /// </summary>
        /// <returns>A new <see cref="CommitType"/> instance.</returns>
        internal static CommitType CreateOne()
        {
            return CreateMany(count: 1).Single();
        }

        /// <summary>
        /// Creates zero one or many well-formed <see cref="CommitType"/> instances.
        /// </summary>
        /// <param name="count">The total number of instances to create. Default is 10.</param>
        /// <returns>A collection of <see cref="CommitType"/> instances.</returns>
        internal static IReadOnlyList<CommitType> CreateMany(int count = 10)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("must be a positive integer", nameof(count));
            }

            return Faker.Generate(count);
        }
    }
}