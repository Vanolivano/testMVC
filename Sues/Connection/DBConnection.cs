namespace Sues.Connection
{
    public class DBConnection
    {
        public string ConnectionString { get; set; }
        private static DBConnection instance;

        private DBConnection() {
            ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\TestDB.mdf';Integrated Security=True";
        }

        public static DBConnection GetInstance()
        {
            if (instance == null)
                instance = new DBConnection();
            return instance;
        }
    }
}