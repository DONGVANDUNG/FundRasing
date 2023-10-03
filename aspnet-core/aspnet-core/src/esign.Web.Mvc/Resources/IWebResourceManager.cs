using System.Collections.Generic;
using Abp;
using Microsoft.AspNetCore.Mvc.Razor;

namespace esign.Web.Resources
{
    public interface IWebResourceManager
    {
        void AddScript(string url, bool addMinifiedOnesign = true);

        void AddScriptTag(string url, List<NameValue> attributes);

        IReadOnlyList<string> GetScripts();

        HelperResult RenderScripts();
    }
}