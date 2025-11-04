using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Commands;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentValidation.Results;
using MediatR;
using RoomAggeregate = Epam.ItMarathon.ApiService.Domain.Aggregate.Room.Room;

namespace Epam.ItMarathon.ApiService.Application.UseCases.User.Handlers
{ 
    public class DeleteUserHandler(IRoomRepository roomRepository) :
        IRequestHandler<DeleteUserRequest, Result<RoomAggeregate, ValidationResult>>
    {
        ///<inheritdoc/>
        public async Task<Result<RoomAggeregate, ValidationResult>> Handle(DeleteUserRequest request,
            CancellationToken cancellationToken)
        {

            var roomResult = await roomRepository.GetByUserCodeAsync(request.UserCode, cancellationToken);
            if (roomResult.IsFailure)
            {
                return roomResult.ConvertFailure<RoomAggeregate>();
            }

            var room = roomResult.Value;

            var result = room.DeleteUser(request.UserCode, request.UserId);
            if (result.IsFailure)
            {
                return result.ConvertFailure<RoomAggeregate>();
            }

            var updateResult = await roomRepository.UpdateAsync(room, cancellationToken);
            if (updateResult.IsFailure)
            {
                return Result.Failure<RoomAggeregate, ValidationResult>(new NotFoundError([
                    new ValidationFailure("RoomUpdate", updateResult.Error)
                ]));
            }

            var roomUpdatingResult = await roomRepository.GetByUserCodeAsync(request.UserCode, cancellationToken);
            return roomUpdatingResult;
        }
    }
}