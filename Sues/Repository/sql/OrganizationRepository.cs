namespace Sues.Repository.sql
{
    using Sues.Domain;
    using Sues.Repository.Filter;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class OrganizationRepository: IOrganizationRepository
    {
        private string _connectionString = Connection.DBConnection.GetInstance().ConnectionString;

        private List<KeyValuePair<string, object>> GetFilterParam(OrganizationFilter filter)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();

            if (filter == null)
                return parameters;

            if (filter.Id.HasValue) parameters.Add(new KeyValuePair<string, object>("@Id", filter.Id));
            if (filter.Name != null && filter.Name != "") parameters.Add(new KeyValuePair<string, object>("@Name", filter.Name));
            if (filter.INN != null && filter.INN != "") parameters.Add(new KeyValuePair<string, object>("@INN", filter.INN));
            if (filter.TeacherId.HasValue) parameters.Add(new KeyValuePair<string, object>("@TeacherId", filter.TeacherId));

            return parameters;
        }

        public void Delete(int id)
        {
            string procedureName = "Organization_Delete";
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
        public int Insert(Organization entity)
        {
            string procedureName = "Organization_Insert";
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
                    ParameterName = "@INN",
                    Value = entity.INN
                };
                command.Parameters.Add(OtherParam1);
                SqlParameter OtherParam2 = new SqlParameter
                {
                    ParameterName = "@TeacherId",
                    Value = entity.TeacherId
                };
                command.Parameters.Add(OtherParam2);
                return (int)command.ExecuteScalar();
            }
        }
        public void Update(Organization entity)
        {
            string procedureName = "Organization_Update";
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
                    ParameterName = "@INN",
                    Value = entity.INN
                };
                command.Parameters.Add(OtherParam1);
                SqlParameter OtherParam2 = new SqlParameter
                {
                    ParameterName = "@TeacherId",
                    Value = entity.TeacherId
                };
                command.Parameters.Add(OtherParam2);
                command.ExecuteNonQuery();
            }
        }
        public IEnumerable<Organization> Select(OrganizationFilter filter)
        {
            string procedureName = "Organization_Select";
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
                List<Organization> resultList = new List<Organization>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        Organization entity = new Organization
                        {
                            Id = result.GetInt32(0),
                            Name = result.GetString(1),
                            INN = result.GetString(2),
                            TeacherId = result.GetInt32(3),
                            TeacherName = result.GetString(4)
                        };
                        resultList.Add(entity);
                    }
                }
                result.Close();
                return resultList;
            }
        }
        public Organization Get(int id)
        {
            return Select(new OrganizationFilter { Id = id }).FirstOrDefault();
        }
    }
}