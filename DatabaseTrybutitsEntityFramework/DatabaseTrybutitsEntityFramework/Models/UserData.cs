using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DatabaseTrybutitsEntityFramework.Models
{
    public partial class UserData
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
    }
    public class UserContext : DbContext
    {
        public DbSet<UserData> UserDatas { get; set; }
    }
    public class getRegex
    {
        public static Regex GetRegex()
        {
        Regex regex = new Regex("(CREATE TABLE)|(DROP TABLE)|(DROP INDEX)|(CREATE INDEX)|(ALTER TABLE)|(ALTER DATABASE)|(CREATE DATABASE)|(INSERT INTO)|( )|(INNER JOIN)|(OUTER JOIN)|(UPDATE)|(;)|(-)|(')|(')", RegexOptions.IgnoreCase);
            return regex;
        }
    }
}
