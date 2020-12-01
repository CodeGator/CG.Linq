using CG.Validations;
using System;
using System.Linq.Expressions;

namespace CG.Linq
{
    /// <summary>
    /// This class is a generic database query criteria.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity associated with the 
    /// criteria.</typeparam>
    public abstract class Criteria<TEntity>
        where TEntity : class
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        private Expression<Func<TEntity, bool>> curExpression;

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method returns the specified Func as a LINQ expression.
        /// </summary>
        /// <returns>An expression.</returns>
        public Expression<Func<TEntity, bool>> AsExpression()
        {
            return curExpression;
        }

        // *******************************************************************

        /// <summary>
        /// This method will chain criteria together using a logical AND.
        /// </summary>
        /// <param name="otherCriteria">The criteria to chain to this one.</param>
        /// <returns>The resulting expression.</returns>
        public Expression<Func<TEntity, bool>> And(
            Criteria<TEntity> otherCriteria
            )
        {
            // Validate the parameter before attempting to use it.
            Guard.Instance().ThrowIfNull(otherCriteria, "otherCriteria");

            // Chain the expressions.
            return AsExpression()
                .AndAlso(otherCriteria.AsExpression());
        }

        // *******************************************************************

        /// <summary>
        /// This method will chain criteria together using a logical OR.
        /// </summary>
        /// <param name="otherCriteria">The criteria to chain to this one.</param>
        /// <returns>The resulting expression.</returns>
        public Expression<Func<TEntity, bool>> Or(
            Criteria<TEntity> otherCriteria
            )
        {
            // Validate the parameter before attempting to use it.
            Guard.Instance().ThrowIfNull(otherCriteria, "otherCriteria");

            // Chain the expressions.
            return AsExpression()
                .OrElse(otherCriteria.AsExpression());
        }

        #endregion

        // *******************************************************************
        // Protected methods.
        // *******************************************************************

        #region Protected methods

        /// <summary>
        /// This method will add an expression to the current criteria.
        /// </summary>
        /// <param name="nextExpression">The expression to add.</param>
        protected void AddCriteria(
            Expression<Func<TEntity, bool>> nextExpression
            )
        {
            curExpression = (curExpression == null)
                                ? nextExpression
                                : curExpression.AndAlso(nextExpression);
        }

        #endregion
    }

}
