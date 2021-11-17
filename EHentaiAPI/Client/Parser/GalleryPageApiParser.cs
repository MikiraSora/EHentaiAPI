﻿using EHentaiAPI.Client.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EHentaiAPI.Client.Parser
{
    public class GalleryPageApiParser
    {
        private static readonly Regex PATTERN_IMAGE_URL = new Regex("<img[^>]*src=\"([^\"]+)\" style");
        private static readonly Regex PATTERN_SKIP_HATH_KEY = new Regex("onclick=\"return nl\\('([^\\)]+)'\\)");
        private static readonly Regex PATTERN_ORIGIN_IMAGE_URL = new Regex("<a href=\"([^\"]+)fullimg.php([^\"]+)\">");


        public static Result parse(String body)
        {
            try
            {
                Match m;
                Result result = new Result();

                var jo = new JObject(body);
                if (jo.ContainsKey("error"))
                {
                    throw new ParseException(jo.getString("error"), body);
                }

                String i3 = jo.getString("i3");
                m = PATTERN_IMAGE_URL.Match(i3);
                if (m.Success)
                {
                    result.imageUrl = ParserUtils.unescapeXml(ParserUtils.trim(m.Groups[(1)].Value));
                }
                String i6 = jo.getString("i6");
                m = PATTERN_SKIP_HATH_KEY.Match(i6);
                if (m.Success)
                {
                    result.skipHathKey = ParserUtils.unescapeXml(ParserUtils.trim(m.Groups[(1)].Value));
                }
                String i7 = jo.getString("i7");
                m = PATTERN_ORIGIN_IMAGE_URL.Match(i7);
                if (m.Success)
                {
                    result.originImageUrl = ParserUtils.unescapeXml(m.Groups[(1)].Value) + "fullimg.php" + ParserUtils.unescapeXml(m.Groups[(2)].Value);
                }

                if (!string.IsNullOrWhiteSpace(result.imageUrl))
                {
                    return result;
                }
                else
                {
                    throw new ParseException("Parse image url and skip hath key error", body);
                }
            }
            catch (Exception e)
            {
                throw new ParseException("Can't parse json", body, e);
            }
        }

        public class Result
        {
            public String imageUrl;
            public String skipHathKey;
            public String originImageUrl;
        }
    }
}