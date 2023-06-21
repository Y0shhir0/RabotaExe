using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;


namespace RabotaExe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string selectedPath;
        private ObservableCollection<string> filesList;

        public event PropertyChangedEventHandler PropertyChanged;

        public string SelectedPath
        {
            get { return selectedPath; }
            set
            {
                selectedPath = value;
                OnPropertyChanged();
                RefreshFilesList();
            }
        }

        public ObservableCollection<string> FilesList
        {
            get { return filesList; }
            set
            {
                filesList = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            FilesList = new ObservableCollection<string>();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedPath = openFileDialog.FileName;
            }
        }

        private void RefreshFilesList()
        {
            if (!string.IsNullOrEmpty(SelectedPath))
            {
                var directory = System.IO.Path.GetDirectoryName(SelectedPath);
                var files = Directory.GetFiles(directory);
                FilesList.Clear();
                foreach (var file in files)
                {
                    FilesList.Add(file);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilesListBox.SelectedItem != null)
            {
                var selectedFile = FilesListBox.SelectedItem.ToString();
                if (MessageBox.Show($"Вы уверены, что хотите удалить {selectedFile}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    File.Delete(selectedFile);
                    RefreshFilesList();
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();

            Dictionary<string, string> knownPaths = new Dictionary<string, string>()
        {
            { "CheckBox1", @"C:\Users\NEW PC\Desktop\блокнот\1" },
            { "Путь 2", @"C:\Users\NEW PC\Desktop\путь 2" },
            { "Путь 3", @"C:\Users\NEW PC\Desktop\путь 3" },
            { "Путь 4", @"C:\Users\NEW PC\Desktop\путь 4" },
            { "Путь 5", @"C:\Users\NEW PC\Desktop\путь 5" },
            { "Путь 6", @"C:\Users\NEW PC\Desktop\путь 6" },
            { "Путь 7", @"C:\Users\NEW PC\Desktop\путь 7" },
            { "Путь 8", @"C:\Users\NEW PC\Desktop\путь 8" },
            { "Путь 9", @"C:\Users\NEW PC\Desktop\путь 9" },
            { "Путь 10", @"C:\Users\NEW PC\Desktop\путь 10" }
        };

            editWindow.PathDictionary = knownPaths;

            if (editWindow.ShowDialog() == true)
            {
                string selectedPath = SelectedPath;
                string targetPath = editWindow.SelectedPath;

                if (!string.IsNullOrEmpty(selectedPath) && !string.IsNullOrEmpty(targetPath))
                {
                    string fileName = System.IO.Path.GetFileName(selectedPath);
                    string destinationPath = System.IO.Path.Combine(targetPath, fileName);

                    try
                    {
                        File.Copy(selectedPath, destinationPath);
                        MessageBox.Show("Файл загружен в выбранный путь: " + destinationPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при копировании файла: " + ex.Message);
                    }
                }
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SelectedPath))
            {
                try
                {
                    Clipboard.SetText(SelectedPath);
                    MessageBox.Show("Файл скопирован.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при копировании файла в буфер обмена: " + ex.Message);
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshFilesList();
        }
    }
}

