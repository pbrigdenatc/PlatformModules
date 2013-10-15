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

namespace DotNetNuke.Modules.Ourspace_SolutionManager2.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_SolutionManager2
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

            //List<Ourspace_SolutionManager2Info> colOurspace_SolutionManager2s = GetOurspace_SolutionManager2s(ModuleID);
            //if (colOurspace_SolutionManager2s.Count != 0)
            //{
            //    strXML += "<Ourspace_SolutionManager2s>";

            //    foreach (Ourspace_SolutionManager2Info objOurspace_SolutionManager2 in colOurspace_SolutionManager2s)
            //    {
            //        strXML += "<Ourspace_SolutionManager2>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_SolutionManager2.Content) + "</content>";
            //        strXML += "</Ourspace_SolutionManager2>";
            //    }
            //    strXML += "</Ourspace_SolutionManager2s>";
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
            //XmlNode xmlOurspace_SolutionManager2s = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_SolutionManager2s");
            //foreach (XmlNode xmlOurspace_SolutionManager2 in xmlOurspace_SolutionManager2s.SelectNodes("Ourspace_SolutionManager2"))
            //{
            //    Ourspace_SolutionManager2Info objOurspace_SolutionManager2 = new Ourspace_SolutionManager2Info();
            //    objOurspace_SolutionManager2.ModuleId = ModuleID;
            //    objOurspace_SolutionManager2.Content = xmlOurspace_SolutionManager2.SelectSingleNode("content").InnerText;
            //    objOurspace_SolutionManager2.CreatedByUser = UserID;
            //    AddOurspace_SolutionManager2(objOurspace_SolutionManager2);
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

            //List<Ourspace_SolutionManager2Info> colOurspace_SolutionManager2s = GetOurspace_SolutionManager2s(ModInfo.ModuleID);

            //foreach (Ourspace_SolutionManager2Info objOurspace_SolutionManager2 in colOurspace_SolutionManager2s)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_SolutionManager2.Content, objOurspace_SolutionManager2.CreatedByUser, objOurspace_SolutionManager2.CreatedDate, ModInfo.ModuleID, objOurspace_SolutionManager2.ItemId.ToString(), objOurspace_SolutionManager2.Content, "ItemId=" + objOurspace_SolutionManager2.ItemId.ToString());
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
