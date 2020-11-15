using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_assignment_kit206.Entity
{
    //KIT506_Hbt_F1500_7
    class Staff
    {
        //calculation of three year average for staffs
        public String threeYearAverage;
        public Staff (List<Publication> publications)
        {
            int currentYear = DateTime.Now.Year;
            int total = 0;
            double tya;
            foreach (Publication p in publications)
            {
                if (p.Available.Year >= (currentYear - 3))
                {
                    total += 1;
                }
            }
            tya = (((double)total) / 3);
            threeYearAverage=String.Format("{0:0.00}", tya);
            
            Performance = String.Format("{0:P2}", tya);
        }
        public String Performance;
    }
}
