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

namespace DotNetNuke.Modules.Ourspace_RegistrationWelcome.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_RegistrationWelcome
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

            //List<Ourspace_RegistrationWelcomeInfo> colOurspace_RegistrationWelcomes = GetOurspace_RegistrationWelcomes(ModuleID);
            //if (colOurspace_RegistrationWelcomes.Count != 0)
            //{
            //    strXML += "<Ourspace_RegistrationWelcomes>";

            //    foreach (Ourspace_RegistrationWelcomeInfo objOurspace_RegistrationWelcome in colOurspace_RegistrationWelcomes)
            //    {
            //        strXML += "<Ourspace_RegistrationWelcome>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_RegistrationWelcome.Content) + "</content>";
            //        strXML += "</Ourspace_RegistrationWelcome>";
            //    }
            //    strXML += "</Ourspace_RegistrationWelcomes>";
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
            //XmlNode xmlOurspace_RegistrationWelcomes = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_RegistrationWelcomes");
            //foreach (XmlNode xmlOurspace_RegistrationWelcome in xmlOurspace_RegistrationWelcomes.SelectNodes("Ourspace_RegistrationWelcome"))
            //{
            //    Ourspace_RegistrationWelcomeInfo objOurspace_RegistrationWelcome = new Ourspace_RegistrationWelcomeInfo();
            //    objOurspace_RegistrationWelcome.ModuleId = ModuleID;
            //    objOurspace_RegistrationWelcome.Content = xmlOurspace_RegistrationWelcome.SelectSingleNode("content").InnerText;
            //    objOurspace_RegistrationWelcome.CreatedByUser = UserID;
            //    AddOurspace_RegistrationWelcome(objOurspace_RegistrationWelcome);
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

            //List<Ourspace_RegistrationWelcomeInfo> colOurspace_RegistrationWelcomes = GetOurspace_RegistrationWelcomes(ModInfo.ModuleID);

            //foreach (Ourspace_RegistrationWelcomeInfo objOurspace_RegistrationWelcome in colOurspace_RegistrationWelcomes)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_RegistrationWelcome.Content, objOurspace_RegistrationWelcome.CreatedByUser, objOurspace_RegistrationWelcome.CreatedDate, ModInfo.ModuleID, objOurspace_RegistrationWelcome.ItemId.ToString(), objOurspace_RegistrationWelcome.Content, "ItemId=" + objOurspace_RegistrationWelcome.ItemId.ToString());
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
