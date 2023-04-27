using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using to_do;

namespace to_do
{
    internal class connection
    {
        string connectionString = "server=localhost;user id=root;password=1234;database=deneme";

        // MySQL veritabanı bağlantısı oluşturmak
        MySqlConnection connection = new MySqlConnection(connectionString);

try
{
    // Bağlantıyı açmak
    connection.Open();

    // Bağlantı açıldıktan sonra yapılacak işlemler buraya yazılır.

}
catch (Exception ex)
{
    // Bağlantı açılamazsa hata mesajı göster
    Console.WriteLine("Bağlantı hatası: " + ex.Message);
}
finally
{
    // Bağlantıyı kapatmak
    connection.Close();
}
    }
}




class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
        SqlConnection connection = CreateConnection(connectionString);
        connection.Open();
        DoSomething(connection);
        connection.Close();
    }

    static SqlConnection CreateConnection(string connectionString)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        return connection;
    }

    static void DoSomething(SqlConnection connection)
    {
        // Do something with the connection here
    }
}

/*
//MySqlCommand command = new MySqlCommand(queryString, connection);
//connection.Open();
//MySqlDataReader reader = command.ExecuteReader();
//while (reader.Read())
//{
//    DateTime myDateTime = (DateTime)reader["myDateTime"]; // Veriyi DateTime tipine dönüştürün
//    string formattedDateTime = myDateTime.ToString("dd/MM/yyyy hh:mm:ss"); // Veriyi istediğiniz formata dönüştürün
//    Console.WriteLine(formattedDateTime); // Dönüştürülmüş veriyi ekrana yazdırın
//}
*/


INSERT INTO ogrenci (tarih_saat) VALUES(CONVERT(DATETIME, '2023-04-07 13:45:00', 120));



class Program
{
    static void Main()
    {
        string connectionString = "Data Source=ServerName;Initial Catalog=DatabaseName;Integrated Security=True";
        string queryString = "SELECT * FROM TableName";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0}, {1}", reader[0], reader[1]));
            }
            reader.Close();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "server=localhost;user id=root;password=yourPassword;database=yourDatabase";
        MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            Console.WriteLine("MySQL version : {0}", connection.ServerVersion);
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
