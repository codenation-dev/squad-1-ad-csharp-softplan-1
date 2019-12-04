using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class OrderType : EnumerationGraphType
    {
        public OrderType()
        {
            Name = "Order";
            Description = "Field to order the data.";
            AddValue("Any", "Any", 0);
            AddValue("Level", "Level", 1);
            AddValue("Frequencia", "Frequência", 2);
        }
    }
}
