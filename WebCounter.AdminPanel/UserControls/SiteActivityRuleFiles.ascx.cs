﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using System.Data;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class SiteActivityRuleFiles : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();
        public Guid siteID = new Guid();
        protected RadAjaxManager radAjaxManager = null;
        public Access access;


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            siteID = ((LeadForceBasePage)Page).SiteId;
            Page.Title = "Файлы - LeadForce";

            access = Access.Check();

            //            gridLinks.AddNavigateUrl = UrlsData.AP_SiteActivityRuleAdd(_ruleTypeId);

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rauFile, lbAddFile);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbAddFile, gridLinks);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbAddFile, rauFile, null, UpdatePanelRenderMode.Inline);

            //RadPanelBar1.Items[0].Visible = false;
            //gridLinks.TagsControlID = "";

            gridLinks.SiteID = siteID;
            gridLinks.Where = new List<GridWhere>();
            gridLinks.Where.Add(new GridWhere { Column = "RuleTypeID", Value = ((int)RuleType.File).ToString() });
            
                var optionTagPanel =
                    ((LeadForceBasePage) Page).CurrentModuleEditionOptions.SingleOrDefault(
                        a => a.SystemName == "TagPanel");
                if (optionTagPanel == null)
                {
                    gridLinks.ShowSelectCheckboxes = false;
                    RadPanelBar1.Visible = false;
                    //leftColumn.Width = "0px";
                    //asideDiv.Visible = false;
                }
            

        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridLinks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridLinks_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (access == null)
                    access = Access.Check();

                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((HyperLink)item.FindControl("spanName")).Text = data["tbl_Links_Name"].ToString();
                ((HyperLink)item.FindControl("spanName")).NavigateUrl = string.Format("{0}/DownloadFile.aspx?id={1}", WebConfigurationManager.AppSettings["webServiceUrl"], data["tbl_Links_ID"].ToString());
                ((HyperLink)item.FindControl("spanName")).Target = "_blank";
                var fsp = new FileSystemProvider();





                ((HyperLink)item.FindControl("urlLink")).Text = String.Format("#Link.{0}#", data["tbl_Links_Code"].ToString());

                string fileSize = FileSystemProvider.GetFileSize(long.Parse(data["tbl_Links_FileSize"].ToString() != "" ? data["tbl_Links_FileSize"].ToString() : "0"));
                ((Literal)item.FindControl("lFileSize")).Text = fileSize;
                ((SiteActivityRuleFile)item.FindControl("ucFile")).FileId = (Guid)data["tbl_Links_ID"];
                ((SiteActivityRuleFile)item.FindControl("ucFile")).Bind();
                ((Literal)item.FindControl("lDescription")).Text = data["tbl_Links_Description"].ToString();

                var lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                lbEdit.CommandArgument = data["ID"].ToString();
                lbEdit.Command += new CommandEventHandler(lbEdit_OnCommand);


                var lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                lbDelete.CommandArgument = data["ID"].ToString();
                lbDelete.Command += new CommandEventHandler(lbDelete_OnCommand);
                lbDelete.Visible = access.Delete;
            }
        }


        protected void uploadedFile_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            lbAddFile.Visible = true;
        }

        protected void lbAddFile_OnClick(object sender, EventArgs e)
        {
            var fsp = new FileSystemProvider();
            if (rauFile.UploadedFiles.Count > 0)
            {
                string fileName = null;
                if (rauFile.UploadedFiles.Count > 0)
                {
                    //fileName = fsp.Upload(CurrentUser.Instance.SiteID, "Links",
                    //                      rauFile.UploadedFiles[0].FileName, rauFile.UploadedFiles[0].InputStream,
                    //                      FileType.Attachment);
                    IFileProvider fileProvider = new FSProvider();
                    fileName = fileProvider.GetFilename(siteID, rauFile.UploadedFiles[0].FileName);
                    fsp.Upload(siteID, fileName, rauFile.UploadedFiles[0].InputStream);

                }
                var file = new tbl_Links();
                file.SiteID = siteID;
                file.Name = fileName;
                file.RuleTypeID = (int)RuleType.File;
                file.URL = fileName;
                file.FileSize = rauFile.UploadedFiles[0].InputStream.Length;
                file.Description = txtDescription.Text;
                file.OwnerID = CurrentUser.Instance.ContactID;
                string code = String.Format("file_[{0}]_[{1}]", DateTime.Now.ToString("ddMMyyyy"), DateTime.Now.ToString("mmss"));
                int maxCode = _dataManager.Links.SelectByCode(siteID, code);
                if (maxCode != 0) maxCode++;
                file.Code = code + (maxCode != 0 ? String.Format("[{0}]", maxCode >= 10 ? maxCode.ToString() : "0" + maxCode.ToString()) : "");

                _dataManager.Links.Add(file);
                txtDescription.Text = "";
                gridLinks.Rebind();
            }
        }

        protected void lbEdit_OnCommand(object sender, CommandEventArgs e)
        {
            ((LinkButton)sender).Parent.FindControl("ucFile").Visible = true;
        }

        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {
            _dataManager.Links.DeleteByID(Guid.Parse(e.CommandArgument.ToString()));
            gridLinks.Rebind();
        }

    }

}