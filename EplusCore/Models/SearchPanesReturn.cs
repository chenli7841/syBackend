using System.Collections.Generic;

namespace WebUI.Models
{
    public class SearchPanesReturn
    {
        public Dictionary<string, IEnumerable<SearchValues>> Options { get; set; }

        public SearchPanesReturn()
        {
            Options = new Dictionary<string, IEnumerable<SearchValues>>();
        }
    }
}
