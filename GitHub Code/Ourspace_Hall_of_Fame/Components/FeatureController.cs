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

namespace DotNetNuke.Modules.Ourspace_Hall_of_Fame.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_Hall_of_Fame
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

            //List<Ourspace_Hall_of_FameInfo> colOurspace_Hall_of_Fames = GetOurspace_Hall_of_Fames(ModuleID);
            //if (colOurspace_Hall_of_Fames.Count != 0)
            //{
            //    strXML += "<Ourspace_Hall_of_Fames>";

            //    foreach (Ourspace_Hall_of_FameInfo objOurspace_Hall_of_Fame in colOurspace_Hall_of_Fames)
            //    {
            //        strXML += "<Ourspace_Hall_of_Fame>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_Hall_of_Fame.Content) + "</content>";
            //        strXML += "</Ourspace_Hall_of_Fame>";
            //    }
            //    strXML += "</Ourspace_Hall_of_Fames>";
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
            //XmlNode xmlOurspace_Hall_of_Fames = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_Hall_of_Fames");
            //foreach (XmlNode xmlOurspace_Hall_of_Fame in xmlOurspace_Hall_of_Fames.SelectNodes("Ourspace_Hall_of_Fame"))
            //{
            //    Ourspace_Hall_of_FameInfo objOurspace_Hall_of_Fame = new Ourspace_Hall_of_FameInfo();
            //    objOurspace_Hall_of_Fame.ModuleId = ModuleID;
            //    objOurspace_Hall_of_Fame.Content = xmlOurspace_Hall_of_Fame.SelectSingleNode("content").InnerText;
            //    objOurspace_Hall_of_Fame.CreatedByUser = UserID;
            //    AddOurspace_Hall_of_Fame(objOurspace_Hall_of_Fame);
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

            //List<Ourspace_Hall_of_FameInfo> colOurspace_Hall_of_Fames = GetOurspace_Hall_of_Fames(ModInfo.ModuleID);

            //foreach (Ourspace_Hall_of_FameInfo objOurspace_Hall_of_Fame in colOurspace_Hall_of_Fames)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_Hall_of_Fame.Content, objOurspace_Hall_of_Fame.CreatedByUser, objOurspace_Hall_of_Fame.CreatedDate, ModInfo.ModuleID, objOurspace_Hall_of_Fame.ItemId.ToString(), objOurspace_Hall_of_Fame.Content, "ItemId=" + objOurspace_Hall_of_Fame.ItemId.ToString());
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
