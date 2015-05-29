﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CubeReportingModule.Admin
{
    public partial class AddTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindTablesToList();
        }

        private void BindTablesToList()
        {
            string[] tableList = {"stringOne", "stringTwo"};
            FromClause.DataSource = tableList;
            FromClause.DataBind();
        }
    }
}