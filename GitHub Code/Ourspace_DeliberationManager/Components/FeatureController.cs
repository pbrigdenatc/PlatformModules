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

namespace DotNetNuke.Modules.Ourspace_DeliberationManager.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_DeliberationManager
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

            //List<Ourspace_DeliberationManagerInfo> colOurspace_DeliberationManagers = GetOurspace_DeliberationManagers(ModuleID);
            //if (colOurspace_DeliberationManagers.Count != 0)
            //{
            //    strXML += "<Ourspace_DeliberationManagers>";

            //    foreach (Ourspace_DeliberationManagerInfo objOurspace_DeliberationManager in colOurspace_DeliberationManagers)
            //    {
            //        strXML += "<Ourspace_DeliberationManager>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_DeliberationManager.Content) + "</content>";
            //        strXML += "</Ourspace_DeliberationManager>";
            //    }
            //    strXML += "</Ourspace_DeliberationManagers>";
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
            //XmlNode xmlOurspace_DeliberationManagers = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_DeliberationManagers");
            //foreach (XmlNode xmlOurspace_DeliberationManager in xmlOurspace_DeliberationManagers.SelectNodes("Ourspace_DeliberationManager"))
            //{
            //    Ourspace_DeliberationManagerInfo objOurspace_DeliberationManager = new Ourspace_DeliberationManagerInfo();
            //    objOurspace_DeliberationManager.ModuleId = ModuleID;
            //    objOurspace_DeliberationManager.Content = xmlOurspace_DeliberationManager.SelectSingleNode("content").InnerText;
            //    objOurspace_DeliberationManager.CreatedByUser = UserID;
            //    AddOurspace_DeliberationManager(objOurspace_DeliberationManager);
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

            //List<Ourspace_DeliberationManagerInfo> colOurspace_DeliberationManagers = GetOurspace_DeliberationManagers(ModInfo.ModuleID);

            //foreach (Ourspace_DeliberationManagerInfo objOurspace_DeliberationManager in colOurspace_DeliberationManagers)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_DeliberationManager.Content, objOurspace_DeliberationManager.CreatedByUser, objOurspace_DeliberationManager.CreatedDate, ModInfo.ModuleID, objOurspace_DeliberationManager.ItemId.ToString(), objOurspace_DeliberationManager.Content, "ItemId=" + objOurspace_DeliberationManager.ItemId.ToString());
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
