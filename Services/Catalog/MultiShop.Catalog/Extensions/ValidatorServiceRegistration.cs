using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MultiShop.Catalog.Extensions
{
    public static class ValidatorServiceRegistration
    {
        public static IServiceCollection AddCatalogValidators(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var validators = assembly.GetTypes()
                .Where(t =>
                    !t.IsAbstract &&
                    !t.IsInterface &&
                    t.GetInterfaces().Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IValidator<>)));

            foreach (var validator in validators)
            {
                var interfaceType = validator.GetInterfaces()
                    .First(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IValidator<>));

                services.AddScoped(interfaceType, validator);
            }

            return services;
        }
    }
}
