// <copyright file="IFactory.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Commits
{
    /// <summary>
    /// Represents a factory for creating <typeparamref name="T"/> instances.
    /// </summary>
    /// <typeparam name="T">The type of instances this factory will create.</typeparam>
    internal interface IFactory<T>
    {
        /// <summary>
        /// Creates a single <typeparamref name="T"/> instance.
        /// </summary>
        /// <returns>A new <typeparamref name="T"/> instance.</returns>
        T Create();
    }
}