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

namespace DotNetNuke.Modules.Ourspace_ResultDetails.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_ResultDetails
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

            //List<Ourspace_ResultDetailsInfo> colOurspace_ResultDetailss = GetOurspace_ResultDetailss(ModuleID);
            //if (colOurspace_ResultDetailss.Count != 0)
            //{
            //    strXML += "<Ourspace_ResultDetailss>";

            //    foreach (Ourspace_ResultDetailsInfo objOurspace_ResultDetails in colOurspace_ResultDetailss)
            //    {
            //        strXML += "<Ourspace_ResultDetails>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_ResultDetails.Content) + "</content>";
            //        strXML += "</Ourspace_ResultDetails>";
            //    }
            //    strXML += "</Ourspace_ResultDetailss>";
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
            //XmlNode xmlOurspace_ResultDetailss = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_ResultDetailss");
            //foreach (XmlNode xmlOurspace_ResultDetails in xmlOurspace_ResultDetailss.SelectNodes("Ourspace_ResultDetails"))
            //{
            //    Ourspace_ResultDetailsInfo objOurspace_ResultDetails = new Ourspace_ResultDetailsInfo();
            //    objOurspace_ResultDetails.ModuleId = ModuleID;
            //    objOurspace_ResultDetails.Content = xmlOurspace_ResultDetails.SelectSingleNode("content").InnerText;
            //    objOurspace_ResultDetails.CreatedByUser = UserID;
            //    AddOurspace_ResultDetails(objOurspace_ResultDetails);
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

            //List<Ourspace_ResultDetailsInfo> colOurspace_ResultDetailss = GetOurspace_ResultDetailss(ModInfo.ModuleID);

            //foreach (Ourspace_ResultDetailsInfo objOurspace_ResultDetails in colOurspace_ResultDetailss)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_ResultDetails.Content, objOurspace_ResultDetails.CreatedByUser, objOurspace_ResultDetails.CreatedDate, ModInfo.ModuleID, objOurspace_ResultDetails.ItemId.ToString(), objOurspace_ResultDetails.Content, "ItemId=" + objOurspace_ResultDetails.ItemId.ToString());
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
