using esign.Sessions.Dto;

namespace esign.Web.Areas.App.Models.Layout
{
    public class FooterViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public string GetesignuctNameWithEdition()
        {
            const string esignuctName = "esign";

            if (LoginInformations.Tenant?.Edition?.DisplayName == null)
            {
                return esignuctName;
            }

            return esignuctName + " " + LoginInformations.Tenant.Edition.DisplayName;
        }
    }

    public class SubheaderViewModel
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}
