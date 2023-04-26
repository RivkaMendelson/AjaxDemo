using System;
using System.Data.SqlClient;

namespace AjaxDemo.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PeopleRepo
    {
        private string _connectionString;

        public PeopleRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            var list = new List<Person>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]

                });
            }

            return list;
        }

        public void Add(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
                "VALUES (@first, @last, @age)";
            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE from People WHERE Id=@id";
            cmd.Parameters.AddWithValue("@id",id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void Edit(Person p)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE People SET FirstName=@firstname, LastName=@lastname, Age=@age WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", p.Id);
            cmd.Parameters.AddWithValue("@firstname", p.FirstName);
            cmd.Parameters.AddWithValue("@lastname", p.LastName);
            cmd.Parameters.AddWithValue("@age", p.Age);

            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
