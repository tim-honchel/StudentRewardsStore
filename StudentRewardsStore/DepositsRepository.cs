using Dapper;
using StudentRewardsStore.Models;
using System.Data;
using System;
using System.Collections.Generic;

namespace StudentRewardsStore
{
    public class DepositsRepository : IDepositsRepository
    {
        private readonly IDbConnection _conn;
        public DepositsRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Deposit> ShowAllDeposits(int storeID)
        {
            return _conn.Query<Deposit>("SELECT * FROM deposits LEFT JOIN students ON deposits._Student_ID = students.StudentID WHERE deposits._Organization_ID = @OrganizationID;", new { OrganizationID = storeID });
        }
        public Deposit ViewDeposit(int depositID)
        {
            return _conn.QuerySingle<Deposit>("SELECT * FROM deposits LEFT JOIN students ON deposits._Student_ID = students.StudentID WHERE deposits.DepositID = @DepositID;", new { DepositID = depositID });
        }
        public void UpdateDeposit(Deposit deposit)
        {
            var originalDeposit = _conn.QuerySingle<Deposit>("SELECT * FROM deposits LEFT JOIN students ON deposits._Student_ID = students.StudentID WHERE deposits.DepositID = @DepositID;", new { DepositID = deposit.DepositID });
            _conn.Execute("UPDATE deposits SET Date = @Date, Amount = @Amount WHERE DepositID = @DepositID", new { Date = deposit.Date, Amount = deposit.Amount, DepositID = deposit.DepositID });
            if (originalDeposit.Amount != deposit.Amount)
            {
                var studentToUpdate = _conn.QuerySingle<Student>("SELECT * FROM students WHERE StudentID = @StudentID;", new { StudentID = deposit._Student_ID }); // retrieves the student so funds can be added to their balance
                studentToUpdate.Balance += deposit.Amount - originalDeposit.Amount;
                _conn.Execute("UPDATE students SET Balance = @Balance WHERE StudentID = @StudentID;", new { Balance = studentToUpdate.Balance, StudentID = studentToUpdate.StudentID });
            }
        }
        public void AddDeposit(Deposit deposit)
        {
            _conn.Execute("INSERT INTO deposits (DepositID, Date, Amount, _Student_ID, _Organization_ID) VALUES (@DepositID, @Date, @Amount, @StudentID, @OrganizationID);", new { DepositID = deposit.DepositID, Date = deposit.Date, Amount = deposit.Amount, StudentID = deposit._Student_ID, OrganizationID = deposit._Organization_ID });
            var studentToUpdate = _conn.QuerySingle<Student>("SELECT * FROM students WHERE StudentID = @StudentID;", new { StudentID = deposit._Student_ID }); // retrieves the student so funds can be added to their balance
            studentToUpdate.Balance += deposit.Amount;
            _conn.Execute("UPDATE students SET Balance = @Balance WHERE StudentID = @StudentID;", new { Balance = studentToUpdate.Balance, StudentID = studentToUpdate.StudentID });
        }
    }
}