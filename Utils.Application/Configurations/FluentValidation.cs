using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Utils.Application.Validators;

namespace Utils.Application.Configurations
{
    public static class FluentValidation
    {
        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<PaginationRequestDtoValidator>();
        }
    }
}
