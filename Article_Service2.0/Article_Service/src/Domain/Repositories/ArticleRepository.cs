using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using Domain.Data;
using Domain.Dto;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly MyDbContext _context;

        public ArticleRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Article> Create(CreateDto createDto)
        {
            var article = new Article
            {
                Title = createDto.Title,
                Description = createDto.Description,
            };

            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();

            return article;
        }

        public async Task<Article> Find(FindDto findDto)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Title == findDto.Title);
            if (article == null)
            {
                throw new NotFoundException("Article not found");
            }
            return article;
        }
    }
}