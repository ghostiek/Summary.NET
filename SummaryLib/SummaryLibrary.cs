using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Reflection;

namespace SummaryLib
{
    public class Parameters
    {
        public string ApiKey { get; set; }
        public string Url { get; set; }
        public int? SentenceCount { get; set; }
        public int? KeywordCount { get; set; }
        public bool? IncludeQuotes { get; set; }
        public bool? IncludeBreaks { get; set; }     
    }

    public class Summary
    {
        private Parameters _parameters = new Parameters();

        #region Builder
        public Summary ApiKey(string _apikey)
        {
            _parameters.ApiKey = _apikey;
            return this;
        }
        public Summary Url(string _url)
        {
            _parameters.Url = _url;
            return this;
        }
        public Summary SentenceCount(int _sentencecount)
        {
            _parameters.SentenceCount = _sentencecount;
            return this;
        }
        public Summary KeywordCount(int _keywordcount)
        {
            _parameters.KeywordCount = _keywordcount;
            return this;
        }
        public Summary IncludeQuotes(bool _includequotes)
        {
            _parameters.IncludeQuotes = _includequotes;
            return this;
        }
        public Summary IncludeBreaks(bool _includebreaks)
        {
            _parameters.IncludeBreaks = _includebreaks;
            return this;
        }
        #endregion


        public async Task<string> GetJSON()
        {
            StringBuilder url = new StringBuilder($@"http://api.smmry.com");
            url.Append(UrlBuilder(UrlParameters()));

            using (var client = new HttpClient())
            using (var responsemessage = await client.GetAsync(url.ToString()))
            using (var content = responsemessage.Content)
            {
                return await content.ReadAsStringAsync();
            }
        }

        public List<string> UrlParameters()
        {
            var parameterList = new List<string>();

            foreach (var item in _parameters.GetType().GetProperties())
            {
                if (item.GetValue(_parameters) != null) parameterList.Add(item.Name);
            }
            
            var translator = new Dictionary<string, string>()
            {
                {"ApiKey","SM_API_KEY" },
                {"Url", "SM_URL" },
                {"SentenceCount", "SM_LENGTH"},
                {"KeywordCount", "SM_KEYWORD_COUNT"},
                {"IncludeQuotes","SM_QUOTE_AVOID" },
                {"IncludeBreaks","SM_WITH_BREAK" }
            };

            var parsedUrlParameters = new List<string>();

            foreach (var item in parameterList)
            {
                parsedUrlParameters.Add(translator.FirstOrDefault(x => x.Key == item).Value);
            }

            return parsedUrlParameters;
            
        }
        //This has yet to add URL variables
        public string UrlBuilder(List<string> urlParameters)
        {
            var urlDictionary = new Dictionary<string, object>()
            {
                {"SM_API_KEY", _parameters.ApiKey},
                { "SM_URL", _parameters.Url },
                {"SM_LENGTH", _parameters.SentenceCount},
                {"SM_KEYWORD_COUNT", _parameters.KeywordCount},
                {"SM_QUOTE_AVOID", _parameters.IncludeQuotes},
                {"SM_WITH_BREAK", _parameters.IncludeBreaks }
            };

            StringBuilder builder = new StringBuilder($"?");
            foreach (var item in urlDictionary)
            {
                builder.Append($"{item.Key}={item.Value}&");
            }
            //Removes the extra &
            builder.Remove(builder.Length - 1, 1); 
            return builder.ToString();
        }

    }
}



/*
 * • SM_API_KEY=N       // Required, N represents your registered API key.
• SM_URL=X           // Optional, X represents the webpage to summarize.
• SM_LENGTH=N        // Optional, N represents the number of sentences returned, default is 7 
• "=N // Optional, N represents how many of the top keywords to return
• SM_QUOTE_AVOID     // Optional, summary will not include quotations
• SM_WITH_BREAK      // Optional, summary will contain string [BREAK] between each sentence
*/






