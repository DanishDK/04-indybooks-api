using System;
using System.Collections.Generic;

namespace IndyBooks.ViewModels
{
    public class SearchResultsVM
    {
        //Properties needed for displaying search results
        //  Notice how the ViewModel doesn't need any changes even when
        //  the entity changes since it only deals with the Search Results view

        public IEnumerable<IndyBooks.Models.Book> Books { get; set; }
        public Boolean IsSale { get; set; }
    }
}


