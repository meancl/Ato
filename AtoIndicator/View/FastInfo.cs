using AtoIndicator.View.EachStockHistory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AtoIndicator.KiwoomLib.TimeLib;
using static AtoIndicator.MainForm;


namespace AtoIndicator.View
{
    public partial class FastInfo : Form
    {
        public MainForm mainForm;
        public int[] targetTimeArr;
        public SoundPlayer crushSoundPlayer;
        public SoundPlayer viSoundPlayer;
        public SoundPlayer upSoundPlayer;


        public FastInfo(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            crushSoundPlayer = new SoundPlayer(@"..\..\..\Sounds\crushAlarm.wav");
            viSoundPlayer = new SoundPlayer(@"..\..\..\Sounds\viAlarm.wav");
            upSoundPlayer = new SoundPlayer(@"..\..\..\Sounds\upAlarm.wav");


            string sString = "STRING";
            string sInt = "INT";
            string sDouble = "DOUBLE";

            listView1.Columns.Add(new ColumnHeader { Name = sString, Text = "종목코드" });
            listView1.Columns.Add(new ColumnHeader { Name = sString, Text = "종목명" });
            listView1.Columns.Add(new ColumnHeader { Name = sDouble, Text = "현재파워" });
            listView1.Columns.Add(new ColumnHeader { Name = sDouble, Text = "초기갭" });
            listView1.Columns.Add(new ColumnHeader { Name = sDouble, Text = "맥스차" });
            listView1.Columns.Add(new ColumnHeader { Name = sDouble, Text = "당일탑" });

            listView1.Columns.Add(new ColumnHeader { Name = sString, Text = "RV" });

            listView1.Columns.Add(new ColumnHeader { Name = sDouble, Text = "vi까지" });
            listView1.Columns.Add(new ColumnHeader { Name = sString, Text = "VI" });

            listView1.Columns.Add(new ColumnHeader { Name = sDouble, Text = "체결속도" });
            listView1.Columns.Add(new ColumnHeader { Name = sDouble, Text = "대금정도" });
            listView1.Columns.Add(new ColumnHeader { Name = sDouble, Text = "매수정도" });

            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "페매수" });
            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "페보조" });
            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "페매보" });
            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "실매수" });


            listView1.Columns.Add(new ColumnHeader { Name = sDouble, Text = "이전분봉" });
            listView1.Columns.Add(new ColumnHeader { Name = sDouble, Text = "현재분봉" });

            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "분당순위" });
            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "총순위" });
            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "순위변경" });


            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "타겟 T" });

            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "히트갯수" });
            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "히트종류" });



            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "에브리" });

            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "총 페이크" });
            listView1.Columns.Add(new ColumnHeader { Name = sInt, Text = "총 애로우" });


            listView1.Columns.Add(new ColumnHeader { Name = sDouble, Text = "갭제외" });

            listView1.Columns.Add(new ColumnHeader { Name = sDouble, Text = "AI 점수" });



            listView1.View = System.Windows.Forms.View.Details;
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.MouseClick += MouseClickHandler;
            listView1.MouseDoubleClick += RowDoubleClickHandler;
            listView1.ColumnClick += ColumnClickHandler;
            listView1.KeyUp += ListViewKeyUpHandller;

            targetTimeArr = new int[mainForm.nStockLength];
            for (int i = 0; i < mainForm.nStockLength; i++)
                targetTimeArr[i] = 0;

            this.KeyPreview = true;
            this.KeyUp += KeyUpHandler;
            this.KeyDown += KeyDownHandler;
            this.groupBox1.DoubleClick += DoubleClickHandler;
            this.DoubleBuffered = true;

            ToolTip tooltip1 = new ToolTip();
            ToolTip tooltip2 = new ToolTip();
            ToolTip tooltip3 = new ToolTip();
            ToolTip tooltip4 = new ToolTip();
            ToolTip tooltip5 = new ToolTip();
            ToolTip tooltip6 = new ToolTip();
            ToolTip tooltip7 = new ToolTip();
            ToolTip tooltip8 = new ToolTip();
            ToolTip tooltip9 = new ToolTip();
            ToolTip tooltip10 = new ToolTip();
            ToolTip tooltip11 = new ToolTip();


            timerDownButton.Click += TimerButtonClickHandler;
            timerUpButton.Click += TimerButtonClickHandler;



            this.reserve1Btn.Click += ReserveButtonClickHandler;
            this.reserve2Btn.Click += ReserveButtonClickHandler;
            this.reserve3Btn.Click += ReserveButtonClickHandler;
            this.reserve4Btn.Click += ReserveButtonClickHandler;
            this.confirmButton.Click += ReserveButtonClickHandler;

            tooltip1.SetToolTip(reserve1Btn, "vi모드");
            tooltip2.SetToolTip(reserve2Btn, "실시간 전고돌파");
            tooltip3.SetToolTip(reserve3Btn, "분봉상 전고돌파 확정");
            tooltip4.SetToolTip(reserve4Btn, "페이크매수분포2 페이크갯수 50 맥스파워 0.1");


            this.FormClosed += FormClosedHandler;

            timer = new System.Timers.Timer(nTimerMilliSec);
            timer.Elapsed += delegate (Object s, System.Timers.ElapsedEventArgs e)
            {
                UpdateTable();
            };
        }

        public void DoubleClickHandler(object sender, EventArgs e)
        {
            groupBox2.Visible = !groupBox2.Visible;
            if (!groupBox2.Visible)
            {
                groupBox1.Dock = DockStyle.Fill;
            }
            else
            {
                groupBox1.Dock = DockStyle.Left;
            }
        }
        public void FormClosedHandler(Object sender, FormClosedEventArgs e)
        {
            timer.Enabled = false;
            this.Dispose();
        }

        public bool isShiftMode;
        public bool isCtrlMode;
        public int nQWSharedNum = 0;
        public int nQWNum1 = 0;
        public int nQWNum2 = 0;

        public void ListViewKeyUpHandller(object sender, KeyEventArgs k)
        {
            char cUp = (char)k.KeyValue;

            if (listView1.FocusedItem != null)
            {
                if (k.KeyCode == Keys.Up || k.KeyCode == Keys.Down)
                {
                    nPrevEaIdx = mainForm.eachStockDict[listView1.FocusedItem.SubItems[0].Text.Trim()];
                }
                else
                {
                    nPrevEaIdx = mainForm.eachStockDict[listView1.FocusedItem.SubItems[0].Text.Trim()];

                    if (!isCtrlMode)
                    {
                        if (cUp == 'Q')
                        {
                            if (nQWNum1 == nQWSharedNum)
                            {
                                nQWNum1 = ++nQWSharedNum;

                                mainForm.ea[nPrevEaIdx].manualReserve.isChosenQ = !mainForm.ea[nPrevEaIdx].manualReserve.isChosenQ;
                                if (mainForm.ea[nPrevEaIdx].manualReserve.isChosenQ)
                                    registerLabel.Text = $"{listView1.FocusedItem.SubItems[0].Text.Trim()} Q창 등록됨";
                                else
                                    registerLabel.Text = $"{listView1.FocusedItem.SubItems[0].Text.Trim()} Q창 등록해제됨";
                            }
                            else
                                nQWNum1 = nQWSharedNum;
                        }

                        if (cUp == 'W')
                        {
                            if (nQWNum1 == nQWSharedNum)
                            {
                                nQWNum1 = ++nQWSharedNum;

                                mainForm.ea[nPrevEaIdx].manualReserve.isChosenW = !mainForm.ea[nPrevEaIdx].manualReserve.isChosenW;
                                if (mainForm.ea[nPrevEaIdx].manualReserve.isChosenW)
                                    registerLabel.Text = $"{listView1.FocusedItem.SubItems[0].Text.Trim()} W창 등록됨";
                                else
                                    registerLabel.Text = $"{listView1.FocusedItem.SubItems[0].Text.Trim()} W창 등록해제됨";
                            }
                            else
                                nQWNum1 = nQWSharedNum;
                        }

                        if (cUp == 'E')
                        {
                            if (nQWNum1 == nQWSharedNum)
                            {
                                nQWNum1 = ++nQWSharedNum;

                                mainForm.ea[nPrevEaIdx].manualReserve.isChosenE = !mainForm.ea[nPrevEaIdx].manualReserve.isChosenE;
                                if (mainForm.ea[nPrevEaIdx].manualReserve.isChosenE)
                                    registerLabel.Text = $"{listView1.FocusedItem.SubItems[0].Text.Trim()} E창 등록됨";
                                else
                                    registerLabel.Text = $"{listView1.FocusedItem.SubItems[0].Text.Trim()} E창 등록해제됨";
                            }
                            else
                                nQWNum1 = nQWSharedNum;
                        }

                        if (cUp == 'R')
                        {
                            if (nQWNum1 == nQWSharedNum)
                            {
                                nQWNum1 = ++nQWSharedNum;

                                mainForm.ea[nPrevEaIdx].manualReserve.isChosenR = !mainForm.ea[nPrevEaIdx].manualReserve.isChosenR;
                                if (mainForm.ea[nPrevEaIdx].manualReserve.isChosenR)
                                    registerLabel.Text = $"{listView1.FocusedItem.SubItems[0].Text.Trim()} R창 등록됨";
                                else
                                    registerLabel.Text = $"{listView1.FocusedItem.SubItems[0].Text.Trim()} R창 등록해제됨";
                            }
                            else
                                nQWNum1 = nQWSharedNum;
                        }

                        if (cUp == 191) // ?
                        {
                            mainForm.ea[nPrevEaIdx].manualReserve.ClearAll();
                            registerLabel.Text = $"{mainForm.ea[nPrevEaIdx].sCode} 전체예약 취소";
                        }
                    }

                }
            }
        }

        public void KeyDownHandler(object sender, KeyEventArgs k)
        {
            char cDown = (char)k.KeyValue;

            if (cDown == 17) // ctrl
            {
                isCtrlMode = true;
                zzimLabel.Text = "ctrl on";
                this.ActiveControl = this.passLenLabel;
            }
            if (cDown == 16) // shift
            {
                isShiftMode = true;
                zzimLabel.Text = "shift on";
                this.ActiveControl = this.passLenLabel;
            }
        }
        public void KeyUpHandler(object sender, KeyEventArgs k)
        {
            char cUp = (char)k.KeyValue;

            keyLabel.Text = $"{cUp}";

            if (isShiftMode && cUp == 27) // esc 
            {
                timer.Enabled = false;
                this.Close();
            }



            if (cUp == 16) // shift
            {
                isShiftMode = false;
                zzimLabel.Text = "shift off";
                this.ActiveControl = this.passLenLabel;
            }

            if (cUp == 17) // ctrl
            {
                isCtrlMode = false;
                zzimLabel.Text = "ctrl off";
                this.ActiveControl = this.passLenLabel;
            }

            if (cUp == 'T')
            {
                timerCheckBox.Checked = !timerCheckBox.Checked;
                if (timerCheckBox.Checked == false)
                    timer.Enabled = false;
            }

            if (isCtrlMode)
            {
                if (cUp == 'C')
                {
                    if (!isUsing)
                    {
                        timer.Enabled = false;
                        timerCheckBox.Checked = false;

                        wCheckBox.Checked = false;
                        qCheckBox.Checked = false;
                        eCheckBox.Checked = false;
                        rCheckBox.Checked = false;

                        crushVisualTxtBox.Text = "";
                        viVisualTxtBox.Text = "";
                        speedVisualTxtBox.Text = "";

                        crushVisualCheckBox.Checked = false;
                        viVisualCheckBox.Checked = false;
                        speedVisualCheckBox.Checked = false;
                        maVisualCheckBox.Checked = false;

                        crushSoundCheckBox.Checked = false;
                        viSoundCheckBox.Checked = false;
                        upSoundCheckBox.Checked = false;

                        passNumTxtBox.Text = "";

                        tTF1.Text = "";
                        tTF2.Text = "";
                        tSG1.Text = "";
                        tSG2.Text = "";
                        tWOG1.Text = "";
                        tWOG2.Text = "";
                        tCP1.Text = "";
                        tCP2.Text = "";
                        tPJ1.Text = "";
                        tPJ2.Text = "";
                        tUPJ1.Text = "";
                        tUPJ2.Text = "";
                        tDPJ1.Text = "";
                        tDPJ2.Text = "";
                        tTTM1.Text = "";
                        tTTM2.Text = "";
                        tBM1.Text = "";
                        tBM2.Text = "";
                        tSM1.Text = "";
                        tSM2.Text = "";
                        tTA1.Text = "";
                        tTA2.Text = "";
                        tHA1.Text = "";
                        tHA2.Text = "";
                        tRA1.Text = "";
                        tRA2.Text = "";
                        tDA1.Text = "";
                        tDA2.Text = "";
                        t1P1.Text = "";
                        t1P2.Text = "";
                        t2P1.Text = "";
                        t2P2.Text = "";
                        t3P1.Text = "";
                        t3P2.Text = "";
                        t4P1.Text = "";
                        t4P2.Text = "";
                        tPD1.Text = "";
                        tPD2.Text = "";
                        tRPD1.Text = "";
                        tRPD2.Text = "";
                        tVI1.Text = "";
                        tVI2.Text = "";
                        tRBP1.Text = "";
                        tRBP2.Text = "";
                        tTFP1.Text = "";
                        tTFP2.Text = "";
                        tEP1.Text = "";
                        tEP2.Text = "";
                        tAIS1.Text = "";
                        tAIS2.Text = "";
                        tSC1.Text = "";
                        tSC2.Text = "";
                        tSUDC1.Text = "";
                        tSUDC2.Text = "";
                        tSUP1.Text = "";
                        tSUP2.Text = "";
                        tSDP1.Text = "";
                        tSDP2.Text = "";
                        tSPD1.Text = "";
                        tSPD2.Text = "";
                        tEC1.Text = "";
                        tEC2.Text = "";
                        tCM1.Text = "";
                        tCM2.Text = "";
                        tPM1.Text = "";
                        tPM2.Text = "";
                        tCC1.Text = "";
                        tCC2.Text = "";
                        tCS1.Text = "";
                        tCS2.Text = "";
                        tAMU1.Text = "";
                        tAMU2.Text = "";
                        tAMD1.Text = "";
                        tAMD2.Text = "";
                        tHS1.Text = "";
                        tHS2.Text = "";
                        tPS1.Text = "";
                        tPS2.Text = "";
                        tPUS1.Text = "";
                        tPUS2.Text = "";
                        tTS1.Text = "";
                        tTS2.Text = "";
                        tTMAX1.Text = "";
                        tTMAX2.Text = "";
                        tTMIN1.Text = "";
                        tTMIN2.Text = "";
                        tTMD1.Text = "";
                        tTMD2.Text = "";
                        tMR1.Text = "";
                        tMR2.Text = "";
                        tTR1.Text = "";
                        tTR2.Text = "";
                        tRDIF1.Text = "";
                        tRDIF2.Text = "";
                        tAI20T1.Text = "";
                        tAI20T2.Text = "";
                        tAI30T1.Text = "";
                        tAI30T2.Text = "";
                        tAI50T1.Text = "";
                        tAI50T2.Text = "";
                        tCRC1.Text = "";
                        tCRC2.Text = "";
                        tUCRC1.Text = "";
                        tUCRC2.Text = "";
                        tT1501.Text = "";
                        tT1502.Text = "";
                        tC1501.Text = "";
                        tC1502.Text = "";
                        tRBA1.Text = "";
                        tRBA2.Text = "";
                        tRBD1.Text = "";
                        tRBD2.Text = "";
                        tFBA1.Text = "";
                        tFBA2.Text = "";
                        tFBD1.Text = "";
                        tFBD2.Text = "";
                        tFAA1.Text = "";
                        tFAA2.Text = "";
                        tFAD1.Text = "";
                        tFAD2.Text = "";
                        tFRA1.Text = "";
                        tFRA2.Text = "";
                        tFRD1.Text = "";
                        tFRD2.Text = "";
                        tPUA1.Text = "";
                        tPUA2.Text = "";
                        tPUD1.Text = "";
                        tPUD2.Text = "";
                        tPDA1.Text = "";
                        tPDA2.Text = "";
                        tPDD1.Text = "";
                        tPDD2.Text = "";
                        tSFD1.Text = "";
                        tSFD2.Text = "";
                        tHIT51.Text = "";
                        tHIT52.Text = "";
                        tHIT81.Text = "";
                        tHIT82.Text = "";
                        tHIT101.Text = "";
                        tHIT102.Text = "";
                        tHIT121.Text = "";
                        tHIT122.Text = "";
                        tCURFT1.Text = "";
                        tCURFT2.Text = "";
                        tCURFC1.Text = "";
                        tCURFC2.Text = "";
                        tFABNum1.Text = "";
                        tFABNum2.Text = "";
                        tFABPlusNum1.Text = "";
                        tFABPlusNum2.Text = "";
                        tTradeCompared1.Text = "";
                        tTradeCompared2.Text = "";
                        tTradeStrength1.Text = "";
                        tTradeStrength2.Text = "";
                        tUntilVi1.Text = "";
                        tUntilVi2.Text = "";
                        tDUP1.Text = "";
                        tDUP2.Text = "";

                    }
                }

                if (cUp == 'A')
                {
                    isReserved = false;
                    timer.Enabled = false;
                    ShowIndicator();
                }

                if (cUp == 'Q')
                {
                    CheckReserve();
                    isRZ = true;
                    nRZNum = 1;
                    ShowIndicator();
                }

                if (cUp == 'W')
                {
                    CheckReserve();
                    isRZ = true;
                    nRZNum = 2;
                    ShowIndicator();
                }

                if (cUp == 'E')
                {
                    CheckReserve();
                    isRZ = true;
                    nRZNum = 3;
                    ShowIndicator();
                }

                if (cUp == 'R')
                {
                    CheckReserve();
                    isRZ = true;
                    nRZNum = 4;
                    ShowIndicator();
                }

                if (cUp == 'P')
                {
                    CheckReserve();
                    isRZ = true;
                    nRZNum = 5;
                    ShowIndicator();
                }

                if (cUp == 'O')
                {
                    CheckReserve();
                    isRZ = true;
                    nRZNum = 6;
                    ShowIndicator();
                }

                if (cUp == 'I')
                {
                    CheckReserve();
                    isRZ = true;
                    nRZNum = 7;
                    ShowIndicator();
                }
                if (cUp == 'U')
                {
                    CheckReserve();
                    isRZ = true;
                    nRZNum = 8;
                    ShowIndicator();
                }
                if (cUp == 'Y')
                {
                    CheckReserve();
                    isRZ = true;
                    nRZNum = 9;
                    ShowIndicator();
                }

                if (cUp == 'J')
                {
                    CheckReserve();
                    isRZ = true;
                    nRZNum = 10;
                    ShowIndicator();
                }

                if (cUp == 'K')
                {
                    CheckReserve();
                    isRZ = true;
                    nRZNum = 11;
                    ShowIndicator();
                }

                if (cUp == 'L')
                {
                    CheckReserve();
                    isRZ = true;
                    nRZNum = 12;
                    ShowIndicator();
                }
            }
            else
            {
                if (cUp == 'Q')
                {
                    if (nQWNum2 == nQWSharedNum)
                    {
                        nQWNum2 = ++nQWSharedNum;

                        mainForm.ea[nPrevEaIdx].manualReserve.isChosenQ = !mainForm.ea[nPrevEaIdx].manualReserve.isChosenQ;
                        if (mainForm.ea[nPrevEaIdx].manualReserve.isChosenQ)
                            registerLabel.Text = $"{ mainForm.ea[nPrevEaIdx].sCode} Q창 등록됨";
                        else
                            registerLabel.Text = $"{mainForm.ea[nPrevEaIdx].sCode} Q창 등록해제됨";
                    }
                    else
                        nQWNum2 = nQWSharedNum;
                }

                if (cUp == 'W')
                {
                    if (nQWNum2 == nQWSharedNum)
                    {
                        nQWNum2 = ++nQWSharedNum;

                        mainForm.ea[nPrevEaIdx].manualReserve.isChosenW = !mainForm.ea[nPrevEaIdx].manualReserve.isChosenW;
                        if (mainForm.ea[nPrevEaIdx].manualReserve.isChosenW)
                            registerLabel.Text = $"{mainForm.ea[nPrevEaIdx].sCode} W창 등록됨";
                        else
                            registerLabel.Text = $"{mainForm.ea[nPrevEaIdx].sCode} W창 등록해제됨";
                    }
                    else
                        nQWNum2 = nQWSharedNum;
                }


                if (cUp == 'E')
                {
                    if (nQWNum2 == nQWSharedNum)
                    {
                        nQWNum2 = ++nQWSharedNum;

                        mainForm.ea[nPrevEaIdx].manualReserve.isChosenE = !mainForm.ea[nPrevEaIdx].manualReserve.isChosenE;
                        if (mainForm.ea[nPrevEaIdx].manualReserve.isChosenE)
                            registerLabel.Text = $"{mainForm.ea[nPrevEaIdx].sCode} E창 등록됨";
                        else
                            registerLabel.Text = $"{mainForm.ea[nPrevEaIdx].sCode} E창 등록해제됨";
                    }
                    else
                        nQWNum2 = nQWSharedNum;
                }

                if (cUp == 'R')
                {
                    if (nQWNum2 == nQWSharedNum)
                    {
                        nQWNum2 = ++nQWSharedNum;

                        mainForm.ea[nPrevEaIdx].manualReserve.isChosenR = !mainForm.ea[nPrevEaIdx].manualReserve.isChosenR;
                        if (mainForm.ea[nPrevEaIdx].manualReserve.isChosenR)
                            registerLabel.Text = $"{mainForm.ea[nPrevEaIdx].sCode} R창 등록됨";
                        else
                            registerLabel.Text = $"{mainForm.ea[nPrevEaIdx].sCode} R창 등록해제됨";
                    }
                    else
                        nQWNum2 = nQWSharedNum;
                }

                if (cUp == 191) // ?
                {
                    mainForm.ea[nPrevEaIdx].manualReserve.ClearAll();
                    registerLabel.Text = $"{mainForm.ea[nPrevEaIdx].sCode} 전체예약 취소";
                }
            }

        }

        public void ShowIndicator()
        {
            if (sortColumn != -1)
                listView1.Columns[sortColumn].Text = listView1.Columns[sortColumn].Text.Substring(0, listView1.Columns[sortColumn].Text.Length - nTipLen);
            sortColumn = -1;
            listView1.Sorting = SortOrder.None;
            listView1.Sort();

            UpdateTable();
        }

        public int GetPassNum(bool[] pas)
        {
            int retval = 0;
            for (int i = 0; i < pas.Length; i++)
                if (pas[i])
                    retval++;
            return retval;
        }
        bool isUsing = false;

        public int nPrevTime;

        // 비교 대상자 변수들
        public ComparePackage TFPack;
        public ComparePackage SGPack;
        public ComparePackage WOGPack;
        public ComparePackage CPPack;
        public ComparePackage PDPack;
        public ComparePackage RPDPack;
        public ComparePackage PJPack;
        public ComparePackage UPJPack;
        public ComparePackage DPJPack;
        public ComparePackage TTMPack;
        public ComparePackage BMPack;
        public ComparePackage SMPack;
        public ComparePackage TAPack;
        public ComparePackage HAPack;
        public ComparePackage RAPack;
        public ComparePackage DAPack;
        public ComparePackage _1PPack;
        public ComparePackage _2PPack;
        public ComparePackage _3PPack;
        public ComparePackage _4PPack;
        public ComparePackage VIPack;
        public ComparePackage RBPPack;
        public ComparePackage TFPPack;
        public ComparePackage EPPack;
        public ComparePackage AISPack;
        public ComparePackage SCPack;
        public ComparePackage SUDCPack;
        public ComparePackage SUPPack;
        public ComparePackage SDPPack;
        public ComparePackage SPDPack;
        public ComparePackage ECPack;
        public ComparePackage CMPack;
        public ComparePackage PMPack;
        public ComparePackage CCPack;
        public ComparePackage CSPack;
        public ComparePackage AMUPack;
        public ComparePackage AMDPack;
        public ComparePackage HSPack;
        public ComparePackage PSPack;
        public ComparePackage PUSPack;
        public ComparePackage TSPack;
        public ComparePackage TMAXPack;
        public ComparePackage TMINPack;
        public ComparePackage TMDPack;
        public ComparePackage MRPack;
        public ComparePackage TRPack;
        public ComparePackage RDIFPack;
        public ComparePackage AI20TPack;
        public ComparePackage AI30TPack;
        public ComparePackage AI50TPack;
        public ComparePackage CRCPack;
        public ComparePackage UCRCPack;
        public ComparePackage T150Pack;
        public ComparePackage C150Pack;
        public ComparePackage RBAPack;
        public ComparePackage RBDPack;
        public ComparePackage FBAPack;
        public ComparePackage FBDPack;
        public ComparePackage FAAPack;
        public ComparePackage FADPack;
        public ComparePackage FRAPack;
        public ComparePackage FRDPack;
        public ComparePackage PUAPack;
        public ComparePackage PUDPack;
        public ComparePackage PDAPack;
        public ComparePackage PDDPack;
        public ComparePackage SFDPack;
        public ComparePackage HIT5Pack;
        public ComparePackage HIT8Pack;
        public ComparePackage HIT10Pack;
        public ComparePackage HIT12Pack;
        public ComparePackage CURFTPack;
        public ComparePackage CURFCPack;
        public ComparePackage FABNumPack;
        public ComparePackage FABPlusNumPack;
        public ComparePackage TradeComparedPack;
        public ComparePackage TradeStrengthPack;
        public ComparePackage UntilViPack;
        public ComparePackage DUPPack;

        // End -- 비교 대상자 변수들


        public void UpdateTable()
        {

            void Func()
            {
                DateTime startTime = DateTime.Now;

                if (isUsing)
                    return;

                isUsing = true;

                if (nPrevTime != mainForm.nSharedTime)
                {
                    nPrevTime = mainForm.nSharedTime;
                    timeLabel.Text = $"현재시간 : {nPrevTime}";
                }

                // Clear the sorting criteria applied to the ListView control


                listView1.Items.Clear();
                listView1.BeginUpdate();






                try
                {
                    int nPass = 0; // pass cnt
                    int nPassLen = 0;
                    int nFullMinusNum = 0;
                    int nFinalPassNum = 0;

                    List<ListViewItem> listViewItemList = new List<ListViewItem>();


                    TFPack.Set(tTF1.Text, tTF2.Text);
                    SGPack.Set(tSG1.Text, tSG2.Text);
                    WOGPack.Set(tWOG1.Text, tWOG2.Text);
                    CPPack.Set(tCP1.Text, tCP2.Text);
                    PDPack.Set(tPD1.Text, tPD2.Text);
                    RPDPack.Set(tRPD1.Text, tRPD2.Text);
                    PJPack.Set(tPJ1.Text, tPJ2.Text);
                    UPJPack.Set(tUPJ1.Text, tUPJ2.Text);
                    DPJPack.Set(tDPJ1.Text, tDPJ2.Text);
                    TTMPack.Set(tTTM1.Text, tTTM2.Text);
                    BMPack.Set(tBM1.Text, tBM2.Text);
                    SMPack.Set(tSM1.Text, tSM2.Text);
                    TAPack.Set(tTA1.Text, tTA2.Text);
                    HAPack.Set(tHA1.Text, tHA2.Text);
                    RAPack.Set(tRA1.Text, tRA2.Text);
                    DAPack.Set(tDA1.Text, tDA2.Text);
                    _1PPack.Set(t1P1.Text, t1P2.Text);
                    _2PPack.Set(t2P1.Text, t2P2.Text);
                    _3PPack.Set(t3P1.Text, t3P2.Text);
                    _4PPack.Set(t4P1.Text, t4P2.Text);
                    VIPack.Set(tVI1.Text, tVI2.Text);
                    RBPPack.Set(tRBP1.Text, tRBP2.Text);
                    TFPPack.Set(tTFP1.Text, tTFP2.Text);
                    EPPack.Set(tEP1.Text, tEP2.Text);
                    AISPack.Set(tAIS1.Text, tAIS2.Text);
                    SCPack.Set(tSC1.Text, tSC2.Text);
                    SUDCPack.Set(tSUDC1.Text, tSUDC2.Text);
                    SUPPack.Set(tSUP1.Text, tSUP2.Text);
                    SDPPack.Set(tSDP1.Text, tSDP2.Text);
                    SPDPack.Set(tSPD1.Text, tSPD2.Text);
                    ECPack.Set(tEC1.Text, tEC2.Text);
                    CMPack.Set(tCM1.Text, tCM2.Text);
                    PMPack.Set(tPM1.Text, tPM2.Text);
                    CCPack.Set(tCC1.Text, tCC2.Text);
                    CSPack.Set(tCS1.Text, tCS2.Text);
                    AMUPack.Set(tAMU1.Text, tAMU2.Text);
                    AMDPack.Set(tAMD1.Text, tAMD2.Text);
                    HSPack.Set(tHS1.Text, tHS2.Text);
                    PSPack.Set(tPS1.Text, tPS2.Text);
                    PUSPack.Set(tPUS1.Text, tPUS2.Text);
                    TSPack.Set(tTS1.Text, tTS2.Text);
                    TMAXPack.Set(tTMAX1.Text, tTMAX2.Text);
                    TMINPack.Set(tTMIN1.Text, tTMIN2.Text);
                    TMDPack.Set(tTMD1.Text, tTMD2.Text);
                    MRPack.Set(tMR1.Text, tMR2.Text);
                    TRPack.Set(tTR1.Text, tTR2.Text);
                    RDIFPack.Set(tRDIF1.Text, tRDIF2.Text);
                    AI20TPack.Set(tAI20T1.Text, tAI20T2.Text);
                    AI30TPack.Set(tAI30T1.Text, tAI30T2.Text);
                    AI50TPack.Set(tAI50T1.Text, tAI50T2.Text);
                    CRCPack.Set(tCRC1.Text, tCRC2.Text);
                    UCRCPack.Set(tUCRC1.Text, tUCRC2.Text);
                    T150Pack.Set(tT1501.Text, tT1502.Text);
                    C150Pack.Set(tC1501.Text, tC1502.Text);
                    RBAPack.Set(tRBA1.Text, tRBA2.Text);
                    RBDPack.Set(tRBD1.Text, tRBD2.Text);
                    FBAPack.Set(tFBA1.Text, tFBA2.Text);
                    FBDPack.Set(tFBD1.Text, tFBD2.Text);
                    FAAPack.Set(tFAA1.Text, tFAA2.Text);
                    FADPack.Set(tFAD1.Text, tFAD2.Text);
                    FRAPack.Set(tFRA1.Text, tFRA2.Text);
                    FRDPack.Set(tFRD1.Text, tFRD2.Text);
                    PUAPack.Set(tPUA1.Text, tPUA2.Text);
                    PUDPack.Set(tPUD1.Text, tPUD2.Text);
                    PDAPack.Set(tPDA1.Text, tPDA2.Text);
                    PDDPack.Set(tPDD1.Text, tPDD2.Text);
                    SFDPack.Set(tSFD1.Text, tSFD2.Text);
                    HIT5Pack.Set(tHIT51.Text, tHIT52.Text);
                    HIT8Pack.Set(tHIT81.Text, tHIT82.Text);
                    HIT10Pack.Set(tHIT101.Text, tHIT102.Text);
                    HIT12Pack.Set(tHIT121.Text, tHIT122.Text);
                    CURFTPack.Set(tCURFT1.Text, tCURFT2.Text);
                    CURFCPack.Set(tCURFC1.Text, tCURFC2.Text);
                    FABNumPack.Set(tFABNum1.Text, tFABNum2.Text);
                    FABPlusNumPack.Set(tFABPlusNum1.Text, tFABPlusNum2.Text);
                    TradeComparedPack.Set(tTradeCompared1.Text, tTradeCompared2.Text);
                    TradeStrengthPack.Set(tTradeStrength1.Text, tTradeStrength2.Text);
                    UntilViPack.Set(tUntilVi1.Text, tUntilVi2.Text);
                    DUPPack.Set(tDUP1.Text, tDUP2.Text);


                    nPass = 0; // pass cnt
                    nPassLen = 0;
                    nFullMinusNum = GetPassNum(new bool[] {
                                                    TFPack.IsChecked(),
                                                    SGPack.IsChecked(),
                                                    WOGPack.IsChecked(),
                                                    CPPack.IsChecked(),
                                                    PDPack.IsChecked(),
                                                    RPDPack.IsChecked(),
                                                    PJPack.IsChecked(),
                                                    UPJPack.IsChecked(),
                                                    DPJPack.IsChecked(),
                                                    TTMPack.IsChecked(),
                                                    BMPack.IsChecked(),
                                                    SMPack.IsChecked(),
                                                    TAPack.IsChecked(),
                                                    HAPack.IsChecked(),
                                                    RAPack.IsChecked(),
                                                    DAPack.IsChecked(),
                                                    _1PPack.IsChecked(),
                                                    _2PPack.IsChecked(),
                                                    _3PPack.IsChecked(),
                                                    _4PPack.IsChecked(),
                                                    VIPack.IsChecked(),
                                                    RBPPack.IsChecked(),
                                                    TFPPack.IsChecked(),
                                                    EPPack.IsChecked(),
                                                    AISPack.IsChecked(),
                                                    SCPack.IsChecked(),
                                                    SUDCPack.IsChecked(),
                                                    SUPPack.IsChecked(),
                                                    SDPPack.IsChecked(),
                                                    SPDPack.IsChecked(),
                                                    ECPack.IsChecked(),
                                                    CMPack.IsChecked(),
                                                    PMPack.IsChecked(),
                                                    CCPack.IsChecked(),
                                                    CSPack.IsChecked(),
                                                    AMUPack.IsChecked(),
                                                    AMDPack.IsChecked(),
                                                    HSPack.IsChecked(),
                                                    PSPack.IsChecked(),
                                                    PUSPack.IsChecked(),
                                                    TSPack.IsChecked(),
                                                    TMAXPack.IsChecked(),
                                                    TMINPack.IsChecked(),
                                                    TMDPack.IsChecked(),
                                                    MRPack.IsChecked(),
                                                    TRPack.IsChecked(),
                                                    RDIFPack.IsChecked(),
                                                    AI20TPack.IsChecked(),
                                                    AI30TPack.IsChecked(),
                                                    AI50TPack.IsChecked(),
                                                    CRCPack.IsChecked(),
                                                    UCRCPack.IsChecked(),
                                                    T150Pack.IsChecked(),
                                                    C150Pack.IsChecked(),
                                                    RBAPack.IsChecked(),
                                                    RBDPack.IsChecked(),
                                                    FBAPack.IsChecked(),
                                                    FBDPack.IsChecked(),
                                                    FAAPack.IsChecked(),
                                                    FADPack.IsChecked(),
                                                    FRAPack.IsChecked(),
                                                    FRDPack.IsChecked(),
                                                    PUAPack.IsChecked(),
                                                    PUDPack.IsChecked(),
                                                    PDAPack.IsChecked(),
                                                    PDDPack.IsChecked(),
                                                    SFDPack.IsChecked(),
                                                    HIT5Pack.IsChecked(),
                                                    HIT8Pack.IsChecked(),
                                                    HIT10Pack.IsChecked(),
                                                    HIT12Pack.IsChecked(),
                                                    CURFTPack.IsChecked(),
                                                    CURFCPack.IsChecked(),
                                                    FABNumPack.IsChecked(),
                                                    FABPlusNumPack.IsChecked(),
                                                    TradeComparedPack.IsChecked(),
                                                    TradeStrengthPack.IsChecked(),
                                                    UntilViPack.IsChecked(),
                                                    DUPPack.IsChecked(),
                });

                    string sPassNum = passNumTxtBox.Text.Trim();
                    int nPassMinusNum = 0;



                    if (!sPassNum.Equals(""))
                    {
                        nPassMinusNum = int.Parse(sPassNum);

                        if (nPassMinusNum > 0)
                        {
                            nFinalPassNum = nPassMinusNum;
                        }
                        else if (nPassMinusNum < 0)
                        {
                            nFinalPassNum = nFullMinusNum + nPassMinusNum;
                        }
                        else
                        {
                            nFinalPassNum = 0;
                        }
                    }
                    else
                        nFinalPassNum = nFullMinusNum;


                    bool isViAlarm = false;
                    bool isCrushAlarm = false;
                    bool isUpAlarm = false;

                    bool isShow;
                    bool isReserveShow;

                    for (int i = 0; i < mainForm.nStockLength; i++)
                    {
                        try
                        {
                            nPass = 0;

                            isShow = false;
                            isReserveShow = true;

                            if (TFPack.IsChecked() && TFPack.Compare(mainForm.ea[i].fakeStrategyMgr.nTotalFakeCount))
                                nPass++;
                            if (SGPack.IsChecked() && SGPack.Compare(mainForm.ea[i].fStartGap))
                                nPass++;
                            if (WOGPack.IsChecked() && WOGPack.Compare(mainForm.ea[i].fPowerWithoutGap))
                                nPass++;
                            if (CPPack.IsChecked() && CPPack.Compare(mainForm.ea[i].fPower))
                                nPass++;
                            if (PDPack.IsChecked() && mainForm.ea[i].nYesterdayEndPrice > 0 && PDPack.Compare((double)(mainForm.ea[i].timeLines1m.nMaxUpFs - mainForm.ea[i].nFs) / mainForm.ea[i].nYesterdayEndPrice))
                                nPass++;
                            if (RPDPack.IsChecked() && RPDPack.Compare(mainForm.ea[i].fTodayMaxPower - mainForm.ea[i].fPower))
                                nPass++;
                            if (PJPack.IsChecked() && PJPack.Compare(mainForm.ea[i].fPowerJar))
                                nPass++;
                            if (UPJPack.IsChecked() && UPJPack.Compare(mainForm.ea[i].fOnlyUpPowerJar))
                                nPass++;
                            if (DPJPack.IsChecked() && DPJPack.Compare(mainForm.ea[i].fOnlyDownPowerJar))
                                nPass++;
                            if (TTMPack.IsChecked() && TTMPack.Compare(mainForm.ea[i].lTotalTradePrice / 100000000.0))
                                nPass++;
                            if (BMPack.IsChecked() && BMPack.Compare(mainForm.ea[i].lOnlyBuyPrice / 100000000.0))
                                nPass++;
                            if (SMPack.IsChecked() && SMPack.Compare(mainForm.ea[i].lOnlySellPrice / 100000000.0))
                                nPass++;
                            if (TAPack.IsChecked() && TAPack.Compare(mainForm.ea[i].timeLines1m.fTotalMedianAngle))
                                nPass++;
                            if (HAPack.IsChecked() && HAPack.Compare(mainForm.ea[i].timeLines1m.fHourMedianAngle))
                                nPass++;
                            if (RAPack.IsChecked() && RAPack.Compare(mainForm.ea[i].timeLines1m.fRecentMedianAngle))
                                nPass++;
                            if (DAPack.IsChecked() && DAPack.Compare(mainForm.ea[i].timeLines1m.fDAngle))
                                nPass++;
                            if (_1PPack.IsChecked() && _1PPack.Compare(mainForm.ea[i].timeLines1m.onePerCandleList.Count))
                                nPass++;
                            if (_2PPack.IsChecked() && _2PPack.Compare(mainForm.ea[i].timeLines1m.twoPerCandleList.Count))
                                nPass++;
                            if (_3PPack.IsChecked() && _3PPack.Compare(mainForm.ea[i].timeLines1m.threePerCandleList.Count))
                                nPass++;
                            if (_4PPack.IsChecked() && _4PPack.Compare(mainForm.ea[i].timeLines1m.fourPerCandleList.Count))
                                nPass++;
                            if (VIPack.IsChecked() && VIPack.Compare(mainForm.ea[i].nViCnt))
                                nPass++;
                            if (RBPPack.IsChecked() && RBPPack.Compare(mainForm.ea[i].fakeStrategyMgr.nAIPassed))
                                nPass++;
                            if (TFPPack.IsChecked() && TFPPack.Compare(mainForm.ea[i].fakeStrategyMgr.nFakeAccumPassed))
                                nPass++;
                            if (EPPack.IsChecked() && EPPack.Compare(mainForm.ea[i].fakeStrategyMgr.nEveryAIPassNum))
                                nPass++;
                            if (AISPack.IsChecked() && AISPack.Compare(mainForm.ea[i].fakeStrategyMgr.fAIScore))
                                nPass++;
                            if (SCPack.IsChecked() && SCPack.Compare(mainForm.ea[i].nStopHogaCnt))
                                nPass++;
                            if (SUDCPack.IsChecked() && SUDCPack.Compare(mainForm.ea[i].nStopUpDownCnt))
                                nPass++;
                            if (SUPPack.IsChecked() && SUPPack.Compare(mainForm.ea[i].fStopMaxPower))
                                nPass++;
                            if (SDPPack.IsChecked() && SDPPack.Compare(mainForm.ea[i].fStopMinPower))
                                nPass++;
                            if (SPDPack.IsChecked() && SPDPack.Compare(mainForm.ea[i].fStopMaxMinDiff))
                                nPass++;
                            if (ECPack.IsChecked() && ECPack.Compare(mainForm.ea[i].fakeStrategyMgr.nEveryAICount))
                                nPass++;
                            if (CMPack.IsChecked() && CMPack.Compare((double)(mainForm.ea[i].timeLines1m.arrTimeLine[mainForm.ea[i].timeLines1m.nPrevTimeLineIdx].nLastFs - mainForm.ea[i].timeLines1m.arrTimeLine[mainForm.ea[i].timeLines1m.nPrevTimeLineIdx].nStartFs) / mainForm.ea[i].nYesterdayEndPrice))
                                nPass++;
                            if (PMPack.IsChecked() && PMPack.Compare((double)(mainForm.ea[i].timeLines1m.arrTimeLine[mainForm.ea[i].timeLines1m.nRealDataIdx].nLastFs - mainForm.ea[i].timeLines1m.arrTimeLine[mainForm.ea[i].timeLines1m.nRealDataIdx].nStartFs) / mainForm.ea[i].nYesterdayEndPrice))
                                nPass++;
                            if (CCPack.IsChecked() && CCPack.Compare(mainForm.ea[i].nChegyulCnt))
                                nPass++;
                            if (CSPack.IsChecked() && CSPack.Compare(mainForm.ea[i].speedStatus.fCur))
                                nPass++;
                            if (AMUPack.IsChecked() && AMUPack.Compare(mainForm.ea[i].fPositiveStickPower))
                                nPass++;
                            if (AMDPack.IsChecked() && AMDPack.Compare(mainForm.ea[i].fNegativeStickPower))
                                nPass++;
                            if (HSPack.IsChecked() && HSPack.Compare(mainForm.ea[i].hogaSpeedStatus.fCur))
                                nPass++;
                            if (PSPack.IsChecked() && PSPack.Compare(mainForm.ea[i].priceMoveStatus.fCur))
                                nPass++;
                            if (PUSPack.IsChecked() && PUSPack.Compare(mainForm.ea[i].priceUpMoveStatus.fCur))
                                nPass++;
                            if (TSPack.IsChecked() && TSPack.Compare(mainForm.ea[i].tradeStatus.fCur))
                                nPass++;
                            if (TMAXPack.IsChecked() && TMAXPack.Compare(mainForm.ea[i].fTodayMaxPower))
                                nPass++;
                            if (TMINPack.IsChecked() && TMINPack.Compare(mainForm.ea[i].fTodayMinPower))
                                nPass++;
                            if (TMDPack.IsChecked() && TMDPack.Compare(mainForm.ea[i].fTodayMaxPower - mainForm.ea[i].fTodayBottomPower))
                                nPass++;
                            if (DUPPack.IsChecked() && DUPPack.Compare(mainForm.ea[i].fPower - mainForm.ea[i].fTodayBottomPower))
                                nPass++;
                            if (MRPack.IsChecked() && MRPack.Compare(mainForm.ea[i].rankSystem.nMinuteSummationRanking))
                                nPass++;
                            if (TRPack.IsChecked() && TRPack.Compare(mainForm.ea[i].rankSystem.nSummationRanking))
                                nPass++;
                            if (RDIFPack.IsChecked() && RDIFPack.Compare(mainForm.ea[i].rankSystem.nSummationMove))
                                nPass++;
                            if (AI20TPack.IsChecked() && AI20TPack.Compare(mainForm.ea[i].fakeStrategyMgr.nAI20Time))
                                nPass++;
                            if (AI30TPack.IsChecked() && AI30TPack.Compare(mainForm.ea[i].fakeStrategyMgr.nAI30Time))
                                nPass++;
                            if (AI50TPack.IsChecked() && AI50TPack.Compare(mainForm.ea[i].fakeStrategyMgr.nAI50Time))
                                nPass++;
                            if (CRCPack.IsChecked() && CRCPack.Compare(mainForm.ea[i].crushMinuteManager.nCurCnt))
                                nPass++;
                            if (UCRCPack.IsChecked() && UCRCPack.Compare(mainForm.ea[i].crushMinuteManager.nUpCnt))
                                nPass++;
                            if (T150Pack.IsChecked() && T150Pack.Compare(mainForm.ea[i].sequenceStrategy.nSpeed150TotalSec))
                                nPass++;
                            if (C150Pack.IsChecked() && C150Pack.Compare(mainForm.ea[i].sequenceStrategy.nSpeed150CurSec))
                                nPass++;
                            if (RBAPack.IsChecked() && RBAPack.Compare(mainForm.ea[i].paperBuyStrategy.nStrategyNum))
                                nPass++;
                            if (RBDPack.IsChecked() && RBDPack.Compare(mainForm.ea[i].paperBuyStrategy.nMinuteLocationCount))
                                nPass++;
                            if (FBAPack.IsChecked() && FBAPack.Compare(mainForm.ea[i].fakeBuyStrategy.nStrategyNum))
                                nPass++;
                            if (FBDPack.IsChecked() && FBDPack.Compare(mainForm.ea[i].fakeBuyStrategy.nMinuteLocationCount))
                                nPass++;
                            if (FAAPack.IsChecked() && FAAPack.Compare(mainForm.ea[i].fakeAssistantStrategy.nStrategyNum))
                                nPass++;
                            if (FADPack.IsChecked() && FADPack.Compare(mainForm.ea[i].fakeAssistantStrategy.nMinuteLocationCount))
                                nPass++;
                            if (FRAPack.IsChecked() && FRAPack.Compare(mainForm.ea[i].fakeResistStrategy.nStrategyNum))
                                nPass++;
                            if (FRDPack.IsChecked() && FRDPack.Compare(mainForm.ea[i].fakeResistStrategy.nMinuteLocationCount))
                                nPass++;
                            if (PUAPack.IsChecked() && PUAPack.Compare(mainForm.ea[i].fakeVolatilityStrategy.nStrategyNum))
                                nPass++;
                            if (PUDPack.IsChecked() && PUDPack.Compare(mainForm.ea[i].fakeVolatilityStrategy.nMinuteLocationCount))
                                nPass++;
                            if (PDAPack.IsChecked() && PDAPack.Compare(mainForm.ea[i].fakeDownStrategy.nStrategyNum))
                                nPass++;
                            if (PDDPack.IsChecked() && PDDPack.Compare(mainForm.ea[i].fakeDownStrategy.nMinuteLocationCount))
                                nPass++;
                            if (SFDPack.IsChecked() && SFDPack.Compare(mainForm.ea[i].fakeStrategyMgr.nSharedMinuteLocationCount))
                                nPass++;
                            if (HIT5Pack.IsChecked() && HIT5Pack.Compare(mainForm.ea[i].fakeStrategyMgr.hitDict25.Count))
                                nPass++;
                            if (HIT8Pack.IsChecked() && HIT8Pack.Compare(mainForm.ea[i].fakeStrategyMgr.hitDict38.Count))
                                nPass++;
                            if (HIT10Pack.IsChecked() && HIT10Pack.Compare(mainForm.ea[i].fakeStrategyMgr.hitDict410.Count))
                                nPass++;
                            if (HIT12Pack.IsChecked() && HIT12Pack.Compare(mainForm.ea[i].fakeStrategyMgr.hitDict312.Count))
                                nPass++;
                            if (CURFTPack.IsChecked() && CURFTPack.Compare(mainForm.ea[i].fakeStrategyMgr.nCurHitType))
                                nPass++;
                            if (CURFCPack.IsChecked() && CURFCPack.Compare(mainForm.ea[i].fakeStrategyMgr.nCurHitNum))
                                nPass++;
                            if (FABNumPack.IsChecked() && FABNumPack.Compare(mainForm.ea[i].fakeAssistantStrategy.nStrategyNum + mainForm.ea[i].fakeBuyStrategy.nStrategyNum))
                                nPass++;
                            if (FABPlusNumPack.IsChecked() && FABPlusNumPack.Compare(mainForm.ea[i].fakeAssistantStrategy.nStrategyNum + mainForm.ea[i].fakeBuyStrategy.nStrategyNum + mainForm.ea[i].fakeBuyStrategy.nStrategyNum / 2))
                                nPass++;
                            if (TradeComparedPack.IsChecked() && TradeComparedPack.Compare(mainForm.ea[i].fTradeRatioCompared))
                                nPass++;
                            if (TradeStrengthPack.IsChecked() && TradeStrengthPack.Compare(mainForm.ea[i].fTs))
                                nPass++;
                            if (UntilViPack.IsChecked() && UntilViPack.Compare((double)(mainForm.ea[i].nUpViPrice - mainForm.ea[i].nFs) / mainForm.ea[i].nYesterdayEndPrice))
                                nPass++;


                            isShow = nPass >= nFinalPassNum;


                            if (isReserved) // 예약ㅇ라면
                            {
                                isReserveShow = false;

                                if (isR1) // r1 조건
                                {
                                    isReserveShow = mainForm.ea[i].isViMode;
                                }
                                else if (isR2) // r2 조건
                                {
                                    isReserveShow = mainForm.nTimeLineIdx == mainForm.ea[i].crushMinuteManager.nCrushRealTimeLineIdx;
                                }
                                else if (isR3) // r3 조건
                                {
                                    isReserveShow = mainForm.nTimeLineIdx == mainForm.ea[i].crushMinuteManager.nCrushTimeLineIdx;
                                }
                                else if (isR4) // r4 조건
                                {
                                    isReserveShow = mainForm.ea[i].fTodayMaxPower >= 0.1 &&
                                                mainForm.ea[i].fakeStrategyMgr.nTotalFakeCount >= 50 &&
                                                mainForm.ea[i].fakeBuyStrategy.nMinuteLocationCount >= 2;
                                }
                                else if (isRZ)
                                {
                                    if (nRZNum == 1)
                                        isReserveShow = mainForm.ea[i].manualReserve.isChosenQ;
                                    else if (nRZNum == 2)
                                        isReserveShow = mainForm.ea[i].manualReserve.isChosenW;
                                    else if (nRZNum == 3)
                                        isReserveShow = mainForm.ea[i].manualReserve.isChosenE;
                                    else if (nRZNum == 4)
                                        isReserveShow = mainForm.ea[i].manualReserve.isChosenR;
                                    else if (nRZNum == 5)
                                        isReserveShow = mainForm.ea[i].manualReserve.reserveArr[MainForm.UP_RESERVE].isSelected;
                                    else if (nRZNum == 6)
                                        isReserveShow = mainForm.ea[i].manualReserve.reserveArr[MainForm.DOWN_RESERVE].isSelected;
                                    else if (nRZNum == 7)
                                        isReserveShow = mainForm.ea[i].manualReserve.reserveArr[MainForm.MA_20M_DOWN_RESERVE].isSelected;
                                    else if (nRZNum == 8)
                                        isReserveShow = mainForm.ea[i].manualReserve.reserveArr[MainForm.MA_2H_DOWN_RESERVE].isSelected;
                                    else if (nRZNum == 9)
                                        isReserveShow = mainForm.ea[i].manualReserve.reserveArr[MainForm.MA_UP_RESERVE].isSelected;
                                    else if (nRZNum == 10)
                                    {
                                        if (mainForm.ea[i].nSelectedConditionJ > 0)
                                            isReserveShow = true;
                                    }
                                    else if (nRZNum == 11)
                                    {
                                        if (mainForm.ea[i].nSelectedConditionK > 0)
                                            isReserveShow = true;
                                    }
                                    else if (nRZNum == 12)
                                    {
                                        if (mainForm.ea[i].nSelectedConditionJ > 0 ||
                                            mainForm.ea[i].nSelectedConditionK > 0 ||
                                            mainForm.ea[i].manualReserve.isChosenQ ||
                                            mainForm.ea[i].manualReserve.isChosenW ||
                                            mainForm.ea[i].manualReserve.isChosenE ||
                                            mainForm.ea[i].manualReserve.isChosenR
                                           )
                                            isReserveShow = true;
                                    }

                                }
                                else // 에러
                                {

                                }
                            }

                            if (qCheckBox.Checked && mainForm.ea[i].manualReserve.isChosenQ)
                                continue;

                            if (wCheckBox.Checked && mainForm.ea[i].manualReserve.isChosenW)
                                continue;

                            if (eCheckBox.Checked && mainForm.ea[i].manualReserve.isChosenE)
                                continue;

                            if (rCheckBox.Checked && mainForm.ea[i].manualReserve.isChosenR)
                                continue;

                            if (isShow && isReserveShow)
                            {
                                targetTimeArr[i]++;
                                nPassLen++;
                                ListViewItem listViewItem = new ListViewItem(new string[] {
                                mainForm.ea[i].sCode,
                                mainForm.ea[i].sCodeName,
                                Math.Round(mainForm.ea[i].fPower, 3).ToString(),
                                Math.Round(mainForm.ea[i].fStartGap, 3).ToString(),
                                Math.Round(mainForm.ea[i].fTodayMaxPower - mainForm.ea[i].fPower, 3).ToString(),
                                Math.Round(mainForm.ea[i].fTodayMaxPower, 3).ToString(),

                                "", // 매수예약

                                Math.Round((double)(mainForm.ea[i].nUpViPrice - mainForm.ea[i].nFs) / mainForm.ea[i].nYesterdayEndPrice, 3).ToString(),
                                mainForm.ea[i].isViMode.ToString(),

                                Math.Round(mainForm.ea[i].speedStatus.fCur, 2).ToString(),
                                Math.Round(mainForm.ea[i].tradeStatus.fCur , 2).ToString(),
                                Math.Round(mainForm.ea[i].pureTradeStatus.fCur , 2).ToString(),

                                mainForm.ea[i].fakeBuyStrategy.nStrategyNum.ToString(),
                                mainForm.ea[i].fakeAssistantStrategy.nStrategyNum.ToString(),
                                (mainForm.ea[i].fakeBuyStrategy.nStrategyNum + mainForm.ea[i].fakeAssistantStrategy.nStrategyNum).ToString(),
                                mainForm.ea[i].paperBuyStrategy.nStrategyNum.ToString(),

                                Math.Round((double)(mainForm.ea[i].timeLines1m.arrTimeLine[mainForm.ea[i].timeLines1m.nRealDataIdx].nLastFs - mainForm.ea[i].timeLines1m.arrTimeLine[mainForm.ea[i].timeLines1m.nRealDataIdx].nStartFs) / mainForm.ea[i].nYesterdayEndPrice, 3).ToString(),
                                Math.Round((double)(mainForm.ea[i].timeLines1m.arrTimeLine[mainForm.ea[i].timeLines1m.nPrevTimeLineIdx].nLastFs - mainForm.ea[i].timeLines1m.arrTimeLine[mainForm.ea[i].timeLines1m.nPrevTimeLineIdx].nStartFs) / mainForm.ea[i].nYesterdayEndPrice, 3).ToString(),

                                mainForm.ea[i].rankSystem.nMinuteSummationRanking.ToString(),
                                mainForm.ea[i].rankSystem.nSummationRanking.ToString(),
                                mainForm.ea[i].rankSystem.nSummationMove.ToString(),


                                targetTimeArr[i].ToString(),

                                mainForm.ea[i].fakeStrategyMgr.nCurHitNum.ToString(),
                                mainForm.ea[i].fakeStrategyMgr.nCurHitType.ToString(),

                                mainForm.ea[i].fakeStrategyMgr.nEveryAICount.ToString(),
                                mainForm.ea[i].fakeStrategyMgr.nTotalFakeCount.ToString(),
                                mainForm.ea[i].fakeStrategyMgr.nTotalArrowCount.ToString(),


                                Math.Round(mainForm.ea[i].fPowerWithoutGap, 3).ToString(),

                                Math.Round(mainForm.ea[i].fakeStrategyMgr.fAIScore, 2).ToString(),

                            });


                                Color myColor;

                                if (mainForm.ea[i].fPowerJar == 0) //  평균 이율
                                    myColor = Color.FromArgb(255, 255, 255);
                                else if (mainForm.ea[i].fPowerJar > 0)
                                {
                                    int nColorStep = (int)(mainForm.ea[i].fPowerJar * 20000);
                                    if (nColorStep > 255)
                                        nColorStep = 255;
                                    myColor = Color.FromArgb(255, 255 - nColorStep, 255 - nColorStep);
                                }
                                else
                                {
                                    int nColorStep = (int)(Math.Abs(mainForm.ea[i].fPowerJar) * 20000);
                                    if (nColorStep > 255)
                                        nColorStep = 255;
                                    myColor = Color.FromArgb(255 - nColorStep, 255 - nColorStep, 255);
                                }


                                listViewItem.UseItemStyleForSubItems = false;





                                for (int restIdx = 0; restIdx < listViewItem.SubItems.Count; restIdx++)
                                {
                                    listViewItem.SubItems[restIdx].BackColor = myColor;
                                }

                                // K
                                try
                                {
                                    Color colorSelection = GetColorBySelectionNum(mainForm.ea[i].nSelectedConditionK, myColor);
                                    listViewItem.SubItems[4].BackColor = colorSelection;
                                }
                                catch
                                {

                                }

                                // J 
                                try
                                {
                                    Color colorSelection = GetColorBySelectionNum(mainForm.ea[i].nSelectedConditionJ, myColor);
                                    listViewItem.SubItems[6].BackColor = colorSelection;
                                }
                                catch
                                {

                                }

                                try // 기본색칠
                                {
                                    if (mainForm.ea[i].manualReserve.isChosenQ)
                                        listViewItem.SubItems[0].BackColor = Color.Green; // Green
                                    if (mainForm.ea[i].manualReserve.isChosenW)
                                        listViewItem.SubItems[1].BackColor = Color.Orange; // Orange 
                                    if (mainForm.ea[i].manualReserve.isChosenE)
                                        listViewItem.SubItems[2].BackColor = Color.SkyBlue; // SkyBlue
                                    if (mainForm.ea[i].manualReserve.isChosenR)
                                        listViewItem.SubItems[3].BackColor = Color.GreenYellow; // GreenYellow
                                   

                                    if (mainForm.ea[i].manualReserve.reserveArr[MainForm.UP_RESERVE].isBuyReserved ||
                                              mainForm.ea[i].manualReserve.reserveArr[MainForm.DOWN_RESERVE].isBuyReserved ||
                                              mainForm.ea[i].manualReserve.reserveArr[MainForm.MA_20M_DOWN_RESERVE].isBuyReserved ||
                                              mainForm.ea[i].manualReserve.reserveArr[MainForm.MA_2H_DOWN_RESERVE].isBuyReserved ||
                                              mainForm.ea[i].manualReserve.reserveArr[MainForm.MA_UP_RESERVE].isBuyReserved)
                                        listViewItem.SubItems[6].BackColor = Color.Black;

                                    if (mainForm.ea[i].manualReserve.reserveArr[MainForm.UP_RESERVE].isSelected)
                                        listViewItem.SubItems[7].BackColor = Color.BlueViolet;
                                    if (mainForm.ea[i].manualReserve.reserveArr[MainForm.UP_RESERVE].isChosen1)
                                        listViewItem.SubItems[8].BackColor = Color.BlueViolet;

                                    if (mainForm.ea[i].manualReserve.reserveArr[MainForm.DOWN_RESERVE].isSelected)
                                        listViewItem.SubItems[9].BackColor = Color.Gold;
                                    if (mainForm.ea[i].manualReserve.reserveArr[MainForm.DOWN_RESERVE].isChosen1)
                                        listViewItem.SubItems[10].BackColor = Color.Gold;

                                    if (mainForm.ea[i].manualReserve.reserveArr[MainForm.MA_20M_DOWN_RESERVE].isSelected)
                                        listViewItem.SubItems[11].BackColor = Color.Turquoise;
                                    if (mainForm.ea[i].manualReserve.reserveArr[MainForm.MA_20M_DOWN_RESERVE].isChosen1)
                                        listViewItem.SubItems[12].BackColor = Color.Turquoise;

                                    if (mainForm.ea[i].manualReserve.reserveArr[MainForm.MA_2H_DOWN_RESERVE].isSelected)
                                        listViewItem.SubItems[13].BackColor = Color.Olive;
                                    if (mainForm.ea[i].manualReserve.reserveArr[MainForm.MA_2H_DOWN_RESERVE].isChosen1)
                                        listViewItem.SubItems[14].BackColor = Color.Olive;

                                    if (mainForm.ea[i].manualReserve.reserveArr[MainForm.MA_UP_RESERVE].isSelected)
                                        listViewItem.SubItems[15].BackColor = Color.Teal;
                                    if (mainForm.ea[i].manualReserve.reserveArr[MainForm.MA_UP_RESERVE].isChosen1)
                                        listViewItem.SubItems[16].BackColor = Color.Teal;
                                }
                                catch
                                {

                                }

                                if (maVisualCheckBox.Checked)
                                {
                                    try
                                    {
                                        if (mainForm.nTimeLineIdx == mainForm.ea[i].nMaGapFallTimeLineIdx)
                                            listViewItem.SubItems[5].BackColor = Color.Firebrick;

                                        if (mainForm.nTimeLineIdx == mainForm.ea[i].nMaGapCrushTimeLineIdx)
                                            listViewItem.SubItems[5].BackColor = Color.Orchid;
                                    }
                                    catch
                                    { }
                                }

                                if (crushVisualCheckBox.Checked)
                                {
                                    try
                                    {
                                        bool isDigit = double.TryParse(crushVisualTxtBox.Text, out double fCrushValue);
                                        if (isDigit)
                                        {
                                            if (mainForm.ea[i].fTodayMaxPower - mainForm.ea[i].fPower <= fCrushValue)
                                                listViewItem.SubItems[4].BackColor = Color.Yellow;
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }

                                if (viVisualCheckBox.Checked)
                                {
                                    try
                                    {
                                        bool isDigit = double.TryParse(viVisualTxtBox.Text, out double fViValue);
                                        if (isDigit)
                                        {
                                            if (((double)(mainForm.ea[i].nUpViPrice - mainForm.ea[i].nFs) / mainForm.ea[i].nYesterdayEndPrice) <= fViValue)
                                                listViewItem.SubItems[7].BackColor = Color.Orange;
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }

                                if (speedVisualCheckBox.Checked)
                                {
                                    try
                                    {
                                        bool isDigit = double.TryParse(speedVisualTxtBox.Text, out double fSpeedValue);
                                        if (isDigit)
                                        {
                                            if (mainForm.ea[i].speedStatus.fCur >= fSpeedValue)
                                                listViewItem.SubItems[9].BackColor = Color.Green;
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }

                                if (downVisualCheckBox.Checked)
                                {
                                    // 가격 이하
                                    try
                                    {
                                        bool isDigit = double.TryParse(downVisualTxtBox.Text, out double fDownValue);
                                        if (isDigit)
                                        {
                                            if (mainForm.ea[i].fTodayMaxPower - mainForm.ea[i].fPower >= fDownValue)
                                                listViewItem.SubItems[4].BackColor = Color.PaleVioletRed;
                                        }
                                    }
                                    catch { }
                                }


                                // 기본 색칠 2 
                                // 무조건 제일 상단에 나오게 하려면
                                try
                                {
                                    if (mainForm.nTimeLineIdx == mainForm.ea[i].crushMinuteManager.nCrushRealTimeLineIdx)
                                        listViewItem.SubItems[4].BackColor = Color.DarkGray;
                                    if (mainForm.nTimeLineIdx == mainForm.ea[i].crushMinuteManager.nCrushTimeLineIdx)
                                        listViewItem.SubItems[5].BackColor = Color.LightSlateGray;
                                }
                                catch { }


                                listViewItemList.Add(listViewItem);

                                // crush sound alarm 
                                if (crushSoundCheckBox.Checked)
                                {
                                    if ((mainForm.nTimeLineIdx == mainForm.ea[i].crushMinuteManager.nCrushRealTimeLineIdx ||
                                        mainForm.nTimeLineIdx == mainForm.ea[i].crushMinuteManager.nCrushTimeLineIdx)
                                        )
                                    {
                                        if (!mainForm.ea[i].soundReserve.isCrushCheck)
                                        {
                                            mainForm.ea[i].soundReserve.isCrushCheck = true;
                                            isCrushAlarm = true;
                                        }
                                    }
                                    else
                                    {
                                        mainForm.ea[i].soundReserve.isCrushCheck = false;
                                    }
                                }

                                // vi sound alarm
                                if (viSoundCheckBox.Checked)
                                {
                                    if ((mainForm.ea[i].isViMode && mainForm.ea[i].speedStatus.fCur >= 30)
                                        )
                                    {
                                        if (!mainForm.ea[i].soundReserve.isViCheck) // 언제 끝나지 
                                        {
                                            mainForm.ea[i].soundReserve.isViCheck = true;
                                            isViAlarm = true;
                                        }
                                    }
                                    else
                                    {
                                        mainForm.ea[i].soundReserve.isViCheck = false;
                                    }
                                }

                                if (upSoundCheckBox.Checked)
                                {
                                    if (mainForm.ea[i].manualReserve.reserveArr[MainForm.UP_RESERVE].dChosenTime != mainForm.ea[i].manualReserve.reserveArr[MainForm.UP_RESERVE].dPrevChosenTime)
                                    {
                                        mainForm.ea[i].manualReserve.reserveArr[MainForm.UP_RESERVE].dPrevChosenTime = mainForm.ea[i].manualReserve.reserveArr[MainForm.UP_RESERVE].dChosenTime;
                                        isUpAlarm = true;
                                    }
                                }

                            }
                        }
                        catch
                        {
                            registerLabel.Text = "오류 발생";
                            timer.Enabled = false;
                            timerCheckBox.Checked = false;
                            return;
                        }

                    }

                    // 조건에 맞는 애들이 있다면 출력
                    if (listViewItemList.Count > 0)
                    {
                        listView1.Items.AddRange(listViewItemList.ToArray());
                        listViewItemList.Clear();
                    }


                    // 알람 확인 및 예약 
                    if (isUpAlarm)
                        mainForm.soundMgr.soundReserveQueue.Enqueue(MainForm.SoundTrack.Up);

                    if (isCrushAlarm)
                        mainForm.soundMgr.soundReserveQueue.Enqueue(MainForm.SoundTrack.Crush);

                    if (isViAlarm)
                        mainForm.soundMgr.soundReserveQueue.Enqueue(MainForm.SoundTrack.VI);



                    try // sound 없는 기계에 soundPlayer.Play()로 문제가 생길까봐... 말이 안되기는 하지만 조심
                    {
                        if (crushSoundCheckBox.Checked || viSoundCheckBox.Checked || upSoundCheckBox.Checked)
                        {

                            // 딜레이 확인 
                            if (mainForm.soundMgr.isSoundCompleted &&
                                mainForm.soundMgr.soundReserveQueue.Count > 0) // 재생이 끝남 
                            {
                                MainForm.SoundTrack sound = mainForm.soundMgr.soundReserveQueue.Dequeue();
                                Task.Run(() =>
                                {
                                    mainForm.soundMgr.isSoundCompleted = false;
                                    try
                                    {
                                        if (sound == MainForm.SoundTrack.Crush)
                                        {
                                            if (crushSoundCheckBox.Checked)
                                                crushSoundPlayer.Play();
                                            else
                                                mainForm.soundMgr.soundReserveQueue.Enqueue(sound);
                                        }
                                        else if (sound == MainForm.SoundTrack.VI)
                                        {
                                            if (viSoundCheckBox.Checked)
                                                viSoundPlayer.Play();
                                            else
                                                mainForm.soundMgr.soundReserveQueue.Enqueue(sound);
                                        }

                                        else if (sound == MainForm.SoundTrack.Up)
                                        {
                                            if (upSoundCheckBox.Checked)
                                                upSoundPlayer.Play();
                                            else
                                                mainForm.soundMgr.soundReserveQueue.Enqueue(sound);
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    finally
                                    {
                                        mainForm.soundMgr.isSoundCompleted = true;
                                    }
                                });
                            }


                        }
                    }
                    catch
                    {
                    }


                    doneLabel.Text = $"done..{++nDoneCnt}";
                    passLenLabel.Text = $"pass {nPassLen}";

                    DateTime endTime = DateTime.Now;
                    // 타이머가 on이라면
                    if (timerCheckBox.Checked)
                    {
                        if (nPassLen > 200 || (endTime - startTime).TotalMilliseconds > 500)  //  타이머 무리무리
                        {
                            timer.Enabled = false;
                            timerCheckBox.Checked = false;
                        }
                        else
                        {

                            timer.Interval = nTimerMilliSec;
                            timer.Enabled = true;
                            timerCheckBox.Checked = true;
                        }
                    }
                    else
                    {
                        timer.Enabled = false;
                        timerCheckBox.Checked = false;
                    }

                    isReserved = isReserved && timer.Enabled;
                }
                catch
                {
                    doneLabel.Text = "Uncomplete";
                }
                finally
                {
                    isUsing = false;
                    listView1.EndUpdate();
                }
            }

            if (listView1.InvokeRequired)
            {
                listView1.Invoke(new MethodInvoker(Func));
            }
            else
                Func();
        }

        public int nDoneCnt = 0;

        public System.Timers.Timer timer;

        public bool isReserved = false;
        public bool isR1 = false;
        public bool isR2 = false;
        public bool isR3 = false;
        public bool isR4 = false;

        public bool isRZ = false;
        public int nRZNum = -1;



        public void CheckReserve()
        {
            isReserved = true;
            isR1 = isR2 = isR3 = isR4 = isRZ = false;
        }


        public int nTimerMilliSec = 300;
        public const int TIMER_MOVING = 100;

        public void TimerButtonClickHandler(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(timerUpButton))
                {
                    nTimerMilliSec += TIMER_MOVING;
                    timer.Interval = nTimerMilliSec;
                    timerLabel.Text = nTimerMilliSec.ToString();
                }
                else if (sender.Equals(timerDownButton))
                {
                    if (nTimerMilliSec > 100)
                    {
                        nTimerMilliSec -= TIMER_MOVING;
                        timer.Interval = nTimerMilliSec;
                        timerLabel.Text = nTimerMilliSec.ToString();
                    }
                }
            }
            catch
            {
                nTimerMilliSec = 300;
                timer.Interval = nTimerMilliSec;
                timerLabel.Text = nTimerMilliSec.ToString();
            }
        }

        private void ReserveButtonClickHandler(object sender, EventArgs e)
        {
            CheckReserve();

            if (sender.Equals(reserve1Btn))
            {
                isR1 = true;
                ShowIndicator();
            }
            else if (sender.Equals(reserve2Btn))
            {
                isR2 = true;
                ShowIndicator();
            }
            else if (sender.Equals(reserve3Btn))
            {
                isR3 = true;
                ShowIndicator();
            }
            else if (sender.Equals(reserve4Btn))
            {
                isR4 = true;
                ShowIndicator();
            }
            else if (sender.Equals(confirmButton))
            {
                isReserved = false;
                timer.Enabled = false;
                ShowIndicator();
            }
        }



        // =======================================
        // 마지막 편집일 : 2023-04-20
        // 1. 행을 더블클릭하면 해당 종목의 차트뷰 폼을 콜한다.
        // =======================================
        public int nPrevEaIdx = 0;
        public void RowDoubleClickHandler(Object sender, MouseEventArgs e)
        {

            try
            {
                if (listView1.FocusedItem != null)
                {
                    nPrevEaIdx = mainForm.eachStockDict[listView1.FocusedItem.SubItems[0].Text.Trim()];
                    CallThreadEachStockHistoryForm(nPrevEaIdx);
                }
            }
            catch { }

        }

        public void MouseClickHandler(Object sender, EventArgs e)
        {
            try
            {
                if (listView1.FocusedItem != null)
                {
                    nPrevEaIdx = mainForm.eachStockDict[listView1.FocusedItem.SubItems[0].Text.Trim()];
                    if (timerCheckBox.Checked)
                        CallThreadEachStockHistoryForm(nPrevEaIdx);
                }
            }
            catch
            {

            }

        }

        public Color GetColorBySelectionNum(int nSelection, Color baseColor)
        {
            Color retColor;
            if (nSelection == 1)
                retColor = Color.MediumSpringGreen;
            else if (nSelection == 2)
                retColor = Color.MediumAquamarine;
            else if (nSelection == 3)
                retColor = Color.Peru;
            else if (nSelection == 4)
                retColor = Color.Tan;
            else if (nSelection == 5)
                retColor = Color.PaleTurquoise;
            else if (nSelection == 6)
                retColor = Color.PowderBlue;
            else if (nSelection > 0)
                retColor = Color.SlateBlue;
            else
                retColor = baseColor;
            return retColor;
        }

        public int sortColumn = -1;
        public const string UP_TIP = " ▲";
        public const string DOWN_TIP = " ▼";
        public int nTipLen = DOWN_TIP.Length;

        public void ColumnClickHandler(Object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                if (sortColumn != -1) // 처음이 아니라면
                    listView1.Columns[sortColumn].Text = listView1.Columns[sortColumn].Text.Substring(0, listView1.Columns[sortColumn].Text.Length - nTipLen);
                sortColumn = e.Column;
                // Set the sort ord6er to ascending by default.
                listView1.Sorting = SortOrder.Descending; // 초기에는 내림차순
                listView1.Columns[sortColumn].Text = listView1.Columns[sortColumn].Text + DOWN_TIP; // 콜롬명 ▼
            }
            else
            {
                listView1.Columns[sortColumn].Text = listView1.Columns[sortColumn].Text.Substring(0, listView1.Columns[sortColumn].Text.Length - nTipLen);
                // Determine what the last sort order was and change it.
                if (listView1.Sorting == SortOrder.Descending)
                {
                    listView1.Sorting = SortOrder.Ascending;
                    listView1.Columns[sortColumn].Text = listView1.Columns[sortColumn].Text + UP_TIP;
                }
                else
                {
                    listView1.Sorting = SortOrder.Descending;
                    listView1.Columns[sortColumn].Text = listView1.Columns[sortColumn].Text + DOWN_TIP;
                }
            }
            // Call the sort method to manually sort.
            listView1.ListViewItemSorter = new MyListViewComparer(listView1.Columns[e.Column].Name, e.Column, listView1.Sorting);
            listView1.Sort();
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        // 리스트뷰 비교 인스턴스
        class MyListViewComparer : IComparer
        {
            private int col;
            private SortOrder order;
            private string s;

            public MyListViewComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }

            public MyListViewComparer(string s, int column, SortOrder order)
            {
                col = column;
                this.order = order;
                this.s = s;
            }

            public int Compare(object x, object y)
            {
                int returnVal = -1;

                if (s.Equals("STRING")) // string | boolean sort
                {
                    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else if (s.Equals("INT")) // int sort
                {
                    int nX = int.Parse(((ListViewItem)x).SubItems[col].Text);
                    int nY = int.Parse(((ListViewItem)y).SubItems[col].Text);

                    if (nX == nY)
                        returnVal = 0;
                    else if (nX < nY)
                        returnVal = -1;
                    else
                        returnVal = 1;
                }
                else if (s.Equals("DOUBLE")) // double sort
                {
                    // 전량매수취소된경우 수익률이 0.0일텐데 이게 건들지 않아서  0.0이지 
                    // canceld는 다른애들과 대비되야 한다. 그래서 수익률 비교하기 전에 isCanceld를 먼저 비교해준다.
                    double fX = double.Parse(((ListViewItem)x).SubItems[col].Text);
                    double fY = double.Parse(((ListViewItem)y).SubItems[col].Text);

                    if (fX == fY)
                        returnVal = 0;
                    else if (fX < fY)
                        returnVal = -1;
                    else
                        returnVal = 1;
                }

                // Determine whether the sort order is descending.
                if (order == SortOrder.Descending)
                    // Invert the value returned by String.Compare.
                    returnVal *= -1;

                return returnVal;
            }
        }


        #region Thread Call Method
        public void CallThreadEachStockHistoryForm(int nCallIdx)
        {
            try
            {
                if ((DateTime.UtcNow - mainForm.ea[nCallIdx].myTradeManager.dLatestApproachTime).TotalSeconds >= 1 && !mainForm.ea[nCallIdx].myTradeManager.isEachStockHistoryExist)
                {
                    mainForm.ea[nCallIdx].myTradeManager.dLatestApproachTime = DateTime.UtcNow;
                    mainForm.ea[nCallIdx].myTradeManager.isEachStockHistoryExist = true;
                    new Thread(() => new EachStockHistoryForm(mainForm, nCallIdx).ShowDialog()).Start();
                }
            }
            catch { }
        }


        #endregion

        private void InitTargetButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < mainForm.nStockLength; i++)
                targetTimeArr[i] = 0;
        }

    }
}
