// <copyright file="CommitDescription.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Commits
{
    /// <summary>
    /// Represents the description of a commit message.
    /// </summary>
    internal class CommitDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommitDescription"/> class.
        /// </summary>
        /// <param name="value">The value to use as a description.</param>
        public CommitDescription(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the description's value.
        /// </summary>
        public string Value { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Value;
        }
    }
}