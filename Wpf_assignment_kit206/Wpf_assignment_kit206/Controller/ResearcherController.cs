using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Wpf_assignment_kit206.Entity;
using Wpf_assignment_kit206.ERDDatabaseAdapter;
using System.Data;
//KIT506_Hbt_F1500_7
namespace Wpf_assignment_kit206.Controller
{
    class ResearcherController
    {
        private List<Researher> researchers;
        //private List<Publication> rp;
        public List<Researher> reserchers { get { return researchers; } set { } }
        //public List<Publication> pp { get { return rp; } set { } }

        private ObservableCollection<Researher> viewableresearchers;
        public ObservableCollection<Researher> VisibleWorkers { get { return viewableresearchers; } set { } }
        public ResearcherController() {
            researchers = DatabaseConn.fetchFullResearcherDetail().OrderBy(o => o.GivenName).ToList();
            viewableresearchers = new ObservableCollection<Researher>(researchers); //this list we will modify later

            foreach (Researher e in researchers)
            {
                PublicationController p= new PublicationController();
                e.publish = p.loadPublicationsFor(e);
                e.position = DatabaseConn.loadPositionFor(e.id);
                if (e.types == type.Staff) {
                    e.sup_student = DatabaseConn.fetchSupervision(e.id);
                    e.calculateStaff(e.publish);

                }
                e.previousJobDetail();

            }
        }
        
        //filterby combo box employment level
        public void FilterByEmploymentLevel(EmploymentLevel e)
        {
            if (e != EmploymentLevel.Any)
            {

                var selected = from Researher r in researchers
                               where e == r.employmentLevel
                               select r;
                viewableresearchers.Clear();
                selected.ToList().ForEach(viewableresearchers.Add);

            }
            else
            {
                var selected = from Researher r in researchers
                               select r;
                viewableresearchers.Clear();
                selected.ToList().ForEach(viewableresearchers.Add);

            }
        }

        //filter researcher by Family Name

        public void FilterByName(string Name)
        {
            if (Name != "" || Name != null)
            {
                var n = from Researher r in researchers
                        where r.FamilyName.ToLower()==Name.ToLower() || r.GivenName.ToLower() == Name.ToLower()|| r.FamilyName.ToLower().Contains(Name.ToLower())|| r.GivenName.ToLower().Contains(Name.ToLower())
                                 select r;
                viewableresearchers.Clear();
                n.ToList().ForEach(viewableresearchers.Add);
            }
            else
            {
                var n = from Researher r in researchers
                                 select r;
                viewableresearchers.Clear();
                n.ToList().ForEach(viewableresearchers.Add);
            }

        }
        public ObservableCollection<Researher> GetViewableList()
        {
            return VisibleWorkers;
        }

       
    }
}
