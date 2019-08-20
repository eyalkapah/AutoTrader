using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class WordService : IWordService
    {
        private readonly ICacheService _cacheService;

        public WordService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public Word GetWord(string wordId)
        {
            var words = _cacheService.Words;

            return words.FirstOrDefault(w => w.Id.Equals(wordId));
        }

        public List<Word> GetWords()
        {
            return _cacheService.Words;
        }
    }
}