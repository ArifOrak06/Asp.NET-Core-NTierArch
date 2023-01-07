using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NTierApp.Core.Entities;
using NTierApp.Core.ResultPattern;
using NTierApp.Core.Services;

namespace NTierApp.WebAPI.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {

        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // ilgili controller'ın methoduna gitmeden burda bir kontrol yapalım.
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }
            // eğer id değeri geldiyse ve db'de varsa yoluna next() ile devam...
            var id = (int)idValue;
            var existEntity = await _service.AnyAsync(x => x.Id == id);

            if (existEntity)
            {
                await next.Invoke();
                return;
            }
            // eğerki db'de yoksa;
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) bulunamamıştır."));

        }
    }
}
