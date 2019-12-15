using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class OrderByType : EnumerationGraphType
    {
        public OrderByType()
        {
            Name = "orderBy";
            Description = "Ordem em que a consulta de logs de erro pode ser apresentada.";
            AddValue("Any", "Any", 0);
            AddValue("Code", "Code", 1);
            AddValue("Message", "Message", 2);
            AddValue("Level", "Level", 3);
        }
    }
}
