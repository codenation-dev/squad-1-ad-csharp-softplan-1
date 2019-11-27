using CentralDeErros.Application.ViewModel;
using CentralDeErros.CrossCutting.CustomTypes;
using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class EnvironmentGraphQLType : EnumerationGraphType
    {
        public EnvironmentGraphQLType()
        {
            Name = "Environment";
            Description = "Environment where the application is running.";
            AddValue("Unknown", "Unknown", 0);
            AddValue("Development", "Development", 1);
            AddValue("Test", "Test", 2);
            AddValue("Acceptance", "Acceptance", 3);
            AddValue("Production", "Production", 4);
        }
    }

    public class ErrorLogType : ObjectGraphType<ErrorLogViewModel>
    {
        public ErrorLogType()
        {
            Field(e => e.Id, type:typeof(IdGraphType));
            Field(e => e.Code);
            Field(e => e.Message);
            Field(e => e.Level);
            Field(e => e.Archieved);
            Field<EnvironmentGraphQLType>("Environment", resolve: e => e.Source.Environment);
        }
    }
}
