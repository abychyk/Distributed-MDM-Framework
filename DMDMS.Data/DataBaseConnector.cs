using DMDMS.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMDMS.Data
{
    public class DataBaseConnector
    {
        private string _configPath;
        private SqlConnection _sqlConnection;

        private List<string> _tableStrings;
        private List<BaseEntity> _tableObjects;

        public DataBaseConnector(string configPath)
        {
            _configPath = configPath;
            _sqlConnection = new SqlConnection();
        }

        private void CreateSqlConnection()
        {
            _sqlConnection.ConnectionString = XmlHelper.ParseSourceConnectionString(_configPath);
        }

        private void OpenSqlConnection()
        {
            CreateSqlConnection();
            _sqlConnection.Open();
        }

        public void ExecuteSqlCommand(string query)
        {
            OpenSqlConnection();
        }

        public List<string> TableStrings
        {
            get
            {
                return _tableStrings;
            }
            set
            {
                _tableStrings = value;
            }
        }

        public List<BaseEntity> TableObjects
        {
            get
            {
                return _tableObjects;
            }
            set
            {
                _tableObjects = value;
            }
        }
    }
}
