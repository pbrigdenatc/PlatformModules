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

namespace DotNetNuke.Modules.Ourspace_Utilities.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_Utilities
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

            //List<Ourspace_UtilitiesInfo> colOurspace_Utilitiess = GetOurspace_Utilitiess(ModuleID);
            //if (colOurspace_Utilitiess.Count != 0)
            //{
            //    strXML += "<Ourspace_Utilitiess>";

            //    foreach (Ourspace_UtilitiesInfo objOurspace_Utilities in colOurspace_Utilitiess)
            //    {
            //        strXML += "<Ourspace_Utilities>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_Utilities.Content) + "</content>";
            //        strXML += "</Ourspace_Utilities>";
            //    }
            //    strXML += "</Ourspace_Utilitiess>";
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
            //XmlNode xmlOurspace_Utilitiess = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_Utilitiess");
            //foreach (XmlNode xmlOurspace_Utilities in xmlOurspace_Utilitiess.SelectNodes("Ourspace_Utilities"))
            //{
            //    Ourspace_UtilitiesInfo objOurspace_Utilities = new Ourspace_UtilitiesInfo();
            //    objOurspace_Utilities.ModuleId = ModuleID;
            //    objOurspace_Utilities.Content = xmlOurspace_Utilities.SelectSingleNode("content").InnerText;
            //    objOurspace_Utilities.CreatedByUser = UserID;
            //    AddOurspace_Utilities(objOurspace_Utilities);
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

            //List<Ourspace_UtilitiesInfo> colOurspace_Utilitiess = GetOurspace_Utilitiess(ModInfo.ModuleID);

            //foreach (Ourspace_UtilitiesInfo objOurspace_Utilities in colOurspace_Utilitiess)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_Utilities.Content, objOurspace_Utilities.CreatedByUser, objOurspace_Utilities.CreatedDate, ModInfo.ModuleID, objOurspace_Utilities.ItemId.ToString(), objOurspace_Utilities.Content, "ItemId=" + objOurspace_Utilities.ItemId.ToString());
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
