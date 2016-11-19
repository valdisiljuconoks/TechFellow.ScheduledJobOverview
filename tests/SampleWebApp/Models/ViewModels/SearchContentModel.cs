using System.Collections.Generic;
using System.Web;
using SampleWebApp.Models.Pages;

namespace SampleWebApp.Models.ViewModels
{
    public class SearchContentModel : PageViewModel<SearchPage>
    {
        public SearchContentModel(SearchPage currentPage) : base(currentPage)
        {
        }

        public bool SearchServiceDisabled { get; set; }
        public string SearchedQuery { get; set; }
        public int NumberOfHits { get; set; }
        public IEnumerable<SearchHit> Hits { get; set; }  

        public class SearchHit
        {
            public string Title { get; set; }
            public string Url { get; set; }
            public string Excerpt { get; set; }
        }
    }
}
