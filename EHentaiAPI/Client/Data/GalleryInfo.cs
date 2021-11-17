﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EHentaiAPI.Client.Data
{
    public class GalleryInfo
    {
        /**
         * ISO 639-1
         */
        public const String S_LANG_JA = "JA";
        public const String S_LANG_EN = "EN";
        public const String S_LANG_ZH = "ZH";
        public const String S_LANG_NL = "NL";
        public const String S_LANG_FR = "FR";
        public const String S_LANG_DE = "DE";
        public const String S_LANG_HU = "HU";
        public const String S_LANG_IT = "IT";
        public const String S_LANG_KO = "KO";
        public const String S_LANG_PL = "PL";
        public const String S_LANG_PT = "PT";
        public const String S_LANG_RU = "RU";
        public const String S_LANG_ES = "ES";
        public const String S_LANG_TH = "TH";
        public const String S_LANG_VI = "VI";

        public readonly static String[] S_LANGS = {
            S_LANG_EN,
            S_LANG_ZH,
            S_LANG_ES,
            S_LANG_KO,
            S_LANG_RU,
            S_LANG_FR,
            S_LANG_PT,
            S_LANG_TH,
            S_LANG_DE,
            S_LANG_IT,
            S_LANG_VI,
            S_LANG_PL,
            S_LANG_HU,
            S_LANG_NL,
    };

        public readonly static Regex[] S_LANG_PATTERNS = {
            new Regex("[(\\[]eng(?:lish)?[)\\]]|英訳", RegexOptions.IgnoreCase),
            // [(（\[]ch(?:inese)?[)）\]]|[汉漢]化|中[国國][语語]|中文|中国翻訳
            new Regex("[(\uFF08\\[]ch(?:inese)?[)\uFF09\\]]|[汉漢]化|中[国國][语語]|中文|中国翻訳", RegexOptions.IgnoreCase),
            new Regex("[(\\[]spanish[)\\]]|[(\\[]Español[)\\]]|スペイン翻訳", RegexOptions.IgnoreCase),
            new Regex("[(\\[]korean?[)\\]]|韓国翻訳", RegexOptions.IgnoreCase),
            new Regex("[(\\[]rus(?:sian)?[)\\]]|ロシア翻訳", RegexOptions.IgnoreCase),
            new Regex("[(\\[]fr(?:ench)?[)\\]]|フランス翻訳", RegexOptions.IgnoreCase),
            new Regex("[(\\[]portuguese|ポルトガル翻訳", RegexOptions.IgnoreCase),
            new Regex("[(\\[]thai(?: ภาษาไทย)?[)\\]]|แปลไทย|タイ翻訳", RegexOptions.IgnoreCase),
            new Regex("[(\\[]german[)\\]]|ドイツ翻訳", RegexOptions.IgnoreCase),
            new Regex("[(\\[]italiano?[)\\]]|イタリア翻訳", RegexOptions.IgnoreCase),
            new Regex("[(\\[]vietnamese(?: Tiếng Việt)?[)\\]]|ベトナム翻訳", RegexOptions.IgnoreCase),
            new Regex("[(\\[]polish[)\\]]|ポーランド翻訳", RegexOptions.IgnoreCase),
            new Regex("[(\\[]hun(?:garian)?[)\\]]|ハンガリー翻訳", RegexOptions.IgnoreCase),
            new Regex("[(\\[]dutch[)\\]]|オランダ翻訳", RegexOptions.IgnoreCase),
    };

        public readonly static String[] S_LANG_TAGS = {
            "language:english",
            "language:chinese",
            "language:spanish",
            "language:korean",
            "language:russian",
            "language:french",
            "language:portuguese",
            "language:thai",
            "language:german",
            "language:italian",
            "language:vietnamese",
            "language:polish",
            "language:hungarian",
            "language:dutch",
    };

        public long gid;
        public String token;
        public String title;
        public String titleJpn;
        public String thumb;
        public int category;
        public String posted;
        public String uploader;
        public float rating;
        public bool rated;
        public String[] simpleTags;
        public int pages;
        public int thumbWidth;
        public int thumbHeight;
        public int spanSize;
        public int spanIndex;
        public int spanGroupIndex;
        /**
         * language from title
         */
        public String simpleLanguage;
        public int favoriteSlot = -2;
        public String favoriteName;

        public GalleryInfo()
        {
        }
        public void generateSLang()
        {
            if (simpleTags != null)
            {
                generateSLangFromTags();
            }
            if (simpleLanguage == null && title != null)
            {
                generateSLangFromTitle();
            }
        }

        private void generateSLangFromTags()
        {
            foreach (String tag in simpleTags)
            {
                for (int i = 0; i < S_LANGS.Length; i++)
                {
                    if (S_LANG_TAGS[i].Equals(tag))
                    {
                        simpleLanguage = S_LANGS[i];
                        return;
                    }
                }
            }
        }

        private void generateSLangFromTitle()
        {
            for (int i = 0; i < S_LANGS.Length; i++)
            {
                if (S_LANG_PATTERNS[i].Match(title)?.Success ?? false)
                {
                    simpleLanguage = S_LANGS[i];
                    return;
                }
            }
            simpleLanguage = null;
        }

        public override string ToString() => $"{this.gid} {titleJpn ?? title}";
    }
}