/*
   Copyright 2015 Shane Lillie

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using System.Windows;

namespace EnergonSoftware.BackpackPlanner.DriveFileExplorer
{
    public partial class MainWindow
    {
        public PlayServicesManager PlayServicesManager { get; }

        private readonly ObservableCollection<Google.Apis.Drive.v2.Data.File> _files = new ObservableCollection<Google.Apis.Drive.v2.Data.File>();

        public MainWindow()
        {
            InitializeComponent();

            PlayServicesManager = new PlayServicesManager();
            DataContext = PlayServicesManager;

            FileList.ItemsSource = _files;
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void MenuFileConnect_Click(object sender, RoutedEventArgs e)
        {
            await PlayServicesManager.ConnectAsync();
            if(!PlayServicesManager.IsConnected) {
                return;
            }

            var fileList = await PlayServicesManager.ListDriveAppFolderFilesAsync();
System.Console.WriteLine($"{fileList.Count} files");

            _files.Clear();
            foreach(Google.Apis.Drive.v2.Data.File file in fileList) {
System.Console.WriteLine($"file: {file.Title}");
                _files.Add(file);
            }
        }

        private async void MenuFileDisconnect_Click(object sender, RoutedEventArgs e)
        {
            await PlayServicesManager.DisconnectAsync();
            _files.Clear();
        }

        private async void FileList_Drop(object sender, DragEventArgs e)
        {
            if(!e.Data.GetDataPresent(DataFormats.FileDrop)) {
                return;
            }

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach(string file in files) {
                Google.Apis.Drive.v2.Data.File driveFile = await PlayServicesManager.SaveFileToDriveAppFolderAsync(file, MimeMapping.GetMimeMapping(file));
                if(null != driveFile) {
System.Console.WriteLine($"new file: {driveFile.Title}");
                    _files.Add(driveFile);
                }
            }
        }
    }
}
