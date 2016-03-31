using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace CL.Javelin.Clients.Shared.Views
{
    public static class ThemeBrushes
    {
        static ThemeBrushes()
        {
            _brushes = new Dictionary<string, Brush>();
        }

        public static void Initialize(ResourceDictionary resourceDictionary)
        {
            Initialize(_brushes, resourceDictionary);
        }

        public static readonly Dictionary<string, Brush> _brushes = new Dictionary<string, Brush>(); 
        private static void Initialize(Dictionary<string, Brush> hash, ResourceDictionary resourceDictionary)
        {
            if (!ReferenceEquals(hash, null) &&
                !ReferenceEquals(resourceDictionary, null))
            {
                hash["SystemControlBackgroundAltHighBrush"] = (Brush)resourceDictionary["SystemControlBackgroundAltHighBrush"];
                hash["SystemControlBackgroundAltMediumLowBrush"] = (Brush)resourceDictionary["SystemControlBackgroundAltMediumLowBrush"];
                hash["SystemControlBackgroundBaseLowBrush"] = (Brush)resourceDictionary["SystemControlBackgroundBaseLowBrush"];
                hash["SystemControlBackgroundBaseMediumBrush"] = (Brush)resourceDictionary["SystemControlBackgroundBaseMediumBrush"];
                hash["SystemControlBackgroundBaseMediumLowBrush"] = (Brush)resourceDictionary["SystemControlBackgroundBaseMediumLowBrush"];
                hash["SystemControlBackgroundChromeMediumLowBrush"] = (Brush)resourceDictionary["SystemControlBackgroundChromeMediumLowBrush"];
                hash["SystemControlBackgroundListMediumBrush"] = (Brush)resourceDictionary["SystemControlBackgroundListMediumBrush"];
                hash["SystemControlDisabledBaseLowBrush"] = (Brush)resourceDictionary["SystemControlDisabledBaseLowBrush"];
                hash["SystemControlDisabledBaseMediumLowBrush"] = (Brush)resourceDictionary["SystemControlDisabledBaseMediumLowBrush"];
                hash["SystemControlDisabledChromeDisabledHighBrush"] = (Brush)resourceDictionary["SystemControlDisabledChromeDisabledHighBrush"];
                hash["SystemControlDisabledTransparentBrush"] = (Brush)resourceDictionary["SystemControlDisabledTransparentBrush"];
                hash["SystemControlForegroundAccentBrush"] = (Brush)resourceDictionary["SystemControlForegroundAccentBrush"];
                hash["SystemControlForegroundBaseHighBrush"] = (Brush)resourceDictionary["SystemControlForegroundBaseHighBrush"];
                hash["SystemControlForegroundBaseLowBrush"] = (Brush)resourceDictionary["SystemControlForegroundBaseLowBrush"];
                hash["SystemControlForegroundBaseMediumBrush"] = (Brush)resourceDictionary["SystemControlForegroundBaseMediumBrush"];
                hash["SystemControlForegroundBaseMediumHighBrush"] = (Brush)resourceDictionary["SystemControlForegroundBaseMediumHighBrush"];
                hash["SystemControlForegroundBaseMediumLowBrush"] = (Brush)resourceDictionary["SystemControlForegroundBaseMediumLowBrush"];
                hash["SystemControlForegroundChromeBlackMediumBrush"] = (Brush)resourceDictionary["SystemControlForegroundChromeBlackMediumBrush"];
                hash["SystemControlForegroundChromeDisabledLowBrush"] = (Brush)resourceDictionary["SystemControlForegroundChromeDisabledLowBrush"];
                hash["SystemControlForegroundTransparentBrush"] = (Brush)resourceDictionary["SystemControlForegroundTransparentBrush"];
                hash["SystemControlHighlightAccentBrush"] = (Brush)resourceDictionary["SystemControlHighlightAccentBrush"];
                hash["SystemControlHighlightAltAccentBrush"] = (Brush)resourceDictionary["SystemControlHighlightAltAccentBrush"];
                hash["SystemControlHighlightAltBaseHighBrush"] = (Brush)resourceDictionary["SystemControlHighlightAltBaseHighBrush"];
                hash["SystemControlHighlightAltBaseMediumBrush"] = (Brush)resourceDictionary["SystemControlHighlightAltBaseMediumBrush"];
                hash["SystemControlHighlightAltBaseMediumHighBrush"] = (Brush)resourceDictionary["SystemControlHighlightAltBaseMediumHighBrush"];
                hash["SystemControlHighlightAltChromeWhiteBrush"] = (Brush)resourceDictionary["SystemControlHighlightAltChromeWhiteBrush"];
                hash["SystemControlHighlightAltTransparentBrush"] = (Brush)resourceDictionary["SystemControlHighlightAltTransparentBrush"];
                hash["SystemControlHighlightBaseHighBrush"] = (Brush)resourceDictionary["SystemControlHighlightBaseHighBrush"];
                hash["SystemControlHighlightBaseMediumBrush"] = (Brush)resourceDictionary["SystemControlHighlightBaseMediumBrush"];
                hash["SystemControlHighlightBaseMediumLowBrush"] = (Brush)resourceDictionary["SystemControlHighlightBaseMediumLowBrush"];
                hash["SystemControlHighlightChromeAltLowBrush"] = (Brush)resourceDictionary["SystemControlHighlightChromeAltLowBrush"];
                hash["SystemControlHighlightChromeHighBrush"] = (Brush)resourceDictionary["SystemControlHighlightChromeHighBrush"];
                hash["SystemControlHighlightListAccentHighBrush"] = (Brush)resourceDictionary["SystemControlHighlightListAccentHighBrush"];
                hash["SystemControlHighlightListAccentLowBrush"] = (Brush)resourceDictionary["SystemControlHighlightListAccentLowBrush"];
                hash["SystemControlHighlightTransparentBrush"] = (Brush)resourceDictionary["SystemControlHighlightTransparentBrush"];
                hash["SystemControlPageBackgroundAltMediumBrush"] = (Brush)resourceDictionary["SystemControlPageBackgroundAltMediumBrush"];
                hash["SystemControlPageBackgroundBaseLowBrush"] = (Brush)resourceDictionary["SystemControlPageBackgroundBaseLowBrush"];
                hash["SystemControlPageTextBaseHighBrush"] = (Brush)resourceDictionary["SystemControlPageTextBaseHighBrush"];
                hash["TextBoxButtonBackgroundThemeBrush"] = (Brush)resourceDictionary["TextBoxButtonBackgroundThemeBrush"];
                hash["TextBoxButtonBorderThemeBrush"] = (Brush)resourceDictionary["TextBoxButtonBorderThemeBrush"];

                DebugPrintBrushes();
            }
        }

        /// <summary>
        /// Copy and paste of the output of this into a theme override resource dictionary
        /// xaml and you can tweak the individual colors
        /// </summary>
        [Conditional("DEBUG")]
        public static void DebugPrintBrushes()
        {
            foreach (string key in _brushes.Keys)
            {
                Brush brush = _brushes[key];
                SolidColorBrush solidBrush = brush as SolidColorBrush;
                if (!ReferenceEquals(solidBrush, null))
                {
                    string s = string.Format("<SolidColorBrush x:Key=\"{0}\" Color=\"{1}\" />", key, solidBrush.Color);
                    Debug.WriteLine(s);
                }
            }
        }
    }
}
