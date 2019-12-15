using CentralDeErros.Application.ViewModel;
using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL.Types
{
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
        }
    }
}
