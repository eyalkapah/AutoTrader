using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IWordService
    {
        WordMatchResult GetMatch(string wordId, string text);

        Word GetWord(string wordId);

        List<Word> GetWords();
    }
}