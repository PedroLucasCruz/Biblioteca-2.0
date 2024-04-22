
namespace Biblioteca._2._0.Metadados.VersaoApi
{
    public class VersaoApi : Attribute
    {
        public string VersaoDaApi { get; set; }

        public VersaoApi()
        {

        }

        public VersaoApi(string versaoDaApi)
        {
            VersaoDaApi = versaoDaApi;
        }
    }
}
