using RpaAeC.Extensions;
using RpaAeC.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    public static async Task Main(string[] args)
    {
        var builder = new HostBuilder()
            .ConfigureServices(services => services.ConfigureServices());

        var host = builder.Build();

        try
        {
            var searchService = host.Services.GetRequiredService<ISearchSearchTrainingService>();
            ArgumentNullException.ThrowIfNull(searchService);

            Console.WriteLine("Iniciando o Processo");

            await searchService.OpenBrowser("https://www.alura.com.br");
            Thread.Sleep(1000);

            string query = "Python";

            Console.WriteLine($"Termo de pesquisa para o teste: {query}");

            var result = await searchService.SearchTrainingchAsync(query);

            if (result.IsFailure)
                throw new Exception(result.ErrorMessage);

            Console.WriteLine("Processo de RPA concluído com sucesso.");

            if (result.Value is not null)
            {                
                Console.WriteLine("Abaixo estão os registros encontrados:\n");
                foreach (var item in result.Value)
                {
                    Console.WriteLine($"Título: {item.Titulo}");
                    Console.WriteLine($"Professor: {item.Professor}");
                    Console.WriteLine($"Carga Horária: {item.CargaHoraria}");
                    Console.WriteLine($"Descrição: {item.Descricao}");
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Um erro ocorreu durante o processo: {ex.Message}");
        }
    }
}