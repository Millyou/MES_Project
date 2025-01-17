using System.Collections.Generic;

namespace WinFormsApp2.ViewInterface
{
    public interface IPopupView
    {
        void PopulatePortCombo(List<string> ports);
        void SelectPort(string selectedPort);
        void SetControlsEnabled(bool portComboEnabled,bool portCombo2Enabled,bool modeComboEnabled, bool connectionBtnEnabled, bool disconnectBtnEnabled);
        void ShowErrorMessage(string message);
        void ShowSuccessMessage(string message);
    }
}