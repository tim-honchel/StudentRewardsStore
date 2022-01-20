﻿using Dapper;
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

        public AdminsRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public void RegisterAdmin(Admin newAdmin)
        {
            newAdmin.Password = encryption(newAdmin.Unhashed);
            _conn.Execute("INSERT INTO admins (ID, Email, Password) VALUES (@ID, @Email, @Password);", new { ID = newAdmin.ID, Email = newAdmin.Email, Password = newAdmin.Password });
        }
        public Admin LoginAdmin(string email, string unhashed)
        {
           string password = encryption(unhashed);
           return _conn.QuerySingle<Admin>("SELECT * FROM admins WHERE Email = @Email AND Password = @Password;", new { Email = email, Password = password });  
        }
        public IEnumerable<Organization> LoginComplex(string email, string unhashed)
        {
            string password = encryption(unhashed);
            //return _conn.Query<Organization>("SELECT * FROM organizations LEFT JOIN admins ON organizations.AdminID = admins.ID WHERE organizations.Email = @Email AND organizations.Password = @Password;", new { Email = email, Password = password });
            return _conn.Query<Organization>("SELECT * FROM organizations");
        }
        public string encryption(string unhashed)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            // Encrypt the unhashed password string
            encrypt = md5.ComputeHash(encode.GetBytes(unhashed));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }
    }
}