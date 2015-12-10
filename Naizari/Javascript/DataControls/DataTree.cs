/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Naizari.Extensions;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using Naizari.Logging;
using Naizari.Javascript.JsonControls;
using Naizari.Helpers;
using Naizari.Test;
using Naizari.Data.Access;
using Naizari.Data.Common;
using Naizari.Helpers.Web;
using Naizari.Javascript.JsonControls.Exceptions;

[assembly: TagPrefix("Naizari.Javascript.DataControls", "data")]
namespace Naizari.Javascript.DataControls
{
    public class AddResult
    {
        public enum StatusCode
        {
            None,
            Success,
            ItemExists,
            Error
        }

        public AddResult() { }

        public AddResult(bool success)
        {
            this.Success = success;
        }

        public AddResult(bool success, string message)
            : this(success)
        {
            this.Message = message;
        }

        public string Message { get; set; }
        public bool Success { get; set; }
        public StatusCode Code { get; set; }
    }

    public class AddRequest
    {
        public AddRequest() { }

        public string TreeName { get; set; }
        public int BranchID { get; set; }
        public NodeTypeEnum Type { get; set; }
        public string Text { get; set; }

        public bool IsValid
        {
            get
            {
                if (BranchID == 0)
                    return Type != NodeTypeEnum.Invalid && !string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(TreeName);
                else
                    return BranchID > -1 && Type != NodeTypeEnum.Invalid && !string.IsNullOrEmpty(Text);
            }
        }

        public string Value { get; set; }
        public override string ToString()
        {
            return string.Format("BranchID: {0}, Type: {1}, Text: {2}", this.BranchID, this.Type.ToString(), this.Text);
        }
    }

    [JsonProxy("dataTree")]
    public class DataTree: DataControl
    {
        Doodad tree;
        public DataTree(): base()
        {
            this.AutoRegisterScript = true;
            this.controlToRender.TagName = "div";
            this.CrossSessionMode = CrossSessionMode.Application;
            this.AddRequiredScript("naizari.javascript.jsui.jquery_cookie.js");
            this.AddRequiredScript("naizari.javascript.jsui.datatree.util.js");
            //this.OnFolderDrop = "function(el){ }";
            this.folderMenuItems = new List<ContextMenuItem>();
            this.fileMenuItems = new List<ContextMenuItem>();
            this.FolderContextMenuItems = 
                "renameFolder:Rename;" +
                "newFolder:New Folder;" +
                "newFile:New File;" +
                "deleteFolder:Delete Folder;";
            this.FileContextMenuItems =
                "renameFile:Rename;" +
                "deleteFile:Delete;";
            this.DeleteFolderPrompt = "";    
        }
 
        /// <summary>
        /// The text to show when deleting a folder.  If this is null or empty
        /// no prompt will be shown before deleting a folder.
        /// </summary>
        public string DeleteFolderPrompt
        {
            get;
            set;
        }

        public override void WireScriptsAndValidate()
        {
            Expect.IsNotNullOrEmpty(this.jsonId, "JsonId must be explicitly set: " + this.ToString());
            this.DomId = this.JsonId;
            DatabaseAgent agent = GetAgent();
            this.tree = Doodad.GetDoodad(this.JsonId, agent);

            this.OnFolderOptionClicked = "JSUI." + this.JsonId + ".folderAction"; // in datatree.util.js
            this.OnFileOptionClicked = "JSUI." + this.JsonId + ".fileAction";
            base.WireScriptsAndValidate();
        }

 
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            
            this.controlToRender.Attributes.Add("ID", this.DomId);
            this.controlToRender.Attributes.Add("name", this.JsonId);
            this.controlToRender.Controls.Add(this.GetHtmlTreeFrom(this.tree));
            this.controlToRender.RenderControl(writer);

            if (this.BackingDoodad.Folders.Length > 0)
            {
                HtmlGenericControl folderContextMenu = ControlHelper.CreateBulletList<ContextMenuItem>(this.folderMenuItems.ToArray(),
                    (item) =>
                    {
                        return CreateContextMenuItem(item);
                    });
                folderContextMenu.Attributes["id"] = this.JsonId + "_foldermenu";
                folderContextMenu.RenderControl(writer);
            }

            if (this.BackingDoodad.Files.Length > 0)
            {
                HtmlGenericControl fileContextMenu = ControlHelper.CreateBulletList<ContextMenuItem>(this.fileMenuItems.ToArray(),
                    (item) =>
                    {
                        return CreateContextMenuItem(item);
                    });
                fileContextMenu.Attributes["id"] = this.JsonId + "_filemenu";
                fileContextMenu.RenderControl(writer);
            }

            if (this.RenderScripts)
                this.RenderConglomerateScript(writer);
        }

        private static HtmlGenericControl CreateContextMenuItem(ContextMenuItem item)
        {
            HtmlGenericControl li = ControlHelper.CreateControl("li", "");
            HtmlGenericControl a = ControlHelper.CreateControl("a", item.Text);
            a.Attributes.Add("href", "#" + item.Action);
            li.Controls.Add(a);
            return li;
        }

        public string OnFolderOptionClicked
        {
            get;
            set;
        }

        public string OnFileOptionClicked
        {
            get;
            set;
        }

        List<ContextMenuItem> folderMenuItems;
        public string FolderContextMenuItems
        {
            get
            {
                return StringExtensions.ToDelimited<ContextMenuItem>(folderMenuItems.ToArray(),
                    (item) =>
                    {
                        return item.Action + ":" + item.Text + ";";
                    });
            }
            set
            {
                this.folderMenuItems.Clear();
                this.folderMenuItems.AddRange(ToContextMenuItems(value));
            }
        }

        private static ContextMenuItem[] ToContextMenuItems(string value)
        {
            return value.Split<ContextMenuItem>(";", (item) =>
            {
                ContextMenuItem ctxItem = item.ToObject<ContextMenuItem>((c) =>
                {
                    string[] values = c.Split(':');
                    Expect.IsTrue(values.Length == 2, "Invalid ContextMenuItem string specified.");
                    return new ContextMenuItem(values[1], values[0]);
                });
                return ctxItem;
            });
        }

        List<ContextMenuItem> fileMenuItems;
        public string FileContextMenuItems
        {
            get
            {
                return StringExtensions.ToDelimited<ContextMenuItem>(fileMenuItems.ToArray(),
                    (item) =>
                    {
                        return item.Action + ":" + item.Text + ";";
                    });
            }
            set
            {
                this.fileMenuItems.Clear();
                this.fileMenuItems.AddRange(ToContextMenuItems(value));
            }
        }

        private HtmlGenericControl GetHtmlTreeFrom(Doodad tree)
        {
            return GetHtmlTreeFrom(tree, this.JsonId);
        }

        [JsonMethod]
        public override string GetHtml(string jsonId)
        {
            Doodad doodad = Doodad.GetDoodad(jsonId, this.GetAgent());
            return ControlHelper.GetRenderedString(GetHtmlTreeFrom(doodad, jsonId));
        }

        [JsonMethod(Verbs.POST)]
        public void MoveTo(int movingId, int targetFolderId)
        {
            Node movingNode = Node.SelectById(movingId, this.GetAgent());
            Expect.IsNotNull(movingNode, "The specified node to move was not found: nodeid(" + movingId.ToString() + ")");
            movingNode.MoveTo((long)targetFolderId);
        }

        [JsonMethod(Verbs.POST)]
        public void RenameNode(int toRenameId, string newName)
        {
            Node toRename = Node.SelectById(toRenameId, this.GetAgent());
            Expect.IsNotNull(toRename, "The specified node to rename was not found: nodeid(" + toRenameId.ToString() + ")");
            toRename.Rename(newName);
        }

        [JsonMethod(Verbs.POST)]
        public AddResult NewFile(int parentId, string text, string dbdata)
        {
            AddResult result = new AddResult();
            result.Success = true;
            result.Code = AddResult.StatusCode.Success;
            try
            {
                Node parent = Node.SelectById(parentId, this.GetAgent());
                Expect.IsNotNull(parent, "The specified parent node was not found: nodeid(" + parentId.ToString() + ")");
                parent.AddFile(text, dbdata);
            }
            catch (NodeNameAlreadyExistsException nnaee)
            {
                result.Success = false;
                result.Code = AddResult.StatusCode.ItemExists;
                result.Message = nnaee.Message;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
#if DEBUG
                result.Message += "\r\n" + ex.StackTrace;
#endif
            }

            return result;
        }

        [JsonMethod(Verbs.POST)]
        public AddResult NewFolder(int parentId, string text, string dbdata)
        {
            AddResult result = new AddResult();
            result.Success = true;
            result.Code = AddResult.StatusCode.Success;
            try
            {
                Node parent = Node.SelectById(parentId, this.GetAgent());
                Expect.IsNotNull(parent, "The specified parent node was not found: nodeid(" + parentId.ToString() + ")");
                parent.AddFolder(text, dbdata);
            }
            catch (NodeNameAlreadyExistsException nnaee)
            {
                result.Success = false;
                result.Code = AddResult.StatusCode.ItemExists;
                result.Message = nnaee.Message;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
#if DEBUG
                result.Message += "\r\n" + ex.StackTrace;
#endif
            }

            return result;
        }

        [JsonMethod(Verbs.POST)]
        public void DeleteFolder(int folderId)
        {
            Node folder = Node.SelectById(folderId, this.GetAgent());
            Expect.IsNotNull(folder, "The specified folder was not found: nodeid(" + folderId.ToString() + ")");
            folder.DeleteNode();
        }

        [JsonMethod(Verbs.POST)]
        public void DeleteFile(int fileId)
        {
            Node file= Node.SelectById(fileId, this.GetAgent());
            Expect.IsNotNull(file, "The specified file was not found: nodeid(" + fileId.ToString() + ")");
            file.DeleteNode();
        }

        public static HtmlGenericControl GetHtmlTreeFrom(Doodad tree, string jsonId)
        {
            HtmlGenericControl topLevelUL = new HtmlGenericControl("ul");
            topLevelUL.Attributes.Add("class", "filetree");
            topLevelUL.Style.Add("display", "none");
            topLevelUL.Attributes.Add("jsonid", jsonId);
            foreach (Node node in tree.Folders)
            {
                topLevelUL.Controls.Add(GetHtmlFolderNode(node, jsonId));
            }

            foreach (Node node in tree.Files)
            {
                topLevelUL.Controls.Add(GetHtmlFileNode(node));
            }

            return topLevelUL;
        }

        public static HtmlGenericControl GetHtmlFolderNode(Node folder, string jsonId)
        {
            Expect.IsTrue(folder.NodeTypeEnum == NodeTypeEnum.Folder, "Specified node was not a folder");

            HtmlGenericControl folderNode = new HtmlGenericControl("li");
            HtmlGenericControl textSpan = new HtmlGenericControl("span");
            textSpan.Attributes.Add("dataid", folder.ID.ToString());
            textSpan.Attributes.Add("id", jsonId + folder.ID);
            HtmlImage image = new HtmlImage();
            image.Src = "images/folder.gif";
            image.Style.Add("display", "none");
            image.Style.Add("margin-right", "3px");
            textSpan.Controls.Add(image);

            HtmlGenericControl subitemUl = new HtmlGenericControl("ul");
            textSpan.InnerText = folder.Text;
            textSpan.Attributes.Add("class", "folder");
            folderNode.Controls.Add(textSpan);
            if (folder.ChildFolders.Length > 0 || folder.ChildFiles.Length > 0)
            {
                folderNode.Controls.Add(subitemUl);
                foreach (Node childFolder in folder.ChildFolders)
                {
                    subitemUl.Controls.Add(GetHtmlFolderNode(childFolder, jsonId));
                }

                foreach (Node childFile in folder.ChildFiles)
                {
                    subitemUl.Controls.Add(GetHtmlFileNode(childFile));
                }
            }
            return folderNode;
        }

        private static HtmlGenericControl GetHtmlFileNode(Node file)
        {
            Expect.IsTrue(file.NodeTypeEnum == NodeTypeEnum.File, "Specified node is not a file");
            Expect.IsNotNull(file, "file Node not specified");
            HtmlGenericControl fileNode = new HtmlGenericControl("li");
            if (file.Data != null)
            {
                try
                {
                    fileNode.Attributes.Add("userdata", Convert.ToString(file.Data));
                }
                catch (Exception ex)
                {
                    LogManager.CurrentLog.AddEntry("An error occurred reading data from nodeid {0}.", ex, file.ID.ToString());
                }
            }

            HtmlGenericControl textSpan = new HtmlGenericControl("span");
            textSpan.Attributes.Add("dataid", file.ID.ToString());
            HtmlImage image = new HtmlImage();
            image.Src = "images/file.gif";
            image.Style.Add("display", "none");
            image.Style.Add("margin-right", "3px");
            textSpan.Controls.Add(image);
            textSpan.Controls.Add(new LiteralControl(file.Text));
            textSpan.Attributes.Add("class", "file");
            fileNode.Controls.Add(textSpan);

            return fileNode;
        }
    }
}
