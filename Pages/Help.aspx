<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="CubeReportingModule.Pages.Help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <a name="Top"></a>
    <h1 dir="ltr">Table of Contents</h1>

    <ol>
        <li><a href="#Intro">Introduction </a></li>
        <li><a href="#UserAccounts">Chapter 1: User Accounts and Logging In </a>

            <ol>
                <li><a href="#LoggingIn">Logging In </a></li>
                <li><a href="#ResetPassword">Resetting Your Password </a></li>
            </ol>
        </li>

        <li><a href="#UserProfiles">Chapter 2: User Profiles </a>
            <ol>
                <li><a href="#Profile">Accessing Your Profile </a></li>
                <li><a href="#Email">Updating Your Email </a></li>
                <li><a href="#Password">Changing Your Password </a></li>
            </ol>
        </li>
        <li><a href="#Reports">Chapter 3: Reports </a>
            <ol>
                <li><a href="#GenerateReport">Generating a Report </a></li>
                <li><a href="#CreateReport">Creating a Report Template </a></li>
                <li><a href="#Events">Scheduling a Report </a></li>
            </ol>
        </li>
        <li><a href="#Admin">Chapter 4: Administration </a>
            <ol>
                <li><a href="#UserRoles">User Roles </a></li>
                <li><a href="#Users">Creating/Deleting a User </a></li>
                <li><a href="#AssignRoles">Assigning a Role to a User </a></li>
                <li><a href="#LockingUsers">Locking/Unlocking a User Account </a></li>
                <li><a href="#AdminResetPassword">Resetting a User Password </a></li>
                <li><a href="#Logs">Viewing Access Logs </a></li>
                <li><a href="#SystemRecovery">System Recovery </a></li>
            </ol>
        </li>
    </ol>

    <br />

    <a name="Intro"></a>
    <h1 dir="ltr">Introduction</h1>
    <p>The Cube Reporting Application is designed to provide reporting functionality to users of the GAIN Inventory Control system. It was created for Cube Global Storage as a means of extending their existing reporting system.</p>
    <p>This application comes installed with several existing report templates, all of which can be customized further. In addition, users can create any number of new templates provided they are familiar with the GAIN database conventions. These reports can be run on command, or can be scheduled to run at a specified interval. All reports are output as either HTML or Excel files, and can be saved directly to the client computer or emailed to any number of users.</p>
    <p>The purpose of this manual is to train users familiar with the GAIN system how to utilize all of the functionality of the Cube Reporting Application. Each chapter deals with one section of the application, and all tasks within that section are explained in a step-by-step guide. Screenshots are used where necessary to make instructions clear. The final section of this guide deals with admin accounts, and is only applicable if you are using a designated Admin or SysAdmin account.</p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="UserAccounts"></a>
    <h1 dir="ltr">Chapter 1: User Accounts and Logging In</h1>
    <p>All users will require a user account to log in and use the application. Before proceeding you will require a username and password which will be provided to you by your Admin. If you do not have a username please contact your Admin before continuing.</p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="LoggingIn"></a>
    <h2 dir="ltr">Logging In</h2>

    <h3>Step 1:</h3>
    <p>Open your web browser and enter the following URL into the address bar:</p>
    <p>http://XXXX/Pages/Login.aspx</p>
    <p>After the page loads, you should see the login screen.</p>
    <img src="../Resources/manual/Login.png" />
    <p>Figure 1.0 - Login Screen</p>
    <p><i>Note: This application is only accessible from within Cube. If your browser does not load the login page ensure you are connected to the local private network. If the problem persists, contact your Admin.</i></p>

    <h3>Step 2:</h3>
    <p>Enter your provided username and password into the appropriate boxes, and press ‘enter’ or click the ‘Log In’ button. This will bring you to the menu screen. </p>
    <p><i>Note: It is strongly recommended that you change your password after logging in for the first time, to protect your account. See <a href="#Password">Chapter 2</a>, for steps on how to change your password.</i></p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="ResetPassword"></a>
    <h2 dir="ltr">Resetting Your Password</h2>

    <p>If you have forgotten your password you can reset it from the login page.</p>
    <h3>Step 1:</h3>
    <p>Click the “Forgotten Your Password?” link below the ‘Log In’ button.</p>
    <img src="../Resources/manual/ForgottenPassword.png"/>
    <p>Figure 1.1 - Link to reset your password</p>

    <h3>Step 2:</h3>
    <p>On the next screen, enter your username into the textbox and click “Submit”.</p>

    <h3>Step 3:</h3>
    <p>On the next screen you are presented with your chosen security question. Enter your answer into the textbox and click “Submit”. Your password will now be mailed to the email address associated with your account. Check your inbox within the next 10 minutes for your new password.</p>
    <img src="../Resources/manual/ForgottenPassword2.png"/>
    <p>Figure 1.3 - Security question page</p>
    <p><i>Note: You should always change your password after you reset it. Application generated passwords are designed to be complex for security reasons, but this makes them very hard to remember. If you must write your password down, never leave it near your work station, such as in a drawer or attached to the screen.</i></p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="UserProfiles"></a>
    <h1 dir="ltr">Chapter 2: User Profiles</h1>

    <p>Every user has a profile associated with their account. The profile keeps track of the user’s information, such as username and email address, and also important system data, such as when they last logged in, and if their account is locked. This chapter will deal with viewing and editing your profile.</p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="Profile"></a>
    <h2 dir="ltr">Accessing Your Profile</h2>

    <p>You can access your user profile from any screen other than the Login page. Be aware that if you leave a page such as the Create Report page, you will lose any data you have already entered.</p>
    <p>To access your profile, click the “Profile” button located in the upper right corner of the screen.</p>
    <img src="../Resources/manual/ProfileButton.png" />
    <p>Figure 2.1 - Button to access your user profile</p>
    <p>This will bring you to the Profile page.</p>
    <img src="../Resources/manual/ProfileScreen.png" />
    <p>Figure 2.2 - User Profile page</p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="Email"></a>
    <h2 dir="ltr">Updating Your Email</h2>

    <h3>Step 1:</h3>
    <p>Enter the new email address in the “New Email Address” box. Ensure it is correct, as this is the only way for you to reset your password.</p>
    <h3>Step 2:</h3>
    <p>Enter your password in the “Current Password” box, and click “Save Changes. You should see a status message appear at the top of the page indicating the change was successful. You should also see the new email address reflected in the User Profile box. If the update was not successful, check the status message for details as to why and try again. Refer to <a href="#Figure2.3">figure 2.3</a> for assistance.</p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="Password"></a>
    <h2 dir="ltr">Changing Your Password</h2>

    <h3>Step 1:</h3>
    <p>Enter your new password in the “New Password” box. Enter the same password into the “Re-Type Password” box.</p>
    <p>Note: Make sure you remember your new password! If you forget it, you will need to reset it using the Password Reset tool. Refer to <a href="#ResetPassword">resetting your password</a> for details.</p>

    <h3>Step 2:</h3>
    <p>Enter your password in the “Current Password” box, and click “Save Changes. You should see a status message appear at the top of the page indicating the change was successful. If the update was not successful, check the status message for details as to why and try again. Refer to <a href="#Figure2.3">figure 2.3</a> for assistance.</p>
    <a name="Figure2.3"></a>
    <img src="../Resources/manual/ProfileUpdate.png" />
    <p>Figure 2.3 - Updating a user profile</p>
    <p><i>Note: It is possible to change both your password and email address at the same time. Simply fill out all boxes on the page and click “Save Changes”. Be careful though, if one of the changes fail, the other can still succeed. Always read the status message carefully to make sure all changes were successful.</i></p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="Reports"></a>
    <h1 dir="ltr">Chapter 3: Reports</h1>

    <p>The core functionality of this application is the ability to generate reports from the GAIN database. This chapter will cover how to run existing reports that have been created by users, create your own report templates, and schedule reports to be run automatically.</p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="GenerateReport"></a>
    <h2 dir="ltr">Generating a Report</h2>

    <p>All reports are accessed through the main menu. The column on the right side of the page is a list of every report template currently in the system.</p>

    <h3>Step 1:</h3>
    <p>From the main menu, click on the button corresponding to the report you wish to generate. This will bring you to the Report Options page for that report.</p>

    <h3>Step 2:</h3>
    <p>Set any restrictions you wish to use by modifying the appropriate controls. If you do not wish to use any restrictions, leave everything as it is. Click “Generate Report”. </p>
    <img src="../Resources/manual/ReportOptions.png" />
    <p>Figure 3.1 - A report options page with all default values</p>
    <p><i>Note: Each report option represents a restriction on the data you wish to be included in the report. By default, there are no restrictions in place. So for example, in the “Generate Empty Spaces” report, if you wish to only see locations with at least 5 empty spots, you would set the “Available Space” counters to 4 and 9999.</i></p>

    <h3>Step 3:</h3>
    <p>You are now viewing a preview of the report in your browser. If you want to sort by one of the columns, click on the blue column name once for descending order, and again for ascending order.</p>

    <h3>Step 4:</h3>
    <p>To save the report as an Excel file, click the “Save to File” button. This saves to your default downloads folder. If you wish to email the report to a user, click on the “Email to User” button. You will be prompted for the email address to send it to. After both of these actions you should see a status message appear at the top of the page confirming that they were successful.</p>
    <img src="../Resources/manual/ReportDisplay.png" />
    <p>Figure 3.2 - An example report preview </p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="CreateReport"></a>
    <h2 dir="ltr">Creating a Report Template</h2>

    <p>Every report you can run is based off of a template. This determines what data is retrieved when you run the report, as well as what restrictions you can place on the data when you run it. </p>

    <h3>Step 1:</h3>
    <p>From the main menu, click on “Create Report”. This will bring you to the Create New Report page.</p>

    <h3>Step 2:</h3>
    <p>Choose a name for your new report template, and enter it in the “Report Name” box.</p>

    <h3>Step 3:</h3>
    <p>Select the table you wish to run the report on, and click “Next”.</p>
    <p><i>Note: Creating report templates requires some knowledge of the GAIN database and its structure. If you are having trouble, contact your Administrator.</i></p>

    <h3>Step 4:</h3>
    <p>Select the columns you wish to include in the report from the chosen table, and click “Next”.</p>

    <h3>Step 5:</h3>
    <p>Now you may add options. An option is something that users can choose to use when they run the report. Each option limits what data appears in the report. To add an option, first name it something descriptive by typing the name into the “Label” box. Next, select which column the option will relate to. Next, choose the “Option Type”. This is what kind of control will be used to change the option. Finally select the comparator to use and the default value. For help with options, see the note below.</p>
    <p><i>Note: An example report option. We have created a new report template for the Barcode table which shows who last updated each barcode. The columns then, are “Barcode”, “Last_Update”, and “Last_By”. But we have a problem. Some entries have a generic Last_By entered as “user” and we don’t always want those entries clogging up our report. So we will add a new option. First call it “Filter User”. We choose the column “Last_By”, option type “Text”, condition “!=” (not equal to), and finally leave initial value blank. Now when we run this report, all we need to do is type “user” into the options text box and it will remove all entries that have “user” in the “Last_By” column!</i></p>

    <h3>Step 6:</h3>
    <p>A restriction is similar to an option, but is always included when the report is run. It is not chosen by the user when they run the report, this also applies to column attributes. To add a restriction first choose a column to use for comparison. Then choose a comparator to use and enter a value to be used for comparison.</p>
    <p><i>Note: All blank values will be automatically removed from each column when the report is displayed. If you don’t want this to happen simply change the attributes of the column in question and check either the “Allow Nulls” or “Allow Empty Strings” checkboxes.</i></p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="Events"></a>
    <h2 dir="ltr">Scheduling a Report</h2>

    <h3>Step 1:</h3>
    <p>From the main menu, click “Modify Scheduled Events”. This will bring you to the Scheduled Events page. Here you will see all currently scheduled reports.</p>
    <img src="../Resources/manual/ScheduledEventsMain.png" />
    <p>Figure 3.3 - Current scheduled reports</p>

    <h3>Step 2:</h3>
    <p>To schedule a new report, click “Add Event”. A new menu will appear below the list of scheduled reports.</p>

    <h3>Step 3:</h3>
    <p>First select which report to run from the dropdown menu. Next choose how often to run the report. So for a weekly report you would choose 7 days. Finally add one or more recipients to the list of people who will receive the report.Simply enter their email address in the appropriate box and click “Add Email Recipient”.</p>
    <img src="../Resources/manual/ScheduledEventsAdd.png" />
    <p>Figure 3.4 - Adding a new scheduled report</p>

    <h3>Step 5:</h3>
    <p>When you are happy with your selections, click “Save” You should immediately see the new scheduled report appear in the list above. </p>

    <h3>Step 6:</h3>
    <p>To delete a scheduled report, simply click the “Delete” button next to the report.</p>
    <p><i>Note: You can only delete scheduled reports belonging to yourself or if you are logged in as an admin account.</i></p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="Admin"></a>
    <h1 dir="ltr">Chapter 4: Administration</h1>

    <p>The following chapter deals with administration of the application itself. Only users with an Admin or SysAdmin account can access these pages and functions.</p>
    <p>If you are logged in as an Admin or SysAdmin, on the main menu you will see an additional panel at the bottom of the screen. All admin functions are accessed from here.</p>
    <p><i>Note: There are several Roles used by the application to verify if a user is authorized to do something. They are referenced throughout this section of the manual. This is a short summary of the different Roles:</i></p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="UserRoles"></a>
    <h2 dir="ltr">User Roles</h2>

    <p>Basic User - A standard user account. Typically most employees will have this Role.</p>
    <p>Admin - Admin accounts can create new users, reset user passwords and lock user accounts. They can access the Role management page, but cannot assign Admin status.</p>
    <p>SysAdmin - There is only a single SysAdmin account, named “deus”. This account cannot be deleted. The SysAdmin Role cannot be granted or revoked. It has full access to the application, including being able to create and delete any user, assign and revoke Admin status, and lock any user account, including Admins.</p>
    <p>Customer - This Role is not currently used by the application. It is included to allow Cube Global Storage to expand the application for client use. It currently has the same privileges as a Basic User.</p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="Users"></a>
    <h2 dir="ltr">Creating/Deleting a User</h2>

    <p>Users cannot register for an account on their own, they must have one created for them by an Admin or SysAdmin.</p>

    <h3>Step 1:</h3>
    <p>From the main menu, click the “Add/Remove Users” button located on the admin panel. This will bring you to the Add User page.</p>
    <img src="../Resources/manual/CreateUser.png" />
    <p>Figure 4.1 - Creating a new user</p>

    <h3>Step 2:</h3>
    <p>Enter the desired user name into the “User Name” box. Next enter the desired password into the “Password” box, and re-type the same password into the “Confirm Password” box. Passwords must be at least 8 characters long.</p>

    <h3>Step 3:</h3>
    <p>Enter a valid email address for the user, then enter a meaningful security question for the user, along with an answer. Click “Create User”. You will get a status message indicating if the creation was successful.</p>
    <p>To delete a user, simply select their name from the dropdown menu at the bottom of the page and click “Remove”.</p>
    <p><i>Note: After logging in, the user should immediately change their password and security question/answer for security purposes.</i></p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="AssignRoles"></a>
    <h2 dir="ltr">Assigning a Role to a User</h2>

    <h3>Step 1:</h3>
    <p>From the main menu, click the “Manage Roles” button located on the admin panel. This will bring you to the Manage Roles page.</p>
    <img src="../Resources/manual/ManageRoles.png" />
    <p>Figure 4.2 - Manage Roles page</p>

    <h3>Step 2:</h3>
    <p>Select the user you wish to modify from the drop down menu. The radio button indicating their current role will become selected.</p>

    <h3>Step 3: </h3>
    <p>Click the radio button corresponding to the role you wish to assign the user. A status message should appear indicating the user’s new role.</p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="LockingUsers"></a>
    <h2 dir="ltr">Locking/Unlocking a User Account</h2>

    <p>Once a user account is locked, that user will be unable to sign in until the lock is lifted. An account is automatically locked if the user enters an incorrect password too many times, and can also be manually locked by an admin.</p>

    <h3>Step 1:</h3>
    <p>From the main menu, click the “User Security” button located on the admin panel. This will bring you to the Security page.</p>
    <img src="../Resources/manual/UserSecurity.png" />
    <p>Figure 4.3 - User Security page</p>

    <h3>Step 2:</h3>
    <p>Select the user you wish to lock or unlock from the dropdown menu on the left. The user details panel on the right side will update with that user’s information.</p>

    <h3>Step 3:</h3>
    <p>Click either the “Unlock Account” or the “Lock Account” button.</p>
    <p><i>Note: If the button is disabled, that means the user account is already in that state. You can see the user’s current state by selecting them from the drop down and looking at the “Suspended" entry in the user details pane.</i></p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="AdminResetPassword"></a>
    <h2 dir="ltr">Resetting a User Password</h2>

    <p>An admin is capable of resetting a user’s password for them in the event of the account being compromised. This generates a new, random password and emails it to the user directly.</p>

    <h3>Step 1:</h3>
    <p>From the main menu, click the “User Security” button located on the admin panel. This will bring you to the Security page.</p>

    <h3>Step 2:</h3>
    <p>Select the user whose password you wish to reset from the dropdown menu on the left. The user details panel on the right side will update with that user’s information.</p>

    <h3>Step 3:</h3>
    <p>Click the “Reset Password” button. A status message will appear confirming that a new password has been sent to the user.</p>
    <p><i>Note: The user should change their password immediately upon logging in, for security purposes. If their account was compromised, they should also change their security question and answer.</i></p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="Logs"></a>
    <h2 dir="ltr">Viewing Access Logs</h2>

    <p>This application keeps logs of activities of all users for security purposes. If you suspect a user account is compromised, viewing the log files can give you valuable information about which account and what they have done.</p>

    <h3>Step 1:</h3>
    <p>From the main menu, click the “View Access Logs” button located on the admin panel. This will bring you to the View Logs page.</p>

    <h3>Step 2: </h3>
    <p>The access log table has several important columns. You can sort the table by clicking on the column title:</p>
    <p>User - Which user performed the action.</p>
    <p>Action - What the user did.</p>
    <p>Date - The timestamp of when the action was performed.</p>
    <img src="../Resources/manual/AccessLogs.png" />
    <p>Figure 4.4 - Viewing the access logs</p>
    <p><i>Note: Admins should review the logs regularly to catch any intrusions as soon as possible. Things to watch for include users accessing pages they do not have access to, users performing actions after closing hours, and users performing actions without logging in first.</i></p>

    <a href="#Top"><h3>Back to top</h3></a>
    <br />

    <a name="SystemRecovery"></a>
    <h2 dir="ltr">System Recovery</h2>

    <p>In the event that the SysAdmin account is compromised, follow these steps to recover it.</p>
    <p><i>Note: Before starting this process, ensure the application is shut down completely on the server machine. Also ensure all clients disconnect their sessions.</i></p>

    <h3>Step 1:</h3>
    <p>Using any SQL client program, connect to the GAIN database. Ask your network administrator if you are unsure how to do this.</p>
    <p><i>Note: The following screenshots are taken using HeidiSQL, a free SQL client. Different clients will have different interfaces.</i></p>

    <h3>Step 2: </h3>
    <p>Locate the table named aspnet_Users, and view the Data in the table. Find the user named “deus” and write down the first 6-8 characters of their “UserId”.</p>
    <img src="../Resources/manual/Recovery1.png" />
    <p>Figure 4.5 - Finding the UserId of “deus”</p>

    <h3>Step 3:</h3>
    <p>Now find the table named aspnet_Membership. Locate the entry whose UserId matches that of the “deus” account. Delete that entry.</p>
    <img src="../Resources/manual/Recovery2.png" />
    <p>Figure 4.5 - Deleting the “deus” user from aspnet_Membership</p>

    <h3>Step 4:</h3>
    <p>Perform step 3 again, but using the aspnet_ UsersInRoles table.</p>

    <h3>Step 5: </h3>
    <p>Return to the aspnet_Users table. Delete the entry for “deus”. Save all changes.</p>

    <h3>Step 6:</h3>
    <p>Restart the application. Log in using the username “deus” and the default password. If you do not know the default password, contact the system administrator. Immediately follow the steps for changing your password located in <a href="#Password">Chapter 2</a>.</p>

    <a href="#Top"><h3>Back to top</h3></a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
