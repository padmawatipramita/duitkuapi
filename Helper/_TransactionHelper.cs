using Binus.WS.Pattern.Entities;
using DuitKu.API.Model;
using DuitKu.API.Output;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DuitKu.API.Helper
{
    public class _TransactionHelper
    {
     /*
        Modified by Padmawati Pramita 
        Date: Minggu, 06/02/2022 - 16:00 WITA
        Purpose: Add new transaction 
     */
        public static int AddNewTransaction(_TransactionModel data)
        {
            try
            {
                // add new transactions
                _TransactionModel newData = new _TransactionModel();

                newData.Balance = data.Balance;
                newData.Date = data.Date;
                newData.Notes = data.Notes;
                newData.UserID = data.UserID;
                newData.TransactionType = data.TransactionType;
                EntityHelper.Add < _TransactionModel>(newData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return 1;
        }

        /*
           Modified by Padmawati Pramita 
           Date: Minggu, 06/02/2022 - 18:00 WITA
           Purpose: Get all transaction 
        */

        public static List<Transaction> GetAllTransaction()
        {
            var returnValue = new List<Transaction>();
            try
            {
                // get all transactions
                var Transaction = EntityHelper.Get<_TransactionModel>().ToList();
                foreach (var dataTransaction in Transaction)
                {
                    Transaction newTrans = new Transaction();

                    newTrans.Date = dataTransaction.Date;
                    newTrans.Balance = dataTransaction.Balance;
                    newTrans.TransactionType = dataTransaction.TransactionType;
                    newTrans.Notes = dataTransaction.Notes;

                    returnValue.Add(newTrans); // Mendapatkan semua data
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return returnValue;
        }

        /*
           Modified by Padmawati Pramita 
           Date: Rabu, 09/02/2022 - 22:00 WITA
           Purpose: Get current month transaction

           Modified by Ariel Sefrian 
           Date: Jumat, 11/02/2022 - 22:04 WIB
           Purpose: added passing parameter 
        */

        public static List<Transaction> GetCurrentMonthTransaction(_TransactionModel data)
        {
            var returnValue = new List<Transaction>();
            try
            {
                // get all transactions
                var Transaction = EntityHelper.Get<_TransactionModel>().Where(x => x.UserID == data.UserID && x.Date.Month.Equals(DateTime.Now.Month)).ToList();
                foreach (var dataTransaction in Transaction)
                {
                    Transaction newTrans = new Transaction();

                    newTrans.Date = dataTransaction.Date;
                    newTrans.Balance = dataTransaction.Balance;
                    newTrans.TransactionType = dataTransaction.TransactionType;
                    newTrans.Notes = dataTransaction.Notes;

                    returnValue.Add(newTrans); // Mendapatkan semua data
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return returnValue;
        }

        /*
          Modified by Padmawati Pramita 
          Date: Rabu, 09/02/2022 - 22:00 WITA
          Purpose: Get spesific transaction based on month 
        */

        public static List<Transaction> GetTransactionMonth(_TransactionModel data)
        {
            var returnValue = new List<Transaction>();
            try
            {
                var Transaction = EntityHelper.Get<_TransactionModel>().Where(x => x.UserID == data.UserID && x.Date.Month == data.TransactionID && x.TransactionType == data.TransactionType).ToList();
               
                foreach (var dataTransaction in Transaction)
                {
                    Transaction newTrans = new Transaction();

                    newTrans.Date = dataTransaction.Date;
                    newTrans.Balance = dataTransaction.Balance;
                    newTrans.TransactionType = dataTransaction.TransactionType;
                    newTrans.Notes = dataTransaction.Notes;

                    returnValue.Add(newTrans); // Mendapatkan semua data
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
           Date: Sabtu, 12/02/2022 - 11:21 WIB
           Purpose: added GetAllTransactionById
        */

        public static List<Transaction> GetAllTransactionById(int? UserIDParam)
        {
            var returnValue = new List<Transaction>();
            try
            {
                var Transaction = EntityHelper.Get<_TransactionModel>().Where(
                    x => x.UserID == UserIDParam).ToList();
                foreach (var dataTransaction in Transaction)
                {
                    Transaction newTrans = new Transaction();

                    newTrans.Date = dataTransaction.Date;
                    newTrans.Balance = dataTransaction.Balance;
                    newTrans.TransactionType = dataTransaction.TransactionType;
                    newTrans.Notes = dataTransaction.Notes;

                    returnValue.Add(newTrans);
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