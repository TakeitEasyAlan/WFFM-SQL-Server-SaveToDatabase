// // // *************************************************************************
// // // *                                                                       *
// // // * http://www.pentia.net/                                                *
// // // *                                                                       *
// // // * Copyright(C) Pentia A/S, 1997 - 2012. All rights reserved (R).        *
// // // *                                                                       *
// // // * LEGAL NOTICE: This is unpublished proprietary source code of Pentia.  *
// // // * The contents of this file are protected by copyright laws and         *
// // // * international copyright treaties, as well as other intellectual       *
// // // * property laws and treaties. These contents may not be extracted,      *
// // // * copied, modified or redistributed either as a whole or part thereof   *
// // // * in any form, and may not be used directly in, or to assist with, the  *
// // // * creation of derivative works of any nature without prior written      *
// // // * permission from Pentia. The above copyright notice does not           *
// // // * evidence any actual or intended publication of this file.             *
// // // *                                                                       *
// // // *************************************************************************

namespace WFFM.SQLServer.SaveToDatabase
{
  public class Constants
  {
    internal struct Settings
    {
      internal struct Name
      {
        public const string Connection = "WFM.ConnectionString";
      }
    }

    static internal string ConnectionString
    {
      get
      {
        return Sitecore.Configuration.Settings.GetConnectionString(Sitecore.Configuration.Settings.GetSetting(Settings.Name.Connection));
      }
    }

  }
}