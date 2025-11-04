using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using FluentValidation.Results;
using MediatR;
using RoomAggeregate = Epam.ItMarathon.ApiService.Domain.Aggregate.Room.Room;

using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;

namespace Epam.ItMarathon.ApiService.Application.UseCases.User.Commands
{
    public record DeleteUserRequest(string UserCode, ulong? UserId)
        : IRequest<Result<RoomAggeregate, ValidationResult>>;
}