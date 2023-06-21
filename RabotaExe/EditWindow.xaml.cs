using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RabotaExe
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Dictionary<string, string> pathDictionary;

        public Dictionary<string, string> PathDictionary
        {
            get { return pathDictionary; }
            set { pathDictionary = value; }
        }
        public string SelectedPath { get; private set; } // Добавлено определение свойства SelectedPath

        public EditWindow()
        {
            InitializeComponent();
            InitializePathDictionary();
        }

        private void InitializePathDictionary()
        {
            PathDictionary = new Dictionary<string, string>
        {
            { "CheckBox1", @"C:\Users\NEW PC\Desktop\блокнот\1" },
            { "Путь 2", @"C:\Users\NEW PC\Desktop\" },
            { "Путь 3", @"C:\Users\NEW PC\Desktop\" },
            // Добавьте нужное количество путей и присвойте им соответствующие значения
        };
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string checkBoxName = checkBox.Name;

            if (PathDictionary.ContainsKey(checkBoxName))
            {
                SelectedPath = PathDictionary[checkBoxName]; // Присваиваем SelectedPath значение выбранного пути
                                                             // Выполните необходимые действия с выбранным путем
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string checkBoxName = checkBox.Name;

            if (PathDictionary.ContainsKey(checkBoxName))
            {
                SelectedPath = null; // При снятии отметки снимаем значение SelectedPath
                                     // Выполните необходимые действия при снятии отметки с CheckBox
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}  
