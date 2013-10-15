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

namespace DotNetNuke.Modules.Ourspace_Phase1ThreadInfo.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_Phase1ThreadInfo
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

            //List<Ourspace_Phase1ThreadInfoInfo> colOurspace_Phase1ThreadInfos = GetOurspace_Phase1ThreadInfos(ModuleID);
            //if (colOurspace_Phase1ThreadInfos.Count != 0)
            //{
            //    strXML += "<Ourspace_Phase1ThreadInfos>";

            //    foreach (Ourspace_Phase1ThreadInfoInfo objOurspace_Phase1ThreadInfo in colOurspace_Phase1ThreadInfos)
            //    {
            //        strXML += "<Ourspace_Phase1ThreadInfo>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_Phase1ThreadInfo.Content) + "</content>";
            //        strXML += "</Ourspace_Phase1ThreadInfo>";
            //    }
            //    strXML += "</Ourspace_Phase1ThreadInfos>";
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
            //XmlNode xmlOurspace_Phase1ThreadInfos = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_Phase1ThreadInfos");
            //foreach (XmlNode xmlOurspace_Phase1ThreadInfo in xmlOurspace_Phase1ThreadInfos.SelectNodes("Ourspace_Phase1ThreadInfo"))
            //{
            //    Ourspace_Phase1ThreadInfoInfo objOurspace_Phase1ThreadInfo = new Ourspace_Phase1ThreadInfoInfo();
            //    objOurspace_Phase1ThreadInfo.ModuleId = ModuleID;
            //    objOurspace_Phase1ThreadInfo.Content = xmlOurspace_Phase1ThreadInfo.SelectSingleNode("content").InnerText;
            //    objOurspace_Phase1ThreadInfo.CreatedByUser = UserID;
            //    AddOurspace_Phase1ThreadInfo(objOurspace_Phase1ThreadInfo);
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

            //List<Ourspace_Phase1ThreadInfoInfo> colOurspace_Phase1ThreadInfos = GetOurspace_Phase1ThreadInfos(ModInfo.ModuleID);

            //foreach (Ourspace_Phase1ThreadInfoInfo objOurspace_Phase1ThreadInfo in colOurspace_Phase1ThreadInfos)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_Phase1ThreadInfo.Content, objOurspace_Phase1ThreadInfo.CreatedByUser, objOurspace_Phase1ThreadInfo.CreatedDate, ModInfo.ModuleID, objOurspace_Phase1ThreadInfo.ItemId.ToString(), objOurspace_Phase1ThreadInfo.Content, "ItemId=" + objOurspace_Phase1ThreadInfo.ItemId.ToString());
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
