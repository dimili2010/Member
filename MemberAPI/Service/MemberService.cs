using System.Data;
using System.Data.SqlClient;
using MemberAPI.Configuration;
using MemberAPI.Models;
using Microsoft.Extensions.Options;

namespace MemberAPI.Service
{
    public class MemberService(IOptions<ConnectionStringOptions> connectionString) : IMemberService
    {
        private readonly ConnectionStringOptions _connectionString = connectionString.Value;

        private async Task<SqlConnection> CreateAndOpenConnectionAsync()
        {
            SqlConnection connection = new SqlConnection(_connectionString.DefaultConnection.ToString());
            await connection.OpenAsync();
            return connection;
        }

        public async Task<bool> DeleteMember(int memberId)
        {
            try
            {
                using SqlConnection connection = await CreateAndOpenConnectionAsync();

                using var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MemberDelete";

                command.Parameters.AddWithValue("@MemberId", memberId);

                await command.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Member> GetMemberById(int id)
        {
            Member member = new();
            try
            {
                using SqlConnection connection = await CreateAndOpenConnectionAsync();

                using SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MemberSelectById";

                command.Parameters.AddWithValue("@MemberId", id);

                using SqlDataReader reader = await command.ExecuteReaderAsync();
                member.MemberId = await reader.ReadAsync() ? id : 0;
                if (member.MemberId != 0)
                {
                    member.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                    member.Email = reader.GetString(reader.GetOrdinal("Email"));
                    member.Gender = reader.GetString(reader.GetOrdinal("Gender"));
                }
            }
            catch (Exception)
            {
                member.MemberId = 0;
            }
            return member;
        }

        public async Task<IEnumerable<Member>> GetMembers()
        {
            var members = new List<Member>();
            try
            {
                using SqlConnection connection = await CreateAndOpenConnectionAsync();

                using SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MemberSelect";

                using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Member member = new()
                    {
                        MemberId = reader.GetInt32(reader.GetOrdinal("MemberId")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Gender = reader.GetString(reader.GetOrdinal("Gender"))
                    };
                    members.Add(member);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return members;
        }

        public async Task<Member> InsertMember(Member member)
        {
            try
            {
                using SqlConnection connection = await CreateAndOpenConnectionAsync();

                using SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MemberInsert";

                command.Parameters.AddWithValue("@FullName", member.FullName);
                command.Parameters.AddWithValue("@Email", member.Email);
                command.Parameters.AddWithValue("@Gender", member.Gender);

                command.Parameters.Add(new SqlParameter("@MemberId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                });

                await command.ExecuteNonQueryAsync();

                member.MemberId = (int)command.Parameters["@MemberId"].Value;
            }
            catch (Exception)
            {
                member.MemberId = 0;
            }
            return member;
        }

        public async Task<Member> UpdateMember(Member member)
        {
            try
            {
                using SqlConnection connection = await CreateAndOpenConnectionAsync();

                using SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MemberUpdate";

                command.Parameters.AddWithValue("@MemberId", member.MemberId);
                command.Parameters.AddWithValue("@FullName", member.FullName);
                command.Parameters.AddWithValue("@Email", member.Email);
                command.Parameters.AddWithValue("@Gender", member.Gender);

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                member.MemberId = 0;
            }
            return member;
        }
    }
}
