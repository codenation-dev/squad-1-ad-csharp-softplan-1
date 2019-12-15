using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class EnvironmentType : EnumerationGraphType
    {
        public EnvironmentType()
        {
            Name = "Environment";
            Description = "Ambiente em que a aplicação que gerou o erro está sendo executada.";
            AddValue("Unknown", "Unknown", 0);
            AddValue("Development", "Development", 1);
            AddValue("Test", "Test", 2);
            AddValue("Acceptance", "Acceptance", 3);
            AddValue("Production", "Production", 4);
        }
    }
}
