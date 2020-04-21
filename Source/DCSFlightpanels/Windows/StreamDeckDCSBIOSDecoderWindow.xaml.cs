﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using ClassLibraryCommon;
using DCS_BIOS;
using DCSFlightpanels.Properties;
using DCSFlightpanels.Shared;
using NonVisuals.StreamDeck;
using Color = System.Drawing.Color;
using Cursors = System.Windows.Input.Cursors;
using DataGrid = System.Windows.Controls.DataGrid;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using TextBox = System.Windows.Controls.TextBox;

namespace DCSFlightpanels.Windows
{
    /// <summary>
    /// This StreamDeck implementation is a big clusterf*ck.
    /// </summary>
    public partial class StreamDeckDCSBIOSDecoderWindow : Window
    {
        private string _streamDeckInstanceId;
        private bool _formLoaded;
        private readonly string _typeToSearch = "Type to search control";
        private Popup _popupSearch;
        private DataGrid _popupDataGrid;
        private readonly IEnumerable<DCSBIOSControl> _dcsbiosControls;
        private DCSBIOSControl _dcsbiosControl;
        private readonly JaceExtended _jaceExtended = new JaceExtended();
        private Dictionary<string, double> _variables = new Dictionary<string, double>();
        private bool _formulaHadErrors = false;
        private string _decoderResult;
        private bool _isDirty = false;
        private bool _populatingData = false;

        private DCSBIOSDecoder _dcsbiosDecoder = null;

        private bool _exitThread;
        
        private readonly string _formulaFile = AppDomain.CurrentDomain.BaseDirectory + "\\formulas.txt";





        public StreamDeckDCSBIOSDecoderWindow(DCSBIOSDecoder dcsbiosDecoder, string streamDeckInstanceId)
        {
            InitializeComponent();
            _dcsbiosDecoder = dcsbiosDecoder;
            DCSBIOSControlLocator.LoadControls();
            _dcsbiosControls = DCSBIOSControlLocator.GetIntegerOutputControls();
            _streamDeckInstanceId = streamDeckInstanceId;
            var thread = new Thread(ThreadLoop);
            thread.Start();
        }

        public StreamDeckDCSBIOSDecoderWindow(string streamDeckInstanceId, EnumStreamDeckButtonNames streamDeckButton)
        {
            InitializeComponent();
            _dcsbiosDecoder = new DCSBIOSDecoder();
            LoadDefaults();
            DCSBIOSControlLocator.LoadControls();
            _dcsbiosControls = DCSBIOSControlLocator.GetIntegerOutputControls();
            _dcsbiosDecoder.StreamDeckButtonName = streamDeckButton;
            _streamDeckInstanceId = streamDeckInstanceId;
            var thread = new Thread(ThreadLoop);
            thread.Start();
        }
        
        private void StreamDeckDCSBIOSDecoderWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_formLoaded)
                {
                    return;
                }

                ShowDecoder();
                _dcsbiosDecoder.IsVisible = true;
                _popupSearch = (Popup)FindResource("PopUpSearchResults");
                _popupSearch.Height = 400;
                _popupDataGrid = ((DataGrid)LogicalTreeHelper.FindLogicalNode(_popupSearch, "PopupDataGrid"));
                _formLoaded = true;
                SetFormState();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(1001, ex);
            }
        }

        private void SetFormState()
        {
            if (!_formLoaded)
            {
                return;
            }

            StackPanelNumberConversion.Visibility = RadioButtonOutputString.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
            StackPanelNumberConversion.IsEnabled = RadioButtonOutputString.IsChecked == true;
            ButtonAddConverter.IsEnabled = RadioButtonOutputString.IsChecked == true;

            ButtonEditConverter.IsEnabled = DataGridDecoders.SelectedItems.Count == 1;
            ButtonDeleteConverter.IsEnabled = DataGridDecoders.SelectedItems.Count == 1;
            ButtonSave.IsEnabled = !string.IsNullOrEmpty(TextBoxDCSBIOSId.Text);

            GroupBoxFormula.IsEnabled = CheckBoxUseFormula.IsChecked == true;
        }

        private void ShowDecoder()
        {
            _populatingData = true;
            if (!string.IsNullOrEmpty(_dcsbiosDecoder.Formula))
            {
                CheckBoxUseFormula.IsChecked = true;
                TextBoxFormula.Text = _dcsbiosDecoder.Formula;
            }

            if (_dcsbiosDecoder.DCSBIOSOutput != null)
            {
                TextBoxDCSBIOSId.Text = _dcsbiosDecoder.DCSBIOSOutput.ControlId;
            }
            ShowConverters();
            UpdateFontInfo();
            _populatingData = false;
        }

        private void ThreadLoop()
        {
            try
            {
                while (!_exitThread)
                {
                    if (_dcsbiosDecoder.ValueUpdated)
                    {
                        try
                        {
                            SetFormulaError(_dcsbiosDecoder.HasErrors ? _dcsbiosDecoder.LastFormulaError : "");
                            SetFormulaResult(_dcsbiosDecoder.FormulaResult);
                            SetRawDCSBIOSValue(_dcsbiosDecoder.UintDcsBiosValue);
                        }
                        catch (Exception e)
                        {
                            SetFormulaError(e.Message);
                        }
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                LabelErrors.Content = "Failed to start thread " + ex.Message;
            }
        }
        
        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (_popupDataGrid.SelectedItems.Count == 1)
                {
                    _dcsbiosControl = (DCSBIOSControl)_popupDataGrid.SelectedItem;
                    _dcsbiosDecoder.DCSBIOSOutput = DCSBIOSControlLocator.GetDCSBIOSOutput(_dcsbiosControl.identifier);

                    TextBoxDCSBIOSId.Text = _dcsbiosControl.identifier;
                    TextBoxSearch.Text = _typeToSearch;
                    SetIsDirty();
                    SetFormState();
                }
                _popupSearch.IsOpen = false;
                SetFormState();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(1006, ex);
            }
        }

        private void AdjustShownPopupData(TextBox textBox)
        {
            _popupSearch.PlacementTarget = textBox;
            _popupSearch.Placement = PlacementMode.Bottom;
            _popupDataGrid.Tag = textBox;
            if (!_popupSearch.IsOpen)
            {
                _popupSearch.IsOpen = true;
            }
            if (_popupDataGrid != null)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    _popupDataGrid.DataContext = _dcsbiosControls;
                    _popupDataGrid.ItemsSource = _dcsbiosControls;
                    _popupDataGrid.Items.Refresh();
                    return;
                }
                var subList = _dcsbiosControls.Where(controlObject => (!string.IsNullOrWhiteSpace(controlObject.identifier) && controlObject.identifier.ToUpper().Contains(textBox.Text.ToUpper()))
                                                                      || (!string.IsNullOrWhiteSpace(controlObject.description) && controlObject.description.ToUpper().Contains(textBox.Text.ToUpper())));
                _popupDataGrid.DataContext = subList;
                _popupDataGrid.ItemsSource = subList;
                _popupDataGrid.Items.Refresh();
            }
        }

        private void TextBoxSearch_OnKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                AdjustShownPopupData((TextBox)sender);
                SetFormState();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(1005, ex);
            }
        }

        private void RepeatButtonActionPressUp_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _dcsbiosDecoder.OffsetY -= StreamDeckConstants.ADJUST_OFFSET_CHANGE_VALUE;
                SetIsDirty();
                Settings.Default.ButtonFaceOffsetY = _dcsbiosDecoder.OffsetY;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void RepeatButtonActionPressDown_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _dcsbiosDecoder.OffsetY += StreamDeckConstants.ADJUST_OFFSET_CHANGE_VALUE;
                SetIsDirty();
                Settings.Default.ButtonFaceOffsetY = _dcsbiosDecoder.OffsetY;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void RepeatButtonActionPressLeft_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _dcsbiosDecoder.OffsetX -= StreamDeckConstants.ADJUST_OFFSET_CHANGE_VALUE;
                SetIsDirty();
                Settings.Default.ButtonFaceOffsetX = _dcsbiosDecoder.OffsetX;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }
        
        private void RepeatButtonActionPressRight_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _dcsbiosDecoder.OffsetX += StreamDeckConstants.ADJUST_OFFSET_CHANGE_VALUE;
                SetIsDirty();
                Settings.Default.ButtonFaceOffsetX = _dcsbiosDecoder.OffsetX;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void TextBoxFormula_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetIsDirty();
            SetFormState();
        }

        private void ButtonFormulaHelp_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var helpWindow = new JaceHelpWindow();
                helpWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBoxDCSBIOSId.Text = "";
                LabelSourceRawValue.Content = "";
                LabelResult.Content = "";
                LabelErrors.Content = "";
                TextBoxSearch.Text = _typeToSearch;
                TextBoxSearch.Foreground = new SolidColorBrush(Colors.Gainsboro);
                _dcsbiosDecoder.RemoveDCSBIOSOutput();
                _dcsbiosControl = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void ButtonTextFaceFont_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var font = Settings.Default.ButtonTextFaceFont;

                if (StreamDeckCommon.SetFontStyle(ref font) == System.Windows.Forms.DialogResult.OK)
                {
                    _dcsbiosDecoder.TextFont = font;
                    SetIsDirty();
                    UpdateFontInfo();
                    SetFormState();
                }
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void ButtonTextFaceFontColor_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var color = Color.Transparent;

                if (StreamDeckCommon.SetFontColor(ref color) == System.Windows.Forms.DialogResult.OK)
                {
                    _dcsbiosDecoder.FontColor = color;
                    SetIsDirty();
                    UpdateFontInfo();
                    SetFormState();
                }
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void ButtonTextFaceBackgroundColor_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var color = Color.Transparent;

                if (StreamDeckCommon.SetBackgroundColor(ref color) == System.Windows.Forms.DialogResult.OK)
                {
                    _dcsbiosDecoder.BackgroundColor = color;
                    SetIsDirty();
                    UpdateFontInfo();
                    SetFormState();
                }
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void UpdateFontInfo()
        {
            TextBoxFontInfo.Text = "Font : " + _dcsbiosDecoder.TextFont.Name + " " +
                                   _dcsbiosDecoder.TextFont.Size + " " +
                                   (_dcsbiosDecoder.TextFont.Bold ? "Bold" : "Regular");
            TextBoxFontInfo.Text = TextBoxFontInfo.Text + "\n" + "Font Color : " + _dcsbiosDecoder.FontColor.ToString();
            TextBoxFontInfo.Text = TextBoxFontInfo.Text + "\n" + "Background Color : " + _dcsbiosDecoder.BackgroundColor.ToString();
        }
        
        private void SetIsDirty()
        {
            if (_populatingData)
            {
                return;
            }
            _isDirty = true;
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBoxSearch_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Text = "";
            textbox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void TextBoxSearch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Text = _typeToSearch;
            textbox.Foreground = new SolidColorBrush(Colors.Gainsboro);
        }

        private void ShowConverters()
        {
            /*RadioButtonOutputString.IsChecked = DCSBIOSDecoders.Count > 0;
            DataGridDecoders.DataContext = DCSBIOSDecoders;
            DataGridDecoders.ItemsSource = DCSBIOSDecoders;*/
            DataGridDecoders.Items.Refresh();
        }

        private void ButtonAddConverter_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = new DCSBIOSConverterWindow();
                window.ShowDialog();
                if (window.DialogResult == true)
                {
                    //_dcsbiosDecoder.Add(window.DCSBIOSComparator);
                    ShowConverters();
                }

                SetFormState();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void ButtonEditConverter_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                /*var window = new DCSBIOSComparatorWindow((DCSBIOSNumberToText)DataGridDecoders.SelectedItems[0]);
                window.ShowDialog();
                if (window.DialogResult == true)
                {
                    _dcsbiosDecoder.Remove((DCSBIOSNumberToText)DataGridDecoders.SelectedItems[0]);
                    _dcsbiosDecoder.Add(window.DCSBIOSComparator);
                    SetIsDirty();
                    ShowDecoders();
                }*/

                SetFormState();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void ButtonDeleteConverter_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                /*_dcsbiosDecoder.Remove((DCSBIOSNumberToText)DataGridDecoders.SelectedItems[0]);
                RadioButtonOutputNumber.IsChecked = DCSBIOSDecoders.Count == 0;
                SetIsDirty();
                ShowDecoders();
                SetFormState();*/
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void RadioButtonOutput_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SetFormState();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }
        /*
        public List<DCSBIOSNumberToText> DCSBIOSDecoders
        {
            get => _dcsbiosDecoder.DCSBIOSDecoders;
        }
        */
        private void DataGridDecoders_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SetFormState();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void CheckBoxUseFormula_OnChange(object sender, RoutedEventArgs e)
        {
            try
            {
                SetFormState();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }
        
        private void LoadDefaults()
        {
            _dcsbiosDecoder.OffsetX = Settings.Default.ButtonFaceOffsetX;
            _dcsbiosDecoder.OffsetY = Settings.Default.ButtonFaceOffsetY;
            _dcsbiosDecoder.TextFont = Settings.Default.ButtonTextFaceFont;
            _dcsbiosDecoder.FontColor = Settings.Default.ButtonTextFaceFontColor;
            _dcsbiosDecoder.BackgroundColor = Settings.Default.ButtonTextFaceBackgroundColor;
        }

        /*
        private void DisplayButtonImage()
        {
            Dispatcher?.BeginInvoke(
                (Action)delegate
                {
                    ButtonImage.Source = CommonStreamDeck.ConvertBitMap(_dcsbiosDecoder.Bitmap);
                });
        }
        */
        private void SetFormulaError(string error)
        {
            Dispatcher?.BeginInvoke(
                (Action)delegate
                {
                    LabelErrors.Content = error;
                });
        }
        
        private void SetRawDCSBIOSValue(uint value)
        {
            Dispatcher?.BeginInvoke(
                (Action)delegate
                {
                    LabelSourceRawValue.Content = value;
                });
        }

        private void SetFormulaResult(double result)
        {
            Dispatcher?.BeginInvoke(
                (Action)delegate
                {
                    LabelResult.Content = result.ToString(CultureInfo.InvariantCulture);
                });
        }

        private void TextBoxFormula_OnKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                _dcsbiosDecoder.Formula = TextBoxFormula.Text.Replace(Environment.NewLine, "");
                SetIsDirty();
                SetFormState();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }
        
        private void TextBlockFormulas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.Start(_formulaFile);
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void Control_OnMouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void Control_OnMouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void LabelInsert_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TextBoxDCSBIOSId.Text))
                {
                    CheckBoxUseFormula.IsChecked = true;
                    TextBoxFormula.Text = TextBoxDCSBIOSId.Text + " " + TextBoxFormula.Text;
                    SystemSounds.Asterisk.Play();
                }
                SetFormState();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }


        private void CloseWindow()
        {
            _exitThread = true;
            Close();
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = true;
                CloseWindow();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isDirty)
                {
                    if (MessageBox.Show("Discard changes?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                DialogResult = false;
                CloseWindow();
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        public DCSBIOSDecoder DCSBIOSDecoder => _dcsbiosDecoder;

        private void RadioButtonIntegerSource_OnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void RadioButtonStringSource_OnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }

        private void CheckBoxStringAsNumber_OnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Common.ShowErrorMessageBox(ex);
            }
        }
    }
}