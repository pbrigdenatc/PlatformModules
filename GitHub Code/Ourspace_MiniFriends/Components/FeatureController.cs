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

namespace DotNetNuke.Modules.Ourspace_MiniFriends.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_MiniFriends
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

            //List<Ourspace_MiniFriendsInfo> colOurspace_MiniFriendss = GetOurspace_MiniFriendss(ModuleID);
            //if (colOurspace_MiniFriendss.Count != 0)
            //{
            //    strXML += "<Ourspace_MiniFriendss>";

            //    foreach (Ourspace_MiniFriendsInfo objOurspace_MiniFriends in colOurspace_MiniFriendss)
            //    {
            //        strXML += "<Ourspace_MiniFriends>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_MiniFriends.Content) + "</content>";
            //        strXML += "</Ourspace_MiniFriends>";
            //    }
            //    strXML += "</Ourspace_MiniFriendss>";
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
            //XmlNode xmlOurspace_MiniFriendss = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_MiniFriendss");
            //foreach (XmlNode xmlOurspace_MiniFriends in xmlOurspace_MiniFriendss.SelectNodes("Ourspace_MiniFriends"))
            //{
            //    Ourspace_MiniFriendsInfo objOurspace_MiniFriends = new Ourspace_MiniFriendsInfo();
            //    objOurspace_MiniFriends.ModuleId = ModuleID;
            //    objOurspace_MiniFriends.Content = xmlOurspace_MiniFriends.SelectSingleNode("content").InnerText;
            //    objOurspace_MiniFriends.CreatedByUser = UserID;
            //    AddOurspace_MiniFriends(objOurspace_MiniFriends);
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

            //List<Ourspace_MiniFriendsInfo> colOurspace_MiniFriendss = GetOurspace_MiniFriendss(ModInfo.ModuleID);

            //foreach (Ourspace_MiniFriendsInfo objOurspace_MiniFriends in colOurspace_MiniFriendss)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_MiniFriends.Content, objOurspace_MiniFriends.CreatedByUser, objOurspace_MiniFriends.CreatedDate, ModInfo.ModuleID, objOurspace_MiniFriends.ItemId.ToString(), objOurspace_MiniFriends.Content, "ItemId=" + objOurspace_MiniFriends.ItemId.ToString());
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
