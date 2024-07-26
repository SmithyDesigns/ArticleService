using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IArticleRepository
    {
        Task<Article> Create(CreateDto CreateDto);
        Task<Article> Find(FindDto findDto);
    }
}