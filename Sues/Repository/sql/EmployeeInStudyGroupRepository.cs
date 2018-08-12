namespace Sues.Repository.sql
{
    using Sues.Domain;
    using Sues.Repository.Filter;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class EmployeeInStudyGroupRepository: IEmployeeInStudyGroupRepository
    {
        private string _connectionString = Connection.DBConnection.GetInstance().ConnectionString;

        private List<KeyValuePair<string, object>> GetFilterParam(EmployeeInStudyGroupFilter filter)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();

            if (filter == null)
                return parameters;

            if (filter.Id.HasValue) parameters.Add(new KeyValuePair<string, object>("@Id", filter.Id));
            if (filter.EmployeeId.HasValue) parameters.Add(new KeyValuePair<string, object>("@EmployeeId", filter.EmployeeId));
            if (filter.StudyGroupId.HasValue) parameters.Add(new KeyValuePair<string, object>("@StudyGroupId", filter.StudyGroupId));

            return parameters;
        }
        public void Delete(int id)
        {
            string procedureName = "EmployeeInStudyGroup_Delete";
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
                command.ExecuteNonQuery();
            }
        }
        public int Insert(EmployeeInStudyGroup entity)
        {
            string procedureName = "EmployeeInStudyGroup_Insert";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(procedureName, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                SqlParameter OtherParam2 = new SqlParameter
                {
                    ParameterName = "@EmployeeId",
                    Value = entity.EmployeeId
                };
                command.Parameters.Add(OtherParam2);
                SqlParameter OtherParam1 = new SqlParameter
                {
                    ParameterName = "@StudyGroupId",
                    Value = entity.StudyGroupId
                };
                command.Parameters.Add(OtherParam1);
                return (int)command.ExecuteScalar();
            }
        }
        public void Update(EmployeeInStudyGroup entity)
        {
            string procedureName = "EmployeeInStudyGroup_Update";
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
                SqlParameter OtherParam1 = new SqlParameter
                {
                    ParameterName = "@EmployeeId",
                    Value = entity.EmployeeId
                };
                command.Parameters.Add(OtherParam1);
                SqlParameter OtherParam2 = new SqlParameter
                {
                    ParameterName = "@StudyGroupId",
                    Value = entity.StudyGroupId
                };
                command.Parameters.Add(OtherParam2);
                command.ExecuteNonQuery();
            }
        }
        public IEnumerable<EmployeeInStudyGroup> Select(EmployeeInStudyGroupFilter filter)
        {
            string procedureName = "EmployeeInStudyGroup_Select";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(procedureName, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                var paramList = GetFilterParam(filter);
                foreach (var param in paramList)
                {
                    SqlParameter Param = new SqlParameter
                    {
                        ParameterName = param.Key,
                        Value = param.Value
                    };
                    command.Parameters.Add(Param);
                }
                var result = command.ExecuteReader();
                List<EmployeeInStudyGroup> resultList = new List<EmployeeInStudyGroup>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        EmployeeInStudyGroup entity = new EmployeeInStudyGroup
                        {
                            Id = result.GetInt32(0),
                            EmployeeId = result.GetInt32(1),
                            StudyGroupId = result.GetInt32(2)
                        };
                        resultList.Add(entity);
                    }
                }
                result.Close();
                return resultList;
            }
        }
        public EmployeeInStudyGroup Get(int id)
        {
            return Select(new EmployeeInStudyGroupFilter { Id = id }).FirstOrDefault();
        }
    }
}