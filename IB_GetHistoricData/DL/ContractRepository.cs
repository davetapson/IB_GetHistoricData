using System;
using IBApi;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace IB_GetHistoricData.DL
{
    internal class ContractRepository
    {
        internal List<Contract> GetAll()
        {
            string selectSP = "Contract_GetAll";

            List<Contract> contracts = new List<Contract>();

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(selectSP, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Contract contract = new Contract
                        {
                            Symbol = reader.GetString(0),
                            SecIdType = reader.GetString(1),
                            Exchange = reader.GetString(2),
                            Currency = reader.GetString(3)
                        };
                        contracts.Add(contract);
                    }
                }
                return contracts;
            }
        }

        internal Contract Get(int id)
        {
            string selectSP = "Contract_Get";
            Contract contract = new Contract();

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(selectSP, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        contract.Symbol = reader.GetString(0);
                        contract.SecIdType = reader.GetString(1);
                        contract.Exchange = reader.GetString(2);
                        contract.Currency = reader.GetString(3);
                    }
                }
            }

            return contract;
        }
    }
}