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

namespace DotNetNuke.Modules.Ourspace_LoginButton.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_LoginButton
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

            //List<Ourspace_LoginButtonInfo> colOurspace_LoginButtons = GetOurspace_LoginButtons(ModuleID);
            //if (colOurspace_LoginButtons.Count != 0)
            //{
            //    strXML += "<Ourspace_LoginButtons>";

            //    foreach (Ourspace_LoginButtonInfo objOurspace_LoginButton in colOurspace_LoginButtons)
            //    {
            //        strXML += "<Ourspace_LoginButton>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_LoginButton.Content) + "</content>";
            //        strXML += "</Ourspace_LoginButton>";
            //    }
            //    strXML += "</Ourspace_LoginButtons>";
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
            //XmlNode xmlOurspace_LoginButtons = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_LoginButtons");
            //foreach (XmlNode xmlOurspace_LoginButton in xmlOurspace_LoginButtons.SelectNodes("Ourspace_LoginButton"))
            //{
            //    Ourspace_LoginButtonInfo objOurspace_LoginButton = new Ourspace_LoginButtonInfo();
            //    objOurspace_LoginButton.ModuleId = ModuleID;
            //    objOurspace_LoginButton.Content = xmlOurspace_LoginButton.SelectSingleNode("content").InnerText;
            //    objOurspace_LoginButton.CreatedByUser = UserID;
            //    AddOurspace_LoginButton(objOurspace_LoginButton);
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

            //List<Ourspace_LoginButtonInfo> colOurspace_LoginButtons = GetOurspace_LoginButtons(ModInfo.ModuleID);

            //foreach (Ourspace_LoginButtonInfo objOurspace_LoginButton in colOurspace_LoginButtons)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_LoginButton.Content, objOurspace_LoginButton.CreatedByUser, objOurspace_LoginButton.CreatedDate, ModInfo.ModuleID, objOurspace_LoginButton.ItemId.ToString(), objOurspace_LoginButton.Content, "ItemId=" + objOurspace_LoginButton.ItemId.ToString());
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
