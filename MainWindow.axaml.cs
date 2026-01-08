using Avalonia.Controls;
using CSharpAlgorithms.Biology.Animalia.Chordata.Mammalia.Primates.Hominidae.Homo.HomoSapiens;
using CSharpAlgorithms.Math;
using CSharpAlgorithms.Media.Images;
using Mods.The_Walking_Dead_DE;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Image = SixLabors.ImageSharp.Image;

namespace GamesModder;
public partial class MainWindow : Window
{
        public MainWindow()
        {
                InitializeComponent();

                Image<Rgba32> image = Image.Load<Rgba32>("Assets/Game Assets/Telltale/The Walking Dead/Defintive Edition/The Final Season/sk62_clementine400_eyes.png");
                Image<Rgba32> mask = Image.Load<Rgba32>("Assets/Game Assets/Telltale/The Walking Dead/Defintive Edition/The Final Season/sk62_clementine400_eyes - Iris Mask.png");
                ImageEditor imageEditor = new(image);

                //imageEditor.SetSaturation(0, mask);
                HSLColour eyeColour = HomoSapiensEyes.ColourDictionary["Grey"];
                //Console.WriteLine(eyeColour);
                //Rgba32 colourized = ColourConverter.HSV_To_RGBA32(eyeColour, 255);
                //Console.WriteLine(colourized);
                imageEditor.Colourize(eyeColour, opacity: 1f, mask: mask, lightnessShift: 0f);

                image.SaveAsPng("result 1.png");


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
}