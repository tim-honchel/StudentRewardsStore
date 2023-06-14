using Dapper;
using StudentRewardsStore.Models;
using System.Data;
using System.Text;
using XSystem.Security.Cryptography;
using System.Collections.Generic;
using System;

namespace StudentRewardsStore
{
    public class AdminsRepository : IAdminsRepository
    {
        private readonly IDbConnection _conn;
        //private Random random;

        public AdminsRepository(IDbConnection conn)
        {
            _conn = conn; // connection to the MySQL admin table via Dapper
        }
        public void RegisterAdmin(Admin newAdmin) // passes in data for a new admin, encrypts the passwords, and inserts the data into the database
        {
            Random random = new Random();
            newAdmin.Salt = random.Next().ToString();
            newAdmin.Password = encryption(newAdmin.Salt, newAdmin.Unhashed);
            _conn.Execute("INSERT INTO admins (AdminID, Email, Salt, Password) VALUES (@AdminID, @Email, @Salt, @Password);", new { AdminID = newAdmin.AdminID, Email = newAdmin.Email, Salt = newAdmin.Salt, Password = newAdmin.Password });
        }
        
        public IEnumerable<Organization> ListStores(int adminID) // passes in an admin's ID and returns a list of all stores associated with that admin
        {
            return _conn.Query<Organization>("SELECT * FROM organizations LEFT JOIN admins ON organizations._AdminID = admins.AdminID WHERE organizations._AdminID = @AdminID;", new { AdminID = adminID });
        }
        public Admin CheckPassword(string email, string unhashed) // passes in an admin's email and unhashed password and returns their data if the email and encrypted password match
        {
            var salt = _conn.QuerySingle<Admin>("SELECT * FROM admins WHERE Email = @Email;", new { Email = email }).Salt;
            if (salt == null)
            {
                salt = "";
            }
            string password = encryption(salt, unhashed);
            return _conn.QuerySingle<Admin>("SELECT * FROM admins WHERE Email = @Email AND Password = @Password;", new { Email = email, Password = password });
        }
        
        public string encryption(string salt, string unhashed) // passes in an unhashed password and using XAct, encrpyts and returns it
        {
            
            
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            // Encrypt the unhashed password string
            encrypt = md5.ComputeHash(encode.GetBytes(salt + unhashed));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }
        public Admin GetAdminID(string email) // passes in an email, queries the database, and returns the data for the admin associated with that email address
        {
            return _conn.QuerySingle<Admin>("SELECT * FROM admins WHERE Email = @Email;", new { Email = email});
        }
        public void LoginAdmin()
        {
            //_conn.Execute("Update admins SET LoggedIn = 'yes', LastAction = @LastAction WHERE AdminID = @AdminID;", new { AdminID = Authentication.AdminID, LastAction = Authentication.LastAction });
        }
        public void LogoutAdmin()
        {
            //_conn.Execute("Update admins SET LoggedIn = 'no' WHERE AdminID = @AdminID;", new { AdminID = Authentication.AdminID });
        }
        public void UpdateLastAction()
        {
            _conn.Execute("Update admins SET LastAction = @LastAction WHERE AdminID = @AdminID;", new { AdminID = Authentication.AdminID, LastAction = Authentication.LastAction });
        }
    }
}