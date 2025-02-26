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
        public const string S_LANG_JA = "JA";
        public const string S_LANG_EN = "EN";
        public const string S_LANG_ZH = "ZH";
        public const string S_LANG_NL = "NL";
        public const string S_LANG_FR = "FR";
        public const string S_LANG_DE = "DE";
        public const string S_LANG_HU = "HU";
        public const string S_LANG_IT = "IT";
        public const string S_LANG_KO = "KO";
        public const string S_LANG_PL = "PL";
        public const string S_LANG_PT = "PT";
        public const string S_LANG_RU = "RU";
        public const string S_LANG_ES = "ES";
        public const string S_LANG_TH = "TH";
        public const string S_LANG_VI = "VI";

        public readonly static string[] S_LANGS = {
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

        public readonly static string[] S_LANG_TAGS = {
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

        public long Gid { get; set; }
        public string Token { get; set; }
        public string Title { get; set; }
        public string TitleJpn { get; set; }
        public string Thumb { get; set; }
        public int Category { get; set; }
        public string Posted { get; set; }
        public string Uploader { get; set; }
        public float Rating { get; set; }
        public bool Rated { get; set; }
        public string[] SimpleTags { get; set; }
        public int Pages { get; set; }
        public int ThumbWidth { get; set; }
        public int ThumbHeight { get; set; }
        public int SpanSize { get; set; }
        public int SpanIndex { get; set; }
        public int SpanGroupIndex { get; set; }

        public string AvaliableTitle => string.IsNullOrWhiteSpace(TitleJpn) ? Title : TitleJpn;

        public string SimpleLanguage { get; set; }
        public int FavoriteSlot { get; set; } = -2;
        public string FavoriteName { get; set; }

        public void GenerateSLang()
        {
            if (SimpleTags != null)
            {
                GenerateSLangFromTags();
            }
            if (SimpleLanguage == null && Title != null)
            {
                GenerateSLangFromTitle();
            }
        }

        private void GenerateSLangFromTags()
        {
            foreach (string tag in SimpleTags)
            {
                for (int i = 0; i < S_LANGS.Length; i++)
                {
                    if (S_LANG_TAGS[i].Equals(tag))
                    {
                        SimpleLanguage = S_LANGS[i];
                        return;
                    }
                }
            }
        }

        private void GenerateSLangFromTitle()
        {
            for (int i = 0; i < S_LANGS.Length; i++)
            {
                if (S_LANG_PATTERNS[i].Match(Title)?.Success ?? false)
                {
                    SimpleLanguage = S_LANGS[i];
                    return;
                }
            }
            SimpleLanguage = null;
        }

        public override string ToString() => $"{this.Gid} {TitleJpn ?? Title}";
    }
}
