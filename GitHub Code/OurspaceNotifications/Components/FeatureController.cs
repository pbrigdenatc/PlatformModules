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

namespace DotNetNuke.Modules.OurspaceNotifications.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for OurspaceNotifications
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

            //List<OurspaceNotificationsInfo> colOurspaceNotificationss = GetOurspaceNotificationss(ModuleID);
            //if (colOurspaceNotificationss.Count != 0)
            //{
            //    strXML += "<OurspaceNotificationss>";

            //    foreach (OurspaceNotificationsInfo objOurspaceNotifications in colOurspaceNotificationss)
            //    {
            //        strXML += "<OurspaceNotifications>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspaceNotifications.Content) + "</content>";
            //        strXML += "</OurspaceNotifications>";
            //    }
            //    strXML += "</OurspaceNotificationss>";
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
            //XmlNode xmlOurspaceNotificationss = DotNetNuke.Common.Globals.GetContent(Content, "OurspaceNotificationss");
            //foreach (XmlNode xmlOurspaceNotifications in xmlOurspaceNotificationss.SelectNodes("OurspaceNotifications"))
            //{
            //    OurspaceNotificationsInfo objOurspaceNotifications = new OurspaceNotificationsInfo();
            //    objOurspaceNotifications.ModuleId = ModuleID;
            //    objOurspaceNotifications.Content = xmlOurspaceNotifications.SelectSingleNode("content").InnerText;
            //    objOurspaceNotifications.CreatedByUser = UserID;
            //    AddOurspaceNotifications(objOurspaceNotifications);
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

            //List<OurspaceNotificationsInfo> colOurspaceNotificationss = GetOurspaceNotificationss(ModInfo.ModuleID);

            //foreach (OurspaceNotificationsInfo objOurspaceNotifications in colOurspaceNotificationss)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspaceNotifications.Content, objOurspaceNotifications.CreatedByUser, objOurspaceNotifications.CreatedDate, ModInfo.ModuleID, objOurspaceNotifications.ItemId.ToString(), objOurspaceNotifications.Content, "ItemId=" + objOurspaceNotifications.ItemId.ToString());
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
