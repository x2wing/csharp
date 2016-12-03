using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        // депозит покупателя и по совместительству сдача
        int MoneySum=0;
        //цисло заказаных банок
        int CountCocaCola = 0;
        int CountPepsi = 0;
        int CountSprite = 0;
        int CountFanta = 0;

        // цена за банку
        int CostCocaCola = 50;
        int CostPepsi = 45;
        int CostSprite = 48;
        int CostFanta = 42;

        //время в секндах
        int Time=0;

        private string TextBoxSum(string TextBoxText, int value)
        {
            return (int.Parse(TextBoxText) + value).ToString();
        }

        private string Buy(string NapitokName, string textBoxText, ref int Cost, ref int Count)
        {
            int Residual = int.Parse(textBoxText);

            if (MoneySum >= Cost)
            {
                if (Residual > 0)
                {
                    Count += 1;
                    MoneySum -= Cost;
                    textBoxText = (Residual - 1).ToString();
                    Output("Вы купили банок " + NapitokName+ " ", Count);
                    Output("Заберите напиток");
                    Output("Сдача: ", MoneySum);
                    Output("Заберите сдачу");
                }
                else
                    Output(NapitokName + " закончилась. Ваша сдача: ", MoneySum);
            }
            else
                Output("Мало денег. Ваша сдача: ", MoneySum);
            return textBoxText;
        }

        private void Output(string text, int value)
        {
            richTextBoxTerm.Text += text + value.ToString() + "\n";
            richTextBoxTerm.SelectionStart = richTextBoxTerm.Text.Length;
            richTextBoxTerm.ScrollToCaret();
        }

        private void Output(string text)
        {
            richTextBoxTerm.Text += text + "\n";
            richTextBoxTerm.SelectionStart = richTextBoxTerm.Text.Length;
            richTextBoxTerm.ScrollToCaret();
        }

        private void LockAutomate()
        {
            button1.Enabled = false;
            button10r.Enabled = false;
            button100r.Enabled = false;
            button50r.Enabled = false;
            button500r.Enabled = false;
            button1r.Enabled = false;
            button5r.Enabled = false;
            button2r.Enabled = false;
            button10rm.Enabled = false;
            buttonBuyCocacola.Enabled = false;
            buttonBuyPepsi.Enabled = false;
            buttonBuySprite.Enabled = false;
            buttonBuyFanta.Enabled = false;
            buttonTakeChange.Enabled = false;
            buttonTakeDrink.Enabled = false;
            buttonCancel.Enabled = false;
            button2.Enabled = false;
            btnBrokenCoin.Enabled = false;
        }

        private void UnlockAutomate()
        {
            button1.Enabled = true;
            button10r.Enabled = true;
            button100r.Enabled = true;
            button50r.Enabled = true;
            button500r.Enabled = true;
            button1r.Enabled = true;
            button5r.Enabled = true;
            button2r.Enabled = true;
            button10rm.Enabled = true;
            buttonBuyCocacola.Enabled = true;
            buttonBuyPepsi.Enabled = true;
            buttonBuySprite.Enabled = true;
            buttonBuyFanta.Enabled = true;
            buttonTakeChange.Enabled = true;
            buttonTakeDrink.Enabled = true;
            buttonCancel.Enabled = true;
            button2.Enabled = true;
            btnBrokenCoin.Enabled = true;
        }

        public Form1()
        {
            InitializeComponent();
            //отображение цены на автомате
            labelCostCocacola.Text= CostCocaCola.ToString()+" руб.";
            labelCostPepsi.Text = CostPepsi.ToString() + " руб.";
            labelCostSprite.Text = CostSprite.ToString() + " руб.";
            labelCostFanta.Text = CostFanta.ToString() + " руб.";

            progressBarTempirature.Value = 23;
            labelTempirature.Text = 23.ToString();
            Output("Вставьте деньги, и выберите товар");
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            UnlockAutomate();
            Output("Работа автомата восстановлена");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxCacacola.Text = "10";
            textBoxPepsi.Text= "10";
            textBoxSprite.Text= "10";
            textBoxFanta.Text = "10";
            richTextBoxTerm.Text += "Запасы пополнены\n";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button10r_Click(object sender, EventArgs e)
        {
            MoneySum += 10;
            //отладка
            Output("полученная сумма:", MoneySum);


        }

        private void button100r_Click(object sender, EventArgs e)
        {
            MoneySum += 100;
            //отладка
            Output("полученная сумма:", MoneySum);
        }

        private void button50r_Click(object sender, EventArgs e)
        {
            MoneySum += 50;
            //отладка
            Output("полученная сумма:", MoneySum);
        }

        private void button500r_Click(object sender, EventArgs e)
        {
            MoneySum += 500;
            //отладка
            Output("полученная сумма:", MoneySum);
        }

        private void button1r_Click(object sender, EventArgs e)
        {
            MoneySum += 1;
            //отладка
            Output("полученная сумма:", MoneySum);
        }

        private void button5r_Click(object sender, EventArgs e)
        {
            MoneySum += 5;
            //отладка
            Output("полученная сумма:", MoneySum);
        }

        private void button2r_Click(object sender, EventArgs e)
        {
            MoneySum += 2;
            //отладка
            Output("полученная сумма:", MoneySum);
        }

        private void button10rm_Click(object sender, EventArgs e)
        {
            MoneySum += 10;
            //отладка
            Output("полученная сумма:", MoneySum);
        }

        private void buttonBuyCocacola_Click(object sender, EventArgs e)
        {
            textBoxCacacola.Text = Buy("Coca cola", textBoxCacacola.Text,ref CostCocaCola, ref CountCocaCola);
        }

        private void buttonBuyPepsi_Click(object sender, EventArgs e)
        {
            textBoxPepsi.Text = Buy("Pepsi", textBoxPepsi.Text, ref CostPepsi, ref CountPepsi);
        }

        private void buttonBuySprite_Click(object sender, EventArgs e)
        {
            textBoxSprite.Text = Buy("Sprite", textBoxSprite.Text, ref CostSprite, ref CountSprite);
        }

        private void buttonBuyFanta_Click(object sender, EventArgs e)
        {
            textBoxFanta.Text = Buy("Fanta", textBoxFanta.Text, ref CostFanta, ref CountFanta);
        }

        private void buttonTakeChange_Click(object sender, EventArgs e)
        {
            if (MoneySum > 0)
            {
                Output("Вы забрали сдачу в размере ", MoneySum);
                MoneySum = 0;
            }
            else
                Output("Забирать нечего ваш баланс:",  0);
        }

        private void buttonTakeDrink_Click(object sender, EventArgs e)
        {
            if ((CountCocaCola == 0) & (CountPepsi == 0) & (CountSprite == 0) & (CountFanta == 0))
                Output("Вы ничего не купили ", 0);
            if (CountCocaCola > 0)
            {
                Output("Вы забрали банок CocaCola: ", CountCocaCola);
                CountCocaCola = 0;
            }
            if (CountPepsi > 0)
            {
                Output("Вы забрали банок Pepsi: ", CountPepsi);
                CountPepsi = 0;
            }
            if (CountSprite > 0)
            {
                Output("Вы забрали банок Sprite: ", CountSprite);
                CountSprite = 0;
            }
            if (CountFanta > 0)
            {
                Output("Вы забрали банок Fanta: ", CountFanta);
                CountFanta = 0;
            }
            

        
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            MoneySum += CountCocaCola * CostCocaCola + CountPepsi * CostPepsi + CountSprite * CostSprite + CountFanta * CostFanta;
            textBoxCacacola.Text = TextBoxSum(textBoxCacacola.Text, CountCocaCola);
            CountCocaCola = 0;
            textBoxPepsi.Text = TextBoxSum(textBoxPepsi.Text, CountPepsi);
            CountPepsi = 0;
            textBoxSprite.Text = TextBoxSum(textBoxSprite.Text, CountSprite);
            CountSprite = 0;
            textBoxFanta.Text = TextBoxSum(textBoxFanta.Text, CountFanta);
            CountFanta = 0;
            Output("Ваш заказ отменен. Ваша сдача: ", MoneySum);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random Tempirature = new Random();
            int Temper=Tempirature.Next(0, 35);
            progressBarTempirature.Value = Temper;
            labelTempirature.Text = Temper.ToString();

            if (progressBarTempirature.Value > 30)
            {
                Output("Внимание! перегрев! вызовите техника. Работа автомата временно прекращена");
                LockAutomate();
            }

        }

        

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void watch_Tick(object sender, EventArgs e)
        {
            labelTime.Text = Time++.ToString() + " секунд";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Output(@"Купюра / монета не считывается.Замените на другую или попробуйте снова");
        }

        private void btnBrokenCoin_Click(object sender, EventArgs e)
        {
            Output(@"Купюра / монета не считывается.Замените на другую или попробуйте снова");
        }
    }
}
