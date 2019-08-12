using AutoTrader.Core.Services;
using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
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

        public Task<List<PreDb>> GetPreDbsAsync()
        {
            return _cacheService.GetPreDbsAsync();
        }

        public async Task<PreDb> GetPreDbAsync(string channel, string bot)
        {
            var preDbs = await GetPreDbsAsync();

            return preDbs.FirstOrDefault(p => p.Channel.ToLower().TrimStart('#').Equals(channel.ToLower().TrimStart('#')) && p.Bot.Equals(bot));
        }
    }
}