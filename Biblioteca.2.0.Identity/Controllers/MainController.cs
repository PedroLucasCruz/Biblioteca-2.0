using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Biblioteca._2._0.Identity.Controllers
{
    [ApiController]
    //Deve ser herdad então será uma classe abstrada 
    public abstract class MainController : Controller
    {
        //Só quem herdar vai ter acesso a coleção de erros
        protected ICollection<string> Erros = new List<string>();

        //Este metodo CustomResponse vai tomar uma decisão com base em uma operação, tudo vai depender se ocorreu um erro ou não
        protected ActionResult CustomResponse(object result = null)
        {
            //Se a operação for válida devolver Ok 
            if (OperacaoValida())
            {
                return Ok(result);
            }

            //Validation Problem Details contem os details, detalhes dos problema encontrados
            //Essa classe implementa um padrão especificado em um RFC, essa RFC diz como uma api deve responder sobre detalhes de erros
            //Então quando você implementar o Validation Problem Details você aplica a prática recomendada na hora de trabalhar com Api 
            return BadRequest(new ValidationProblemDetails(
                new Dictionary<string, string[]> //O Dictionary é o modelo que o objeto a ser montado vai seguir 
            {
                {"Mensagens", Erros.ToArray() } //Todos os erros que forem passados no response do BadRequest será um detalhado devido a lista de erros ser passada neste parametro
            }));
        }

        //Uma sobre carga de um metodo já existente CustomResponse que recebe o ModelStateDictionary
        //Conforme encontrar erros na view Model vai percorrer e adicionar na coleção 
        //Retorno o CustomResponse com parametro opcional no final que recebe o object 
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
           var erros = modelState.Values.SelectMany(e => e.Errors);

            //erros.Select(x =>
            //{              
            // AdicionarErroProcessamento(x.ErrorMessage);
            // return x.ErrorMessage;
            //});

            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }


        protected bool OperacaoValida()
        {
            //Se existir algum erro dentro desta coleção quer dizer que a operação não aconteceu com sucesso por que houve um erro            
            return !Erros.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }

    }
}
