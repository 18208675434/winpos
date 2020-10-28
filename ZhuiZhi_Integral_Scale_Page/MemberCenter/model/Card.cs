using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.BatchSaleCardUI.Model
{
    public class Card
    {
        decimal balance;

        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        decimal depositbalance;

        public decimal Depositbalance
        {
            get { return depositbalance; }
            set { depositbalance = value; }
        }
        string memberid;

        public string Memberid
        {
            get { return memberid; }
            set { memberid = value; }
        }
        string newcardid;

        public string Newcardid
        {
            get { return newcardid; }
            set { newcardid = value; }
        }
        string oldcardid;

        public string Oldcardid
        {
            get { return oldcardid; }
            set { oldcardid = value; }
        }
        string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        decimal rewardbalance;

        public decimal Rewardbalance
        {
            get { return rewardbalance; }
            set { rewardbalance = value; }
        }
        string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        string tenantid;

        public string Tenantid
        {
            get { return tenantid; }
            set { tenantid = value; }
        }

    }
}
