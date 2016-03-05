using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        List<Attribute> matrixAttr = new List<Attribute>();
        TextBox[] textBox;
        double[,] A1;
        double[] tempArray;

        int x = 0;
        int y = 0;
        int step = 0;
        int colPosition = 0;
        int rawPosition = 0;

        public Form1()
        {
            /*for (int i = 0; i < 3; i++)
            {
                Controls.Add(new TextBox()
                {
                    Name = "TextBox" + i,
                    Location = new System.Drawing.Point(40 + 150 * i, 70),
                    Size = new System.Drawing.Size(100, 30)
                });
            }*/
            //StartButton.Enabled = false;
            InitializeComponent();
        }

        private void setupTextBoxes()
        {
            int count = 0;
            textBox = new TextBox[x * y];
            for (int i = 0; i < x; i++)
            {
                int height = 30 + 100 * i;
                for (int j = 0; j < y; j++, count++)
                {
                    textBox[count] = new TextBox();
                    textBox[count].Name = "TextBox" + count;
                    textBox[count].Location = new System.Drawing.Point(40 + 150 * j, height);
                    textBox[count].Size = new System.Drawing.Size(100, 30);
                    Controls.Add(textBox[count]);
                }
            }
        }

        private void removeTextBoxes()
        {
            foreach (TextBox t in textBox)
            {
                Controls.Remove(t);
                t.Dispose();
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            OutBox.Clear();

            step = 0;

            x = int.Parse(Xbox.Text);
            y = int.Parse(Ybox.Text);

            A1 = new double[x, y];
            fillMatrix();

            fillMatrixAttr();

            do
            {
                step++;

                findColPosition();
                
                tempArray = new double[x - 1];
                fillTempArray();

                findRawPosition();

                cutFullLine();
                cutAllLines();
                cutResultLine();

                rebuildMatrixAttr();

                print();
            } while (repeat());

            sort();
            print();
        }

        private void fillMatrix()
        {
            for (int i = 0, index = 0; i < x; i++)
                for (int j = 0; j < y; j++, index++)
                    A1[i, j] = parseElement(index);
        }

        private double parseElement(int index)
        {
            return double.Parse(parseLine()[index]);
        }

        private List<string> parseLine()
        {
            return EnterBox.Text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private void fillMatrixAttr()
        {
            for (int i = y - x, count = 0; i < y; count++, i++)
            {
                matrixAttr.Add(new Attribute()
                {
                    value = A1[count, y - 1]
                });

                if (i == y - 1)
                    matrixAttr[count].name = "P";
                else
                    matrixAttr[count].name = "x" + i;
            }
        }

        private void findColPosition()
        {
            double min = 0;
            colPosition = 0;
            // Find min element in last line and set colPosition
            for (int i = 0; i < y; i++)
                if (A1[x - 1, i] < 0 && min > A1[x - 1, i])
                {
                    min = A1[x - 1, i];
                    colPosition = i;
                }
        }

        private void fillTempArray()
        {
            // A1[Last col] / A1[colPosition]
            for (int i = 0; i < tempArray.Length; i++)
                tempArray[i] = A1[i, y - 1] / A1[i, colPosition];
        }

        private void findRawPosition()
        {
            double min = tempArray[0];
            rawPosition = 0;
            // Find min element and set rawPosition
            for (int i = 0; i < tempArray.Length; i++)
                if (min > tempArray[i])
                {
                    min = tempArray[i];
                    rawPosition = i;
                }
        }

        private void cutFullLine()
        {
            double num = A1[rawPosition, colPosition];
            // A1[rawPosition] / A1[rawPosition, colPosition]
            for (int i = 0; i < y; i++)
                A1[rawPosition, i] = A1[rawPosition, i] / num;
        }

        private void cutAllLines()
        {
            // A1[raws] - A1[rawPosition] * A1[colPosition]
            for (int i = 0; i < x - 1; i++)
                if (i != rawPosition)
                {
                    double num = A1[i, colPosition];
                    for (int j = 0; j < y; j++)
                        A1[i, j] = A1[i, j] - A1[rawPosition, j] * Math.Abs(num);
                }
        }

        private void cutResultLine()
        {
            double num = A1[x - 1, colPosition];
            // A1[Last raw] + A1[rawPosition] * A1[colPosition]
            for (int i = 0; i < y; i++)
                A1[x - 1, i] = A1[x - 1, i] + A1[rawPosition, i] * Math.Abs(num);
        }

        private void rebuildMatrixAttr()
        {
            // Change values and rawPosition.name beacause of new basic solution
            for (int i = 0; i < matrixAttr.Count; i++)
            {
                matrixAttr[i].value = A1[i, y - 1];

                if (i == rawPosition)
                    matrixAttr[i].name = "x" + i;
            }
        }

        private void print()
        {
            // Print out
            OutBox.AppendText("Step #" + step);
            for (int i = 0; i < x; i++)
            {
                OutBox.AppendText(matrixAttr[i].name + " = " + matrixAttr[i].value + "\n");
            }
        }

        private void sort()
        {
            // Sort by Name column
            matrixAttr.Sort((a, b) => {
                if (a.name != "P" && b.name != "P")
                    return a.name.CompareTo(b.name);

                return 0;
            });
        }

        private bool repeat()
        {
            // Repeat?
            double min = 0;
            for (int i = 0; i < y; i++)
                if (A1[x - 1, i] < 0 && min > A1[x - 1, i])
                    min = A1[x - 1, i];

            return bool.Parse(min.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //removeTextBoxes();

            x = int.Parse(Xbox.Text);
            y = int.Parse(Ybox.Text);

            setupTextBoxes();
            StartButton.Enabled = true;
        }
    }
}