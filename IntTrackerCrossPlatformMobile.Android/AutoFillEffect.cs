using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.Reflection;
using System.ComponentModel;
//using XamAutofill.PlatformConfiguration.AndroidSpecific;
using Entry = Xamarin.Forms.Entry;
using IntTrackerCrossPlatformMobile.Pages;

[assembly: ResolutionGroupName("com.adenearnshaw")]
[assembly: ExportEffect(typeof(IntTrackerCrossPlatformMobile.Droid.AutoFillEffect), "AutofillEffect")]
namespace IntTrackerCrossPlatformMobile.Droid
{
    class AutoFillEffect : PlatformEffect
    {
#pragma warning disable XA0001 // Find issues with Android API usage
        protected override void OnAttached()
        {
            try
            {
                if (!IsSupported())
                    return;

                var control = Control as Android.Widget.EditText;

                var hint = GetAndroidAutofillHint();
                control.ImportantForAutofill = Android.Views.ImportantForAutofill.Yes;
                control.SetAutofillHints(hint);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: {0}", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            if (!IsSupported())
                return;

            Control.SetAutofillHints(string.Empty);

            Control.ImportantForAutofill = Android.Views.ImportantForAutofill.Auto;
        }

        private bool IsSupported()
        {
            return Element as Entry != null &&
                   Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O;
        }

        private string GetAndroidAutofillHint()
        {
            var hint = ((Entry)Element).OnThisPlatform().AutofillHint();
            var constantValue = GetEnumDescription(hint);
            return constantValue;
        }

        private static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes != null && attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
#pragma warning restore XA0001 // Find issues with Android API usage
    
    }
}