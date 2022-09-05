using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movian.Api.Controllers;
using Movian.Api.Models;
using Movian.Business.Interfaces;
using Movian.Business.Models;

namespace Movian.Api.Versions.V1.Controllers
{
  [Route("api/[controller]")]
  public class ProductController : BaseController
  {
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductController(INotifier notifier,
                             IProductRepository productRepository,
                             IProductService productService,
                             IMapper mapper) : base(notifier)
    {
      _productRepository = productRepository;
      _productService = productService;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
      var products = _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAllAsync());
      return CustomResponse(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetById(Guid id)
    {
      var product = await GetProductAndSupplierById(id);
      return CustomResponse(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Add([FromBody] ProductDto product)
    {
      if (!ModelState.IsValid) CustomResponse(ModelState);

      var imgName = $"{Guid.NewGuid()}_{product.Name}";

      if (!UploadFile(product.ImageUpload, imgName))
        return CustomResponse();

      product.Image = imgName;

      await _productService.AddAsync(_mapper.Map<Product>(product));
      return CustomResponse(product);
    }

    [HttpPost("add-alternative")]
    public async Task<ActionResult<ProductDto>> AddProductWithIFormFile([FromBody] ProductImageDto product)
    {
      if (!ModelState.IsValid) CustomResponse(ModelState);

      var imgName = $"{Guid.NewGuid()}_{product.Name}";

      if (!(await UploadFileAsync(product.ImageUpload, imgName)))
        return CustomResponse();

      product.Image = imgName;

      await _productService.AddAsync(_mapper.Map<Product>(product));
      return CustomResponse(product);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductDto>> Update(Guid id, [FromBody] ProductDto product)
    {
      if (id != product.Id)
      {
        NotifyError("The inputed Id in the URI is not the same of the DTO try again");
        CustomResponse();
      }

      var persistedProduct = await GetProductAndSupplierById(id);

      if (persistedProduct is null)
        return NotFound();

      product.Image = persistedProduct.Image;

      if (!ModelState.IsValid) CustomResponse(ModelState);

      if (product.ImageUpload is not null)
      {
        var imgName = $"{Guid.NewGuid()}_{product.Name}";

        if (!UploadFile(product.ImageUpload, imgName))
          return CustomResponse();

        product.Image = imgName;
      }

      persistedProduct.Name = product.Name;
      persistedProduct.Description = product.Description;
      persistedProduct.Value = product.Value;
      persistedProduct.Active = product.Active;

      await _productService.UpdateAsync(_mapper.Map<Product>(persistedProduct));
      return CustomResponse(product);
    }

    private bool UploadFile(string file, string fileName)
    {
      if (string.IsNullOrWhiteSpace(file))
      {
        NotifyError("Provide an image for this product");
        return false;
      }

      var imgByteArr = Convert.FromBase64String(file);
      var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", fileName);

      if (!System.IO.File.Exists(path))
      {
        NotifyError("Already exists an image with this name");
        return false;
      }

      System.IO.File.WriteAllBytes(path, imgByteArr);

      return true;
    }

    private async Task<bool> UploadFileAsync(IFormFile file, string imgPrefix)
    {
      if (file == null || file.Length <= 0)
      {
        NotifyError("Provide an image for this product");
        return false;
      }

      var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/app/demo-webapi/scr/assets", imgPrefix + file.FileName);

      if (System.IO.File.Exists(path))
      {
        NotifyError("Already exists a file with this name");
        return false;
      }

      using (var stream = new FileStream(path, FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }

      return true;
    }

    private async Task<ProductDto> GetProductAndSupplierById(Guid id)
      => _mapper.Map<ProductDto>(await _productRepository.GetProductAndSupplierById(id));
  }
}