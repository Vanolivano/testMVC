namespace Sues.Repository.sql
{
    using Sues.Domain;
    using Sues.Repository.Filter;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class TeacherRepository: ITeacherRepository
    {
        private string _connectionString = Connection.DBConnection.GetInstance().ConnectionString;

        private List<KeyValuePair<string, object>> GetFilterParam(TeacherFilter filter)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();

            if (filter == null)
                return parameters;

            if (filter.Id.HasValue) parameters.Add(new KeyValuePair<string, object>("@Id", filter.Id));
            if (filter.Name != null && filter.Name != "") parameters.Add(new KeyValuePair<string, object>("@Name", filter.Name));
            if (filter.Email != null && filter.Email != "") parameters.Add(new KeyValuePair<string, object>("@OrganizationId", filter.Email));

            return parameters;
        }
        public void Delete(int id)
        {
            string procedureName = "Teacher_Delete";
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
        public int Insert(Teacher entity)
        {
            string procedureName = "Teacher_Insert";
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
                    ParameterName = "@Email",
                    Value = entity.Email
                };
                command.Parameters.Add(OtherParam1);
                return (int)command.ExecuteScalar();
            }
        }
        public void Update(Teacher entity)
        {
            string procedureName = "Teacher_Update";
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
                    ParameterName = "@Email",
                    Value = entity.Email
                };
                command.Parameters.Add(OtherParam1);
                command.ExecuteNonQuery();
            }
        }
        public IEnumerable<Teacher> Select(TeacherFilter filter)
        {
            string procedureName = "Teacher_Select";
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
                List<Teacher> resultList = new List<Teacher>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        Teacher entity = new Teacher
                        {
                            Id = result.GetInt32(0),
                            Name = result.GetString(1),
                            Email = result.IsDBNull(2)? null : result.GetString(2)
                        };
                        resultList.Add(entity);
                    }
                }
                result.Close();
                return resultList;
            }
        }
        public Teacher Get(int id)
        {
            return Select(new TeacherFilter { Id = id }).FirstOrDefault();
        }
    }
}