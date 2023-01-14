using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ORSAPR.Model;
using ORSAPR.Tests;

namespace ORSAPR.View
{
    public partial class MainWindow : System.Windows.Forms.Form
    { 
        /// <summary>
        /// переменная цвета поля ошибки
        /// </summary>
        private readonly Color _errorColor = Color.LightPink;

        /// <summary>
        /// переменная цвета пустого поля
        /// </summary>
        private readonly Color _emptyColor = Color.White;

        /// <summary>
        /// переменная цвета корректно введенных данных
        /// </summary>
        private readonly Color _trueColor = Color.LightGreen;

        /// <summary>
        /// выделение памяти под объект параметра
        /// </summary>
        private ChiselData _chiselData = new ChiselData();

        /// <summary>
        /// выделение памяти под объект компас приложения
        /// </summary>
        private KompasConnector _kompasApp;

        /// <summary>
        /// выделение памяти под объект vменеджера
        /// </summary>
        private Manager _manager;

        public MainWindow()
        {
            InitializeComponent();
            CheckAffterInput();
        }

        /// <summary>
        /// метод замены точек на запятые, запрет ввода символов кроме double
        /// </summary>
        /// <param name="textBox"></param>
        private void PointValidation(TextBox textBox)
        {           
            string tmp = textBox.Text.Trim();
            string outS = string.Empty;
            bool comma = true;            
            foreach (char ch in tmp)
            {
                if (Char.IsDigit(ch) || (ch == ',' && comma))
                {                 
                    outS += ch;
                    if (ch == ',')
                        comma = false;
                    if(outS.ToString()[0] == ',' || outS.ToString()[0] == '0')
                    {

                        textBox.SelectionStart = outS.Length;
                    }
                }
            }
            textBox.Text = outS;
        }

        /// <summary>
        /// вывод после ошибки сообщения в тултип 
        /// </summary>
        /// <param name="exception"></param>
        private void OutputAfterErrorTextBox(TextBox textBox,
            Exception exception)
        {
            textBox.BackColor = _errorColor;
            var errorMessage = exception.Message;       
            toolTipInformation.SetToolTip(textBox, errorMessage);
        }         

        /// <summary>
        /// функция одбработчик ввода запятой первым символом
        /// </summary>
        /// <param name="textBox"></param>
        private void StartsWithComma(TextBox textBox)
        {
            if (Convert.ToString(textBox.Text).StartsWith(","))
            {
                if (textBox.Text.Length != 0)
                {
                    textBox.Text = textBox.Text.Insert(0, "0");
                }
            }
            
        }
        
        /// <summary>
        /// функция одбработчик ввода запятой последним символом
        /// </summary>
        /// <param name="textBox"></param>
        private void EndsWithComma(TextBox textBox)
        {
            if (textBox.Text.Contains(","))
            {
                textBox.Text = textBox.Text.TrimEnd('0');
            }
            if (Convert.ToString(textBox.Text).EndsWith(","))
            {
                if (textBox.Text.Length != 0)
                {
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                }
            }            
            if ((Convert.ToString(textBox.Text).All(x => ",0".Contains(x)) && textBox.Text != ""))
            {
                textBox.Text = "0";
            }
            else if (!Convert.ToString(textBox.Text).Equals("0"))
            {
                textBox.Text = textBox.Text.TrimStart('0');
            }

        }

        /// <summary>
        /// ограничивает ввод символов и знаков, в том
        /// случае если они были использованы ранее
        /// </summary>
        /// <param name="e"></param>
        /// <param name="textBox"></param>
        private void IfKeyPress(KeyPressEventArgs e, TextBox textBox)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
            if (e.KeyChar == (char)Keys.Space && ButtonBuild.Enabled)
            {
                ButtonBuild_Click(null, null);                
            }          
            if (Char.IsNumber(e.KeyChar) | ((e.KeyChar == Convert.ToChar(","))
                && !textBox.Text.Contains(","))
                | e.KeyChar == '\b' | e.KeyChar == (char)3 | e.KeyChar == (char)22
                | e.KeyChar == (char)1 | e.KeyChar == (char)24)
            {              
                return;
            }
            else e.Handled = true;
        }

        /// <summary>
        /// функция изменения текста в текстбоксе
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="errorMessage"></param>
        private void IfTextBoxTextChanged(TextBox textBox, double chiselData)
        {
            try
            {
                PointValidation(textBox);
                StartsWithComma(textBox);
                ReloadChiselData(textBox, chiselData);
                if (textBox.Text == "")
                {
                    textBox.BackColor = _emptyColor;                                
                }
                else
                {
                    textBox.BackColor = _trueColor;
                }
                toolTipInformation.SetToolTip(textBox, string.Empty);        
            }          
            catch (ArgumentException exception)
            {
                OutputAfterErrorTextBox(textBox, exception);               
            }
            catch (Exception exception)
            {
                OutputAfterErrorTextBox(textBox, exception);              
            }
        }

        /// <summary>
        /// функция обработчика события выхода из текстбокса
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="errorMessage"></param>
        private void IfTextBoxLeave(TextBox textBox)
        {
            try
            {
                EndsWithComma(textBox);
            }
            catch (ArgumentException exception)
            {
                OutputAfterErrorTextBox(textBox, exception);              
            }
        }        

        /// <summary>
        /// функция обновления данных фигуры
        /// </summary>
        private void ReloadChiselData(TextBox textBox, double chiselData)
        {           
            if ((chiselData == _chiselData.Width) && (textBox == TextBoxWidth) &&
                TextBoxWidth.Text != string.Empty)
            {              
                _chiselData.Width = Convert.ToDouble(TextBoxWidth.Text);                                   
            }         
            if ((chiselData == _chiselData.Length) && (textBox == TextBoxLength) &&
                TextBoxLength.Text != string.Empty)
            {              
                _chiselData.Length = Convert.ToDouble(TextBoxLength.Text);                             
            }        
            if ((chiselData == _chiselData.Height) && (textBox == TextBoxHeight) &&
                TextBoxHeight.Text != string.Empty)
            {              
                _chiselData.Height = Convert.ToDouble(TextBoxHeight.Text);      
            }
            if ((chiselData == _chiselData.BladeLength) && (textBox == TextBoxBladeLength) &&
               TextBoxBladeLength.Text != string.Empty)
            {
                _chiselData.BladeLength = Convert.ToDouble(TextBoxBladeLength.Text);
            }
            if ((chiselData == _chiselData.InnerLength) && (textBox == TextBoxInnerLength) &&
                TextBoxInnerLength.Text != string.Empty)
            {
                _chiselData.InnerLength = Convert.ToDouble(TextBoxInnerLength.Text);
            }
            if ((chiselData == _chiselData.InnerWidth) && (textBox == TextBoxInnerWidth) &&
                TextBoxInnerWidth.Text != string.Empty)
            {
                _chiselData.InnerWidth = Convert.ToDouble(TextBoxInnerWidth.Text);
            }
        }
      
        /// <summary>
        /// функция проверки правильности введенных данных во всех полях при изменении одного
        /// </summary>
        private void CheckAffterInput()
        {
            this._chiselData = null;
            this._chiselData = new ChiselData();
            IfTextBoxTextChanged(TextBoxWidth, _chiselData.Width);
            IfTextBoxTextChanged(TextBoxLength, _chiselData.Length);
            IfTextBoxTextChanged(TextBoxHeight, _chiselData.Height);
            IfTextBoxTextChanged(TextBoxBladeLength, _chiselData.BladeLength);
            IfTextBoxTextChanged(TextBoxInnerLength, _chiselData.InnerLength);
            IfTextBoxTextChanged(TextBoxInnerWidth, _chiselData.InnerWidth);
            OnOffBuildButton();
        }

        /// <summary>
        /// функция активатор кнопки Build
        /// </summary>
        private void OnOffBuildButton()
        {
            if(RadioButtonLocksmith.Checked == true)
            {
                if ((_chiselData.Width != 0 && _chiselData.Length != 0 && _chiselData.Height != 0
                    && _chiselData.BladeLength != 0 && _chiselData.InnerLength != 0
                    && _chiselData.InnerWidth != 0) && (TextBoxWidth.Text != string.Empty
                    && TextBoxLength.Text != string.Empty && TextBoxHeight.Text != string.Empty
                    && TextBoxBladeLength.Text != string.Empty && TextBoxInnerLength.Text != string.Empty
                    && TextBoxInnerWidth.Text != string.Empty))
                {
                    ButtonBuild.Enabled = true;
                }
                else
                {
                    ButtonBuild.Enabled = false;
                }
            }
            if (RadioButtonPeak.Checked == true)
            {
                if ((_chiselData.Width != 0 && _chiselData.Length != 0 && _chiselData.Height != 0
                    && _chiselData.BladeLength != 0) && (TextBoxWidth.Text != string.Empty
                    && TextBoxLength.Text != string.Empty && TextBoxHeight.Text != string.Empty
                    && TextBoxBladeLength.Text != string.Empty))
                {
                    ButtonBuild.Enabled = true;
                }
                else
                {
                    ButtonBuild.Enabled = false;
                }
            }
        }

        /// <summary>
        /// функция вывода информации по допустимым параметрам ввода
        /// </summary>
        private void InformationTool()
        {
            if (TextBoxWidth.BackColor == _emptyColor)
            {
                toolTipInformation.SetToolTip(TextBoxWidth, "10mm <= W <= 30mm");
            }
            if (TextBoxLength.BackColor == _emptyColor)
            {
                if (TextBoxWidth.BackColor != _trueColor)
                {
                    toolTipInformation.SetToolTip(TextBoxLength, "100mm <= L <= 300mm");
                }
                if (TextBoxWidth.BackColor == _trueColor && Convert.ToDouble(TextBoxWidth.Text) == 30)
                {
                    toolTipInformation.SetToolTip(TextBoxLength,
                        $"{10 * _chiselData.Width}mm <= L <= 300mm");
                }
                else if (TextBoxWidth.BackColor == _trueColor && Convert.ToDouble(TextBoxWidth.Text) >= 29)
                {
                    toolTipInformation.SetToolTip(TextBoxLength,
                        $"{10 * _chiselData.Width}mm <= L < 300mm");
                }
                else if (TextBoxWidth.BackColor == _trueColor)
                {
                    toolTipInformation.SetToolTip(TextBoxLength,
                        $"{10 * _chiselData.Width}mm <= L < {10 * _chiselData.Width + 10}mm");
                }
            }
            if (TextBoxHeight.BackColor == _emptyColor)
            {
                if (TextBoxWidth.BackColor != _trueColor)
                {
                    toolTipInformation.SetToolTip(TextBoxHeight, "6mm <= H <= 24mm");
                }
                if (TextBoxWidth.BackColor == _trueColor)
                {
                    toolTipInformation.SetToolTip(TextBoxHeight,
                        $" {0.6 * _chiselData.Width}mm <= H <= {0.8 * _chiselData.Width}mm.");
                }
            }
            if (TextBoxBladeLength.BackColor == _emptyColor)
            {
                if (TextBoxLength.BackColor != _trueColor)
                {
                    toolTipInformation.SetToolTip(TextBoxBladeLength, "40mm <= l1 <= 150mm");
                }
                if (TextBoxLength.BackColor == _trueColor)
                {
                    toolTipInformation.SetToolTip(TextBoxBladeLength,
                        $"{0.4 * _chiselData.Length}mm <= l1 <= {0.5 * _chiselData.Length}mm");
                }
            }
            if (TextBoxInnerLength.BackColor == _emptyColor)
            {
                if (TextBoxLength.BackColor != _trueColor)
                {
                    toolTipInformation.SetToolTip(TextBoxInnerLength, "10mm <= l2 <= 75mm");
                }
                if (TextBoxLength.BackColor == _trueColor)
                {
                    toolTipInformation.SetToolTip(TextBoxInnerLength,
                        $"10mm <= l2 <= {0.25 * _chiselData.Length}mm");
                }
            }
            if (TextBoxInnerWidth.BackColor == _emptyColor)
            {
                if (TextBoxWidth.BackColor != _trueColor && TextBoxInnerLength.BackColor != _trueColor)
                {
                    toolTipInformation.SetToolTip(TextBoxInnerWidth, "5mm <= w1 <= 15mm");
                }
                if (TextBoxWidth.BackColor == _trueColor && TextBoxInnerLength.BackColor == _trueColor)
                {
                    if (Convert.ToDouble(TextBoxWidth.Text) < Convert.ToDouble(TextBoxInnerLength.Text))
                    {
                        toolTipInformation.SetToolTip(TextBoxInnerWidth,
                            $"5mm <= w1 <= {0.5 * _chiselData.Width}mm");
                    }
                    else
                    {
                        toolTipInformation.SetToolTip(TextBoxInnerWidth,
                            $"5mm <= w1 <= {0.5 * _chiselData.InnerLength}mm");
                    }
                }
                if (TextBoxWidth.BackColor == _trueColor && TextBoxInnerLength.BackColor != _trueColor)
                {
                    toolTipInformation.SetToolTip(TextBoxInnerWidth,
                        $"5mm <= w1 <= {0.5 * _chiselData.Width}mm");
                }
                if (TextBoxWidth.BackColor != _trueColor && TextBoxInnerLength.BackColor == _trueColor)
                {
                    toolTipInformation.SetToolTip(TextBoxInnerWidth,
                        $"5mm <= w1 <= {0.5 * _chiselData.InnerLength}mm");
                }
            }
        }

        /// <summary>
        /// обраьотчик события наведения мыши на текстовое поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_MouseMove(object sender, MouseEventArgs e)
        {
            InformationTool();
        }
           
        /// <summary>
        /// обработчик события выбора модели зубила
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Click(object sender, EventArgs e)
        {
            
            if(RadioButtonLocksmith.Checked == true)
            {
                PanelPictureChisel.BackgroundImage = Properties.Resources.Chisel3D;
                LabelInnerLength.Visible = true;
                LabelInnerWidth.Visible = true;
                TextBoxInnerLength.Visible = true;
                TextBoxInnerWidth.Visible = true;
                
                LabelWidth.Text = "W - Chisel width";
                LabelLength.Text = "L - Chisel length";
                LabelHeight.Text = "H - Chisel height";
                LabelBladeLength.Text = "l1 – Chisel blade length";               
            }

            if (RadioButtonPeak.Checked == true)
            {
                PanelPictureChisel.BackgroundImage = Properties.Resources.ChiselPeak3D;
                LabelInnerLength.Visible = false;
                LabelInnerWidth.Visible = false;
                TextBoxInnerLength.Visible = false;
                TextBoxInnerWidth.Visible = false;        
                 
                LabelWidth.Text ="W - Chisel blade width";
                LabelLength.Text ="L - Chisel length";
                LabelHeight.Text = "D/H - Chisel diameter";
                LabelBladeLength.Text = "l1 – Chisel blade length";                            
            }
            OnOffBuildButton();
        }

        /// <summary>
        /// обработчик события изменения текста в текстбоксе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            CheckAffterInput();
        }       

        /// <summary>
        /// обработчик события нажатия на клавишу в текстбоксе ширины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxWidth);
        }     
    
        /// <summary>
        /// обработчик события выхода из текстбокса ширины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxWidth_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxWidth);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события нажатия на клавишу в текстбоксе длины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxLenght_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxLength);          
        }     

        /// <summary>
        /// обработчик события выхода из текстбокса длины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxLenght_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxLength);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события нажатия на клавишу в текстбоксе высоты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxHeight);
        }      

        /// <summary>
        /// обработчик события выхода из текстбокса высоты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxHeight_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxHeight);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события нажатия на клавишу в текстбоксе длины лезвия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxBladeLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxBladeLength);
        }        

        /// <summary>
        /// обработчик события выхода из текстбокса длины лезвия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxBladeLength_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxBladeLength);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события нажатия на клавишу в текстбоксе длины внутреннего выреза 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInnerLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxInnerLength);
        }        

        /// <summary>
        /// обрабочик события выхода из текстбокса длины внутреннего выреза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInnerLength_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxInnerLength);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события нажатия на клавишу в текстбоксе ширины внутреннего выреза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInnerWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxInnerWidth);
        }       

        /// <summary>
        /// обработчик события выхода из текстбокса ширины внутреннего выреза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInnerWidth_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxInnerWidth);
            CheckAffterInput();
        }
        /// <summary>
        /// обработчик события нажатия на кнопку "построить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBuild_Click(object sender, EventArgs e)
        {
            _kompasApp = new KompasConnector();
            if (!_kompasApp.CreateDocument3D())
            {
                return;
            }
            _manager = new Manager(_kompasApp);

            if (_manager != null)
            {
                if (RadioButtonLocksmith.Checked == true)
                {
                    _manager.BuildModelLocksmith(_chiselData);
                }
                else if (RadioButtonPeak.Checked == true)
                {
                    _manager.BuildModelPeak(_chiselData);
                }

            }
        }

        /// <summary>
        /// обработчик события закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormParameters_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (_kompasApp == null)
            {
                string messageBoxText = $"Do you really want to close the window?" +
                               $"{ Environment.NewLine}The entered data will be deleted.";
                string caption = "Exit";
                MessageBoxButtons button = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Question;
                DialogResult result = MessageBox.Show(messageBoxText, caption, button, icon,
                    MessageBoxDefaultButton.Button1);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            if (_kompasApp != null)
            {
                string messageBoxText = $"Do you want to close the compass application" +
                    $" together with this window?" +
                               $"{ Environment.NewLine}The entered data will be deleted.";
                string caption = "Exit";
                MessageBoxButtons button = MessageBoxButtons.YesNoCancel;
                MessageBoxIcon icon = MessageBoxIcon.Question;
                DialogResult result = MessageBox.Show(messageBoxText, caption, button, icon,
                    MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (result == DialogResult.Yes)
                {
                    try
                    {
                        _kompasApp.DestructApp();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Kompas Application is already closed!");
                    }                  
                }
            }
        }
    }
}
