using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Wpf_assignment_kit206.Entity
{
    //KIT506_Hbt_F1500_7
    public class Position
    {
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public EmploymentLevel level { get; set; }
        //Automatically set title
        public string Title {
            get
            {
                if (level == EmploymentLevel.A)
                {
                    return "PostDoc";
                }
                else if (level == EmploymentLevel.B) { return "Lecturer"; }
                else if (level == EmploymentLevel.C)
                {
                    return "Senior lecturer";
                }
                else if (level == EmploymentLevel.D)
                {
                    return "Associate professor";
                }
                else if (level == EmploymentLevel.E)
                {
                    return "Professor";
                }
                else
                {
                    return "Student";
                }
                }
            set{ }
        }
        //return title if called
        public string ToTitle(EmploymentLevel l) {
            if (l == EmploymentLevel.A)
            {
                return "PostDoc";
            }
            else if (l == EmploymentLevel.B) { return "Lecturer"; }
            else if (l == EmploymentLevel.C)
            {
                return "Senior lecturer";
            }
            else if (l == EmploymentLevel.D)
            {
                return "Associate professor";
            }
            else if (l == EmploymentLevel.E)
            {
                return "Professor";
            }
            else if (l == EmploymentLevel.student)
            {
                return "Student";
            }
            else {
                return "UNDEFINED";
            }
            }
        }
    }
