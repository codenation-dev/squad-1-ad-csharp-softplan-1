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
}
