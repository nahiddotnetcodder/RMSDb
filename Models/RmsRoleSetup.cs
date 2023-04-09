using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMS.Models
{
    public class RmsRoleSetup
    {
        [Key]
        public int RRSId { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Role Name")]
        public string RRSName { get; set; }
        [StringLength(250)]
        [Display(Name = "Role Description")]
        public string RRSDescription { get; set; }
        [Required]
        [Display(Name = "Current Status")]
        public bool RRSIsActive { get; set; }
        [Display(Name = "Sales")]
        public bool POSales { get; set; }
        [Display(Name = "Bill Print")]
        public bool POBillPrint { get; set; }
        [Display(Name = "Bill Receive")]
        public bool POBillReceive { get; set; }

        [Display(Name = "Daily Sales Report")]
        public bool PIDailySalesReport { get; set; }
        [Display(Name = "Date to Date Sales Report")]
        public bool PIDateToDateSalesReport { get; set; }
        [Display(Name = "Waiter wise Sales Report")]
        public bool PIWaiterWiseSalesReport { get; set; }
        [Display(Name = "Table wise Sales Report")]
        public bool PITableWiseSalesReport { get; set; }
        [Display(Name = "Item wise Sales Report")]
        public bool PIItemWiseSalesReport { get; set; }
        
        [Display(Name = "Menu Entry")]
        public bool PMMenuEntry { get; set; }
        [Display(Name = "Food Type")]
        public bool PMFoodType { get; set; }
        [Display(Name = "Kitchen Information")]
        public bool PMKitchenInformation { get; set; }
        [Display(Name = "Table Entry")]
        public bool PMTableEntry { get; set; }
        [Display(Name = "Day Close")]
        public bool PMDayClose { get; set; }

        [Display(Name = "Item Receive")]
        public bool SIItemReceive { get; set; }
        [Display(Name = "Item Issue")]
        public bool SIItemIssue { get; set; }

        [Display(Name = "Date to Date Item Receive Report")]
        public bool SIDateToDateItemReceiveReport { get; set;}
        [Display(Name = "Date to Date Item Issue Report")]
        public bool SIDateToDateItemIssueReport { get; set; }
        [Display(Name = "Item Stock")]
        public bool SIItemStock { get; set; }
        [Display(Name = "Supplier wise Item Receive Report")]
        public bool SISupplierWiseItemReciveReport { get; set; }
        [Display(Name = "Department wise Item Issue Report")]
        public bool SIDepartmentWiseItemIssueReport { get; set; }
        [Display(Name = "Item Bin Card")]
        public bool SIItemBinCard { get; set; }

        [Display(Name = "Item Generation")]
        public bool SMItemGeneration { get; set; }
        [Display(Name = "Unit")]
        public bool SMUnit { get; set; }
        [Display(Name = "Sub Category")]
        public bool SMSubCategory { get; set; }
        [Display(Name = "Store Category")]
        public bool SMStoreCategory { get; set; }
        [Display(Name ="Store Suppliers")]
        public bool SMStoreSuppliers { get; set; }
        [Display(Name = "Day Close")]
        public bool SMDayClose { get; set; }

        [Display(Name = "Employee Salary")]
        public bool HOEmployeeSalary { get; set; }
        [Display(Name = "Employee Roaster")]
        public bool HOEmployeeRoaster { get; set; }
        [Display(Name = "Employee Attendance")]
        public bool HOEmployeeAttendance { get; set; }
        [Display(Name = "Leave Details")]
        public bool HOLeaveDetails { get; set; }

        [Display(Name = "Daily Attendance Report")]
        public bool HIDailyAttendanceReport { get;set; }
        [Display(Name = "Employee wise Leave Report")]
        public bool HIEmployeeWiseLeaveReport { get; set; }
        [Display(Name = "Salary Statement")]
        public bool HISalaryStatement { get; set; }
        [Display(Name = "Pay Slip")]
        public bool HIPaySlip { get; set; }
        [Display(Name = "Roaster Report")]
        public bool HIRoasterReport { get; set; }

        [Display(Name = "Employee Details")]
        public bool HMEmployeeDetails { get; set; }
        [Display(Name = "Designation")]
        public bool HMDesignation { get; set; }
        [Display(Name = "Department")]
        public bool HMDepartment { get; set; }
        [Display(Name = "Work Status")]
        public bool HMWorkStatus { get; set; }
        [Display(Name = "Weekend")]
        public bool HMWeekend { get; set; }
        [Display(Name = "Leave Policy")]
        public bool HMLeavePolicy { get; set; }
        [Display(Name = "Salary Policy")]
        public bool HMSalaryPolicy { get; set; }
        [Display(Name = "Holidays")]
        public bool HMHolidays { get; set; }

        [Display(Name = "Payment")]
        public bool ATPayment { get; set; }
        [Display(Name = "Deposits")]
        public bool ATDeposits { get; set; }
        [Display(Name = "Bank Account Transfers")]
        public bool ATBankAccountTransfers { get; set; }
        [Display(Name = "Journal Entry")]
        public bool ATJournalEntry { get; set; }
        [Display(Name = "Recouncile Bank Account")]
        public bool ATRecouncileBankAccount { get; set; }

        [Display(Name = "Chat of Account")]
        public bool AIChatOfAccount { get; set; }
        [Display(Name = "List Of Journal Entry")]
        public bool AIListOfJournalEntry { get; set; }
        [Display(Name = "GL Account Transactions")]
        public bool AIGLAccountTransactions { get; set; }
        [Display(Name = "Annual Exp. Breakdown")]
        public bool AIAnnualExpBreakdown { get; set; }
        [Display(Name = "Balance Sheets")]
        public bool AIBalanceSheets { get; set; }
        [Display(Name = "Profit and Loss Statement")]
        public bool AIProfitAndLossStatement { get; set; }
        [Display(Name = "Trial Balance")]
        public bool AITrialBalance { get; set; }
        [Display(Name = "Audit Trail")]
        public bool AIAuditTrail { get; set; }

        [Display(Name = "Bank Accounts")]
        public bool AMBankAccounts { get; set; }
        [Display(Name = "GL Accounts")]
        public bool AMGLAccounts { get; set; }
        [Display(Name = "GL Accounts Groups")]
        public bool AMGLAccountsGroups { get; set; }
        [Display(Name = "GL Accounts Classes")]
        public bool AMGLAccountClasses { get; set; }
        [Display(Name = "GL Fiscal Year")]
        public bool AMGLFiscalYear { get; set; }

        [Display(Name = "Restaurant Setup")]
        public bool SRRestaurantSetup { get; set; }
        [Display(Name = "User Account Setup")]
        public bool SRUserAccountSetup { get; set; }
        [Display(Name = "Access Setup")]
        public bool SRAccessSetup { get; set; }
        [Display(Name = "System & GL Setup")]
        public bool SRSystemAndGLSetup { get; set; }

        [Display(Name = "Recipe Manager")]
        public bool SRecipeManager { get; set; }
        [Display(Name = "DB Backup & Download")]
        public bool SMDBBackupAndDownload { get; set; }
    }
}

