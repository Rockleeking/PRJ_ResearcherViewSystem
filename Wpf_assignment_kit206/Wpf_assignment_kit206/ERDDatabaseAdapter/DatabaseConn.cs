using MySql.Data.MySqlClient;
using MySql.Data.Types  ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf_assignment_kit206.Entity;
using System.Data;
//KIT506_Hbt_F1500_7
namespace Wpf_assignment_kit206.ERDDatabaseAdapter
{
    abstract class DatabaseConn
    {
        //MYSQL Database Connection literals
        private static bool reportingErrors = false;
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
        /// <summary>
        /// Creates and returns (but does not open) the connection to the database.
        /// </summary>
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }
        //Loads postions list into a researcher detail.
        public static List<Position> loadPositionFor( int id)
        {
            List<Position> posit = new List<Position>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from position where id= " + id + "", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    posit.Add(new Position { level = ParseEnum<EmploymentLevel>( rdr.GetString(1)),
                        start_date = rdr.GetDateTime(2),
                        end_date = (rdr.IsDBNull(3)) ? DateTime.Now : rdr.GetDateTime(3) });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading researcher", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return posit;
        }
        //fetch basic researcher details
        public static List<Researher> fetchBasicResearcherDetail()
        {
            List<Researher> research = new List<Researher>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select given_name, family_name, title from researcher,id", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    research.Add(new Researher { id = rdr.GetInt32(3), GivenName = rdr.GetString(0) ,FamilyName= rdr.GetString(1),Title=rdr.GetString(2) });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading researcher", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return research;
        }
        //fetch complete researcher detail
        public static List<Researher> fetchFullResearcherDetail()
        {
            List<Researher> research = new List<Researher>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();
                
                MySqlCommand cmd = new MySqlCommand("select * from researcher", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    research.Add(new Researher { id = rdr.GetInt32(0),types = ParseEnum<type>(rdr.GetString(1)), GivenName = rdr.GetString(2), FamilyName = rdr.GetString(3),Title = rdr.GetString(4), Unit = rdr.GetString(5), Campus =rdr.GetString(6), Email = rdr.GetString(7),Photo = rdr.GetString(8), degree = (rdr.IsDBNull(9)) ? "" : rdr.GetString(9), supervisor_id = (rdr.IsDBNull(10)) ? 0 : rdr.GetInt32(10), employmentLevel = (rdr.IsDBNull(11)) ? ParseEnum<EmploymentLevel>("student") : ParseEnum<EmploymentLevel>(rdr.GetString(11)), utas_start = rdr.GetDateTime(12),current_start = rdr.GetDateTime(13) });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading researcher", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return research;
        }
        //Fetch Complete publication detail
        public static List<Publication> fetchFullPublicationDetail(int id)
        {
            List<Publication> publications = new List<Publication>();
            
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

               // MySqlCommand cmd = new MySqlCommand("SELECT  title, authors, year, type, cite_as, available FROM publication as pu, researcher_publication as rp WHERE pu.doi=rp.doi AND rp.researcher_id=" + id + "", conn);
               MySqlCommand cmd = new MySqlCommand("SELECT pub.doi, title, authors, year, type, cite_as, available" +
                                                    " FROM publication as pub, researcher_publication as respub" +
                                                    " where pub.doi=respub.doi and respub.researcher_id=" + id + "", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    publications.Add(new Publication { 
                        DOI = rdr.GetString(0),
                        Title= rdr.GetString(1),
                        Authors = rdr.GetString(2),
                        Year = rdr.GetInt32(3),
                        type = ParseEnum<OutputType>(rdr.GetString(4)),
                        CiteAs = rdr.GetString(5),
                        Available = rdr.GetDateTime(6)
                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading publication", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return publications;
        }
        //fetch supervision of a Researcher
        public static List<Student> fetchSupervision(int id)
        {
            List<Student> students = new List<Student>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;


            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT given_name, family_name, degree FROM researcher WHERE supervisor_id=" + id + "", conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {

                        students.Add(new Student
                        {
                            Name = rdr.GetString(0) + "" + rdr.GetString(1),
                            Degree = rdr.GetString(2)
                        });
                    }
                
            }

            catch (MySqlException e)
            {
                ReportError("loading supervisions", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return students;
        }
        //filter by name
       


        
        // Exception Error reporting
            private static void ReportError(string msg, Exception e)
        {
            if (reportingErrors)
            {
                MessageBox.Show("An error occurred while " + msg + ". Try again later.\n\nError Details:\n" + e,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
         
       
        }
    }



