using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools;
using System.Threading;
using System.Windows.Forms.Integration;

namespace EmailMRU
{  
    public partial class ThisAddIn
    {
        #region Members
        private CustomTaskPane taskPane;
        private ElementHostContainer container;
        private Outlook.Explorer ex;
        private EmailList emailList = new EmailList();
        
        private bool selectedEmail = false;

        private string paneName = "Emails";
        #endregion


        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {            
            foreach(Outlook.Explorer explorer in Application.Explorers)
            {
                var mailitem = explorer.Selection[1] as Outlook.MailItem;
                if (mailitem != null)
                {
                    ex = explorer;
                    explorer.SelectionChange += Explorer_SelectionChange;
                }
            }
            
            //Create WPF list control
            var wpfMailListControl = new MailListControl(new Action<Email>(SelectEmail), new Action<Email>(OpenEmail));
            wpfMailListControl.DataContext = emailList;
            
            //House the list in an element host so we can add it to a taskpane - this is because taskpanes do not
            //support WPF controls by default
            ElementHost eh = new ElementHost() { Child = wpfMailListControl };
            eh.Dock = System.Windows.Forms.DockStyle.Fill;
            
            //Create a container and add the wpf control
            container = new ElementHostContainer();
            container.Controls.Add(eh);

            //Add the container to the taskpane
            taskPane = Globals.ThisAddIn.CustomTaskPanes.Add(container, paneName);
            taskPane.Visible = true;
            taskPane.Width = 175;
            taskPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
        }        

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            ex.SelectionChange -= Explorer_SelectionChange;
            ex = null;
        }

        #region Outlook Event Handlers
        void Explorer_SelectionChange()
        {
            //if this selection change event was caused by a click in the taskpane, ignore it
            if (selectedEmail == true)
            {
                selectedEmail = false;
                return;
            }

            //Only want the case where there is 1 item selected
            if (ex.Selection.Count == 1)
            {
                //make sure it is a mailitem that is selected
                var mailitem = ex.Selection[ex.Selection.Count] as Outlook.MailItem;
                if (mailitem != null)
                {
                    //add the newly selected email to the list
                    emailList.Add(mailitem);
                }
            }
        }
        #endregion

        #region Taskpane Click Events Handlers
        private void SelectEmail(Email email)
        {
            selectedEmail = true;
            ex.ClearSelection();
            if(ex.IsItemSelectableInView(email.mail))
                ex.AddToSelection(email.mail);
        }

        private void OpenEmail(Email email)
        {
            selectedEmail = true;
            if (ex.IsItemSelectableInView(email.mail))
                ((Outlook.MailItem)ex.Session.GetItemFromID(email.ID)).Display();
        }
        #endregion

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
