using Domain.Dto;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IArticleService
    {
        Task<Article> Create(CreateDto createDto);

        Task<Article> Find(FindDto findDto);
    }
}
