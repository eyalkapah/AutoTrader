using AutoTrader.Models.Entities;
using AutoTrader.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Extensions
{
    public static class ReleaseExtensions
    {
        public static void ExtractGroup(this ReleaseBase release)
        {
            if (!release.Name.Contains('-'))
                throw new InvalidReleaseFormatException("Failed to extract group name, '-' delimiter was not found");

            release.Group = release.Name.Split('-').Last();
        }

        public static void ExtractArtistAndTitle(this AudioRelease release, char delimiter)
        {
            var list = release.Name.Split(delimiter);

            if (list.Length < 2)
                throw new InvalidReleaseFormatException("Failed to extract artist and title, not enough delimiters were found");

            release.Artist = list[0];
            release.Title = list[1];

            if (release.Group.Equals(release.Title))
                throw new InvalidReleaseFormatException("Failed to extract artist and title, group and title are the same");
        }
    }
}