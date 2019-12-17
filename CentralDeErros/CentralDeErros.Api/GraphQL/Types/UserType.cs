using CentralDeErros.Application.ViewModel;
using GraphQL.Types;
using System.Diagnostics.CodeAnalysis;

namespace CentralDeErros.Api.GraphQL.Types
{
    [ExcludeFromCodeCoverage]
    public class UserType : ObjectGraphType<UserViewModel>
    {
        public UserType()
        {
            Description = "Objeto que representa o usuário que terá acesso aos logs de erro.";
            Field(p => p.Id, type: typeof(IdGraphType)).Description("Identificação do usuário. Gerado automático.");
            Field(p => p.Login).Description("Campo utilizado para realizar login.");
            Field(p => p.Name).Description("Nome do usuário.");
            Field(p => p.Email).Description("E-mail do usuário.");
            Field(p => p.Active).Description("Informa se o usuário está ativo.");
            Field(p => p.Role).Description("Informa a Role do usuário.");
        }
    }
}
