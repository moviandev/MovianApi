using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Movian.Api.Models;

namespace Movian.Api.Extensions
{
  public class ProductBinder : IModelBinder
  {
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
      if (bindingContext == null)
      {
        throw new ArgumentNullException(nameof(bindingContext));
      }

      var serializeOptions = new JsonSerializerOptions
      {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNameCaseInsensitive = true
      };

      var productDto = JsonSerializer.Deserialize<ProductImageDto>(bindingContext.ValueProvider.GetValue("product").FirstOrDefault(), serializeOptions);
      productDto.ImageUpload = bindingContext.ActionContext.HttpContext.Request.Form.Files.FirstOrDefault();

      bindingContext.Result = ModelBindingResult.Success(productDto);

      return Task.CompletedTask;
    }
  }
}