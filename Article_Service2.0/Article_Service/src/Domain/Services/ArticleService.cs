using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<Article> Create(CreateDto createDto)
        {
            if (string.IsNullOrEmpty(createDto.Title) || string.IsNullOrEmpty(createDto.Html))
            {
                throw new ArgumentException("Title and Html are required.", nameof(createDto));
            }

            try
            {
                return await _articleRepository.Create(createDto);
            }
            catch (DbUpdateException ex)
            {
                throw new ArgumentException("Failed to create article. Please check the database connection.", nameof(createDto), ex);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occurred while creating the article.", nameof(createDto), ex);
            }
        }

        public async Task<Article> Find(FindDto findDto)
        {
            if (findDto == null)
            {
                throw new ArgumentNullException(nameof(findDto), "FindDto is required");
            }

            try
            {
                return await _articleRepository.Find(findDto);
            }
            catch (Exception)
            {
                throw new NotFoundException("Article not found");
            }
        }

        // public async Task<Article> UpdateAsync(UpdateDTO article)
        // {
        //     return await _articleRepository.UpdateAsync(article);
        // }

        // public async Task<Article> DeleteAsync(int id)
        // {
        //     return await _articleRepository.DeleteAsync(id);
        // }

        // public async Task<IEnumerable<Article>> GetAllAsync()
        // {
        //     return await _articleRepository.GetAllAsync();
        // }
    }
}