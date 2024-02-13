using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtoReplayer
{
    public partial class RegisterCodeForm : Form
    {
        public AtoReplayer mainForm;
        public RegisterCodeForm(AtoReplayer parentForm)
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyUp += KeyUpHandler;
            this.KeyDown += KeyDownHandler;

            mainForm = parentForm;
        }

        public void KeyUpHandler(object sender, KeyEventArgs k)
        {
            char cUp = (char)k.KeyValue;



            if (cUp == 17) // ctrl
            {
                isCtrlPushed = false;
            }

            if (cUp == 16) // shift
            {
                isShiftPushed = false;
            }

            if (cUp == 32) // space 
            {
                isSpacePushed = false;
            }



            if (isSpacePushed)
            {
                if (cUp == 16)
                {
                    mainForm.viewList.Clear();
                    mainForm.nViewIdx = 0;
                    mainForm.nViewPass = 0;

                    try
                    {

                        string[] messageArray = registerTextBox.Text.Split('\n');

                        mainForm.sRegisterMessage = registerTextBox.Text;

                        for (int i = 0; i < messageArray.Length; i++)
                        {
                            try
                            {
                                string[] messages = messageArray[i].Split('\t');

                                DateTime dT = DateTime.Parse(messages[0]);
                                string sC = messages[1].Split('\r')[0];

                                mainForm.viewList.Add(new AtoReplayer.ViewData
                                {
                                    dTradeTime = dT,
                                    sCodeName = sC
                                }
                                );
                                mainForm.isViewSetting = true;

                                if (mainForm.nViewPass == 0)
                                {
                                    if (mainForm.sCodeNameTxtBox.InvokeRequired)
                                    {
                                        mainForm.sCodeNameTxtBox.Invoke(new MethodInvoker(delegate
                                        {
                                            mainForm.sCodeNameTxtBox.Text = sC;
                                            mainForm.dTradeTimeDateTimePicker.Value = dT;
                                        }));
                                    }
                                    else
                                    {
                                        mainForm.sCodeNameTxtBox.Text = sC;
                                        mainForm.dTradeTimeDateTimePicker.Value = dT;
                                    }

                                }

                                mainForm.nViewPass++;
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        this.Close();
                    }
                }

                if(cUp == 'C')
                {
                    if(!mainForm.sRegisterMessage.Equals(""))
                        registerTextBox.Text = mainForm.sRegisterMessage;
                }

            }

        }

        public bool isCtrlPushed;
        public bool isShiftPushed;
        public bool isSpacePushed;


        public void KeyDownHandler(object sender, KeyEventArgs e)
        {
            char cPressed = (char)e.KeyValue; // 대문자로 준다.


            if (cPressed == 17) // ctrl
            {
                isCtrlPushed = true;
            }

            if (cPressed == 16) // shift
            {
                isShiftPushed = true;
            }

            if (cPressed == 32) // space 
            {
                isSpacePushed = true;
            }

        }
    }
}
