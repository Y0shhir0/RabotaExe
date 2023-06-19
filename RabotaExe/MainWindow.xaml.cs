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
            string selectedFile = FilesListBox.SelectedItem as string;
            if (selectedFile != null)
            {
                string filePath = System.IO.Path.Combine(SelectedPath, selectedFile);
                string fileContent = File.ReadAllText(filePath, Encoding.UTF8);

                EditWindow editWindow = new EditWindow(filePath, fileContent);
                if (editWindow.ShowDialog() == true)
                {
                    // Получить измененное содержимое файла
                    string editedContent = editWindow.FileContent;

                    // Записать измененное содержимое файла с указанной кодировкой
                    File.WriteAllText(filePath, editedContent, Encoding.UTF8);

                    // Обновить список файлов
                    RefreshFilesList();
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshFilesList();
        }
    }
}

