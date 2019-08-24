using System.Collections.Generic;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IComplexWordService
    {
        ComplexWord GetComplexWord(string id);

        IEnumerable<ComplexWord> GetComplexWords();
    }
}