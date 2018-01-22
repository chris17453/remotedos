using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.IO.Compression;

namespace dmLauncher{
    public class Program {
        private static readonly string appDirectory = System.IO.Directory.GetParent(System.IO.Path.GetDirectoryName(Application.ExecutablePath)).FullName;
        private static readonly bool shouldPromptForUpgrade = true;
        private static readonly bool disableAutomaticChecking = false;
        private static readonly string assemblyFilePath = appDirectory +"\\bin\\dmRDClient-PC.dll";
        private static readonly string launcherClassName = "dmRDClient_PC.Launcher";
        private static readonly Regex versionNumberRegex = new Regex(@"([0-9]+\.)*[0-9]+");
        private static readonly string latestVersionUrl = "http://www.remotedos.com/meta/version";
        private static readonly string latestVersionDownload = "http://www.remotedos.com/meta/build";
        private static readonly string latestVersionInfoFile = appDirectory + "\\config\\version.txt";
        

        [STAThread]
        public static void Main(string[] args) {
            Assembly binaries = GetAssembly();

            if(binaries == null) {
                MessageBox.Show(
                    "First-time download of binaries failed.\n\n" +
                    "This could either be because the server is down or your\n" +
                    "internet connection down.",
                    "FAIL");
            } else {
                iPlugin.iPlugin plugin= (iPlugin.iPlugin)binaries.CreateInstance("dmRDClient_PC.Launcher");
                if(plugin != null) {
                    if(args.Length == 0) {
                        MessageBox.Show("No application specified.","Error Loading");
                    }
                    try {
                        plugin.Launch(args);
                    } catch (Exception ex){
                    }
                } else {
                    MessageBox.Show("Failed to load important system files.");
                }
            }
        }

        private static string GetLocalVersionNumber() {
            if(File.Exists(latestVersionInfoFile) && File.Exists(assemblyFilePath)) {
                return File.ReadAllText(latestVersionInfoFile);
            }
            return null;
        }

        private static void SetLocalVersionNumber(string version) {
            File.WriteAllText(latestVersionInfoFile, version);
        }

        private static string GetLatestVersion() {
            Uri latestVersionUri = new Uri(latestVersionUrl);
            WebClient webClient = new WebClient();
            string receivedData = string.Empty;

            try {
                receivedData = webClient.DownloadString(latestVersionUrl).Trim();
            } catch(WebException) {
                // server or connection is having issues
            }

            // Just in case the server returned something other than a valid version number. 
            return versionNumberRegex.IsMatch(receivedData)
                ? receivedData
                : null;
        }

        private static Assembly GetLocalAssembly() {
            if(File.Exists(assemblyFilePath)) {
                try {
                    return Assembly.LoadFrom(assemblyFilePath);
                } catch(Exception) { }
            }
            return null;
        }

        private static Assembly GetAssembly() {
            bool localAssemblyExists = File.Exists(assemblyFilePath);

            if(disableAutomaticChecking && localAssemblyExists) {
                return GetLocalAssembly();
            }

            string latestVersion = GetLatestVersion();

            if(latestVersion == null) {
                return GetLocalAssembly();
            }

            string localVersion = GetLocalVersionNumber();

            if(ShallIDownloadTheLatestBinaries(localVersion, latestVersion, shouldPromptForUpgrade)) {
                bool success = DownloadLatestAssembly();
                if(success) {
                    SetLocalVersionNumber(latestVersion);
                }
            }

            return GetLocalAssembly();
        }

        private static bool ShallIDownloadTheLatestBinaries(string localVersion, string latestVersion, bool shouldAskFirst) {
            if(localVersion == latestVersion) {
                return false;
            }

            if(!shouldAskFirst) {
                return true;
            }

            return DialogResult.Yes == MessageBox.Show(
                "There is a newer version available. Would you like to download it?",
                "New Version",MessageBoxButtons.YesNo);
        }

        private static bool DownloadLatestAssembly() {
            WebClient downloader = new WebClient();
            try {
                
                byte[] fileList= downloader.DownloadData(latestVersionDownload);
                string fileString=Encoding.ASCII.GetString(fileList);
                string[] files = fileString.Split('\n');

                //download it all first...
                foreach(string token in files) {
                    if(token.Trim() == "") continue;
                    string[] tokens = token.Split(',');
                    if(tokens[2].Trim() == "") continue;
                    string file = tokens[0].Trim();
                    string path = tokens[1].Trim();
                    string webURL = tokens[2].Trim() + path.Replace('\\', '/') + "/" + file;
                    string temporaryPath = appDirectory + "\\temp\\" + file;

                    if(webURL != "" ) {
                        byte[] latestVersionBytes = downloader.DownloadData(webURL);
                        System.IO.File.WriteAllBytes(temporaryPath, latestVersionBytes);
                    }
                }
                //then move it!
                foreach(string token in files) {
                    if(token.Trim() == "") continue;
                    string[] tokens = token.Split(',');
                    string file = tokens[0].Trim();
                    string path = tokens[1].Trim();
                    string webURL = tokens[2].Trim() + path.Replace('\\', '/') + "/" + file;
                    string temporaryPath = appDirectory + "\\temp\\" + file;
                    string localPath = appDirectory + "\\" + path + "\\" + file;

                    if(File.Exists(localPath)) {
                        File.Delete(localPath);
                    }
                    if(File.Exists(temporaryPath)) {
                        File.Move(temporaryPath, localPath);
                    }
                }

            } catch(Exception) {
                MessageBox.Show("Update Failed", "Update");
                return false;
            }
            return true;
        }
    }
}