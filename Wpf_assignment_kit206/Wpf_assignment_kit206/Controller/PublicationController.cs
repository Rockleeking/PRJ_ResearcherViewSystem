using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Wpf_assignment_kit206.Entity;
using Wpf_assignment_kit206.ERDDatabaseAdapter;
//KIT506_Hbt_F1500_7
namespace Wpf_assignment_kit206.Controller
{
    class PublicationController
    {
        private List<Publication> publication;
        public List<Publication> publicat { get { return publication; } set { } }
      
        public List<Publication> loadPublicationsFor(Researher r) {
            //loading pulication controller with publication detail for researcher
            publication = DatabaseConn.fetchFullPublicationDetail(r.id).OrderByDescending(o => o.Year).ToList(); ;
            //sending publication details of a researcher
            return publicat;
        }
    }
}
