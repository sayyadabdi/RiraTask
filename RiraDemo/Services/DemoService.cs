using Grpc.Core;

namespace RiraDemo.Services
{
    public class DemoService : MyService.MyServiceBase
    {
        private readonly DbService _dbService;

        public DemoService(DbService dbService)
        {
            _dbService = dbService;
        }

        public override async Task<GetResultModel> Get(PagingModel request, ServerCallContext context)
        {
            var result = new GetResultModel();

            var models = await _dbService.GetAsync(request, default);

            result.Models.AddRange(models.Select(m => m.ToMyModel()));

            return result;
        }

        public override async Task<MyModel> Create(MyModel request, ServerCallContext context)
        {
            return (await _dbService.CreateAsync(new Model().FromMyModel(request), default)).ToMyModel();
        }

        public override async Task<MyModel> Edit(MyModel request, ServerCallContext context)
        {
            return (await _dbService.EditAsync(new Model().FromMyModel(request), default)).ToMyModel();
        }

        public override async Task<Empty> Delete(Int32Model request, ServerCallContext context)
        {
            await _dbService.DeleteAsync(request.Number, default);

            return new Empty();
        }
    }
}
