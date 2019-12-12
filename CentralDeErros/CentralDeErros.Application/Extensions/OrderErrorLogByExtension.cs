using CentralDeErros.Application.ViewModel;
using CentralDeErros.CrossCutting.CustomTypes;
using System.Collections.Generic;
using System.Linq;

namespace CentralDeErros.Application.Extensions
{
    public static class OrderErrorLogByExtension
    {
        public static IList<ErrorLogViewModel> OrderErrorLogBy(this IEnumerable<ErrorLogViewModel> source,
            OrderErrorLogByField? orderBy)
        {
            switch (orderBy)
            {
                case OrderErrorLogByField.Code:
                    return source.OrderBy(p => p.Code).ToList();
                case OrderErrorLogByField.Level:
                    return source.OrderBy(p => p.Level).ToList();
                case OrderErrorLogByField.Message:
                    return source.OrderBy(p => p.Message).ToList();
                default:
                    return source.ToList();
            }

        }
    }
}
