using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Core.Service
{
    public interface IQueryBase
    {

    }

    /// <summary>
    /// Defines contract of the parameter less query.
    /// </summary>
    public interface IQuery : IQueryBase
    {
        /// <summary>
        /// Executes this query.
        /// </summary>
        void Execute();
    }

    /// <summary>
    /// Defines contract of the query with input and no output. 
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    public interface IQueryIn<in TRequest> : IQueryBase
    {
        /// <summary>
        /// Executes this query.
        /// </summary>
        /// <param name="request">The request.</param>
        void Execute(TRequest request);
    }
    public interface IQueryOut<out TResponse> : IQueryBase
    {
        TResponse Execute();
    }

    /// <summary>
    /// Defines contract of the query with input and output.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IQuery<out TResponse, in TRequest> : IQueryBase
    {
        /// <summary>
        /// Executes this query.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Executed query result.</returns>
        TResponse Execute(TRequest request);
    }
}
