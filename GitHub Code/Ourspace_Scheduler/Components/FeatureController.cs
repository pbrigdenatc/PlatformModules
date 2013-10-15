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

namespace DotNetNuke.Modules.Ourspace_Scheduler.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_Scheduler
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

            //List<Ourspace_SchedulerInfo> colOurspace_Schedulers = GetOurspace_Schedulers(ModuleID);
            //if (colOurspace_Schedulers.Count != 0)
            //{
            //    strXML += "<Ourspace_Schedulers>";

            //    foreach (Ourspace_SchedulerInfo objOurspace_Scheduler in colOurspace_Schedulers)
            //    {
            //        strXML += "<Ourspace_Scheduler>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_Scheduler.Content) + "</content>";
            //        strXML += "</Ourspace_Scheduler>";
            //    }
            //    strXML += "</Ourspace_Schedulers>";
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
            //XmlNode xmlOurspace_Schedulers = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_Schedulers");
            //foreach (XmlNode xmlOurspace_Scheduler in xmlOurspace_Schedulers.SelectNodes("Ourspace_Scheduler"))
            //{
            //    Ourspace_SchedulerInfo objOurspace_Scheduler = new Ourspace_SchedulerInfo();
            //    objOurspace_Scheduler.ModuleId = ModuleID;
            //    objOurspace_Scheduler.Content = xmlOurspace_Scheduler.SelectSingleNode("content").InnerText;
            //    objOurspace_Scheduler.CreatedByUser = UserID;
            //    AddOurspace_Scheduler(objOurspace_Scheduler);
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

            //List<Ourspace_SchedulerInfo> colOurspace_Schedulers = GetOurspace_Schedulers(ModInfo.ModuleID);

            //foreach (Ourspace_SchedulerInfo objOurspace_Scheduler in colOurspace_Schedulers)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_Scheduler.Content, objOurspace_Scheduler.CreatedByUser, objOurspace_Scheduler.CreatedDate, ModInfo.ModuleID, objOurspace_Scheduler.ItemId.ToString(), objOurspace_Scheduler.Content, "ItemId=" + objOurspace_Scheduler.ItemId.ToString());
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
