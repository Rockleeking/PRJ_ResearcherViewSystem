using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//KIT506_Hbt_F1500_7
namespace Wpf_assignment_kit206.Entity
{
    public enum EmploymentLevel {student,A, B, C, D, E, Any };

    public enum type { Staff, Student };
    class Researher
    {
        //get basic details
        public int id { get; set; }
        public string GivenName { get; set; }
        public string Name { get { return "" + GivenName + " " + FamilyName +""; } set { } }
        public string FamilyName { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }
        public String Campus { get; set; }
        public EmploymentLevel employmentLevel { get; set; }
        public List <Publication> publish { get; set; }
        public List<Position> position { get; set; }
        public List<Student> sup_student { get; set; }
        public string threeYearAverage { get; set; }
        public string performance { get; set; }
        public String previousStart { get; set; }
        public String previousEnd{ get; set; }
        public String previousJob { get; set; }
        public string Email { get; set; }
        public type types { get; set; }

        public string degree { get; set; }
        public DateTime utas_start { get; set; }
        public DateTime current_start { get; set; }

        public int supervisor_id { get; set; }

        public string Photo { get; set; }
        //get current job position detail
        public Position GetCurrentJob() {
            Position p = new Position();
            p.start_date = current_start;
            p.level = employmentLevel;
            return p;
        }
        //get string of current position
        public String CurrentJobTitle
        {
            get
            {
                String t = "";
                foreach (Position p in position)
                {
                    if (p.level == employmentLevel)
                    {
                        t = p.ToTitle(employmentLevel);
                    }
                }
                return t;
            }
            set { }
        }
        //calculate previous job detail
        public void previousJobDetail() {
            if (employmentLevel == EmploymentLevel.E && (position.Count > 1))
            {
                foreach (Position p in position)
                {
                    if (p.level == EmploymentLevel.D)
                    {
                        previousEnd = p.end_date.Date.ToString("MM/dd/yyyy");
                        previousStart = p.start_date.Date.ToString("MM/dd/yyyy");
                        previousJob = p.Title;
                        break;
                    }

                }
            }
            else if (employmentLevel == EmploymentLevel.D && (position.Count > 1))
            {
                foreach (Position p in position)
                {
                    if (p.level == EmploymentLevel.C)
                    {
                        previousEnd = p.end_date.Date.ToString("MM/dd/yyyy");
                        previousStart = p.start_date.Date.ToString("MM/dd/yyyy");
                        previousJob = p.Title;
                        break;
                    }

                }
            }
            else if (employmentLevel == EmploymentLevel.C && (position.Count > 1))
            {
                foreach (Position p in position)
                {
                    if (p.level == EmploymentLevel.B)
                    {
                        previousEnd = p.end_date.Date.ToString("MM/dd/yyyy");
                        previousStart = p.start_date.Date.ToString("MM/dd/yyyy");
                        previousJob = p.Title;
                        break;

                    }

                }
            }
            else if (employmentLevel == EmploymentLevel.B && (position.Count > 1))
            {
                foreach (Position p in position)
                {
                    if (p.level == EmploymentLevel.A)
                    {
                        previousEnd = p.end_date.Date.ToString("MM/dd/yyyy");
                        previousStart = p.start_date.Date.ToString("MM/dd/yyyy");
                        previousJob = p.Title;
                        break;
                    }

                }
            }
            else if (employmentLevel == EmploymentLevel.A && (position.Count > 1))
            {

                previousStart = current_start.Date.ToString("MM/dd/yyyy");
                previousEnd = "\t\t";
                previousJob = CurrentJobTitle;

            }
            else if (employmentLevel == EmploymentLevel.student)
            {
                previousStart = "\t\t";
                previousEnd = "\t\t";
                previousJob = CurrentJobTitle;
            }
            else
            {
                previousStart = current_start.Date.ToString("MM/dd/yyyy");
                previousEnd = "\t\t";
                previousJob = CurrentJobTitle;
            }
        }

        //calculate staff to get three year average
        public void calculateStaff(List<Publication> lp) {

            Staff s = new Staff(lp);
            threeYearAverage = s.threeYearAverage;
            performance = s.Performance;

        }
        //calculate tenure of current position
        public string Tenure
        {
            get
            {
                return String.Format("{0:0.00}", ((double)DateTime.Now.Year + (((double)DateTime.Now.Month) / 12)) - ((double)current_start.Year + (((double)current_start.Month) / 12)));
            }
            set { }
        }
        //calculate publication count and return
        public int PublicationCount {
            get
            {
                return publish.Count();
            }
            set { }
        }
        //get supersion count
        public int supervisionNumer {
            get
            {
                if (types == type.Staff)
                {

                    return sup_student.Count();
                }
                else {
                    return 0;
                }
            }
            set { }
            
        }
        //tostring
        public override string ToString()
        {
            return String.Format("{0}\t{1}, {2}", id, Title,Name) ;
        }
        
    }
}

