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

namespace DotNetNuke.Modules.Ourspace_LoginPageInfo.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_LoginPageInfo
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

            //List<Ourspace_LoginPageInfoInfo> colOurspace_LoginPageInfos = GetOurspace_LoginPageInfos(ModuleID);
            //if (colOurspace_LoginPageInfos.Count != 0)
            //{
            //    strXML += "<Ourspace_LoginPageInfos>";

            //    foreach (Ourspace_LoginPageInfoInfo objOurspace_LoginPageInfo in colOurspace_LoginPageInfos)
            //    {
            //        strXML += "<Ourspace_LoginPageInfo>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_LoginPageInfo.Content) + "</content>";
            //        strXML += "</Ourspace_LoginPageInfo>";
            //    }
            //    strXML += "</Ourspace_LoginPageInfos>";
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
            //XmlNode xmlOurspace_LoginPageInfos = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_LoginPageInfos");
            //foreach (XmlNode xmlOurspace_LoginPageInfo in xmlOurspace_LoginPageInfos.SelectNodes("Ourspace_LoginPageInfo"))
            //{
            //    Ourspace_LoginPageInfoInfo objOurspace_LoginPageInfo = new Ourspace_LoginPageInfoInfo();
            //    objOurspace_LoginPageInfo.ModuleId = ModuleID;
            //    objOurspace_LoginPageInfo.Content = xmlOurspace_LoginPageInfo.SelectSingleNode("content").InnerText;
            //    objOurspace_LoginPageInfo.CreatedByUser = UserID;
            //    AddOurspace_LoginPageInfo(objOurspace_LoginPageInfo);
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

            //List<Ourspace_LoginPageInfoInfo> colOurspace_LoginPageInfos = GetOurspace_LoginPageInfos(ModInfo.ModuleID);

            //foreach (Ourspace_LoginPageInfoInfo objOurspace_LoginPageInfo in colOurspace_LoginPageInfos)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_LoginPageInfo.Content, objOurspace_LoginPageInfo.CreatedByUser, objOurspace_LoginPageInfo.CreatedDate, ModInfo.ModuleID, objOurspace_LoginPageInfo.ItemId.ToString(), objOurspace_LoginPageInfo.Content, "ItemId=" + objOurspace_LoginPageInfo.ItemId.ToString());
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
