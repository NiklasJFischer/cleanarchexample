using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Abstractions.Repositories;
using ChatAPI.Application.Commands.Core;

namespace ChatAPI.Application.Decorators
{
    public class UnitOfWorkDecorator<TCommand, TResult>(ICommandService<TCommand, TResult> decoratee, IUnitOfWork unitOfWork) : ICommandService<TCommand, TResult> where TCommand : Command
    {
        public CommandResult<TResult> Execute(TCommand command)
        {
            if (!command.IsWriteCommand)
            {
                return decoratee.Execute(command);
            }

            using var transaction = unitOfWork.BeginTransaction();
            var result = decoratee.Execute(command);
            if (result.StatusCode == CommandResultStatusCode.Success)
            {

                unitOfWork.SaveChanges();
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }

            return result;
        }
    }
}
