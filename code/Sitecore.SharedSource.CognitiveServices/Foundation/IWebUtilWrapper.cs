﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.SharedSource.CognitiveServices.Foundation
{
    public interface IWebUtilWrapper
    {
        string GetQueryString(string key, string defaultValue = "");
    }
}