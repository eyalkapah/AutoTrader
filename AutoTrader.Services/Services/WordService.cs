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

        public Task<List<Word>> GetWordsAsync()
        {
            return _cacheService.GetWordsAsync();
        }

        public async Task<Word> GetWordAsync(string wordId)
        {
            var words = await _cacheService.GetWordsAsync();

            return words.FirstOrDefault(w => w.Id.Equals(wordId));
        }

        public async Task<WordMatchResult> GetMatch(string wordId, string text)
        {
            var word = await GetWordAsync(wordId);

            return word.GetMatch(text);
        }
    }
}