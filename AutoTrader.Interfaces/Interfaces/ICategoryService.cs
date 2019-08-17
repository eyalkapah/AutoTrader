﻿using System.Threading.Tasks;
using AutoTrader.Core.Enums;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryBySectionIdAsync(string sectionId);
    }
}