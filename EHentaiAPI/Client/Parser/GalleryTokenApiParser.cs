﻿using EHentaiAPI.Client.Exceptions;
using EHentaiAPI.Utils.ExtensionMethods;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHentaiAPI.Client.Parser
{
    public class GalleryTokenApiParser
    {

        /**
         * {
         * "tokenlist": [
         * {
         * "gid":618395,
         * "token":"0439fa3666"
         * }
         * ],
         * "error":"maomao is moe~"
         * }
         */
        public static string Parse(string body)
        {
            var jo = JsonConvert.DeserializeObject<JObject>(body);
            var ja = jo.GetJSONArray("tokenlist");

            try
            {
                return ja[0].GetString("token");
            }
            catch (Exception)
            {
                //ExceptionUtils.throwIfFatal(e);
                throw new EhException(jo.GetString("error"));
            }
        }
    }
}
