using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BuildingBlocks.CQRS
{
    public interface ICommand : ICommand<Unit>
    {
    }
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
