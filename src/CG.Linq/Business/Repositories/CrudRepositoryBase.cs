﻿using CG.Business.Models;
using CG.Business.Repositories.Options;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Business.Repositories
{
    /// <summary>
    /// This class is a base implementation of the <see cref="ICrudRepository{TModel, TKey}"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The options type associated with the repository.</typeparam>
    /// <typeparam name="TModel">The model type associated with the repository.</typeparam>
    /// <typeparam name="TKey">The key type associated with the model.</typeparam>
    public abstract class CrudRepositoryBase<TOptions, TModel, TKey> :
        LinqRepositoryBase<TOptions, TModel, TKey>,
        ICrudRepository<TModel, TKey>
        where TModel : class, IModel<TKey>
        where TOptions : IOptions<RepositoryOptions>
        where TKey : new()
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="CrudRepositoryBase{TOptions, TModel, TKey}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the repository.</param>
        protected CrudRepositoryBase(
            TOptions options
            ) : base(options)
        {

        }

        #endregion 

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method adds a new <typeparamref name="TModel"/> to the 
        /// repository.
        /// </summary>
        /// <param name="model">The model to use for the operation. </param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns the recently
        /// added <typeparamref name="TModel"/> object.</returns>
        public abstract Task<TModel> AddAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method updates a <typeparamref name="TModel"/> in the 
        /// repository.
        /// </summary>
        /// <param name="model">The model to use for the operation. </param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns the recently
        /// updated <typeparamref name="TModel"/> object.</returns>
        public abstract Task<TModel> UpdateAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method deletes a <typeparamref name="TModel"/> from the 
        /// repository.
        /// </summary>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation.</returns>
        public abstract Task DeleteAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        #endregion
    }



    /// <summary>
    /// This class is a base implementation of the <see cref="ICrudRepository{TModel, TKey1, TKey2}"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The options type associated with the repository.</typeparam>
    /// <typeparam name="TModel">The model type associated with the repository.</typeparam>
    /// <typeparam name="TKey1">The key 1 type associated with the model.</typeparam>
    /// <typeparam name="TKey2">The key 2 type associated with the model.</typeparam>
    public abstract class CrudRepositoryBase<TOptions, TModel, TKey1, TKey2> :
        LinqRepositoryBase<TOptions, TModel, TKey1, TKey2>,
        ICrudRepository<TModel, TKey1, TKey2>
        where TModel : class, IModel<TKey1, TKey2>
        where TOptions : IOptions<RepositoryOptions>
        where TKey1 : new()
        where TKey2 : new()
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="CrudRepositoryBase{TOptions, TModel, TKey1, TKey2}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the repository.</param>
        protected CrudRepositoryBase(
            TOptions options
            ) : base(options)
        {

        }

        #endregion 

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method adds a new <typeparamref name="TModel"/> to the 
        /// repository.
        /// </summary>
        /// <param name="model">The model to use for the operation. </param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns the recently
        /// added <typeparamref name="TModel"/> object.</returns>
        public abstract Task<TModel> AddAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method updates a <typeparamref name="TModel"/> in the 
        /// repository.
        /// </summary>
        /// <param name="model">The model to use for the operation. </param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns the recently
        /// updated <typeparamref name="TModel"/> object.</returns>
        public abstract Task<TModel> UpdateAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method deletes a <typeparamref name="TModel"/> from the 
        /// repository.
        /// </summary>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation.</returns>
        public abstract Task DeleteAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        #endregion
    }




    /// <summary>
    /// This class is a base implementation of the <see cref="ICrudRepository{TModel, TKey1, TKey2, TKey3}"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The options type associated with the repository.</typeparam>
    /// <typeparam name="TModel">The model type associated with the repository.</typeparam>
    /// <typeparam name="TKey1">The key 1 type associated with the model.</typeparam>
    /// <typeparam name="TKey2">The key 2 type associated with the model.</typeparam>
    /// <typeparam name="TKey3">The key 3 type associated with the model.</typeparam>
    public abstract class CrudRepositoryBase<TOptions, TModel, TKey1, TKey2, TKey3> :
        LinqRepositoryBase<TOptions, TModel, TKey1, TKey2, TKey3>,
        ICrudRepository<TModel, TKey1, TKey2, TKey3>
        where TModel : class, IModel<TKey1, TKey2, TKey3>
        where TOptions : IOptions<RepositoryOptions>
        where TKey1 : new()
        where TKey2 : new()
        where TKey3 : new()
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="CrudRepositoryBase{TOptions, TModel, TKey1, TKey2, TKey3}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the repository.</param>
        protected CrudRepositoryBase(
            TOptions options
            ) : base(options)
        {

        }

        #endregion 

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method adds a new <typeparamref name="TModel"/> to the 
        /// repository.
        /// </summary>
        /// <param name="model">The model to use for the operation. </param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns the recently
        /// added <typeparamref name="TModel"/> object.</returns>
        public abstract Task<TModel> AddAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method updates a <typeparamref name="TModel"/> in the 
        /// repository.
        /// </summary>
        /// <param name="model">The model to use for the operation. </param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns the recently
        /// updated <typeparamref name="TModel"/> object.</returns>
        public abstract Task<TModel> UpdateAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method deletes a <typeparamref name="TModel"/> from the 
        /// repository.
        /// </summary>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation.</returns>
        public abstract Task DeleteAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        #endregion
    }
}
