// <copyright file="ConventCommitsCustomization.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Commits.UnitTests
{
    using AutoFixture;
    using Convent.Commits;
    using Convent.Commits.UnitTests.Fakers;

    /// <summary>
    /// Provides GitSharp specific customizations to a <see cref="IFixture"/>.
    /// </summary>
    public class ConventCommitsCustomization : ICustomization
    {
        /// <inheritdoc/>
        public void Customize(IFixture fixture)
        {
            fixture.Register<CommitType>(() => CommitTypeFaker.CreateOne());
        }
    }
}