// <copyright file="CommitType.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Commits
{
    /// <summary>
    /// Represents a conventional commit type.
    /// </summary>
    /// <remarks>
    /// Can be extended to incorporate custom commit types as required.
    /// </remarks>
    public class CommitType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommitType"/> class.
        /// </summary>
        /// <param name="name">The name of the commit type.</param>
        protected CommitType(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets a commit type which represents adding a feature.
        /// </summary>
        public static CommitType Feature => new CommitType("feat");

        /// <summary>
        /// Gets a commit type which represents fixing a bug/issue.
        /// </summary>
        public static CommitType Fix => new CommitType("fix");

        /// <summary>
        /// Gets a commit type which represents performing a chore.
        /// </summary>
        public static CommitType Chore => new CommitType("chore");

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string Name { get; }

        /// <inheritdoc/>
        public sealed override string ToString()
        {
            return this.Name;
        }
    }
}