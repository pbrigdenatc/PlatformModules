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

namespace DotNetNuke.Modules.Ourspace_LocaleOverwriter.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_LocaleOverwriter
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

            //List<Ourspace_LocaleOverwriterInfo> colOurspace_LocaleOverwriters = GetOurspace_LocaleOverwriters(ModuleID);
            //if (colOurspace_LocaleOverwriters.Count != 0)
            //{
            //    strXML += "<Ourspace_LocaleOverwriters>";

            //    foreach (Ourspace_LocaleOverwriterInfo objOurspace_LocaleOverwriter in colOurspace_LocaleOverwriters)
            //    {
            //        strXML += "<Ourspace_LocaleOverwriter>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_LocaleOverwriter.Content) + "</content>";
            //        strXML += "</Ourspace_LocaleOverwriter>";
            //    }
            //    strXML += "</Ourspace_LocaleOverwriters>";
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
            //XmlNode xmlOurspace_LocaleOverwriters = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_LocaleOverwriters");
            //foreach (XmlNode xmlOurspace_LocaleOverwriter in xmlOurspace_LocaleOverwriters.SelectNodes("Ourspace_LocaleOverwriter"))
            //{
            //    Ourspace_LocaleOverwriterInfo objOurspace_LocaleOverwriter = new Ourspace_LocaleOverwriterInfo();
            //    objOurspace_LocaleOverwriter.ModuleId = ModuleID;
            //    objOurspace_LocaleOverwriter.Content = xmlOurspace_LocaleOverwriter.SelectSingleNode("content").InnerText;
            //    objOurspace_LocaleOverwriter.CreatedByUser = UserID;
            //    AddOurspace_LocaleOverwriter(objOurspace_LocaleOverwriter);
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

            //List<Ourspace_LocaleOverwriterInfo> colOurspace_LocaleOverwriters = GetOurspace_LocaleOverwriters(ModInfo.ModuleID);

            //foreach (Ourspace_LocaleOverwriterInfo objOurspace_LocaleOverwriter in colOurspace_LocaleOverwriters)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_LocaleOverwriter.Content, objOurspace_LocaleOverwriter.CreatedByUser, objOurspace_LocaleOverwriter.CreatedDate, ModInfo.ModuleID, objOurspace_LocaleOverwriter.ItemId.ToString(), objOurspace_LocaleOverwriter.Content, "ItemId=" + objOurspace_LocaleOverwriter.ItemId.ToString());
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
