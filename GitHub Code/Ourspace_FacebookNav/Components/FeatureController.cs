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

namespace DotNetNuke.Modules.Ourspace_FacebookNav.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_FacebookNav
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

            //List<Ourspace_FacebookNavInfo> colOurspace_FacebookNavs = GetOurspace_FacebookNavs(ModuleID);
            //if (colOurspace_FacebookNavs.Count != 0)
            //{
            //    strXML += "<Ourspace_FacebookNavs>";

            //    foreach (Ourspace_FacebookNavInfo objOurspace_FacebookNav in colOurspace_FacebookNavs)
            //    {
            //        strXML += "<Ourspace_FacebookNav>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_FacebookNav.Content) + "</content>";
            //        strXML += "</Ourspace_FacebookNav>";
            //    }
            //    strXML += "</Ourspace_FacebookNavs>";
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
            //XmlNode xmlOurspace_FacebookNavs = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_FacebookNavs");
            //foreach (XmlNode xmlOurspace_FacebookNav in xmlOurspace_FacebookNavs.SelectNodes("Ourspace_FacebookNav"))
            //{
            //    Ourspace_FacebookNavInfo objOurspace_FacebookNav = new Ourspace_FacebookNavInfo();
            //    objOurspace_FacebookNav.ModuleId = ModuleID;
            //    objOurspace_FacebookNav.Content = xmlOurspace_FacebookNav.SelectSingleNode("content").InnerText;
            //    objOurspace_FacebookNav.CreatedByUser = UserID;
            //    AddOurspace_FacebookNav(objOurspace_FacebookNav);
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

            //List<Ourspace_FacebookNavInfo> colOurspace_FacebookNavs = GetOurspace_FacebookNavs(ModInfo.ModuleID);

            //foreach (Ourspace_FacebookNavInfo objOurspace_FacebookNav in colOurspace_FacebookNavs)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_FacebookNav.Content, objOurspace_FacebookNav.CreatedByUser, objOurspace_FacebookNav.CreatedDate, ModInfo.ModuleID, objOurspace_FacebookNav.ItemId.ToString(), objOurspace_FacebookNav.Content, "ItemId=" + objOurspace_FacebookNav.ItemId.ToString());
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
