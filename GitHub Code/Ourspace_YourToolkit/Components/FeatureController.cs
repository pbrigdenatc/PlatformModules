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

namespace DotNetNuke.Modules.Ourspace_YourToolkit.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_YourToolkit
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

            //List<Ourspace_YourToolkitInfo> colOurspace_YourToolkits = GetOurspace_YourToolkits(ModuleID);
            //if (colOurspace_YourToolkits.Count != 0)
            //{
            //    strXML += "<Ourspace_YourToolkits>";

            //    foreach (Ourspace_YourToolkitInfo objOurspace_YourToolkit in colOurspace_YourToolkits)
            //    {
            //        strXML += "<Ourspace_YourToolkit>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_YourToolkit.Content) + "</content>";
            //        strXML += "</Ourspace_YourToolkit>";
            //    }
            //    strXML += "</Ourspace_YourToolkits>";
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
            //XmlNode xmlOurspace_YourToolkits = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_YourToolkits");
            //foreach (XmlNode xmlOurspace_YourToolkit in xmlOurspace_YourToolkits.SelectNodes("Ourspace_YourToolkit"))
            //{
            //    Ourspace_YourToolkitInfo objOurspace_YourToolkit = new Ourspace_YourToolkitInfo();
            //    objOurspace_YourToolkit.ModuleId = ModuleID;
            //    objOurspace_YourToolkit.Content = xmlOurspace_YourToolkit.SelectSingleNode("content").InnerText;
            //    objOurspace_YourToolkit.CreatedByUser = UserID;
            //    AddOurspace_YourToolkit(objOurspace_YourToolkit);
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

            //List<Ourspace_YourToolkitInfo> colOurspace_YourToolkits = GetOurspace_YourToolkits(ModInfo.ModuleID);

            //foreach (Ourspace_YourToolkitInfo objOurspace_YourToolkit in colOurspace_YourToolkits)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_YourToolkit.Content, objOurspace_YourToolkit.CreatedByUser, objOurspace_YourToolkit.CreatedDate, ModInfo.ModuleID, objOurspace_YourToolkit.ItemId.ToString(), objOurspace_YourToolkit.Content, "ItemId=" + objOurspace_YourToolkit.ItemId.ToString());
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
