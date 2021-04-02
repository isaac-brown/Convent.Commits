// <copyright file="CommitScope.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Commits
{
    /// <summary>
    /// Represents the scope of a commit message.
    /// </summary>
    internal class CommitScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommitScope"/> class.
        /// </summary>
        /// <param name="value">The value to use as a scope.</param>
        internal CommitScope(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the scope's value.
        /// </summary>
        public string Value { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Value;
        }
    }
}