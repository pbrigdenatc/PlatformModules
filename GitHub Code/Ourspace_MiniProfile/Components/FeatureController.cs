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

namespace DotNetNuke.Modules.Ourspace_MiniProfile.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_MiniProfile
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

            //List<Ourspace_MiniProfileInfo> colOurspace_MiniProfiles = GetOurspace_MiniProfiles(ModuleID);
            //if (colOurspace_MiniProfiles.Count != 0)
            //{
            //    strXML += "<Ourspace_MiniProfiles>";

            //    foreach (Ourspace_MiniProfileInfo objOurspace_MiniProfile in colOurspace_MiniProfiles)
            //    {
            //        strXML += "<Ourspace_MiniProfile>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_MiniProfile.Content) + "</content>";
            //        strXML += "</Ourspace_MiniProfile>";
            //    }
            //    strXML += "</Ourspace_MiniProfiles>";
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
            //XmlNode xmlOurspace_MiniProfiles = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_MiniProfiles");
            //foreach (XmlNode xmlOurspace_MiniProfile in xmlOurspace_MiniProfiles.SelectNodes("Ourspace_MiniProfile"))
            //{
            //    Ourspace_MiniProfileInfo objOurspace_MiniProfile = new Ourspace_MiniProfileInfo();
            //    objOurspace_MiniProfile.ModuleId = ModuleID;
            //    objOurspace_MiniProfile.Content = xmlOurspace_MiniProfile.SelectSingleNode("content").InnerText;
            //    objOurspace_MiniProfile.CreatedByUser = UserID;
            //    AddOurspace_MiniProfile(objOurspace_MiniProfile);
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

            //List<Ourspace_MiniProfileInfo> colOurspace_MiniProfiles = GetOurspace_MiniProfiles(ModInfo.ModuleID);

            //foreach (Ourspace_MiniProfileInfo objOurspace_MiniProfile in colOurspace_MiniProfiles)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_MiniProfile.Content, objOurspace_MiniProfile.CreatedByUser, objOurspace_MiniProfile.CreatedDate, ModInfo.ModuleID, objOurspace_MiniProfile.ItemId.ToString(), objOurspace_MiniProfile.Content, "ItemId=" + objOurspace_MiniProfile.ItemId.ToString());
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
