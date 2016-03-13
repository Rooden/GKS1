using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private StartForm _startForm = new StartForm();

        private List<Attribute> _lstMatrixAttr = new List<Attribute>();
        private TextBox[] _txtList;
        private ComboBox[] _cboList;
        private Label[] _lblList;
        private double[,] _a1;
        private double[] _tempArray;

        private int _numOfCond;
        private int _numOfX;
        private int _step;
        private int _colPosition;
        private int _rawPosition;
        private int _numOfNewX;
        private int _deviation;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            _startForm.ShowDialog();

            if (_startForm.numOfCond == 0)
                return;
            _numOfCond = _startForm.numOfCond + 1; // +1 for F(x)
            _numOfX = _startForm.numOfX + 1; // +1 for sum in each line and F(x) value

            SetupTextBoxes();

            StartButton.Enabled = true;
        }

        private void SetupTextBoxes()
        {
            int count = 0;
            int cbo = 0;
            _txtList = new TextBox[_numOfCond * _numOfX];
            _lblList = new Label[_numOfCond * _numOfX];
            _cboList = new ComboBox[_numOfCond - 1];

            for (int i = 0; i < _numOfCond; i++)
            {
                int height = 30 + 30 * i;
                for (int j = 0; j < _numOfX; j++, count++)
                {
                    if (j != _numOfX - 1)
                    {
                        _txtList[count] = NewTextBox(j, 15 + 53 * j, height);

                        _lblList[count] = NewLabel(j, 48 + 53 * j, height);

                        Controls.Add(_lblList[count]);
                        Controls.Add(_txtList[count]);
                    }
                    else if (i == _numOfCond - 1)
                    {
                        _lblList[count] = new Label
                        {
                            Text = "C =",
                            Location = new Point(37 + 53 * j, height + 3),
                            AutoSize = true
                        };

                        _txtList[count] = NewTextBox(j, 60 + 53 * j, height);

                        Controls.Add(_lblList[count]);
                        Controls.Add(_txtList[count]);
                    }
                    else
                    {
                        _cboList[cbo] = NewComboBox(15 + 53 * j, height);

                        _txtList[count] = NewTextBox(j, 60 + 53 * j, height);

                        Controls.Add(_cboList[cbo++]);
                        Controls.Add(_txtList[count]);
                    }
                }
            }

            OutBox.AppendText("Step1 finish");
        }

        private static TextBox NewTextBox(int j, int width, int height)
        {
            var temp = new TextBox
            {
                Text = "0",
                Location = new Point(width, height),
                Size = new Size(30, 20)
            };

            return temp;
        }

        private static Label NewLabel(int j, int width, int height)
        {
            var temp = new Label
            {
                Text = "x" + (j + 1),
                Location = new Point(width, height + 5),
                AutoSize = true
            };

            return temp;
        }

        private static ComboBox NewComboBox(int width, int height)
        {
            var temp = new ComboBox
            {
                Location = new Point(width, height),
                Size = new Size(37, 20),
                Items = { ">=", "<=" },
                SelectedIndex = 0
            };

            return temp;
        }

        private void RemoveTextBoxes()
        {
            foreach (TextBox t in _txtList)
            {
                Controls.Remove(t);
                t.Dispose();
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            OutBox.Clear();

            _step = 0;

            FillMatrix();

            FillMatrixAttr();

            do
            {
                _step++;

                FindColPosition();

                _tempArray = new double[_numOfCond - 1];
                FillTempArray();

                FindRawPosition();

                CutFullLine();
                CutAllLines();
                CutResultLine();

                RebuildMatrixAttr();

                Print();
            } while (Repeat());

            Sort();
            Print();
        }

        private void FindNumberOfNewX()
        {
            _numOfNewX = 0;
            foreach (var comboBox in _cboList)
                if (comboBox.SelectedIndex == 0)
                {
                    _numOfNewX++;
                }
                else
                {
                    _deviation++;
                    _numOfNewX += 2;
                }
        }

        private void FillMatrix()
        {
            FindNumberOfNewX();

            _a1 = new double[_numOfCond, _numOfX + _numOfNewX + 1];
            _a1[_numOfCond - 1, 0] = 1;

            for (int i = 0, count = 0; i < _numOfCond; i++)
                for (int j = 1; j < _numOfX; j++, count++)
                {
                    var temp = _txtList[count].Text;

                    if (i == _numOfCond - 1)
                        _a1[i, j] = -Convert.ToDouble(temp);
                    else
                        _a1[i, j] = Convert.ToDouble(temp);

                    if (j == _numOfX - 1)
                    {
                        temp = _txtList[++count].Text;
                        _a1[i, _numOfX + _numOfNewX] = Convert.ToDouble(temp);
                    }
                }

            FillMatrixWithNewX();
            _numOfX += _numOfNewX + 1;
        }

        private void FillMatrixWithNewX()
        {
            for (int i = 0; i < _cboList.Length; i++)
                switch (_cboList[i].SelectedIndex)
                {
                    case 0:
                        int index = _numOfX + i;
                        _a1[i, index] = 1;
                        break;
                    case 1:
                        index = _numOfX + i;
                        _a1[i, index] = -1;
                        _a1[i, index + _deviation] = 1;
                        break;
                }
        }

        private void FillMatrixAttr()
        {
            for (int i = _numOfX - _numOfCond, count = 0; i < _numOfX; count++, i++)
            {
                _lstMatrixAttr.Add(new Attribute
                {
                    value = _a1[count, _numOfX - 1]
                });

                if (i == _numOfX - 1)
                    _lstMatrixAttr[count].name = "P";
                else
                    _lstMatrixAttr[count].name = "x" + i;
            }
        }

        private void FindColPosition()
        {
            double min = 0;
            _colPosition = 0;
            // Find min element in last line and set colPosition
            for (int i = 0; i < _numOfX; i++)
                if (_a1[_numOfCond - 1, i] < 0 && min > _a1[_numOfCond - 1, i])
                {
                    min = _a1[_numOfCond - 1, i];
                    _colPosition = i;
                }
        }

        private void FillTempArray()
        {
            // A1[Last col] / A1[colPosition]
            for (int i = 0; i < _tempArray.Length; i++)
                _tempArray[i] = _a1[i, _numOfX - 1] / _a1[i, _colPosition];
        }

        private void FindRawPosition()
        {
            double min = _tempArray[0];
            _rawPosition = 0;
            // Find min element and set rawPosition
            for (int i = 0; i < _tempArray.Length; i++)
                if (min > _tempArray[i])
                {
                    min = _tempArray[i];
                    _rawPosition = i;
                }
        }

        private void CutFullLine()
        {
            double num = _a1[_rawPosition, _colPosition];
            // A1[rawPosition] / A1[rawPosition, colPosition]
            for (int i = 0; i < _numOfX; i++)
                _a1[_rawPosition, i] = _a1[_rawPosition, i] / num;
        }

        private void CutAllLines()
        {
            // A1[raws] - A1[rawPosition] * A1[colPosition]
            for (int i = 0; i < _numOfCond - 1; i++)
                if (i != _rawPosition)
                {
                    double num = _a1[i, _colPosition];
                    for (int j = 0; j < _numOfX; j++)
                        _a1[i, j] = _a1[i, j] - _a1[_rawPosition, j] * Math.Abs(num);
                }
        }

        private void CutResultLine()
        {
            double num = _a1[_numOfCond - 1, _colPosition];
            // A1[Last raw] + A1[rawPosition] * A1[colPosition]
            for (int i = 0; i < _numOfX; i++)
                _a1[_numOfCond - 1, i] = _a1[_numOfCond - 1, i] + _a1[_rawPosition, i] * Math.Abs(num);
        }

        private void RebuildMatrixAttr()
        {
            // Change values and rawPosition.name beacause of new basic solution
            for (int i = 0; i < _lstMatrixAttr.Count; i++)
            {
                _lstMatrixAttr[i].value = _a1[i, _numOfX - 1];

                if (i == _rawPosition)
                    _lstMatrixAttr[i].name = "x" + i;
            }
        }

        private void Print()
        {
            // Print out
            OutBox.AppendText("Step #" + _step);
            for (int i = 0; i < _numOfCond; i++)
            {
                OutBox.AppendText(_lstMatrixAttr[i].name + " = " + _lstMatrixAttr[i].value + "\n");
            }
        }

        private void Sort()
        {
            // Sort by Name column
            _lstMatrixAttr.Sort((a, b) =>
            {
                if (a.name != "P" && b.name != "P")
                    return a.name.CompareTo(b.name);

                return 0;
            });
        }

        private bool Repeat()
        {
            // Repeat?
            double min = 0;
            for (int i = 0; i < _numOfX; i++)
                if (_a1[_numOfCond - 1, i] < 0 && min > _a1[_numOfCond - 1, i])
                    min = _a1[_numOfCond - 1, i];

            return min < 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _startForm.ShowDialog();

            if (_startForm.numOfCond == 0)
                return;
            _numOfCond = _startForm.numOfCond + 1; // +1 for F(x)
            _numOfX = _startForm.numOfX + 1; // +1 for sum in each line

            SetupTextBoxes();
        }
    }
}