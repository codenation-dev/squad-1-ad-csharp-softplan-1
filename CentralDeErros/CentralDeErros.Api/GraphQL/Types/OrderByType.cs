using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class OrderByType : EnumerationGraphType
    {
        public OrderByType()
        {
            Name = "orderBy";
            Description = "Order by one of these fields.";
            AddValue("Any", "Any", 0);
            AddValue("Code", "Code", 1);
            AddValue("Message", "Message", 2);
            AddValue("Level", "Level", 2);
        }
    }
}
