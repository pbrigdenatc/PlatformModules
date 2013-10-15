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

namespace DotNetNuke.Modules.Ourspace_Dashboard.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_Dashboard
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

            //List<Ourspace_DashboardInfo> colOurspace_Dashboards = GetOurspace_Dashboards(ModuleID);
            //if (colOurspace_Dashboards.Count != 0)
            //{
            //    strXML += "<Ourspace_Dashboards>";

            //    foreach (Ourspace_DashboardInfo objOurspace_Dashboard in colOurspace_Dashboards)
            //    {
            //        strXML += "<Ourspace_Dashboard>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_Dashboard.Content) + "</content>";
            //        strXML += "</Ourspace_Dashboard>";
            //    }
            //    strXML += "</Ourspace_Dashboards>";
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
            //XmlNode xmlOurspace_Dashboards = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_Dashboards");
            //foreach (XmlNode xmlOurspace_Dashboard in xmlOurspace_Dashboards.SelectNodes("Ourspace_Dashboard"))
            //{
            //    Ourspace_DashboardInfo objOurspace_Dashboard = new Ourspace_DashboardInfo();
            //    objOurspace_Dashboard.ModuleId = ModuleID;
            //    objOurspace_Dashboard.Content = xmlOurspace_Dashboard.SelectSingleNode("content").InnerText;
            //    objOurspace_Dashboard.CreatedByUser = UserID;
            //    AddOurspace_Dashboard(objOurspace_Dashboard);
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

            //List<Ourspace_DashboardInfo> colOurspace_Dashboards = GetOurspace_Dashboards(ModInfo.ModuleID);

            //foreach (Ourspace_DashboardInfo objOurspace_Dashboard in colOurspace_Dashboards)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_Dashboard.Content, objOurspace_Dashboard.CreatedByUser, objOurspace_Dashboard.CreatedDate, ModInfo.ModuleID, objOurspace_Dashboard.ItemId.ToString(), objOurspace_Dashboard.Content, "ItemId=" + objOurspace_Dashboard.ItemId.ToString());
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
