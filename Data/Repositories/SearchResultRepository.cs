using RpaAeC.Data;
using RpaAeC.Domain.Entities;

namespace RpaAeC.Data.Repositories
{
    public class SearchResultRepository : ISearchResultRepository
    {
        private readonly AppDbContext _context;

        public SearchResultRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddResultsBulkAsync(List<SearchTraining> results)
        {
            _context.Training.AddRange(results);
            await _context.SaveChangesAsync();
        }
    }
}
