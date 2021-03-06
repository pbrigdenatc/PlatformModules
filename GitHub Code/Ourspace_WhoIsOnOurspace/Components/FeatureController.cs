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

namespace DotNetNuke.Modules.Ourspace_WhoIsOnOurspace.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_WhoIsOnOurspace
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

            //List<Ourspace_WhoIsOnOurspaceInfo> colOurspace_WhoIsOnOurspaces = GetOurspace_WhoIsOnOurspaces(ModuleID);
            //if (colOurspace_WhoIsOnOurspaces.Count != 0)
            //{
            //    strXML += "<Ourspace_WhoIsOnOurspaces>";

            //    foreach (Ourspace_WhoIsOnOurspaceInfo objOurspace_WhoIsOnOurspace in colOurspace_WhoIsOnOurspaces)
            //    {
            //        strXML += "<Ourspace_WhoIsOnOurspace>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_WhoIsOnOurspace.Content) + "</content>";
            //        strXML += "</Ourspace_WhoIsOnOurspace>";
            //    }
            //    strXML += "</Ourspace_WhoIsOnOurspaces>";
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
            //XmlNode xmlOurspace_WhoIsOnOurspaces = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_WhoIsOnOurspaces");
            //foreach (XmlNode xmlOurspace_WhoIsOnOurspace in xmlOurspace_WhoIsOnOurspaces.SelectNodes("Ourspace_WhoIsOnOurspace"))
            //{
            //    Ourspace_WhoIsOnOurspaceInfo objOurspace_WhoIsOnOurspace = new Ourspace_WhoIsOnOurspaceInfo();
            //    objOurspace_WhoIsOnOurspace.ModuleId = ModuleID;
            //    objOurspace_WhoIsOnOurspace.Content = xmlOurspace_WhoIsOnOurspace.SelectSingleNode("content").InnerText;
            //    objOurspace_WhoIsOnOurspace.CreatedByUser = UserID;
            //    AddOurspace_WhoIsOnOurspace(objOurspace_WhoIsOnOurspace);
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

            //List<Ourspace_WhoIsOnOurspaceInfo> colOurspace_WhoIsOnOurspaces = GetOurspace_WhoIsOnOurspaces(ModInfo.ModuleID);

            //foreach (Ourspace_WhoIsOnOurspaceInfo objOurspace_WhoIsOnOurspace in colOurspace_WhoIsOnOurspaces)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_WhoIsOnOurspace.Content, objOurspace_WhoIsOnOurspace.CreatedByUser, objOurspace_WhoIsOnOurspace.CreatedDate, ModInfo.ModuleID, objOurspace_WhoIsOnOurspace.ItemId.ToString(), objOurspace_WhoIsOnOurspace.Content, "ItemId=" + objOurspace_WhoIsOnOurspace.ItemId.ToString());
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
