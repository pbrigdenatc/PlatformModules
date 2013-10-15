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

namespace DotNetNuke.Modules.Ourspace_SubmittedProposals.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_SubmittedProposals
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

            //List<Ourspace_SubmittedProposalsInfo> colOurspace_SubmittedProposalss = GetOurspace_SubmittedProposalss(ModuleID);
            //if (colOurspace_SubmittedProposalss.Count != 0)
            //{
            //    strXML += "<Ourspace_SubmittedProposalss>";

            //    foreach (Ourspace_SubmittedProposalsInfo objOurspace_SubmittedProposals in colOurspace_SubmittedProposalss)
            //    {
            //        strXML += "<Ourspace_SubmittedProposals>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_SubmittedProposals.Content) + "</content>";
            //        strXML += "</Ourspace_SubmittedProposals>";
            //    }
            //    strXML += "</Ourspace_SubmittedProposalss>";
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
            //XmlNode xmlOurspace_SubmittedProposalss = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_SubmittedProposalss");
            //foreach (XmlNode xmlOurspace_SubmittedProposals in xmlOurspace_SubmittedProposalss.SelectNodes("Ourspace_SubmittedProposals"))
            //{
            //    Ourspace_SubmittedProposalsInfo objOurspace_SubmittedProposals = new Ourspace_SubmittedProposalsInfo();
            //    objOurspace_SubmittedProposals.ModuleId = ModuleID;
            //    objOurspace_SubmittedProposals.Content = xmlOurspace_SubmittedProposals.SelectSingleNode("content").InnerText;
            //    objOurspace_SubmittedProposals.CreatedByUser = UserID;
            //    AddOurspace_SubmittedProposals(objOurspace_SubmittedProposals);
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

            //List<Ourspace_SubmittedProposalsInfo> colOurspace_SubmittedProposalss = GetOurspace_SubmittedProposalss(ModInfo.ModuleID);

            //foreach (Ourspace_SubmittedProposalsInfo objOurspace_SubmittedProposals in colOurspace_SubmittedProposalss)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_SubmittedProposals.Content, objOurspace_SubmittedProposals.CreatedByUser, objOurspace_SubmittedProposals.CreatedDate, ModInfo.ModuleID, objOurspace_SubmittedProposals.ItemId.ToString(), objOurspace_SubmittedProposals.Content, "ItemId=" + objOurspace_SubmittedProposals.ItemId.ToString());
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
