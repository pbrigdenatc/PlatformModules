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

namespace DotNetNuke.Modules.Ourspace_SearchBox.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_SearchBox
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

            //List<Ourspace_SearchBoxInfo> colOurspace_SearchBoxs = GetOurspace_SearchBoxs(ModuleID);
            //if (colOurspace_SearchBoxs.Count != 0)
            //{
            //    strXML += "<Ourspace_SearchBoxs>";

            //    foreach (Ourspace_SearchBoxInfo objOurspace_SearchBox in colOurspace_SearchBoxs)
            //    {
            //        strXML += "<Ourspace_SearchBox>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_SearchBox.Content) + "</content>";
            //        strXML += "</Ourspace_SearchBox>";
            //    }
            //    strXML += "</Ourspace_SearchBoxs>";
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
            //XmlNode xmlOurspace_SearchBoxs = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_SearchBoxs");
            //foreach (XmlNode xmlOurspace_SearchBox in xmlOurspace_SearchBoxs.SelectNodes("Ourspace_SearchBox"))
            //{
            //    Ourspace_SearchBoxInfo objOurspace_SearchBox = new Ourspace_SearchBoxInfo();
            //    objOurspace_SearchBox.ModuleId = ModuleID;
            //    objOurspace_SearchBox.Content = xmlOurspace_SearchBox.SelectSingleNode("content").InnerText;
            //    objOurspace_SearchBox.CreatedByUser = UserID;
            //    AddOurspace_SearchBox(objOurspace_SearchBox);
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

            //List<Ourspace_SearchBoxInfo> colOurspace_SearchBoxs = GetOurspace_SearchBoxs(ModInfo.ModuleID);

            //foreach (Ourspace_SearchBoxInfo objOurspace_SearchBox in colOurspace_SearchBoxs)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_SearchBox.Content, objOurspace_SearchBox.CreatedByUser, objOurspace_SearchBox.CreatedDate, ModInfo.ModuleID, objOurspace_SearchBox.ItemId.ToString(), objOurspace_SearchBox.Content, "ItemId=" + objOurspace_SearchBox.ItemId.ToString());
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
