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

namespace DotNetNuke.Modules.Ourspace_Microprofile.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_Microprofile
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

            //List<Ourspace_MicroprofileInfo> colOurspace_Microprofiles = GetOurspace_Microprofiles(ModuleID);
            //if (colOurspace_Microprofiles.Count != 0)
            //{
            //    strXML += "<Ourspace_Microprofiles>";

            //    foreach (Ourspace_MicroprofileInfo objOurspace_Microprofile in colOurspace_Microprofiles)
            //    {
            //        strXML += "<Ourspace_Microprofile>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_Microprofile.Content) + "</content>";
            //        strXML += "</Ourspace_Microprofile>";
            //    }
            //    strXML += "</Ourspace_Microprofiles>";
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
            //XmlNode xmlOurspace_Microprofiles = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_Microprofiles");
            //foreach (XmlNode xmlOurspace_Microprofile in xmlOurspace_Microprofiles.SelectNodes("Ourspace_Microprofile"))
            //{
            //    Ourspace_MicroprofileInfo objOurspace_Microprofile = new Ourspace_MicroprofileInfo();
            //    objOurspace_Microprofile.ModuleId = ModuleID;
            //    objOurspace_Microprofile.Content = xmlOurspace_Microprofile.SelectSingleNode("content").InnerText;
            //    objOurspace_Microprofile.CreatedByUser = UserID;
            //    AddOurspace_Microprofile(objOurspace_Microprofile);
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

            //List<Ourspace_MicroprofileInfo> colOurspace_Microprofiles = GetOurspace_Microprofiles(ModInfo.ModuleID);

            //foreach (Ourspace_MicroprofileInfo objOurspace_Microprofile in colOurspace_Microprofiles)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_Microprofile.Content, objOurspace_Microprofile.CreatedByUser, objOurspace_Microprofile.CreatedDate, ModInfo.ModuleID, objOurspace_Microprofile.ItemId.ToString(), objOurspace_Microprofile.Content, "ItemId=" + objOurspace_Microprofile.ItemId.ToString());
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
