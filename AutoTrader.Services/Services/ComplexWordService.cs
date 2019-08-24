using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class ComplexWordService : IComplexWordService
    {
        private readonly ICacheService _cacheService;

        public ComplexWordService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public IEnumerable<ComplexWord> GetComplexWords()
        {
            var complexWords = _cacheService.ComplexWords;

            if (complexWords == null)
                throw new UndefinedException(typeof(ComplexWord));

            return complexWords;
        }

        public ComplexWord GetComplexWord(string id)
        {
            var complexWords = GetComplexWords();

            return complexWords.FirstOrDefault(c => c.Id.Equals(id));
        }
    }
}