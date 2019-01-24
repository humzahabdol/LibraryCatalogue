using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
    public class SearchClass : ISearchClass
    {
        public bool Search(List<string> keywords, List<string> strings)
        {
            foreach (string keyword in keywords)
            {
                string keywordUpper = keyword.ToUpper();
                List<string> foundStrings = (from s in strings where s.Trim().ToUpper().Contains(keywordUpper) select s).ToList();
                if (foundStrings.Count == 0)
                {
                    return false;
                }
            }

            return true;
        }
        public List<string> SplitKeywords(string s)
        {
            char[] delimiters = { ';' };
            string[] words = s.Split(delimiters);
            List<string> keywords = new List<string>(words.Length);
            foreach (string word in words)
            {
                if (word != string.Empty)
                {
                    keywords.Add(word);
                }
            }
            return keywords;
        }
    }

    public interface ISearchClass
    {
        bool Search(List<string> keywords, List<string> strings);
        List<string> SplitKeywords(string s);
    }
}
