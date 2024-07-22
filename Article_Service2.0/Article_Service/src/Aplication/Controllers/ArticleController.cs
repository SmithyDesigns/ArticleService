using Domain.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest("CreateDto is required");
            }

            var article = await _articleService.Create(createDto);

            var articleDto = new ArticleDto
            {
                Title = article.Title,
                Html = article.Html
            };

            return Ok(articleDto);
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find([FromQuery] FindDto findDto)
        {
            if (findDto == null)
            {
                return BadRequest("FindDto is required");
            }

            var article = await _articleService.Find(findDto);
            return Ok(article);
        }
    }
}
