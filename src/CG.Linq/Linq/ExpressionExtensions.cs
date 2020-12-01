using CG.Validations;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CG.Linq
{
    /// <summary>
    /// This class provides extension methods related to the <see cref="Expression"/> 
    /// type.
    /// </summary>
    public static partial class ExpressionExtensions
    {
        // *******************************************************************
        // Types.
        // *******************************************************************

        #region Types

        /// <summary>
        /// This class is a custom expression visitor for deconstructing LINQ
        /// expressions at runtime. 
        /// </summary>
        class _ExpressionVisitor<T> : ExpressionVisitor
        {
            // *******************************************************************
            // Properties.
            // *******************************************************************

            #region Properties

            /// <summary>
            /// This field contains the expression parameter.
            /// </summary>
            private readonly ParameterExpression _parameter;

            #endregion

            // *******************************************************************
            // Constructors.
            // *******************************************************************

            #region Constructors

            /// <summary>
            /// This constructor creates a new instance of the <see cref="_ExpressionVisitor{T}"/>
            /// class.
            /// </summary>
            /// <param name="parameter">The parameter expression to use for the operation./</param>
            public _ExpressionVisitor(
                ParameterExpression parameter
                )
            {
                // Validate the parameters before attempting to use them.
                Guard.Instance().ThrowIfNull(parameter, nameof(parameter));

                // Save the reference.
                _parameter = parameter;
            }

            #endregion

            // *******************************************************************
            // Protected methods.
            // *******************************************************************

            #region Protected methods

            /// <summary>
            /// This method is called whenever the visitor visits a parameter expression.
            /// </summary>
            /// <param name="node">The expression to visit.</param>
            /// <returns>The modified expression.</returns>
            protected override Expression VisitParameter(ParameterExpression node)
            {
                // Return our parameter instead.
                return _parameter;
            }

            // *******************************************************************

            /// <summary>
            /// This method is called whenever the vistor visits a member expression.
            /// </summary>
            /// <param name="node">The expression to visit.</param>
            /// <returns>The modified expression.</returns>
            protected override Expression VisitMember(
                MemberExpression node
                )
            {
                // Are we looking at a property access expression?
                if (node.Member.MemberType == System.Reflection.MemberTypes.Property)
                {
                    // Get the original property name.
                    var otherMember = typeof(T).GetProperty(node.Member.Name);

                    // Make an expression substituting our parameter type.
                    var memberExpression = Expression.Property(Visit(node.Expression), otherMember);

                    // Return the results.
                    return memberExpression;
                }
                else
                {
                    // Give the base class a chance.
                    return base.VisitMember(node);
                }
            }

            #endregion
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method rewrites the specified LINQ expression to an identical 
        /// one using another type. The expression itself doesn't change, just 
        /// the referenced type. 
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDest">The destination type.</typeparam>
        /// <param name="source">The LINQ expression to be rewritten.</param>
        /// <returns>The rewritten LINQ expression.</returns>
        public static Expression<Func<TDest, bool>> RewriteType<TSource, TDest>(
            this Expression<Func<TSource, bool>> source
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(source, nameof(source));

            // Create an expression parameter for the entity type.
            var param = Expression.Parameter(typeof(TDest));

            // Deconstruct the LINQ expression.
            var result = new _ExpressionVisitor<TDest>(
                param
                ).Visit(source.Body);

            // Rewrite the expression to use the new type.
            var dest = Expression.Lambda<Func<TDest, bool>>(
                result,
                param
                );

            // Return the new expression.
            return dest;
        }

        // *******************************************************************

        /// <summary>
        /// This method combines two expressions using a logical AND.
        /// </summary>
        /// <typeparam name="T">The type associated with the expressions.</typeparam>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>An LINQ expression.</returns>
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right
            )
        {
            return BinaryOnExpressions(left, ExpressionType.AndAlso, right);
        }

        // *******************************************************************

        /// <summary>
        /// This method combines two expressions using a logical OR.
        /// </summary>
        /// <typeparam name="T">The type associated with the expressions.</typeparam>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>An LINQ expression.</returns>
        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right
            )
        {
            return BinaryOnExpressions(left, ExpressionType.OrElse, right);
        }

        // *******************************************************************

        /// <summary>
        /// This method converts the left and right hand expressions into a 
        /// binary expression.
        /// </summary>
        /// <typeparam name="T">The type associated with the expressions.</typeparam>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="binaryType">The type of binary operator to apply.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>An LINQ expression.</returns>
        public static Expression<Func<T, bool>> BinaryOnExpressions<T>(
            this Expression<Func<T, bool>> left,
            ExpressionType binaryType,
            Expression<Func<T, bool>> right
            )
        {
            // Invoke that lambda with my parameter and give me the bool back, KKTHX
            var rightInvoke = Expression.Invoke(right, left.Parameters.Cast<Expression>());

            // Make a binary expression between the results (i.e. AndAlso(&&), OrElse(||), etc)
            var binExpression = Expression.MakeBinary(binaryType, left.Body, rightInvoke);

            // Wrap it in a lambda and send it back
            return Expression.Lambda<Func<T, bool>>(binExpression, left.Parameters);
        }

        #endregion
    }

}
