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
                Html = createDto.Html,
                // CreatedAt = DateTime.Now,
                // UpdatedAt = DateTime.Now,
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

        // public async Task<Article> UpdateAsync(UpdateDTO update)
        // {
        //     var existingProduct = await _context.Articles.FindAsync(article.Id);

        //     if (existingProduct == null)
        //     {
        //         throw new NotFoundException("Article not found");
        //     }

        //     existingProduct.Id = product.Id;
        //     existingProduct.Name = product.Name;
        //     existingProduct.Price = product.Price;
        //     existingProduct.Description = product.Description;
        //     existingProduct.IsActive = product.IsActive;
        //     existingProduct.UpdatedAt = DateTime.Now;

        //     await _context.SaveChangesAsync();
        //     return existingProduct;
        // }

        // public async Task<Product> DeleteAsync(int id)
        // {
        //     var existingProduct = await _context.Products.FindAsync(id);
        //     if (existingProduct == null)
        //     {
        //         throw new NotFoundException("Product not found");
        //     }

        //     _context.Products.Remove(existingProduct);
        //     await _context.SaveChangesAsync();
        //     return existingProduct;
        // }

        // public async Task<IEnumerable<Product>> GetAllAsync()
        // {
        //     var existingProducts = await _context.Products.ToListAsync();
        //     if (existingProducts.Any())
        //     {
        //         return existingProducts;
        //     }
        //     else
        //     {
        //         throw new NotFoundException("No products found");
        //     }
        // }
    }
}