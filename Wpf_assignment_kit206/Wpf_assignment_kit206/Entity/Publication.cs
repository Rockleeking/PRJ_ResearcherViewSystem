using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Wpf_assignment_kit206.Entity
{
    //KIT506_Hbt_F1500_7
    public enum OutputType { Conference, Journal, Other };
    public class Publication
    {
        public string DOI { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public int Year { get; set; }
        public OutputType type { get; set; }
        public string CiteAs { get; set; }
        public DateTime Available { get; set; }
        //calculate age of publication
        public int Age
        {
            get { return DateTime.Now.Year - Year; }
            set { }
        }

        public override string ToString()
        {
            return String.Format("{0} :{1}",Year,Title);
        }
    }
}