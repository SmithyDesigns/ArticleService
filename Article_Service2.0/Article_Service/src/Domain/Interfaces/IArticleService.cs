using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
