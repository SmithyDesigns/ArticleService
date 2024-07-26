using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    // [Authorize]
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
                Description = article.Description
            };

            return new JsonResult(articleDto);
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find([FromBody] FindDto findDto)
        {
            if (findDto == null)
            {
                return BadRequest("FindDto is required");
            }

            var article = await _articleService.Find(findDto);
            return new JsonResult(article);
        }
    }
}
