﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CubeReportingModule.Models;
using System.Web.Security;
using System.Diagnostics;

namespace CubeReportingModule.Pages
{
    public partial class ManageReports : System.Web.UI.Page
    {
        Repository repo = new Repository();

        protected void Page_Load(object sender, EventArgs e)
        {
            //MembershipUser currentUser = Membership.GetUser();
            //string username = currentUser.UserName;

            //if ((Roles.IsUserInRole("Admin")) || (Roles.IsUserInRole("SysAdmin")))
            //{
            //    Display.AutoGenerateDeleteButton = true;
            //}
        }

        public bool SetModifyVisibility(string creator)
        {
            if (creator == null)
            {
                return false;
            }

            string currentUsername = Membership.GetUser().UserName;

            bool visible = creator.Equals(currentUsername);

            return visible;
        }

        public bool SetDeleteVisibility()
        {
            if ((Roles.IsUserInRole("Admin")) || (Roles.IsUserInRole("SysAdmin")))
            {
                return true;
            }

            return false;
        }

        public IQueryable<Report> GetReportsAsQuery()
        {
            IQueryable<Report> allReports = repo.Reports.AsQueryable<Report>().OrderBy(report => report.Name);
            return allReports;
        }

        public IEnumerable<Report> GetReports()
        {
            IEnumerable<Report> allReports = repo.Reports.OrderBy(report => report.Name);
            return allReports;
        }

        protected void Display_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Display.PageIndex = e.NewPageIndex;
            DataBind();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void Display_UpdateItem(int id)
        {
            Debug.WriteLine("Id: " + id);
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void Display_DeleteItem(int id)
        {

        }

        protected void Modify_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int reportId = Convert.ToInt32(Server.HtmlDecode(button.CommandArgument));
            Display_UpdateItem(reportId);
        }

        protected void Delete_Click(object sender, EventArgs e)
        {

        }
    }
}