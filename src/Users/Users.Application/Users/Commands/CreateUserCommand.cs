﻿using CSharpFunctionalExtensions;
using MediatR;
using Users.Infrastructure.Data;

namespace Users.Application.Users.Commands;
public class CreateUserCommand : IRequest<Result>
{
    public User User { get; set; }

    public CreateUserCommand(User user)
    {
        User = user;
    }
}