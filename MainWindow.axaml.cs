using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using CSharpAlgorithms.Biology.Animalia.Chordata.Mammalia.Primates.Hominidae.Homo.HomoSapiens;
using CSharpAlgorithms.Computer;
using CSharpAlgorithms.Math;
using CSharpAlgorithms.Media.Images;
using Mods.The_Walking_Dead_DE;
using SixLabors.ImageSharp.PixelFormats;
using TelltaleTextureTool;
using TelltaleTextureTool.Graphics;
using CAImage = CSharpAlgorithms.Media.Images.Image;
using SixLaborsImage = SixLabors.ImageSharp.Image;

namespace GamesModder;

public partial class MainWindow : Window
{
        private Dictionary<string, string> gameInstallLocationsDict = [];

        public MainWindow()
        {
                InitializeComponent();

                gameInstallLocationsDict = SaveUtils.LoadDict<string, string>("Game Installation Cach.txt");

                if (gameInstallLocationsDict.TryGetValue("TWD DE", out string? value))
                        TWDDEInstallPath.Text = value;

                TWDDEInstallPath.TextChanged += (s, e) =>
                {
                        if (string.IsNullOrEmpty(TWDDEInstallPath.Text))
                                return;

                        string path = TWDDEInstallPath.Text;

                        if (!Directory.Exists(path))
                                return;

                        gameInstallLocationsDict["TWD DE"] = path;

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

                Closing += (s, e) =>
                {
                        SaveUtils.SaveDict("Game Installation Cach.txt", gameInstallLocationsDict);
                };
        }

        private void ClementineFSEyeColourChanged(object? sender, SelectionChangedEventArgs e)
        {
                ComboBox dropdown = (ComboBox)sender!;
                int index = dropdown.SelectedIndex;
                ComboBoxItem item = (ComboBoxItem)dropdown.Items[index]!;
                string label = (string)item.Content!;

                Console.WriteLine($"Setting clementine FS eye colour to {label}");

                CAImage image = CAImage.LoadDDS("Assets/Game Assets/Telltale/The Walking Dead/Defintive Edition/The Final Season/sk62_clementine400_eyes.dds");
                CAImage mask = SixLaborsImage.Load<Rgba32>("Assets/Game Assets/Telltale/The Walking Dead/Defintive Edition/The Final Season/sk62_clementine400_eyes - Iris Mask.png");

                ImageEditor imageEditor = new(image);
                HSLColour eyeColour = HomoSapiensEyes.ColourDictionary[label];
                imageEditor.Colourize(eyeColour, opacity: 1f, mask: mask, lightnessShift: 0f);

                imageEditor.Image.SaveDDS("temp/sk62_clementine400_eyes.dds");

                if (TWDDEInstallPath?.Text is not null)
                        ImageConverter.DDS_To_D3DTX("temp/sk62_clementine400_eyes.dds", "Assets/Game Assets/Telltale/The Walking Dead/Defintive Edition/The Final Season/sk62_clementine400_eyes.json", $"{TWDDEInstallPath?.Text}/Archives/sk62_clementine400_eyes.d3dtx");

                if (File.Exists("temp/sk62_clementine400_eyes.dds"))
                        File.Delete("temp/sk62_clementine400_eyes.dds");
        }
}