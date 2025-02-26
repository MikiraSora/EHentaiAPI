﻿using EHentaiAPI.ExtendFunction;
using EHentaiAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EHentaiAPI.Client
{
    public class EhRequestBuilder
    {
        private string url;
        private Dictionary<string, string> headerMap = new Dictionary<string, string>();

        private string method = "GET";
        private CookieContainer cookie;
        private HttpContent content;

        public EhRequestBuilder(string url)
        {
            this.url = url;
        }

        public EhRequestBuilder(string url, string referer) : this(url, referer, null)
        {

        }

        public EhRequestBuilder(string url, string referer, string origin) : this(url)
        {
            if (!string.IsNullOrWhiteSpace(referer))
            {
                addHeader("Referer", referer);
            }
            if (!string.IsNullOrWhiteSpace(origin))
            {
                addHeader("Origin", origin);
            }
        }

        public void addHeader(string headerName, string value)
        {
            headerMap[headerName] = value;
        }

        public EhRequestBuilder post(HttpContent content)
        {
            method = "POST";
            this.content = content;
            return this;
        }

        public IRequest build()
        {
            var req = Request.Create(url);
            req.Method = method.ToUpper();

            foreach (var pair in headerMap)
            {
                req.Headers.Add(pair.Key, pair.Value);
            }

            if (content != null)
            {
                req.Content = content;
            }

            if (cookie!=null)
            {
                req.Cookies = cookie;
            }

            return req;
        }

        public EhRequestBuilder cookies(CookieContainer cookieContainer)
        {
            cookie = cookieContainer;
            return this;
        }
    }
}
