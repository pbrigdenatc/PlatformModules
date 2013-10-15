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

namespace DotNetNuke.Modules.Ourspace_AttachmentComponent.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_AttachmentComponent
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

            //List<Ourspace_AttachmentComponentInfo> colOurspace_AttachmentComponents = GetOurspace_AttachmentComponents(ModuleID);
            //if (colOurspace_AttachmentComponents.Count != 0)
            //{
            //    strXML += "<Ourspace_AttachmentComponents>";

            //    foreach (Ourspace_AttachmentComponentInfo objOurspace_AttachmentComponent in colOurspace_AttachmentComponents)
            //    {
            //        strXML += "<Ourspace_AttachmentComponent>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_AttachmentComponent.Content) + "</content>";
            //        strXML += "</Ourspace_AttachmentComponent>";
            //    }
            //    strXML += "</Ourspace_AttachmentComponents>";
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
            //XmlNode xmlOurspace_AttachmentComponents = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_AttachmentComponents");
            //foreach (XmlNode xmlOurspace_AttachmentComponent in xmlOurspace_AttachmentComponents.SelectNodes("Ourspace_AttachmentComponent"))
            //{
            //    Ourspace_AttachmentComponentInfo objOurspace_AttachmentComponent = new Ourspace_AttachmentComponentInfo();
            //    objOurspace_AttachmentComponent.ModuleId = ModuleID;
            //    objOurspace_AttachmentComponent.Content = xmlOurspace_AttachmentComponent.SelectSingleNode("content").InnerText;
            //    objOurspace_AttachmentComponent.CreatedByUser = UserID;
            //    AddOurspace_AttachmentComponent(objOurspace_AttachmentComponent);
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

            //List<Ourspace_AttachmentComponentInfo> colOurspace_AttachmentComponents = GetOurspace_AttachmentComponents(ModInfo.ModuleID);

            //foreach (Ourspace_AttachmentComponentInfo objOurspace_AttachmentComponent in colOurspace_AttachmentComponents)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_AttachmentComponent.Content, objOurspace_AttachmentComponent.CreatedByUser, objOurspace_AttachmentComponent.CreatedDate, ModInfo.ModuleID, objOurspace_AttachmentComponent.ItemId.ToString(), objOurspace_AttachmentComponent.Content, "ItemId=" + objOurspace_AttachmentComponent.ItemId.ToString());
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
