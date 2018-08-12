namespace Sues.Repository.sql
{
    using Sues.Domain;
    using Sues.Repository.Filter;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class CourseRepository: ICourseRepository
    {
        private string _connectionString = Connection.DBConnection.GetInstance().ConnectionString;

        private List<KeyValuePair<string, object>> GetFilterParam(CourseFilter filter)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();

            if (filter == null)
                return parameters;

            if (filter.Id.HasValue) parameters.Add(new KeyValuePair<string, object>("@Id", filter.Id));
            if (filter.Name != null && filter.Name != "") parameters.Add(new KeyValuePair<string, object>("@Name", filter.Name));

            return parameters;
        }
        public void Delete(int id)
        {
            string procedureName = "Course_Delete";
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
        public int Insert(Course entity)
        {
            string procedureName = "Course_Insert";
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
                return (int)command.ExecuteScalar();

            }
        }
        public void Update(Course entity)
        {
            string procedureName = "Course_Update";
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
                command.ExecuteNonQuery();
            }
        }
        public IEnumerable<Course> Select(CourseFilter filter)
        {
            string procedureName = "Course_Select";
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
                List<Course> resultList = new List<Course>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        Course entity = new Course
                        {
                            Id = result.GetInt32(0),
                            Name = result.GetString(1)
                        };
                        resultList.Add(entity);
                    }
                }
                result.Close();
                return resultList;
            }
        }
        public Course Get(int id)
        {
            return Select(new CourseFilter { Id = id }).FirstOrDefault();
        }
    }
}