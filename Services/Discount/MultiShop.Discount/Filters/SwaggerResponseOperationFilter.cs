using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MultiShop.Discount.Filters
{
    public class SwaggerResponseOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Responses.TryAdd("400", new OpenApiResponse
            {
                Description = "Geçersiz istek. Model doğrulama hatası oluştu."
            });

            operation.Responses.TryAdd("401", new OpenApiResponse
            {
                Description = "Yetkisiz erişim. Lütfen giriş yapınız."
            });

            operation.Responses.TryAdd("403", new OpenApiResponse
            {
                Description = "Bu işlem için yetkiniz bulunmamaktadır."
            });

            operation.Responses.TryAdd("404", new OpenApiResponse
            {
                Description = "İstenen kaynak bulunamadı."
            });

            operation.Responses.TryAdd("409", new OpenApiResponse
            {
                Description = "Çakışma hatası. Aynı kayıt zaten mevcut."
            });

            operation.Responses.TryAdd("422", new OpenApiResponse
            {
                Description = "İş kuralı ihlali. Gönderilen veri işlenemedi."
            });

            operation.Responses.TryAdd("500", new OpenApiResponse
            {
                Description = "Sunucu hatası. Lütfen daha sonra tekrar deneyiniz."
            });
        }
    }
}
