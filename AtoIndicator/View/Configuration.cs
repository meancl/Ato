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
    public partial class Configuration : Form
    {
        private MainForm mainForm;
        private double fOneMove = 0.0005;

        public Configuration(MainForm mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            this.FormClosed += FormClosedHandler;
            gapCheckBox.CheckedChanged += CheckBoxCheckedHandler;

            delayTimeLabel.Text = mainForm.DEFAULT_DELAY_TIME.ToString();
            delayPaddingLabel.Text = Math.Round(mainForm.DEFAULT_DELAY_BOTTOM_PAD, 3).ToString();
            ceilingLabel.Text = Math.Round(mainForm.DEFAULT_FIXED_CEILING, 3).ToString();
            floorLabel.Text = Math.Round(mainForm.DEFAULT_FIXED_BOTTOM, 3).ToString();

            ceilingDownButton.Click += FixedChangeClickHandler;
            ceilingUpButton.Click += FixedChangeClickHandler;
            floorDownButton.Click += FixedChangeClickHandler;
            floorUpButton.Click += FixedChangeClickHandler;
            delayTimeDownButton.Click += FixedChangeClickHandler;
            delayTimeUpButton.Click += FixedChangeClickHandler;
            delayPaddingDownButton.Click += FixedChangeClickHandler;
            delayPaddingUpButton.Click += FixedChangeClickHandler;

        }

        private void CheckBoxCheckedHandler(object sender, EventArgs e)
        {
            if (gapCheckBox.Checked)
            {
                fOneMove = 0.001;
            }
            else
            {
                fOneMove = 0.0005;
            }
        }


        private void FormClosedHandler(Object sender, FormClosedEventArgs e)
        {
            mainForm.isConfigurationExist = false;
            this.Dispose();
        }

        private void FixedChangeClickHandler(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(ceilingDownButton))
                {
                    if (mainForm.DEFAULT_FIXED_CEILING - fOneMove >= 0)
                    {
                        mainForm.DEFAULT_FIXED_CEILING -= fOneMove;
                        ceilingLabel.Text = Math.Round(mainForm.DEFAULT_FIXED_CEILING, 4).ToString();
                    }
                }
                else if (sender.Equals(ceilingUpButton))
                {
                    mainForm.DEFAULT_FIXED_CEILING += fOneMove;
                    ceilingLabel.Text = Math.Round(mainForm.DEFAULT_FIXED_CEILING, 4).ToString();
                }
                else if (sender.Equals(floorDownButton))
                {
                    mainForm.DEFAULT_FIXED_BOTTOM -= fOneMove;
                    floorLabel.Text = Math.Round(mainForm.DEFAULT_FIXED_BOTTOM, 4).ToString();
                }
                else if (sender.Equals(floorUpButton))
                {
                    if (mainForm.DEFAULT_FIXED_BOTTOM + fOneMove < -0.0023)
                    {
                        mainForm.DEFAULT_FIXED_BOTTOM += fOneMove;
                        floorLabel.Text = Math.Round(mainForm.DEFAULT_FIXED_BOTTOM, 4).ToString();
                    }
                }
                else if (sender.Equals(delayPaddingDownButton))
                {
                    if (mainForm.DEFAULT_DELAY_BOTTOM_PAD >= fOneMove)
                    {
                        mainForm.DEFAULT_DELAY_BOTTOM_PAD -= fOneMove;
                    }
                    delayPaddingLabel.Text = Math.Round(mainForm.DEFAULT_DELAY_BOTTOM_PAD, 4).ToString();
                }
                else if (sender.Equals(delayPaddingUpButton))
                {
                    mainForm.DEFAULT_DELAY_BOTTOM_PAD += fOneMove;
                    delayPaddingLabel.Text = Math.Round(mainForm.DEFAULT_DELAY_BOTTOM_PAD, 4).ToString();
                }

                else if (sender.Equals(delayTimeDownButton))
                {
                    if (mainForm.DEFAULT_DELAY_TIME > 0)
                    {
                        mainForm.DEFAULT_DELAY_TIME--;
                        delayTimeLabel.Text = mainForm.DEFAULT_DELAY_TIME.ToString();
                    }
                }
                else if (sender.Equals(delayTimeUpButton))
                {
                    mainForm.DEFAULT_DELAY_TIME++;
                    delayTimeLabel.Text = mainForm.DEFAULT_DELAY_TIME.ToString();
                }
            }
            catch
            {

            }
        }
    }

}
