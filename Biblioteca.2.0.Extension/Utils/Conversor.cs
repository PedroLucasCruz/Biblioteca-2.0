using AutoMapper;

namespace Biblioteca._2._0.Application.Util
{
    public class Conversor<TOrigem, TDestino>
    {

        private static MapperConfiguration _Config;

        private static IMapper _Mapper;

        public Conversor()
        {
            _Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOrigem, TDestino>();
                cfg.AllowNullCollections = true;
            });

            _Mapper = _Config.CreateMapper();
        }


        /// <summary>
        /// Converte um Objeto em DTO ou DTO em Objeto, basta inverter a ordem.
        /// </summary>
        /// <param name="obj">O Objeto que será convertido para o Tipo informado na Origem</param>
        /// <returns>Retorna um objeto carregado em conformidade com o Tipo Informado</returns>
        public TDestino ConvertaPara(TOrigem obj)
        {


            _Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOrigem, TDestino>();
                cfg.AllowNullCollections = true;
            });


            try
            {

                _Mapper = _Config.CreateMapper();

            }
            catch (Exception)
            {

            }


            return _Mapper.Map<TDestino>(obj);


        }

        public TOrigem ConvertaParaTipo<TOrigem>(TDestino obj) where TOrigem : class
        {
            try
            {
                // Reutilize o _mapper já inicializado
                return _Mapper.Map<TOrigem>(obj);
            }
            catch (Exception ex)
            {
                // Registre ou relato a exceção para depuração
                Console.WriteLine($"Erro ao converter objeto: {ex.Message}");
                // Rejogue a exceção ou retorne um valor padrão, dependendo dos requisitos
                throw;
            }
        }
    }
}
