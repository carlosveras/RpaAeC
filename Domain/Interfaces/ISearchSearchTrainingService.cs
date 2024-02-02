using RpaAeC.Domain.Abstractions;
using RpaAeC.Domain.Entities;

namespace RpaAeC.Domain.Interfaces
{
    public interface ISearchSearchTrainingService
    {
        Task OpenBrowser(string url);
        Task<Training<List<SearchTraining>>> SearchTrainingchAsync(string query);
    }
}
