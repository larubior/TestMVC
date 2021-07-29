using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestMVC.Data.Interfaces;
using TestMVC.Model.DTO.User;

namespace TestMVC.Data.Operations
{
    public class UserRepository: IUserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Querys

        private readonly string getAll = "spGetAllUsers";
        private readonly string get = "spGetUser";
        private readonly string insert = "spInsertUser";
        private string usernameExists = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
        private readonly string updatePassword = "UPDATE Users SET HashPassword = @HashPassword WHERE Id = @Id";
        private readonly string update = "spUpdateUser";
        private readonly string changeStatus = "UPDATE Users SET Status = @NewStatus WHERE Id = @Id";
        private readonly string delete = "spDelete";

        #endregion

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public async Task<ICollection<UserDTO>> GetAllAsync()
        {
            List<UserDTO> result = new List<UserDTO>();

            using (var conn = Connection)
            {
                result = conn.Query<UserDTO>(getAll, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        public async Task<UserDTO> GetAsync(int id)
        {
            using (var conn = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                return await conn.QueryFirstOrDefaultAsync<UserDTO>(get, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> UsernameExistsAsync(int? id, string username)
        {
            bool result = false;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Username", username);

            if(id != null)
            {
                parameters.Add("@Id", id);
            }

            using (IDbConnection conn = Connection)
            {
                result = await conn.ExecuteScalarAsync<bool>(usernameExists, parameters, commandType: CommandType.Text);
            }

            return result;
        }

        public async Task SaveAsync(string email, string username, string hashPassword, char genre)
        {
            using (var conn = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Email", email);
                parameters.Add("@Username", username);
                parameters.Add("@HashPassword", hashPassword);
                parameters.Add("@Genre", genre);

                await conn.ExecuteAsync(insert, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdatePasswordAsync(int id, string hashPassword)
        {
            using (var conn = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@HashPassword", hashPassword);

                await conn.ExecuteAsync(updatePassword, parameters, commandType: CommandType.Text);
            }
        }

        public async Task UpdateUserAsync(int id, string username, string email)
        {
            using (var conn = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@Username", username);
                parameters.Add("@Email", email);

                await conn.ExecuteAsync(update, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ChangeStatusAsync(int id, bool status)
        {
            using (var conn = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@NewStatus", status);

                await conn.ExecuteAsync(changeStatus, parameters, commandType: CommandType.Text);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                await conn.ExecuteAsync(delete, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
