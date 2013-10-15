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

namespace DotNetNuke.Modules.Ourspace_Statistics.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_Statistics
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

            //List<Ourspace_StatisticsInfo> colOurspace_Statisticss = GetOurspace_Statisticss(ModuleID);
            //if (colOurspace_Statisticss.Count != 0)
            //{
            //    strXML += "<Ourspace_Statisticss>";

            //    foreach (Ourspace_StatisticsInfo objOurspace_Statistics in colOurspace_Statisticss)
            //    {
            //        strXML += "<Ourspace_Statistics>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_Statistics.Content) + "</content>";
            //        strXML += "</Ourspace_Statistics>";
            //    }
            //    strXML += "</Ourspace_Statisticss>";
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
            //XmlNode xmlOurspace_Statisticss = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_Statisticss");
            //foreach (XmlNode xmlOurspace_Statistics in xmlOurspace_Statisticss.SelectNodes("Ourspace_Statistics"))
            //{
            //    Ourspace_StatisticsInfo objOurspace_Statistics = new Ourspace_StatisticsInfo();
            //    objOurspace_Statistics.ModuleId = ModuleID;
            //    objOurspace_Statistics.Content = xmlOurspace_Statistics.SelectSingleNode("content").InnerText;
            //    objOurspace_Statistics.CreatedByUser = UserID;
            //    AddOurspace_Statistics(objOurspace_Statistics);
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

            //List<Ourspace_StatisticsInfo> colOurspace_Statisticss = GetOurspace_Statisticss(ModInfo.ModuleID);

            //foreach (Ourspace_StatisticsInfo objOurspace_Statistics in colOurspace_Statisticss)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_Statistics.Content, objOurspace_Statistics.CreatedByUser, objOurspace_Statistics.CreatedDate, ModInfo.ModuleID, objOurspace_Statistics.ItemId.ToString(), objOurspace_Statistics.Content, "ItemId=" + objOurspace_Statistics.ItemId.ToString());
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
