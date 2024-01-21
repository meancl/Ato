using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtoIndicator.View
{
    public partial class RegisterBlockByCode : Form
    {
        public MainForm mainForm;

        public RegisterBlockByCode(MainForm parentForm)
        {
            InitializeComponent();

            mainForm = parentForm;

            this.KeyPreview = true;
            this.KeyUp += KeyUpHandler;
            this.KeyDown += KeyDownHandler;
        }

        bool isCtrlPushed = false;
        bool isShiftPushed = false;
        bool isSpacePushed = false;
        public void KeyUpHandler(object sender, KeyEventArgs e)
        {
            char cUp = (char)e.KeyValue;
            if (cUp == 17)
                isCtrlPushed = false;

            if (cUp == 16 ) 
                isShiftPushed = false;

            if (cUp == 32)
                isSpacePushed = false;

        }

        public void KeyDownHandler(object sender, KeyEventArgs e)
        {
            char cDown = (char)e.KeyValue;
            if (cDown == 17)
                isCtrlPushed = true;

            if (cDown == 16)
                isShiftPushed = true;

            if (cDown == 32)
                isSpacePushed = true;

            if (isCtrlPushed && isShiftPushed && isSpacePushed)
                RegisterByTxt();

        }

        const int JNum = 1;
        const int KNum = 2;
        const int WNum = 3;


        public void RegisterByTxt()
        {
            try
            {
                string message = registerTxtBox.Text;
                string[] messageArray = message.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string aMessage in messageArray)
                {
                    string[] stockArray = aMessage.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    if (stockArray.Length > 0)
                    {
                        int nSpecificNum = 0;

                        string title = stockArray[0].Trim();

                        if (title.Equals("J") || title.Equals("j"))
                        {
                            nSpecificNum = JNum;
                        }
                        else if(title.Equals("K") || title.Equals("k"))
                        {
                            nSpecificNum = KNum;
                        }
                        else if (title.Equals("W") || title.Equals("w"))
                        {
                            nSpecificNum = WNum;
                        }
                        else if (title.Equals("clear") || title.Equals("cls"))
                        {
                            for (int n = 0; n < mainForm.nStockLength; n++)
                            {
                                mainForm.ea[n].nSelectedConditionJ = 0;
                                mainForm.ea[n].nSelectedConditionK = 0;
                                mainForm.ea[n].manualReserve.isChosenW = false;
                            }
                            continue;
                        }

                        for (int i = 1; i < stockArray.Length; i++)
                        {

                            string[] lineArray = stockArray[i].Split('\t');

                            int nCurIdx = mainForm.eachStockNameDict[lineArray[0].Trim()];

                            switch (nSpecificNum)
                            {
                                case JNum:
                                    mainForm.ea[nCurIdx].nSelectedConditionJ++;
                                    break;
                                case KNum:
                                    mainForm.ea[nCurIdx].nSelectedConditionK++;
                                    break;
                                case WNum:
                                    mainForm.ea[nCurIdx].manualReserve.isChosenW = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}
