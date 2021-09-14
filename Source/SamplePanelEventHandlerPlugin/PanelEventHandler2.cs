﻿namespace SamplePanelEventPlugin
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Windows.Forms;

    using MEF;

    /*
     * Use this class as a template for your plugin.
     * Reference the MEF project where the interface and necessary files are located.
     */
    [Export(typeof(IPanelEventHandler))]
    [ExportMetadata("Name", "Sample Plugin 2")]
    public class PanelEventHandler2 : IPanelEventHandler
    {
        public void PanelEvent(string profile, string panelHidId, int panelId, int switchId, bool pressed, SortedList<int, IKeyPressInfo> keySequence)
        {
            /*
             * Your code here
             */
            MessageBox.Show(profile + " PanelID : " + panelId + " SwitchId : " + switchId + " Action : " + (pressed ? "Pressed" : "Released"));
        }

        public void Settings()
        {
            var name = this.GetType()
                .GetCustomAttributes(false)
                .OfType<ExportMetadataAttribute>()
                .Single(attribute => attribute.Name == "Name").Value;
            MessageBox.Show("This would be the settings for plugin " + name, "You clicked the plugin menu.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
