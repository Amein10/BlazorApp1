using System.Data.SqlClient;
using System.Data;

namespace BlazorApp1.Data
{
    public class Sql
    {
        public static string connectionString = "Data Source=192.168.1.3; Database=MemeDB;User ID=sa;Password=Passw0rd;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static List<Anime> Read()
        {
            List<Anime> list = new List<Anime>();
            SqlConnection con = new(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM AnimeTable", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Anime anime = new Anime() { Id = dr.GetInt32(0), name = dr.GetString(1), url = dr.GetString(2) };
                list.Add(anime);
            }
            con.Close();

            return list;
        }

        public static void Create(Anime anime)
        {
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("INSERT INTO AnimeTable (name, url) VALUES (@name, @animeurl)", conn);
                cmd.Parameters.AddWithValue("@name", anime.name);
                cmd.Parameters.Add("@animeurl", SqlDbType.NVarChar).Value = anime.url;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void Delete(int id)
        {
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("DELETE FROM AnimeTable WHERE Id = @id", conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
