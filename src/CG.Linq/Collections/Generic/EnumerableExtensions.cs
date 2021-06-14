using CG.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CG.Collections.Generic
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IEnumerable{T}"/>
    /// type.
    /// </summary>
    public static partial class EnumerableExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method creates a smaller batch of items from a larger enumerable 
        /// sequence.
        /// </summary>
        /// <typeparam name="T">The type associated with the sequence.</typeparam>
        /// <param name="sequence">The sequence to use for the operation.</param>
        /// <param name="batchSize">The number of items to include in the batch.</param>
        /// <returns>An enumerable sequence of <typeparamref name="T"/> items.</returns>
        public static IEnumerable<IEnumerable<T>> Batch<T>(
            this IEnumerable<T> sequence,
            int batchSize
        )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(sequence, nameof(sequence))
                .ThrowIfLessThanZero(batchSize, nameof(batchSize));

            // Loop and create the batch.
            for (int x = 0; x < sequence.Count(); x += batchSize)
            {
                // Return a smaller chunk.
                yield return sequence.Skip(x)
                    .Take(batchSize);
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method recursively projects each element of a sequence into an 
        /// <see cref="IEnumerable{T}"/> and flattens the resulting sequences 
        /// into a single <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type associated with the sequence.</typeparam>
        /// <param name="sequence">The sequence to use for the operation.</param>
        /// <param name="selector">The selector to apply for the operation.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> whose elements are the result 
        /// of recursively invoking the one-to-many transform function on each element 
        /// of the input sequence.</returns>
        public static IEnumerable<T> SelectManyR<T>(
            this IEnumerable<T> sequence,
            Func<T, IEnumerable<T>> selector
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(sequence, nameof(sequence))
                .ThrowIfNull(selector, nameof(selector));

            // Loop through each item in the sequence.
            foreach (T element in sequence)
            {
                yield return element;

                // Get any child elements
                var children = selector(element);

                // Loop through any child elements.
                foreach (T child in children.SelectManyR(selector))
                {
                    // Return the child.
                    yield return child;
                }
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method returns alternate elements of the specified enumerable 
        /// sequence.
        /// </summary>
        /// <typeparam name="T">The type of associated element.</typeparam>
        /// <param name="source">The enumerable sequence to use for the operation.</param>
        /// <returns>An enumerable sequence containing alternate elements of 
        /// the original sequence.</returns>
        public static IEnumerable<T> AlternateElements<T>(
            this IEnumerable<T> source
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(source, nameof(source));

            // Loop through the sequence.
            var i = 0;
            foreach (var element in source)
            {
                // Is this an alternate element?
                if (i % 2 == 0)
                {
                    // Return the element.
                    yield return element;
                }
                i++;
            }
        }

        #endregion
    }
}
