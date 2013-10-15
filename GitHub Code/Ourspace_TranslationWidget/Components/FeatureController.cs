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

namespace DotNetNuke.Modules.Ourspace_TranslationWidget.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Ourspace_TranslationWidget
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

            //List<Ourspace_TranslationWidgetInfo> colOurspace_TranslationWidgets = GetOurspace_TranslationWidgets(ModuleID);
            //if (colOurspace_TranslationWidgets.Count != 0)
            //{
            //    strXML += "<Ourspace_TranslationWidgets>";

            //    foreach (Ourspace_TranslationWidgetInfo objOurspace_TranslationWidget in colOurspace_TranslationWidgets)
            //    {
            //        strXML += "<Ourspace_TranslationWidget>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOurspace_TranslationWidget.Content) + "</content>";
            //        strXML += "</Ourspace_TranslationWidget>";
            //    }
            //    strXML += "</Ourspace_TranslationWidgets>";
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
            //XmlNode xmlOurspace_TranslationWidgets = DotNetNuke.Common.Globals.GetContent(Content, "Ourspace_TranslationWidgets");
            //foreach (XmlNode xmlOurspace_TranslationWidget in xmlOurspace_TranslationWidgets.SelectNodes("Ourspace_TranslationWidget"))
            //{
            //    Ourspace_TranslationWidgetInfo objOurspace_TranslationWidget = new Ourspace_TranslationWidgetInfo();
            //    objOurspace_TranslationWidget.ModuleId = ModuleID;
            //    objOurspace_TranslationWidget.Content = xmlOurspace_TranslationWidget.SelectSingleNode("content").InnerText;
            //    objOurspace_TranslationWidget.CreatedByUser = UserID;
            //    AddOurspace_TranslationWidget(objOurspace_TranslationWidget);
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

            //List<Ourspace_TranslationWidgetInfo> colOurspace_TranslationWidgets = GetOurspace_TranslationWidgets(ModInfo.ModuleID);

            //foreach (Ourspace_TranslationWidgetInfo objOurspace_TranslationWidget in colOurspace_TranslationWidgets)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOurspace_TranslationWidget.Content, objOurspace_TranslationWidget.CreatedByUser, objOurspace_TranslationWidget.CreatedDate, ModInfo.ModuleID, objOurspace_TranslationWidget.ItemId.ToString(), objOurspace_TranslationWidget.Content, "ItemId=" + objOurspace_TranslationWidget.ItemId.ToString());
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
