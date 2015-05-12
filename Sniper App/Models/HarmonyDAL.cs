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
    public class HarmonyDAL
    {
        //private const string connectionString = "Data Source=chav-adk11-1;Initial Catalog=V12_L_Harmony;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        private const string connectionString = "Data Source=(localdb)\\V11.0;Initial Catalog=HE_V11_D_Harmony;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        private const int TIMEOUT = 30;
        public Logging log= new Logging();

        public List<HarmonyAccount> getHarmonyAccounts(string option, string criteria)
        {
            List<HarmonyAccount> accounts = new List<HarmonyAccount>();

            string bcn = option == "BCN" ? criteria : string.Empty;
            string fNumber = option == "F Number" ? criteria : string.Empty;
            string accountName = option == "AccountName" ? "%" + criteria + "%" : string.Empty;

            string connectionString = "Data Source=chav-adk11-1;Initial Catalog=V12_L_Harmony;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "dbo.usp_AMS_SelectAccountsByBCNFNumAccName";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BCN", bcn);
                    command.Parameters.AddWithValue("@FNumber", fNumber);
                    command.Parameters.AddWithValue("@Account_Name", accountName);
                    command.Parameters.AddWithValue("@Version_ID", "12ABACAB-F05F-45DD-B765-53DCA0E7D559");


                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HarmonyAccount account = new HarmonyAccount();
                            account.AccountId = reader["Account Id"].ToString();
                            account.HarmonyAccountNumber = reader["Harmony Account Number"].ToString();
                            account.AccountName = reader["Account Name"].ToString();
                            accounts.Add(account);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.LogException(ex);
            }

            return accounts;
        }

        public List<Bcn> getBcnNumbers(string account)
        {
            List<Bcn> bcns = new List<Bcn>();

            try
            {
                string connectionString = "Data Source=chav-adk11-1;Initial Catalog=V12_L_Harmony;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "dbo.usp_AMS_SelectBCNbyAccountKey";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AccountKey", account);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bcn bcn = new Bcn();
                            bcn.BcnNumber = reader["BCN"].ToString();
                            bcn.describition = reader["Description"].ToString();
                            bcn.bcnId = reader["BCNID"].ToString();
                            bcn.fNumber = account;
                            bcns.Add(bcn);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.LogException(ex);
            }


            return bcns;
        }

       

        public void AddBCNtoAccount(string accountID, string BCN)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("dbo.usp_InsertBillingDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = TIMEOUT;

                    command.Parameters.Add(new SqlParameter("@BCNID", Guid.NewGuid()));
                    command.Parameters.Add(new SqlParameter("@AccountID", accountID));
                    command.Parameters.Add(new SqlParameter("@BCN", BCN));
                    command.Parameters.Add(new SqlParameter("@Description", BCN));
                    command.Parameters.Add(new SqlParameter("@Active", 1));
                    command.Parameters.Add(new SqlParameter("@OriginationCD", Convert.ToInt32(0)));
                    command.Parameters.Add(new SqlParameter("@AuditLogonId", Guid.NewGuid()));
                    command.Parameters.Add(new SqlParameter("@LastUpdateDate", DateTime.UtcNow));
                    command.Parameters.Add(new SqlParameter("@ModifiedByMachineId", "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"));
                    command.Parameters.Add(new SqlParameter("@PrimaryBCN", Convert.ToInt32(0)));
                    command.Parameters.Add(new SqlParameter("@Lapsed", Convert.ToInt32(0)));
                    command.Parameters.Add(new SqlParameter("@SicCd", string.Empty));
                    command.Parameters.Add(new SqlParameter("@Zip", string.Empty));

                    command.ExecuteNonQuery();

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                log.LogException(ex);     
            }           
        }

        public void removeBCNToAccount(string BcnId)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("dbo.usp_AMS_UpdateBCNbyBCNID", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = TIMEOUT;

                    command.Parameters.AddWithValue("@BcnId", BcnId);
                    command.Parameters.Add(new SqlParameter("@AuditID", Guid.NewGuid()));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.LogException(ex);    
            }
            

        }        
        
    }
}