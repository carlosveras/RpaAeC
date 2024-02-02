using RpaAeC.Domain.Entities;

namespace RpaAeC.Data.Repositories
{
    public interface ISearchResultRepository
    {
        Task AddResultsBulkAsync(List<SearchTraining> results);
    }
}
