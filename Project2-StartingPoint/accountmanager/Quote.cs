using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace accountmanager
{
    public class Quote
    {
        //variables that correspond to sql database
        private int quoteId;
        private string quoteText;
        private string qFirstName;
        private string qLastName;
        private int rating;
        private string author;
        private string genre1;
        private string genre2;
        private string genre3;

        //properties for the variables
        public int QuoteId { get => quoteId; set => quoteId = value; }
        public string QuoteText { get => quoteText; set => quoteText = value; }
        public string QFirstName { get => qFirstName; set => qFirstName = value; }
        public string QLastName { get => qLastName; set => qLastName = value; }
        public int Rating { get => rating; set => rating = value; }
        public string Genre1 { get => genre1; set => genre1 = value; }
        public string Genre2 { get => genre2; set => genre2 = value; }
        public string Genre3 { get => genre3; set => genre3 = value; }
        public string Author { get => author; set => author = value; }
    }
}