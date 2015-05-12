using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sniper_App.Models
{
    public class SniperDAL
    {        
        private const int TIMEOUT = 30;
        public string connectionString;
        public Logging log = new Logging();
        private bool local = true;     // use true for you local database, false for the server

        public SniperDAL ()
        {
            if (local == true)
            {
                //connectionString = "Data Source=IRELF5Q3XW1\\SQLEXPRESS;Initial Catalog=Sniper;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
                connectionString = "Data Source=(localdb)\\v11.0;Initial Catalog=Sniper;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            }
            else
            {
                connectionString = "Data Source=chav-adk11-1;Initial Catalog=Sniper;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            }
        }
        #region User
        public List<Group> getGroups()
        {
            List<Group> groups = new List<Group>();

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "dbo.usp_UDT_SelectGroups";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Group group = new Group();
                            group.GroupID = Convert.ToInt32(reader["GroupID"]);
                            group.GroupName = reader["GroupName"].ToString();
                            group.Active = Convert.ToBoolean(reader["Active"]);
                            group.TimeStamp = Convert.ToDateTime(reader["TimeStamp"]);
                            groups.Add(group);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.LogException(ex);
            }


            return groups;
        }

        
        public List<User> getUserInfo()
        {
            List<User> users = new List<User>();

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "dbo.usp_UDT_SelectUsers";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.LanID = reader["LanID"].ToString();
                            user.FirstName = reader["FirstName"].ToString();
                            user.LastName = reader["LastName"].ToString();
                            user.UserType = Convert.ToInt32(reader["UserType"]);
                            user.Active = Convert.ToBoolean(reader["Active"]);
                            user.TimeStamp = Convert.ToDateTime(reader["TimeStamp"]);
                            //user.Group_GroupID = Convert.ToInt32(reader["Group_GroupID"]);
                            users.Add(user);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.LogException(ex);
            }


            return users;
        }

        public void addUser(String LanID, String FirstName, String LastName, int UserType/*, int Group_GroupID*/)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("dbo.usp_UDT_AddUser", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = TIMEOUT;

                    command.Parameters.Add(new SqlParameter("@LanID", LanID));
                    command.Parameters.Add(new SqlParameter("@FirstName", FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", LastName));
                    command.Parameters.Add(new SqlParameter("@UserType", UserType));
                    command.Parameters.Add(new SqlParameter("@Active", 1));
                    /*command.Parameters.Add(new SqlParameter("@Group_GroupID", Group_GroupID));*/

                    command.ExecuteNonQuery();

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                log.LogException(ex);
            }
        }


        public void updateUser(String LanID, String FirstName, String LastName, int UserType /*,int Group_GroupID*/,bool Active)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("dbo.usp_UDT_UpdateUser", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = TIMEOUT;

                    command.Parameters.Add(new SqlParameter("@LanID", LanID));
                    command.Parameters.Add(new SqlParameter("@FirstName", FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", LastName));
                    command.Parameters.Add(new SqlParameter("@UserType", UserType));
                    command.Parameters.Add(new SqlParameter("@Active", Active));
                    //command.Parameters.Add(new SqlParameter("@Group_GroupID", Group_GroupID));

                    command.ExecuteNonQuery();

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                log.LogException(ex);
            }
        }
        #endregion User
        #region Audits

        public List<Audit> GetAudits()
        {
            List<Audit> audits = new List<Audit>();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                 using (connection)
                {

                    SqlCommand command = new SqlCommand("usp_UDT_SelectAuditByDate", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@SearchDate", DateTime.Now));
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Audit auditOBJ = new Audit();
                            auditOBJ.LanID = reader["LanID"].ToString();
                            auditOBJ.Application.Name = reader["System"].ToString();
                            auditOBJ.AppEnvironmentID = Int32.Parse(reader["Enviroment"].ToString());
                            auditOBJ.Event = reader["Event"].ToString();
                            auditOBJ.KeyValue = reader["KeyValue"].ToString();
                            auditOBJ.FixID = Int32.Parse(reader["FixId"].ToString());
                            auditOBJ.ValueStart = reader["ValueStart"].ToString();
                            auditOBJ.ValueEnd = reader["ValueEnd"].ToString();
                            //auditOBJ.TimeStamp = reader["TimeStamp"].ToString();

                            audits.Add(auditOBJ);
                        }
                    }

                connection.Close();
                 }
            }
            catch (Exception ex)
            {
                log.LogException(ex);      
            }

            return audits;
        }

        public List<AuditUsers> GetAuditsUsers()
        {
            List<AuditUsers> result = new List<AuditUsers>();
            SqlConnection connection = new SqlConnection(connectionString);


            try
            {
                using (connection)
                {

                    SqlCommand command = new SqlCommand("usp_UDT_SelectDifferentAuditsUsers", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            AuditUsers person = new AuditUsers();
                            person.LanID = reader["LanID"].ToString();
                            person.count = reader["TimesUsed"].ToString();
                            result.Add(person);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.LogException(ex);
            }
            return result;
        }
        public List<AuditUsers> GetAuditsUsersFixes()
        {
            List<AuditUsers> result = new List<AuditUsers>();
            SqlConnection connection = new SqlConnection(connectionString);


            try
            {
                using (connection)
                {

                    SqlCommand command = new SqlCommand("usp_UDT_SelectDifferentAuditsUsersFix", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            AuditUsers person = new AuditUsers();
                            person.LanID = reader["LanID"].ToString();
                            person.count = reader["TimesUsed"].ToString();
                            result.Add(person);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.LogException(ex);
            }
            return result;
        }
        public List<AuditEvents> GetAuditsEvents()
        {
            List<AuditEvents> result = new List<AuditEvents>();
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {

                    SqlCommand command = new SqlCommand("usp_UDT_SelectAuditEvents", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            AuditEvents person = new AuditEvents();
                            person.eventName = reader["Event"].ToString();
                            person.count = reader["TimesUsed"].ToString();
                            result.Add(person);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.LogException(ex);
            }
            return result;
        }
        public List<AuditEvents> GetAuditsEventsSniper()
        {
            List<AuditEvents> result = new List<AuditEvents>();
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {

                    SqlCommand command = new SqlCommand("usp_UDT_SelectAuditEventsSniper", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            AuditEvents person = new AuditEvents();
                            person.eventName = reader["Event"].ToString();
                            person.count = reader["TimesUsed"].ToString();
                            result.Add(person);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.LogException(ex);
            }
            return result;
        }
        public List<Audit> GetAuditsCriteria(int procedure, DateTime searchDate, DateTime endDate, string lanId, string eventType, string fixtype)
        {
            List<Audit> audits = new List<Audit>();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {

                    SqlCommand command = new SqlCommand("usp_UDT_SelectAuditWithGivenProcedure", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@procedure", procedure));
                    command.Parameters.Add(new SqlParameter("@SearchDate", searchDate));
                    command.Parameters.Add(new SqlParameter("@EndDate", endDate));
                    command.Parameters.Add(new SqlParameter("@LanID", lanId));
                    command.Parameters.Add(new SqlParameter("@Event", eventType));
                    command.Parameters.Add(new SqlParameter("@FixType", fixtype));

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Audit auditOBJ = new Audit();
                            //auditOBJ.AuditId = reader["Audit ID"].ToString();
                            auditOBJ.LanID = reader["LanID"].ToString();
                            auditOBJ.Application.Name = reader["System"].ToString();
                            auditOBJ.AppEnvironmentID = Int32.Parse(reader["Enviroment"].ToString());
                            auditOBJ.Event = reader["Event"].ToString();
                            auditOBJ.KeyValue = reader["KeyValue"].ToString();
                            auditOBJ.FixID = Int32.Parse(reader["FixID"].ToString());
                            auditOBJ.ValueStart = reader["ValueStart"].ToString();
                            auditOBJ.ValueEnd = reader["ValueEnd"].ToString();
                            //auditOBJ.TimeStamp = reader["Timestamp"].ToString();

                            audits.Add(auditOBJ);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.LogException(ex);    
            }
           
            return audits;
        }

        public void RecordSearchAudits(string userID, string system, string enviroment, string eventType, string keyval, int FixId, string valstart, string valend)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                //using (connection)
                //{
                //    connection.Open();

                //    SqlCommand command = new SqlCommand("dbo.usp_UDT_InsertAuditDetails", connection);
                //    command.CommandType = CommandType.StoredProcedure;
                //    command.CommandTimeout = TIMEOUT;

                //    command.Parameters.Add(new SqlParameter("@LanID", userID));
                //    command.Parameters.Add(new SqlParameter("@System", system));
                //    command.Parameters.Add(new SqlParameter("@Enviroment", enviroment));
                //    command.Parameters.Add(new SqlParameter("@Event", eventType));
                //    command.Parameters.Add(new SqlParameter("@KeyValue", keyval));
                //    command.Parameters.Add(new SqlParameter("@FixId", FixId));
                //    command.Parameters.Add(new SqlParameter("@ValueStart", valstart));
                //    command.Parameters.Add(new SqlParameter("@ValueEnd", valend));

                //    command.ExecuteNonQuery();

                //    connection.Close();
                //}

            }
            catch (Exception ex)
            {
              log.LogException(ex);
            }

        }
        #endregion Audit
        #region Applications
        public string CheckStoredProcedures(string database, string storedProc)
        {
            SqlConnection connection;

            if (database == "Harmony")
            {
                connection = new SqlConnection("Data Source=IRELF5Q3XW1\\SQLEXPRESS;Initial Catalog=HE_V11_D_Harmony;Integrated Security = true;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            }
            else
            {
                connection = new SqlConnection(connectionString);
            }

            try
            {
                using (connection)
                {

                    connection.Open();

                    SqlCommand command = new SqlCommand(storedProc, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = TIMEOUT;

                    command.ExecuteNonQuery();

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                //log.LogException(ex); Need an error to be thrown, think of it as a hack
                return ex.ToString();
            }
            return "Check Successful";
        }

        [HttpGet]
        public string AddDatabase(string name, string server, bool active)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {

                using (connection)
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("dbo.usp_UDT_InsertDatabase", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = TIMEOUT;

                    command.Parameters.Add(new SqlParameter("@Name", name));
                    command.Parameters.Add(new SqlParameter("@Server", server));
                    command.Parameters.Add(new SqlParameter("@Active", active));
                    command.Parameters.Add(new SqlParameter("@TimeStamp", DateTime.UtcNow));

                    command.ExecuteNonQuery();

                    connection.Close();
                }

            } 
            catch (Exception ex)
            {
                log.LogException(ex);    
            }

            return "Update Successful";
        }

        [HttpGet]
        public string EditDatabase(string dbID, string name, string server, bool active)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {

                using (connection)
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("dbo.usp_UDT_UpdateDatabase", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = TIMEOUT;

                    command.Parameters.Add(new SqlParameter("@DatabaseID", dbID));
                    command.Parameters.Add(new SqlParameter("@Name", name));
                    command.Parameters.Add(new SqlParameter("@Server", server));
                    command.Parameters.Add(new SqlParameter("@Active", active));
                    command.Parameters.Add(new SqlParameter("@TimeStamp", DateTime.UtcNow));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.LogException(ex);
            }


            return "Update Successful";
        }

        [HttpGet]
        public string RemoveDatabase(string dbID)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {

                using (connection)
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("dbo.usp_UDT_DeleteDatabase", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = TIMEOUT;

                    command.Parameters.Add(new SqlParameter("@DatabaseID", dbID));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.LogException(ex);
            } 

            return "Update Successful";
        }
        #endregion Application
    }
}
