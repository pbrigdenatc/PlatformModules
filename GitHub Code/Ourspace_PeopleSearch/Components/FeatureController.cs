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

namespace DotNetNuke.Modules.Ourspace_PeopleSearch.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_PeopleSearch
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

            //List<Ourspace_PeopleSearchInfo> colOurspace_PeopleSearchs = GetOurspace_PeopleSearchs(ModuleID);
            //if (colOurspace_PeopleSearchs.Count != 0)
            //{
            //    strXML += "<Ourspace_PeopleSearchs>";

            //    foreach (Ourspace_PeopleSearchInfo objOurspace_PeopleSearch in colOurspace_PeopleSearchs)
            //    {
            //        strXML += "<Ourspace_PeopleSearch>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_PeopleSearch.Content) + "</content>";
            //        strXML += "</Ourspace_PeopleSearch>";
            //    }
            //    strXML += "</Ourspace_PeopleSearchs>";
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
            //XmlNode xmlOurspace_PeopleSearchs = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_PeopleSearchs");
            //foreach (XmlNode xmlOurspace_PeopleSearch in xmlOurspace_PeopleSearchs.SelectNodes("Ourspace_PeopleSearch"))
            //{
            //    Ourspace_PeopleSearchInfo objOurspace_PeopleSearch = new Ourspace_PeopleSearchInfo();
            //    objOurspace_PeopleSearch.ModuleId = ModuleID;
            //    objOurspace_PeopleSearch.Content = xmlOurspace_PeopleSearch.SelectSingleNode("content").InnerText;
            //    objOurspace_PeopleSearch.CreatedByUser = UserID;
            //    AddOurspace_PeopleSearch(objOurspace_PeopleSearch);
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

            //List<Ourspace_PeopleSearchInfo> colOurspace_PeopleSearchs = GetOurspace_PeopleSearchs(ModInfo.ModuleID);

            //foreach (Ourspace_PeopleSearchInfo objOurspace_PeopleSearch in colOurspace_PeopleSearchs)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_PeopleSearch.Content, objOurspace_PeopleSearch.CreatedByUser, objOurspace_PeopleSearch.CreatedDate, ModInfo.ModuleID, objOurspace_PeopleSearch.ItemId.ToString(), objOurspace_PeopleSearch.Content, "ItemId=" + objOurspace_PeopleSearch.ItemId.ToString());
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
