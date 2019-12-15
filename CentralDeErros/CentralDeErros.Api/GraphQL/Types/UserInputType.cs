﻿using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class UserInputType : InputObjectGraphType
    {
        public UserInputType()
        {
            Name = "userInput";
            Field<IdGraphType>("id");
            Field<StringGraphType>("login");
            Field<StringGraphType>("name");
            Field<StringGraphType>("email");
            Field<StringGraphType>("password");
            Field<StringGraphType>("role");
            Field<BooleanGraphType>("active");
        }
    }
}
