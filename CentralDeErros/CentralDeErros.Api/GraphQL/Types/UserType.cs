using CentralDeErros.Application.ViewModel;
using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class UserType : ObjectGraphType<UserViewModel>
    {
        public UserType()
        {
            Field(p => p.Id, type: typeof(IdGraphType));
            Field(p => p.Login);
            Field(p => p.Name);
            Field(p => p.Email);
            Field(p => p.Active);
        }
    }
}
