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

namespace DotNetNuke.Modules.Ourspace_FacebookLanding.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_FacebookLanding
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

            //List<Ourspace_FacebookLandingInfo> colOurspace_FacebookLandings = GetOurspace_FacebookLandings(ModuleID);
            //if (colOurspace_FacebookLandings.Count != 0)
            //{
            //    strXML += "<Ourspace_FacebookLandings>";

            //    foreach (Ourspace_FacebookLandingInfo objOurspace_FacebookLanding in colOurspace_FacebookLandings)
            //    {
            //        strXML += "<Ourspace_FacebookLanding>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_FacebookLanding.Content) + "</content>";
            //        strXML += "</Ourspace_FacebookLanding>";
            //    }
            //    strXML += "</Ourspace_FacebookLandings>";
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
            //XmlNode xmlOurspace_FacebookLandings = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_FacebookLandings");
            //foreach (XmlNode xmlOurspace_FacebookLanding in xmlOurspace_FacebookLandings.SelectNodes("Ourspace_FacebookLanding"))
            //{
            //    Ourspace_FacebookLandingInfo objOurspace_FacebookLanding = new Ourspace_FacebookLandingInfo();
            //    objOurspace_FacebookLanding.ModuleId = ModuleID;
            //    objOurspace_FacebookLanding.Content = xmlOurspace_FacebookLanding.SelectSingleNode("content").InnerText;
            //    objOurspace_FacebookLanding.CreatedByUser = UserID;
            //    AddOurspace_FacebookLanding(objOurspace_FacebookLanding);
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

            //List<Ourspace_FacebookLandingInfo> colOurspace_FacebookLandings = GetOurspace_FacebookLandings(ModInfo.ModuleID);

            //foreach (Ourspace_FacebookLandingInfo objOurspace_FacebookLanding in colOurspace_FacebookLandings)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_FacebookLanding.Content, objOurspace_FacebookLanding.CreatedByUser, objOurspace_FacebookLanding.CreatedDate, ModInfo.ModuleID, objOurspace_FacebookLanding.ItemId.ToString(), objOurspace_FacebookLanding.Content, "ItemId=" + objOurspace_FacebookLanding.ItemId.ToString());
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
