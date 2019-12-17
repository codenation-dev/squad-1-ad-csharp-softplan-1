using CentralDeErros.Application.ViewModel;
using GraphQL.Types;
using System.Diagnostics.CodeAnalysis;

namespace CentralDeErros.Api.GraphQL.Types
{
    [ExcludeFromCodeCoverage]
    public class ErrorLogType : ObjectGraphType<ErrorLogViewModel>
    {
        public ErrorLogType()
        {
            Description = "Objeto que representa um log de erro.";
            Field(e => e.Id, type:typeof(IdGraphType)).Description("Identificação do erro. Gerado automático.");
            Field(e => e.Code).Description("Código de erro.");
            Field(e => e.Message).Description("Mensagem do erro.");
            Field(e => e.Level).Description("Level do erro.");
            Field(e => e.Archieved).Description("Indica se o log de erro está arquivado.");
            Field<EnvironmentType>("Environment", resolve: e => e.Source.Environment);
        }
    }
}
