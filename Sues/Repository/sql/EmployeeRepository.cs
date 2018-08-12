﻿namespace Sues.Repository.sql
{
    using Sues.Domain;
    using Sues.Repository.Filter;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class EmployeeRepository: IEmployeeRepository
    {
        private string _connectionString = Connection.DBConnection.GetInstance().ConnectionString;

        private List<KeyValuePair<string, object>> GetFilterParam(EmployeeFilter filter)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();

            if (filter == null)
                return parameters;

            if (filter.Id.HasValue) parameters.Add(new KeyValuePair<string, object>("@Id", filter.Id));
            if (filter.Name != null && filter.Name != "") parameters.Add(new KeyValuePair<string, object>("@Name", filter.Name));
            if (filter.OrganizationId.HasValue) parameters.Add(new KeyValuePair<string, object>("@OrganizationId", filter.OrganizationId));

            return parameters;
        }

        public void Delete(int id)
        {
            string procedureName = "Employee_Delete";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(procedureName, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                SqlParameter IdParam = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id
                };
                command.Parameters.Add(IdParam);
                var result = command.ExecuteNonQuery();
            }
        }

        public int Insert(Employee entity)
        {
            string procedureName = "Employee_Insert";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(procedureName, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                SqlParameter NameParam = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = entity.Name
                };
                command.Parameters.Add(NameParam);
                SqlParameter OtherParam1 = new SqlParameter
                {
                    ParameterName = "@OrganizationId",
                    Value = entity.OrganizationId
                };
                command.Parameters.Add(OtherParam1);              
                return (int)command.ExecuteScalar();

            }
        }

        public void Update(Employee entity)
        {
            string procedureName = "Employee_Update";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(procedureName, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                SqlParameter IdParam = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = entity.Id
                };
                command.Parameters.Add(IdParam);
                SqlParameter NameParam = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = entity.Name
                };
                command.Parameters.Add(NameParam);
                SqlParameter OtherParam1 = new SqlParameter
                {
                    ParameterName = "@OrganizationId",
                    Value = entity.OrganizationId
                };
                command.Parameters.Add(OtherParam1);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Employee> Select(EmployeeFilter filter)
        {
            string procedureName = "Employee_Select";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(procedureName, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                var paramList = GetFilterParam(filter);
                foreach(var param in paramList)
                {
                    SqlParameter Param = new SqlParameter
                    {
                        ParameterName = param.Key,
                        Value = param.Value
                    };
                    command.Parameters.Add(Param);
                }
                var result = command.ExecuteReader();
                List<Employee> resultList = new List<Employee>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        Employee entity = new Employee
                        {
                            Id = result.GetInt32(0),
                            Name = result.GetString(1),
                            OrganizationId = result.GetInt32(2)
                        };
                        resultList.Add(entity);
                    }
                }
                result.Close();
                return resultList;
            }
        }

        public Employee Get(int id)
        {
            return Select(new EmployeeFilter { Id = id }).FirstOrDefault();
        }

    }
}