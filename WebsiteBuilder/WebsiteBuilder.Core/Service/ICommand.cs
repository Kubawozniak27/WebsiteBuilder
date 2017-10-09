using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Core.Service
{
    public interface ICommandBase
    {

    }

    /// <summary>
    /// Defines contract of the parameter less command.
    /// </summary>
    public interface ICommand : ICommandBase
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        OperationResult Execute();
    }

    /// <summary>
    /// Defines contract of the command with input and no output. 
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    public interface ICommandIn<in TRequest> : ICommandBase
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="newsletterIds">The request.</param>
        /// <param name="request">request</param>
        OperationResult Execute(TRequest request);
    }

    public interface ICommandOut<out TResponse> : ICommandBase where TResponse : OperationResult
    {
        TResponse Execute();
    }

    /// <summary>
    /// Defines contract of the command with input and output.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface ICommand<out TResponse, in TRequest> : ICommandBase where TResponse : OperationResult
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Executed command result.</returns>
        TResponse Execute(TRequest request);
    }
}
