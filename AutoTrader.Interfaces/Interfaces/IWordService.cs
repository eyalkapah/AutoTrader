using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IWordService
    {
        Task<WordMatchResult> GetMatch(string wordId, string text);

        Task<Word> GetWordAsync(string wordId);
    }
}