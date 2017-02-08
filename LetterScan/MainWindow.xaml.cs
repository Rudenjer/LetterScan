using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace LetterScan
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string,TextBlock> LetterBlocks;
        private Dictionary<string, TextBlock> NumberBlocks;
        private Dictionary<string, Rectangle> Rectangles;
        private string[] LatAlphabet;
        private int[] CurLetters;

        public MainWindow()
        {
            InitializeComponent();

            LatAlphabet = new string[]
            {
                "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п",
                "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ь", "ъ", "ы", "э", "ю", "я"
            };

            //LatAlphabet = new string[]
            //{
            //    "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
            //    "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
            //};

            CurLetters = new int[LatAlphabet.Length];

            LetterBlocks= new Dictionary<string, TextBlock>();
            NumberBlocks = new Dictionary<string, TextBlock>();
            Rectangles = new Dictionary<string, Rectangle>();

            int ind = 0;
            foreach (UIElement var in LetterGrid.Children)
            {
                if (var is TextBlock && ind<LatAlphabet.Length)
                {
                    TextBlock h = (TextBlock)var;
                    if (h.Name.Contains("Letter"))
                    {
                        LetterBlocks.Add(LatAlphabet[ind], h);
                        h.Text = LatAlphabet[ind];
                    }
                    else
                    {
                        NumberBlocks.Add(LatAlphabet[ind++], h);
                        h.Text = 0.ToString();
                    }
                }
            }

            ind = 0;
            foreach (UIElement var in LetterGrid.Children)
            {
                if (var is Rectangle && ind < LatAlphabet.Length)
                {
                    Rectangle r = (Rectangle)var;
                    Rectangles.Add(LatAlphabet[ind++], r);
                }
            }
            
            ButtonAnalisys.Click += Button_Click;
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (LatAlphabet!=null)
            {
                CurLetters = new int[LatAlphabet.Length];

                UpdateLetterArray();

                UpdateTextBoxes();

                UpdateRectangles();
            }
        }

        private void UpdateLetterArray()
        {
            foreach (char letter in TB.Text)
            {
                if (LatAlphabet.Contains(Char.ToLower(letter).ToString()))
                {
                    string curLetter = Char.ToLower(letter).ToString();

                    CurLetters[Array.IndexOf(LatAlphabet, curLetter)] =
                        CurLetters[Array.IndexOf(LatAlphabet, curLetter)] + 1;
                }
            }
        }

        private void UpdateRectangles()
        {
            int ind;
            ind = 0;

            foreach (Rectangle rec in Rectangles.Values)
            {
                rec.Width = CurLetters[ind++]*3;
            }
        }

        private void UpdateTextBoxes()
        {
            int ind = 0;

            foreach (TextBlock tb in NumberBlocks.Values)
            {
                tb.Text = CurLetters[ind++].ToString();
            }
        }

        private void AnalisysTextEnter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            char[] inputString = TB1.Text.ToCharArray();
            string outputString = "";
            int shift = Convert.ToInt32(TBNumber.Text);
            int ind = 0;
            foreach (char letter in inputString)
            {
                if (LatAlphabet.Contains(letter.ToString()))
                {
                    int letterNum = Array.IndexOf(LatAlphabet, inputString[ind].ToString());
                    letterNum = (letterNum + shift)%(LatAlphabet.Length);

                    outputString += LatAlphabet[letterNum];
                }
                else
                {
                    outputString += letter;

                }
                ind++;
            }

            TB2.Text = outputString;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LetterGrid.Visibility = System.Windows.Visibility.Hidden;
            Analisys.Visibility= System.Windows.Visibility.Visible;
        }
    }
}
