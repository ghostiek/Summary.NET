using System;
using Smmry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmmryTest
{
    [TestClass]
    public class SmmryParametersTest
    {
        [TestMethod]
        public void GetSmmryParameters()
        {
            var smmryParam = new SmmryParameters()
            {
                ApiKey = "ApiKey",
                Url = "https://en.wikipedia.org/wiki/Augustus",
                SentenceCount = 3,
                KeywordCount = 24,
                IncludeBreaks = true,
                IncludeQuotes = true
            };
            Assert.AreEqual("?SM_API_KEY=ApiKey&SM_LENGTH=3&SM_KEYWORD_COUNT=24&SM_WITH_BREAK=True&SM_QUOTE_AVOID=True&SM_URL=https://en.wikipedia.org/wiki/Augustus", smmryParam.ToString());
        }
    }
}
