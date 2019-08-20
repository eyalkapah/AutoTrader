using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IWordService
    {

        Word GetWord(string wordId);

        List<Word> GetWords();
    }
}