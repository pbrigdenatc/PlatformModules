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

namespace DotNetNuke.Modules.Ourspace_ResultsManager.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_ResultsManager
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

            //List<Ourspace_ResultsManagerInfo> colOurspace_ResultsManagers = GetOurspace_ResultsManagers(ModuleID);
            //if (colOurspace_ResultsManagers.Count != 0)
            //{
            //    strXML += "<Ourspace_ResultsManagers>";

            //    foreach (Ourspace_ResultsManagerInfo objOurspace_ResultsManager in colOurspace_ResultsManagers)
            //    {
            //        strXML += "<Ourspace_ResultsManager>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_ResultsManager.Content) + "</content>";
            //        strXML += "</Ourspace_ResultsManager>";
            //    }
            //    strXML += "</Ourspace_ResultsManagers>";
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
            //XmlNode xmlOurspace_ResultsManagers = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_ResultsManagers");
            //foreach (XmlNode xmlOurspace_ResultsManager in xmlOurspace_ResultsManagers.SelectNodes("Ourspace_ResultsManager"))
            //{
            //    Ourspace_ResultsManagerInfo objOurspace_ResultsManager = new Ourspace_ResultsManagerInfo();
            //    objOurspace_ResultsManager.ModuleId = ModuleID;
            //    objOurspace_ResultsManager.Content = xmlOurspace_ResultsManager.SelectSingleNode("content").InnerText;
            //    objOurspace_ResultsManager.CreatedByUser = UserID;
            //    AddOurspace_ResultsManager(objOurspace_ResultsManager);
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

            //List<Ourspace_ResultsManagerInfo> colOurspace_ResultsManagers = GetOurspace_ResultsManagers(ModInfo.ModuleID);

            //foreach (Ourspace_ResultsManagerInfo objOurspace_ResultsManager in colOurspace_ResultsManagers)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_ResultsManager.Content, objOurspace_ResultsManager.CreatedByUser, objOurspace_ResultsManager.CreatedDate, ModInfo.ModuleID, objOurspace_ResultsManager.ItemId.ToString(), objOurspace_ResultsManager.Content, "ItemId=" + objOurspace_ResultsManager.ItemId.ToString());
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
