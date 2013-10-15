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

namespace DotNetNuke.Modules.Ourspace_ThreadDetails.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_ThreadDetails
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

            //List<Ourspace_ThreadDetailsInfo> colOurspace_ThreadDetailss = GetOurspace_ThreadDetailss(ModuleID);
            //if (colOurspace_ThreadDetailss.Count != 0)
            //{
            //    strXML += "<Ourspace_ThreadDetailss>";

            //    foreach (Ourspace_ThreadDetailsInfo objOurspace_ThreadDetails in colOurspace_ThreadDetailss)
            //    {
            //        strXML += "<Ourspace_ThreadDetails>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_ThreadDetails.Content) + "</content>";
            //        strXML += "</Ourspace_ThreadDetails>";
            //    }
            //    strXML += "</Ourspace_ThreadDetailss>";
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
            //XmlNode xmlOurspace_ThreadDetailss = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_ThreadDetailss");
            //foreach (XmlNode xmlOurspace_ThreadDetails in xmlOurspace_ThreadDetailss.SelectNodes("Ourspace_ThreadDetails"))
            //{
            //    Ourspace_ThreadDetailsInfo objOurspace_ThreadDetails = new Ourspace_ThreadDetailsInfo();
            //    objOurspace_ThreadDetails.ModuleId = ModuleID;
            //    objOurspace_ThreadDetails.Content = xmlOurspace_ThreadDetails.SelectSingleNode("content").InnerText;
            //    objOurspace_ThreadDetails.CreatedByUser = UserID;
            //    AddOurspace_ThreadDetails(objOurspace_ThreadDetails);
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

            //List<Ourspace_ThreadDetailsInfo> colOurspace_ThreadDetailss = GetOurspace_ThreadDetailss(ModInfo.ModuleID);

            //foreach (Ourspace_ThreadDetailsInfo objOurspace_ThreadDetails in colOurspace_ThreadDetailss)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_ThreadDetails.Content, objOurspace_ThreadDetails.CreatedByUser, objOurspace_ThreadDetails.CreatedDate, ModInfo.ModuleID, objOurspace_ThreadDetails.ItemId.ToString(), objOurspace_ThreadDetails.Content, "ItemId=" + objOurspace_ThreadDetails.ItemId.ToString());
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
