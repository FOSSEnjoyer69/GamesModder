using System.IO.Compression;
using CSharpAlgorithms.Computer;

namespace Mods.The_Walking_Dead_DE;
public static class TWDDEModManager
{
    public static bool IsLoadAnyLevelInstalled(string installPath) => IsModInstalled(installPath, "Mods/The Walking Dead DE/Load Any Level 3.2.1-7-3-2-1-1633464184.zip");
    public static bool InstallLoadAnyLevel(string installPath, bool install) => InstallMod(installPath, "Mods/The Walking Dead DE/Load Any Level 3.2.1-7-3-2-1-1633464184.zip", install);

    public static bool IsGraphicBlackDisablerInstalled(string installPath) => IsModInstalled(installPath, "Mods/The Walking Dead DE/GraphicBlackDisabler.zip");
    public static bool InstallGraphicBlackDisabler(string installPath, bool install) => InstallMod(installPath, "Mods/The Walking Dead DE/GraphicBlackDisabler.zip", install);

    public static bool IsNoBlackLinesInstalled(string installPath) => IsModInstalled(installPath, "Mods/The Walking Dead DE/NoBlackLine.zip");
    public static bool InstallNoBlackLines(string installPath, bool install) => InstallMod(installPath, "Mods/The Walking Dead DE/NoBlackLine.zip", install);

    public static bool IsModInstalled(string gameInstallPath, string modZipPath)
    {
        string twddeArchivePath = $"{gameInstallPath}/Archives";
        return DirectoryUtils.ContainsTemplate(ZipFile.OpenRead(modZipPath), twddeArchivePath);
    }
    public static bool InstallMod(string gameInstallPath, string modZipPath, bool install)
    {
        string twddeArchivePath = $"{gameInstallPath}/Archives";

        try
        {
            switch (install)
            {
                case true:
                    ZipUtils.UnzipAndCopyContent(modZipPath, twddeArchivePath);
                    break;
                case false:
                    DirectoryUtils.DeleteUsingTemplate(ZipFile.OpenRead(modZipPath), twddeArchivePath);
                    break;
            }
            return true;
        }
        catch (System.Exception)
        {
            return false;
            throw;
        }
    }
}