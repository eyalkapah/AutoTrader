using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IWordService
    {
        Task<List<Word>> GetWordsAsync();

        Task<WordMatchResult> GetMatch(string wordId, string text);

        Task<Word> GetWordAsync(string wordId);
    }
}