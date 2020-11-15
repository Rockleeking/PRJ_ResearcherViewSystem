using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//KIT506_Hbt_F1500_7
namespace Wpf_assignment_kit206.Entity
{
    class Student
    {
        //calculation for students
        public string Name { get; set; }
        public string Degree { get; set; }
        public override string ToString()
        {
            return String.Format("{0}\t({1})",Name, Degree);
        }
    }
    
}
