/*
' Copyright (c) 2010 DotNetNuke Corporation
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System.Collections.Generic;
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace DotNetNuke.Modules.Ourspace_TextEditor.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_TextEditor
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class FeatureController : IPortable, ISearchable, IUpgradeable
    {

        #region Public Methods



        #endregion

        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        public string ExportModule(int ModuleID)
        {
            //string strXML = "";

            //List<Ourspace_TextEditorInfo> colOurspace_TextEditors = GetOurspace_TextEditors(ModuleID);
            //if (colOurspace_TextEditors.Count != 0)
            //{
            //    strXML += "<Ourspace_TextEditors>";

            //    foreach (Ourspace_TextEditorInfo objOurspace_TextEditor in colOurspace_TextEditors)
            //    {
            //        strXML += "<Ourspace_TextEditor>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_TextEditor.Content) + "</content>";
            //        strXML += "</Ourspace_TextEditor>";
            //    }
            //    strXML += "</Ourspace_TextEditors>";
            //}

            //return strXML;

            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            //XmlNode xmlOurspace_TextEditors = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_TextEditors");
            //foreach (XmlNode xmlOurspace_TextEditor in xmlOurspace_TextEditors.SelectNodes("Ourspace_TextEditor"))
            //{
            //    Ourspace_TextEditorInfo objOurspace_TextEditor = new Ourspace_TextEditorInfo();
            //    objOurspace_TextEditor.ModuleId = ModuleID;
            //    objOurspace_TextEditor.Content = xmlOurspace_TextEditor.SelectSingleNode("content").InnerText;
            //    objOurspace_TextEditor.CreatedByUser = UserID;
            //    AddOurspace_TextEditor(objOurspace_TextEditor);
            //}

            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        {
            //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

            //List<Ourspace_TextEditorInfo> colOurspace_TextEditors = GetOurspace_TextEditors(ModInfo.ModuleID);

            //foreach (Ourspace_TextEditorInfo objOurspace_TextEditor in colOurspace_TextEditors)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_TextEditor.Content, objOurspace_TextEditor.CreatedByUser, objOurspace_TextEditor.CreatedDate, ModInfo.ModuleID, objOurspace_TextEditor.ItemId.ToString(), objOurspace_TextEditor.Content, "ItemId=" + objOurspace_TextEditor.ItemId.ToString());
            //    SearchItemCollection.Add(SearchItem);
            //}

            //return SearchItemCollection;

            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        public string UpgradeModule(string Version)
        {
            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        #endregion

    }

}
