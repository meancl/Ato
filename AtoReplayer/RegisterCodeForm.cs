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

            mainForm = parentForm;
        }

        public void KeyUpHandler(object sender, KeyEventArgs k)
        {
            char cUp = (char)k.KeyValue;

            if(cUp == 16)
            {
                mainForm.viewList.Clear();
                mainForm.nViewIdx = 0;
                mainForm.nViewPass = 0;

                try
                {

                    string[] messageArray = registerTextBox.Text.Split('\n');

                    for (int i = 0; i < messageArray.Length; i++)
                    {
                        try
                        {
                            string[] messages = messageArray[i].Split('\t');

                            mainForm.viewList.Add(new AtoReplayer.ViewData
                            {
                                dTradeTime = DateTime.Parse(messages[0]),
                                sCodeName = messages[1]
                            }
                            );
                            mainForm.isViewSetting = true;

                            if (mainForm.nViewPass == 0)
                            {
                                if (mainForm.sCodeNameTxtBox.InvokeRequired)
                                {
                                    mainForm.sCodeNameTxtBox.Invoke(new MethodInvoker(delegate
                                    {
                                        mainForm.sCodeNameTxtBox.Text = messages[1];
                                        mainForm.dTradeTimeDateTimePicker.Value = DateTime.Parse(messages[0]);
                                    }));
                                }
                                else
                                {
                                    mainForm.sCodeNameTxtBox.Text = messages[1];
                                    mainForm.dTradeTimeDateTimePicker.Value = DateTime.Parse(messages[0]);
                                }

                            }

                            mainForm.nViewPass++;
                        }
                        catch (Exception ex ){
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

        }
    }
}
