namespace Sues.Repository.sql
{
    using Sues.Domain;
    using Sues.Repository.Filter;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class StudyGroupRepository: IStudyGroupRepository
    {
        private string _connectionString = Connection.DBConnection.GetInstance().ConnectionString;

        private List<KeyValuePair<string, object>> GetFilterParam(StudyGroupFilter filter)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();

            if (filter == null)
                return parameters;

            if (filter.Id.HasValue) parameters.Add(new KeyValuePair<string, object>("@Id", filter.Id));
            if (filter.Name != null && filter.Name != "") parameters.Add(new KeyValuePair<string, object>("@Name", filter.Name));
            if (filter.TeacherId.HasValue) parameters.Add(new KeyValuePair<string, object>("@TeacherId", filter.TeacherId));
            if (filter.CourseId.HasValue) parameters.Add(new KeyValuePair<string, object>("@CourseId", filter.CourseId));

            return parameters;
        }
        public void Delete(int id)
        {
            string procedureName = "StudyGroup_Delete";
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
        public int Insert(StudyGroup entity)
        {
            string procedureName = "StudyGroup_Insert";
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
                SqlParameter OtherParam2 = new SqlParameter
                {
                    ParameterName = "@TeacherId",
                    Value = entity.TeacherId
                };
                command.Parameters.Add(OtherParam2);
                SqlParameter OtherParam1 = new SqlParameter
                {
                    ParameterName = "@CourseId",
                    Value = entity.CourseId
                };
                command.Parameters.Add(OtherParam1);
                return (int)command.ExecuteScalar();
            }
        }
        public void Update(StudyGroup entity)
        {
            string procedureName = "StudyGroup_Update";
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
                SqlParameter OtherParam2 = new SqlParameter
                {
                    ParameterName = "@TeacherId",
                    Value = entity.TeacherId
                };
                command.Parameters.Add(OtherParam2);
                SqlParameter OtherParam1 = new SqlParameter
                {
                    ParameterName = "@CourseId",
                    Value = entity.CourseId
                };
                command.Parameters.Add(OtherParam1);
                command.ExecuteNonQuery();
            }
        }
        public IEnumerable<StudyGroup> Select(StudyGroupFilter filter)
        {
            string procedureName = "StudyGroup_Select";
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
                List<StudyGroup> resultList = new List<StudyGroup>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        StudyGroup entity = new StudyGroup
                        {
                            Id = result.GetInt32(0),
                            Name = result.GetString(1),
                            TeacherId = result.GetInt32(2),
                            CourseId = result.GetInt32(3)
                        };
                        resultList.Add(entity);
                    }
                }
                result.Close();
                return resultList;
            }
        }
        public StudyGroup Get(int id)
        {
            return Select(new StudyGroupFilter { Id = id }).FirstOrDefault();
        }
    }
}