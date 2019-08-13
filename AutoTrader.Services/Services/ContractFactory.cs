﻿using AutoTrader.Core.Enums;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Shared.Models;
using System;
using System.Linq;

namespace AutoTrader.Services.Services
{
    public static class ContractFactory
    {
        public static Category GetCategory(CategoryContract category)
        {
            if (category == null)
                return null;

            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Type = (CategoryType)category.Type,
                Sections = category.Sections.Select(section => GetSection(section)).ToList()
            };
        }

        public static Section GetSection(SectionContract section)
        {
            if (section == null)
                return null;

            return new Section
            {
                Id = section.Id,
                Name = section.Name,
                Description = section.Description,
                Delimiter = section.Delimiter,
                Package = GetPackage(section.Package)
            };
        }

        public static Site GetSite(SiteContract site)
        {
            if (site == null)
                return null;

            return new Site
            {
                Id = site.Id,
                Name = site.Name,
                Status = (SiteStatus)site.Status,
                Rank = site.Rank,
                Enrollments = site.Enrollments.Select(e => GetEnrollment(e)).ToList(),
                Logins = new Logins
                {
                    Total = site.Logins.Total,
                    Upload = site.Logins.Upload,
                    Download = site.Logins.Download
                },
                IrcInfo = site.IrcInfo.Select(i => GetIrcInfo(i)).ToList()
            };
        }

        private static SiteIrcInfo GetIrcInfo(SiteIrcInfoContract ircInfo)
        {
            if (ircInfo == null)
                return null;

            return new SiteIrcInfo
            {
                Channel = ircInfo.Channel,
                Bot = ircInfo.Bot
            };
        }

        public static Enrollment GetEnrollment(EnrollmentContract enrollment)
        {
            if (enrollment == null)
                return null;

            return new Enrollment
            {
                Id = enrollment.Id,
                Affils = enrollment.Affils.ToList(),
                SectionId = enrollment.SectionId,
                Status = (EnrollmentStatus)enrollment.Status,
                Packages = enrollment.Packages.Select(p => GetPackage(p)).ToList()
            };
        }

        internal static Branch GetBranch(BranchContract priority)
        {
            if (priority == null)
                return null;

            return new Branch
            {
                Id = priority.Id,
                Name = priority.Name,
                SectionId = priority.Section,
                BubbleLevel = priority.BubbleLevel,
                RaceActivityInSeconds = priority.RaceActivityInSeconds,
                IsEnabled = priority.Enabled
            };
        }

        internal static PreDb GetPreDb(PreDbContract preDb)
        {
            if (preDb == null)
                return null;

            return new PreDb
            {
                Id = preDb.Id,
                Name = preDb.Name,
                Channel = preDb.Channel,
                Bot = preDb.Bot,
                IsEnabled = preDb.Enabled
            };
        }

        public static Package GetPackage(PackageContract package)
        {
            if (package == null)
                return null;

            return new Package
            {
                Id = package.Id,
                Name = package.Name,
                Applicability = (PackageApplicability)package.Applicability,
                WordId = package.WordId
            };
        }

        public static Word GetWord(WordContract word)
        {
            if (word == null)
                return null;

            return new Word
            {
                Id = word.Id,
                Name = word.Name,
                Description = word.Description,
                Classification = word.Classification,
                Pattern = word.Pattern,
                IgnorePattern = word.Ignore,
                ForbiddenPattern = word.Forbidden
            };
        }

        public static ComplexWord GetComplexWord(ComplexWordContract complexWord)
        {
            if (complexWord == null)
                return null;

            return new ComplexWord
            {
                Id = complexWord.Id,
                Name = complexWord.Name,
                Description = complexWord.Description,
                Classification = complexWord.Classification,
                WordIds = complexWord.Words.ToList()
            };
        }
    }
}