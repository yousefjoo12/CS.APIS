using Core.Repositories.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Project.APIS.Extensions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        { 

            services.AddScoped(typeof(IGenericRepositories<>), typeof(GenericRepositories<>)); 
            
            services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                          .SelectMany(p => p.Value.Errors)
                                                          .Select(E => E.ErrorMessage)
                                                          .ToList();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });


            return services;
        }
    }

    //public static WebApplication UseSwaggerMiddleWare(this WebApplication app)
    //{ 
    //    app.UseSwagger();
    //    app.UseSwaggerUI();
    //    return app;

    //}

}
