using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpAlgorithms.Media.Images;
using Mods.The_Walking_Dead_DE;

namespace GamesModder;
public partial class MainWindow : Window
{
        public MainWindow()
        {
                InitializeComponent();

                TWDDEInstallPath.TextChanged += (s, e) =>
                {
                        if (string.IsNullOrEmpty(TWDDEInstallPath.Text))
                                return;

                        string path = TWDDEInstallPath.Text;

                        TWDDELoadAnyLevelToggle.IsChecked = TWDDEModManager.IsLoadAnyLevelInstalled(path);
                        TWDDEGraphicBlack.IsChecked = !TWDDEModManager.IsGraphicBlackDisablerInstalled(path);
                        TWDDEBlackLines.IsChecked = !TWDDEModManager.IsNoBlackLinesInstalled(path);
                };

                TWDDELoadAnyLevelToggle.IsCheckedChanged += (s, e) =>
                {
                        string? twddeArchivePath = TWDDEInstallPath.Text;
                        if (twddeArchivePath is null)
                                return;

                        bool enable = TWDDELoadAnyLevelToggle.IsChecked ?? false;
                        TWDDEModManager.InstallLoadAnyLevel(twddeArchivePath, enable);
                };

                TWDDEGraphicBlack.IsCheckedChanged += (s, e) =>
                {
                        string? twddeArchivePath = TWDDEInstallPath.Text;
                        if (twddeArchivePath is null)
                                return;

                        bool enable = !(TWDDEGraphicBlack.IsChecked ?? false);
                        TWDDEModManager.InstallGraphicBlackDisabler(twddeArchivePath, enable);
                };

                TWDDEBlackLines.IsCheckedChanged += (s, e) =>
                {
                        string? twddeArchivePath = TWDDEInstallPath.Text;
                        if (twddeArchivePath is null)
                                return;

                        bool enable = !(TWDDEBlackLines.IsChecked ?? false);
                        TWDDEModManager.InstallNoBlackLines(twddeArchivePath, enable);
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