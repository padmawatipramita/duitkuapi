using Binus.WS.Pattern.Entities;
using DuitKu.API.Model;
using DuitKu.API.Output;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DuitKu.API.Helper
{
    public class _UserHelper
    {
        public static List<UserDetail> GetAllUser()
        {
            var returnValue = new List<UserDetail>();

            try
            {
                // get all user
                foreach (var dataUser in EntityHelper.Get<_UserModel>().ToList())
                {
                    var income = EntityHelper.Get<_TransactionModel>().Where(x => 
                        x.UserID == dataUser.UserID && x.TransactionType == "Income").Select(x => 
                        x.Balance).ToList().Sum();

                    var expense = EntityHelper.Get<_TransactionModel>().Where(x => 
                        x.UserID == dataUser.UserID && x.TransactionType == "Expense").Select(x => 
                        x.Balance).ToList().Sum();

                    UserDetail newUser = new UserDetail();

                    newUser.UserEmail = dataUser.UserEmail;
                    newUser.UserName = dataUser.UserName;
                    newUser.UserBalance = (int)dataUser.UserBalance;
                    newUser.UserFinalBalance = (int)dataUser.UserBalance + (income - expense);

                    returnValue.Add(newUser);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return returnValue;
        }

        /*
        Modified by Ariel Sefrian
        Date: Senin, 07/02/2022 - 21:31 WIB
        Purpose: added throw new exception if the email already exists
        */

        public static int AddNewUser(_UserModel data)
        {
            try
            {
                // add new user
                foreach (var dataUser in EntityHelper.Get<_UserModel>().Where(x => x.UserEmail.Equals(data.UserEmail)) .ToList())
                {
                    throw new Exception("Email already exists!");
                }

                EntityHelper.Add(new _UserModel()
                {
                    UserName = data.UserName,
                    UserEmail = data.UserEmail,
                    UserPassword = data.UserPassword,
                    UserBalance = data.UserBalance,
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return 1;
        }

        /*
        Modified by Ariel Sefrian
        Date: Senin, 07/02/2022 - 21:41 WIB
        Purpose: added throw new exception if the email already exists
        */

        public static int ChangePersonalData(_UserModel data)
        {
            var returnValue = new _UserModel();
            var userModel = EntityHelper.Get<_UserModel>().ToList();

            try
            {
                // update personal data
                returnValue = (
                    from um in userModel.Where(dataRow => dataRow.UserID == data.UserID)
                    select new _UserModel()
                    {
                        UserID = um.UserID,
                        UserName = um.UserName,
                        UserEmail = um.UserEmail,
                        UserPassword = um.UserPassword,
                        UserBalance = um.UserBalance,
                    }).FirstOrDefault();

                if (returnValue == null)
                {
                    throw new Exception("User ID not found!");
                }

                foreach (var dataUser in EntityHelper.Get<_UserModel>().Where(x => x.UserEmail.Equals(data.UserEmail)).ToList())
                {
                    throw new Exception("Email already exists!");
                }

                EntityHelper.Update(new _UserModel()
                {
                    UserID = data.UserID,
                    UserName = data.UserName ?? returnValue.UserName,
                    UserEmail = data.UserEmail ?? returnValue.UserEmail,
                    UserPassword = data.UserPassword ?? returnValue.UserPassword,
                    UserBalance = data.UserBalance ?? returnValue.UserBalance,
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return 1;
        }

        /*
        
        Modified by Padmawati Pramita
        Date: Minggu, 06/02/2022 - 18.00 WITA
        Purpose: Adding income, expenses calculation for Final Balance

        Modified by Ariel Sefrian
        Date: Minggu, 06/02/2022 - 22:41 WIB
        Purpose: fix the getSpecificUser codes in _UserHelper

       
        */
        public static List<SpecificUserDetail> GetSpecificUserById(int? UserIDParam)
        {
            var returnValue = new List<SpecificUserDetail>();
            var userModel = EntityHelper.Get<_UserModel>().ToList();

            try
            {
                // get specific user by userID
                var income = EntityHelper.Get<_TransactionModel>().Where(
                    x => x.UserID == UserIDParam && x.TransactionType == "Income").Select(
                    x => x.Balance).ToList().Sum();

                var expense = EntityHelper.Get<_TransactionModel>().Where(
                    x => x.UserID == UserIDParam && x.TransactionType == "Expense").Select(
                    x => x.Balance).ToList().Sum();

                returnValue = (
                   from um in userModel.Where(dataRow => dataRow.UserID == UserIDParam)
                   select new SpecificUserDetail
                   {
                       UserID = um.UserID,
                       UserName = um.UserName,
                       UserEmail = um.UserEmail,
                       Password = um.UserPassword,
                       UserBalance = (int)um.UserBalance,
                       UserFinalBalance = (int)um.UserBalance + (income - expense),
                   }).ToList();

                if (returnValue.Capacity.Equals(0))
                {
                    throw new Exception("Account not found!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return returnValue;
        }

        /*
        Modified by Ariel Sefrian
        Date: Senin, 07/02/2022 - 21:11 WIB
        Purpose: added getSpecificUserByEmail
        */

        public static List<SpecificUserDetail> GetSpecificUserByEmail(String Email)
        {
            var returnValue = new List<SpecificUserDetail>();
            var userModel = EntityHelper.Get<_UserModel>().ToList();
            var income = 0;
            var expense = 0;

            try
            {
                // get specific user by email
                foreach (var dataUser in EntityHelper.Get<_UserModel>().Where(x => x.UserEmail.Equals(Email)) .ToList())
                {
                    income = EntityHelper.Get<_TransactionModel>().Where(x =>
                        x.UserID == dataUser.UserID && x.TransactionType == "Income").Select(x =>
                        x.Balance).ToList().Sum();

                    expense = EntityHelper.Get<_TransactionModel>().Where(x =>
                        x.UserID == dataUser.UserID && x.TransactionType == "Expense").Select(x =>
                        x.Balance).ToList().Sum();
                }

                returnValue = (
                from um in userModel.Where(dataRow => dataRow.UserEmail == Email)
                select new SpecificUserDetail
                {
                    UserID = um.UserID,
                    UserName = um.UserName,
                    UserEmail = um.UserEmail,
                    Password = um.UserPassword,
                    UserBalance = (int)um.UserBalance,
                    UserFinalBalance = (int)um.UserBalance + (income - expense),
                }).ToList();

                if (returnValue.Capacity.Equals(0))
                {
                    throw new Exception("Account not found!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return returnValue;
        }
    }
}