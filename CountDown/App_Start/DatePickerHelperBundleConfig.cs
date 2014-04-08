using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(CountDown.App_Start.DatePickerHelperBundleConfig), "RegisterBundles")]

namespace CountDown.App_Start
{
	public class DatePickerHelperBundleConfig
	{
		public static void RegisterBundles()
		{
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
            "~/Scripts/bootstrap-datepicker.js"));

            BundleTable.Bundles.Add(new StyleBundle("~/Content/datepicker").Include(
            "~/Content/bootstrap-datepicker.css"));
		}
	}
}