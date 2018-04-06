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

    public interface ICommand : ICommandBase
    {
        OperationResult Execute();
    }

    public interface ICommandIn<in TRequest> : ICommandBase
    {
        OperationResult Execute(TRequest request);
    }

    public interface ICommandOut<out TResponse> : ICommandBase where TResponse : OperationResult
    {
        TResponse Execute();
    }

    public interface ICommand<out TResponse, in TRequest> : ICommandBase where TResponse : OperationResult
    {
        TResponse Execute(TRequest request);
    }
}
