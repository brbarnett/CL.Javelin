using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;

namespace CL.Javelin.Clients.Shared.Views
{
    public static class SystemColors
    {
        public static Color SystemAltHighColor;
        public static Color SystemAltLowColor;
        public static Color SystemAltMediumColor;
        public static Color SystemAltMediumHighColor;
        public static Color SystemAltMediumLowColor;
        public static Color SystemBaseHighColor;
        public static Color SystemBaseLowColor;
        public static Color SystemBaseMediumColor;
        public static Color SystemBaseMediumHighColor;
        public static Color SystemBaseMediumLowColor;
        public static Color SystemChromeAltLowColor;
        public static Color SystemChromeBlackHighColor;
        public static Color SystemChromeBlackLowColor;
        public static Color SystemChromeBlackMediumLowColor;
        public static Color SystemChromeBlackMediumColor;
        public static Color SystemChromeDisabledHighColor;
        public static Color SystemChromeDisabledLowColor;
        public static Color SystemChromeHighColor;
        public static Color SystemChromeLowColor;
        public static Color SystemChromeMediumColor;
        public static Color SystemChromeMediumLowColor;
        public static Color SystemChromeWhiteColor;
        public static Color SystemListLowColor;
        public static Color SystemListMediumColor;

        public static Color SystemColorButtonFaceColor;
        public static Color SystemColorButtonTextColor;
        public static Color SystemColorGrayTextColor;
        public static Color SystemColorHighlightColor;
        public static Color SystemColorHighlightTextColor;
        public static Color SystemColorHotlightColor;
        public static Color SystemColorWindowColor;
        public static Color SystemColorWindowTextColor;


        public static void Initialize(ResourceDictionary resourceDictionary)
        {
            SystemAltHighColor = (Color)resourceDictionary["SystemAltHighColor"];
            SystemAltLowColor = (Color)resourceDictionary["SystemAltLowColor"];
            SystemAltMediumColor = (Color)resourceDictionary["SystemAltMediumColor"];
            SystemAltMediumHighColor = (Color)resourceDictionary["SystemAltMediumHighColor"];
            SystemAltMediumLowColor = (Color)resourceDictionary["SystemAltMediumLowColor"];
            SystemBaseHighColor = (Color)resourceDictionary["SystemBaseHighColor"];
            SystemBaseLowColor = (Color)resourceDictionary["SystemBaseLowColor"];
            SystemBaseMediumColor = (Color)resourceDictionary["SystemBaseMediumColor"];
            SystemBaseMediumHighColor = (Color)resourceDictionary["SystemBaseMediumHighColor"];
            SystemBaseMediumLowColor = (Color)resourceDictionary["SystemBaseMediumLowColor"];
            SystemChromeAltLowColor = (Color)resourceDictionary["SystemChromeAltLowColor"];
            SystemChromeBlackHighColor = (Color)resourceDictionary["SystemChromeBlackHighColor"];
            SystemChromeBlackLowColor = (Color)resourceDictionary["SystemChromeBlackLowColor"];
            SystemChromeBlackMediumLowColor = (Color)resourceDictionary["SystemChromeBlackMediumLowColor"];
            SystemChromeBlackMediumColor = (Color)resourceDictionary["SystemChromeBlackMediumColor"];
            SystemChromeDisabledHighColor = (Color)resourceDictionary["SystemChromeDisabledHighColor"];
            SystemChromeDisabledLowColor = (Color)resourceDictionary["SystemChromeDisabledLowColor"];
            SystemChromeHighColor = (Color)resourceDictionary["SystemChromeHighColor"];
            SystemChromeLowColor = (Color)resourceDictionary["SystemChromeLowColor"];
            SystemChromeMediumColor = (Color)resourceDictionary["SystemChromeMediumColor"];
            SystemChromeMediumLowColor = (Color)resourceDictionary["SystemChromeMediumLowColor"];
            SystemChromeWhiteColor = (Color)resourceDictionary["SystemChromeWhiteColor"];
            SystemListLowColor = (Color)resourceDictionary["SystemListLowColor"];
            SystemListMediumColor = (Color)resourceDictionary["SystemListMediumColor"];

            SystemColorButtonFaceColor = (Color)resourceDictionary["SystemListMediumColor"];
            SystemColorButtonTextColor = (Color)resourceDictionary["SystemListMediumColor"];
            SystemColorGrayTextColor = (Color)resourceDictionary["SystemListMediumColor"];
            SystemColorHighlightColor = (Color)resourceDictionary["SystemListMediumColor"];
            SystemColorHighlightTextColor = (Color)resourceDictionary["SystemListMediumColor"];
            SystemColorHotlightColor = (Color)resourceDictionary["SystemListMediumColor"];
            SystemColorWindowColor = (Color)resourceDictionary["SystemListMediumColor"];
            SystemColorWindowTextColor = (Color)resourceDictionary["SystemListMediumColor"];
        }
    }
}
