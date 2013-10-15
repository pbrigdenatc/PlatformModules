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

namespace DotNetNuke.Modules.Ourspace_FB_Test.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_FB_Test
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

            //List<Ourspace_FB_TestInfo> colOurspace_FB_Tests = GetOurspace_FB_Tests(ModuleID);
            //if (colOurspace_FB_Tests.Count != 0)
            //{
            //    strXML += "<Ourspace_FB_Tests>";

            //    foreach (Ourspace_FB_TestInfo objOurspace_FB_Test in colOurspace_FB_Tests)
            //    {
            //        strXML += "<Ourspace_FB_Test>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_FB_Test.Content) + "</content>";
            //        strXML += "</Ourspace_FB_Test>";
            //    }
            //    strXML += "</Ourspace_FB_Tests>";
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
            //XmlNode xmlOurspace_FB_Tests = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_FB_Tests");
            //foreach (XmlNode xmlOurspace_FB_Test in xmlOurspace_FB_Tests.SelectNodes("Ourspace_FB_Test"))
            //{
            //    Ourspace_FB_TestInfo objOurspace_FB_Test = new Ourspace_FB_TestInfo();
            //    objOurspace_FB_Test.ModuleId = ModuleID;
            //    objOurspace_FB_Test.Content = xmlOurspace_FB_Test.SelectSingleNode("content").InnerText;
            //    objOurspace_FB_Test.CreatedByUser = UserID;
            //    AddOurspace_FB_Test(objOurspace_FB_Test);
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

            //List<Ourspace_FB_TestInfo> colOurspace_FB_Tests = GetOurspace_FB_Tests(ModInfo.ModuleID);

            //foreach (Ourspace_FB_TestInfo objOurspace_FB_Test in colOurspace_FB_Tests)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_FB_Test.Content, objOurspace_FB_Test.CreatedByUser, objOurspace_FB_Test.CreatedDate, ModInfo.ModuleID, objOurspace_FB_Test.ItemId.ToString(), objOurspace_FB_Test.Content, "ItemId=" + objOurspace_FB_Test.ItemId.ToString());
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
