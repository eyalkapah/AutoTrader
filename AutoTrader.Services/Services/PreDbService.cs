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
    public class PreDbService : IPreDbService
    {
        private readonly ICacheService _cacheService;

        public PreDbService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public PreDb GetPreDb(string channel, string bot)
        {
            var preDbs = GetPreDbs();

            return preDbs.FirstOrDefault(p => p.Channel.ToLower().TrimStart('#').Equals(channel.ToLower().TrimStart('#')) && p.Bot.Equals(bot));
        }

        public IEnumerable<PreDb> GetPreDbs()
        {
            var preDbs = _cacheService.PreDbs;

            if (preDbs == null)
                throw new UndefinedException(typeof(PreDb));

            return preDbs;
        }
    }
}