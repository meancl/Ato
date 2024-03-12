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



            if (isCtrlPushed)
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

                                DateTime dT = DateTime.Parse(messages[1].Split('\r')[0]);
                                string sC = messages[2].Split('\r')[0];
                                int nLoc = int.Parse(messages[0].Split('\r')[0]);
                                string sCode = messages[3].Split('\r')[0];
                                int nFB = int.Parse(messages[4].Split('\r')[0]);

                                mainForm.viewList.Add(new AtoReplayer.ViewData
                                {
                                    dTradeTime = dT,
                                    sCodeName = sC,
                                    sCode = sCode,
                                    nMaxFakeBuyCnt=  nFB,
                                    nCompLoc = nLoc
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
                                            mainForm.compLocButton.Text = nLoc.ToString();
                                            mainForm.compLocLabel.Text = nLoc.ToString();
                                        }));
                                    }
                                    else
                                    {
                                        mainForm.sCodeNameTxtBox.Text = sC;
                                        mainForm.dTradeTimeDateTimePicker.Value = dT;
                                        mainForm.compLocButton.Text = nLoc.ToString();
                                        mainForm.compLocLabel.Text = nLoc.ToString();
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

                if(cUp == 'S')
                {
                    string [] sDataToShuffle = registerTextBox.Text.Split('\n');

                    ShuffleArray<string>(ref sDataToShuffle);

                    registerTextBox.Text = string.Join("\n", sDataToShuffle);  
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

        public void ShuffleArray<T>(ref T[] array)
        {
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
        }
    }
}
