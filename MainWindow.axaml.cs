using System;
using System.IO;
using System.IO.Compression;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpAlgorithms.Computer;
using CSharpAlgorithms.Media.Images;

namespace GamesModder;

public partial class MainWindow : Window
{
        public MainWindow()
        {
                InitializeComponent();

                TWDDEInstallPath.TextChanged += (s, e) =>
                {
                        LoadAnyLevelBtn.IsChecked = DirectoryUtils.ContainsTemplate(ZipFile.OpenRead("Mods/The Walking Dead DE/Load Any Level 3.2.1-7-3-2-1-1633464184.zip"), $"{TWDDEInstallPath.Text}/Archives");
                };

                LoadAnyLevelBtn.IsCheckedChanged += (s, e) =>
                {
                        string twddeArchivePath = $"{TWDDEInstallPath.Text}/Archives";

                        bool enable = LoadAnyLevelBtn.IsChecked ?? false;
                        Console.WriteLine(enable);
                        switch (enable)
                        {
                                case true:
                                        ZipUtils.UnzipAndCopyFiles("Mods/The Walking Dead DE/Load Any Level 3.2.1-7-3-2-1-1633464184.zip", twddeArchivePath);
                                        break;
                                case false:
                                        DirectoryUtils.DeleteUsingTemplate(ZipFile.OpenRead("Mods/The Walking Dead DE/Load Any Level 3.2.1-7-3-2-1-1633464184.zip"), twddeArchivePath);
                                        break;
                        }
                };
        }

        private async void OpenLoadFile(object? sender, RoutedEventArgs e)
        {
                var dialog = new OpenFileDialog
                {
                        Title = "Select A File",
                        AllowMultiple = false,
                        Filters =
                        {
                                new FileDialogFilter { Name = ".d3dtx Files", Extensions = { "d3dtx" } },
                                new FileDialogFilter { Name = "All Files", Extensions = { "*" } }
                        }
                };

                string[] resultFilePaths = await dialog.ShowAsync(this);
                if (resultFilePaths is null || resultFilePaths.Length == 0)
                        return;

                string filePath = resultFilePaths[0];
                string fileNameWithExtension = Path.GetFileName(filePath);

                ImageConverter.D3DTX_To_DDS(filePath, $"Output/{fileNameWithExtension}");
        }
}