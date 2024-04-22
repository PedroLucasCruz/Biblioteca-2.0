
namespace Biblioteca._2._0.Identity.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; } //Chave
        public int ExpiracaoHoras { get; set; } //quanto tempo vai ser valido em expiração por horas
        public string Emissor { get; set; } //Quem é o emissor 
        public string ValidoEm { get; set; } //Onde ele é válido que seria a audiencia 
    }
}
