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

namespace DotNetNuke.Modules.Ourspace_ProposedSolutions.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_ProposedSolutions
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

            //List<Ourspace_ProposedSolutionsInfo> colOurspace_ProposedSolutionss = GetOurspace_ProposedSolutionss(ModuleID);
            //if (colOurspace_ProposedSolutionss.Count != 0)
            //{
            //    strXML += "<Ourspace_ProposedSolutionss>";

            //    foreach (Ourspace_ProposedSolutionsInfo objOurspace_ProposedSolutions in colOurspace_ProposedSolutionss)
            //    {
            //        strXML += "<Ourspace_ProposedSolutions>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_ProposedSolutions.Content) + "</content>";
            //        strXML += "</Ourspace_ProposedSolutions>";
            //    }
            //    strXML += "</Ourspace_ProposedSolutionss>";
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
            //XmlNode xmlOurspace_ProposedSolutionss = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_ProposedSolutionss");
            //foreach (XmlNode xmlOurspace_ProposedSolutions in xmlOurspace_ProposedSolutionss.SelectNodes("Ourspace_ProposedSolutions"))
            //{
            //    Ourspace_ProposedSolutionsInfo objOurspace_ProposedSolutions = new Ourspace_ProposedSolutionsInfo();
            //    objOurspace_ProposedSolutions.ModuleId = ModuleID;
            //    objOurspace_ProposedSolutions.Content = xmlOurspace_ProposedSolutions.SelectSingleNode("content").InnerText;
            //    objOurspace_ProposedSolutions.CreatedByUser = UserID;
            //    AddOurspace_ProposedSolutions(objOurspace_ProposedSolutions);
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

            //List<Ourspace_ProposedSolutionsInfo> colOurspace_ProposedSolutionss = GetOurspace_ProposedSolutionss(ModInfo.ModuleID);

            //foreach (Ourspace_ProposedSolutionsInfo objOurspace_ProposedSolutions in colOurspace_ProposedSolutionss)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_ProposedSolutions.Content, objOurspace_ProposedSolutions.CreatedByUser, objOurspace_ProposedSolutions.CreatedDate, ModInfo.ModuleID, objOurspace_ProposedSolutions.ItemId.ToString(), objOurspace_ProposedSolutions.Content, "ItemId=" + objOurspace_ProposedSolutions.ItemId.ToString());
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
