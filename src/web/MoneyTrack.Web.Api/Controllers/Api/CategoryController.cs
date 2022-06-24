using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Web.Api.Models.Entities;

namespace MoneyTrack.Web.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController:MoneyTrackController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody]CategoryModel categoryModel)
        {
            var categoryDto = _mapper.Map<CategoryDto>(categoryModel);

            categoryDto.User = new UserDto { Id = GetCurrentUserId() };

            await _categoryService.AddCategory(categoryDto);

            return Ok();
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var categoriesDto = await _categoryService.GetCategories(GetCurrentUserId());

            var result = _mapper.Map<List<CategoryModel>>(categoriesDto);

            return Ok(result);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] CategoryModel categoryModel)
        {
            var categoryDto = _mapper.Map<CategoryDto>(categoryModel);

            await _categoryService.Update(categoryDto);

            return Ok();
        }
    }
}
