using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)//Доступ к кнопке
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }


        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)//Ограничение ввода (только цифры)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                if (Int32.Parse(textBox2.Text) > 10 || Int32.Parse(textBox2.Text) < 3)//Условие о количестве судей
                {
                    MessageBox.Show("Судей не может быть меньше 3 и больше 10");//Вывод ошибки при неправильном заполнении количества судей
                }
                else
                {
                    int part = Int32.Parse(textBox1.Text);//Кол-во участников
                    int judges = Int32.Parse(textBox2.Text);//Кол-во судей
                    double[,] Marks = new double[part, judges];//Массив оценок

                    int rows = part + 1;//Строки
                    int columns = judges + 2;//Столбцы
                    dataGridView1.RowCount = rows;//Указываем кол-во строк в таблице
                    dataGridView1.ColumnCount = columns;//Указываем кол-во столбцов в таблице
                    
                    dataGridView1.Rows[0].Cells[columns - 1].Value = "Итог";//Название последнего столбца
                    for (int i = 1; i < rows; i++)//Заполнение заголовков
                    {
                        dataGridView1.Rows[i].Cells[0].Value = $"Фигурист {i}";
                    }
                    for (int i = 1; i < columns - 1; i++)//Заполнение заголовков
                    {
                        dataGridView1.Rows[0].Cells[i].Value = $"Судья {i}";
                    }
                    Random rand = new Random();
                    for (int i = 0; i < part; i++)//Цикл заполнения массива рандомом
                    {
                        for (int j = 0; j < judges; j++)
                        {
                            Marks[i, j] = Convert.ToDouble(rand.Next(100) / 10.0 + 0.1);
                        }
                    }

                    for (int i = 1; i < rows; i++)//Заполнение таблицы элементами массива оценок
                    {
                        for (int j = 1; j < columns - 1; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = Marks[i - 1, j - 1];
                        }
                    }

                    double[] summ = new double[part];//Массив оценок
                    for (int i = 0; i < part; i++)
                    {
                        double sum = 0;//Сумма 
                        double max = 0;//Максимум
                        double min = 11;//Минимум
                        for (int j = 0; j < judges; j++)
                        {
                            sum += Marks[i, j];
                            if (max < Marks[i, j])//Находим максимум строкм
                            {
                                max = Marks[i, j];
                            }
                            if (min > Marks[i, j])//Находим минимум строки
                            {
                                min = Marks[i, j];
                            }

                        }
                        summ[i] = (sum - max - min) / (Convert.ToDouble(judges) - 2);//Нахождение итоговых оценок
                                                                                    
                    }

                    for (int i = 1; i < rows; i++)//Цикл вывода итоговых оценок
                    {
                        dataGridView1.Rows[i].Cells[columns - 1].Value = Math.Round(summ[i - 1], 1);
                    }

                    
                }
            
           
        }
    }
}
