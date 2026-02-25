using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Auth.Commands.RegisterStudent
{
    public class RegisterStudentErrors
    {
        public static readonly Error EmailAlreadyExists = Error.Conflict(
       code: "Auth.EmailExists",
       description: "Email is already registered.");

        public static readonly Error RegisterFailed = Error.Failure(
            code: "Auth.RegisterFailed",
            description: "Failed to register the student. Please try again.");
    }
}
