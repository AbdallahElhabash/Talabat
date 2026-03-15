using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Talabat_Project.Errors;
using Talabat_Project.Helper;

namespace Talabat_Project.Extenssions
{
    public static class ServicesExtenssion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
            Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
            Services.AddAutoMapper(typeof(MappingProfile));
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                       .SelectMany(p => p.Value.Errors)
                                                       .Select(p => p.ErrorMessage)
                                                       .ToArray();
                    var ApiValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ApiValidationErrorResponse);
                };
            });
              return Services;
        }
    }
}
