using Microsoft.EntityFrameworkCore;

namespace RiraDemo.Services
{
    public class DbService : DbContext
    {
        public DbSet<Model> Models { get; set; }

        public DbService(DbContextOptions options) : base(options)
        {

        }

        public Task<List<Model>> GetAsync(PagingModel pagingModel, CancellationToken cancellationToken)
        {
            return Models.Skip((pagingModel.PageNumber - 1) * pagingModel.PageSize)
                         .Take(pagingModel.PageSize)
                         .ToListAsync(cancellationToken);
        }

        public async Task<Model> CreateAsync(Model model, CancellationToken cancellationToken)
        {
            await Models.AddAsync(model, cancellationToken);

            await SaveChangesAsync(cancellationToken);

            return model;
        }

        public async Task<Model> EditAsync(Model model, CancellationToken cancellationToken)
        {
            var m = await Models.FirstAsync(m => m.Id == model.Id, cancellationToken);

            m.BirthDate = model.BirthDate;
            m.FirstName = model.FirstName;
            m.LastName = model.LastName;
            m.NationalCode = model.NationalCode;

            await SaveChangesAsync(cancellationToken);

            return m;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var m = await Models.FirstAsync(m => m.Id == id, cancellationToken);

            Models.Remove(m);

            await SaveChangesAsync(cancellationToken);
        }
    }
}
