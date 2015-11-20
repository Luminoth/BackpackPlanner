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

using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows;
using System.Windows.Input;

using log4net;

namespace EnergonSoftware.BackpackPlanner.DriveFileExplorer
{
    public partial class MainWindow
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));

        public PlayServicesManager PlayServicesManager { get; }

        private readonly ObservableCollection<Google.Apis.Drive.v2.Data.File> _files = new ObservableCollection<Google.Apis.Drive.v2.Data.File>();

        public MainWindow()
        {
            InitializeComponent();

            PlayServicesManager = new PlayServicesManager();
            DataContext = PlayServicesManager;

            FileList.ItemsSource = _files;
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await PlayServicesManager.DisconnectAsync();
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void MenuFileConnect_Click(object sender, RoutedEventArgs e)
        {
            await PlayServicesManager.ConnectAsync();
            if(!PlayServicesManager.IsConnected) {
                MessageBox.Show("There was an error connecting to Google Services!", "Connection Error", MessageBoxButton.OK);
                return;
            }

            var fileList = await PlayServicesManager.ListDriveAppFolderFilesAsync();
            Logger.Debug($"Retrieved {fileList.Count} files");

            _files.Clear();
            foreach(Google.Apis.Drive.v2.Data.File file in fileList) {
                _files.Add(file);
            }
        }

        private async void MenuFileDisconnect_Click(object sender, RoutedEventArgs e)
        {
            await PlayServicesManager.DisconnectAsync();
            _files.Clear();
        }

        private async void MenuViewRefresh_Click(object sender, RoutedEventArgs e)
        {
            if(!PlayServicesManager.IsConnected) {
                return;
            }

            var fileList = await PlayServicesManager.ListDriveAppFolderFilesAsync();
            Logger.Debug($"Retrieved {fileList.Count} files");

            _files.Clear();
            foreach(Google.Apis.Drive.v2.Data.File file in fileList) {
                _files.Add(file);
            }
        }

        private async void FileList_Drop(object sender, DragEventArgs e)
        {
            if(!e.Data.GetDataPresent(DataFormats.FileDrop)) {
                return;
            }

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach(string filePath in files) {
                string fileName = Path.GetFileName(filePath);
                Google.Apis.Drive.v2.Data.File existingDriveFile = _files.FirstOrDefault(x => x.Title == fileName);
                using(Stream stream = new FileStream(filePath, FileMode.Open)) {
                    Google.Apis.Drive.v2.Data.File newDriveFile;
                    if(null != existingDriveFile) {
                        MessageBoxResult result = MessageBox.Show($"The file {fileName} already exists, do you wish to replace it?", "Replace File", MessageBoxButton.YesNo);
                        if(result == MessageBoxResult.No) {
                            return;
                        }

                        newDriveFile = await PlayServicesManager.UpdateFileInDriveAppFolderAsync(existingDriveFile, Path.GetFileName(filePath), MimeMapping.GetMimeMapping(filePath), stream);
                        if(null == newDriveFile) {
                            MessageBox.Show($"There was an error saving the file {filePath}!", "Save Error", MessageBoxButton.OK);
                            continue;
                        }

                        Logger.Debug($"Updated file: {newDriveFile.Title} ({newDriveFile.Id})");
                        _files.Remove(existingDriveFile);
                        _files.Add(newDriveFile);
                    } else {
                        newDriveFile = await PlayServicesManager.SaveFileToDriveAppFolderAsync(Path.GetFileName(filePath), MimeMapping.GetMimeMapping(filePath), stream);
                        if(null == newDriveFile) {
                            MessageBox.Show($"There was an error saving the file {filePath}!", "Save Error", MessageBoxButton.OK);
                            continue;
                        }

                        Logger.Debug($"Added new file: {newDriveFile.Title} ({newDriveFile.Id})");
                        _files.Add(newDriveFile);
                    }
                }
            }
        }

        private async void FileListItem_Download(object sender, RoutedEventArgs e)
        {
            Google.Apis.Drive.v2.Data.File selectedFile = _files.ElementAt(FileList.SelectedIndex);
            if(null == selectedFile) {
                return;
            }

            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if(dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) {
                return;
            }

            string filePath = Path.Combine(dialog.SelectedPath, selectedFile.Title);
            if(File.Exists(filePath)) {
                MessageBoxResult result = MessageBox.Show($"The file {filePath} already exists, do you wish to replace it?", "Replace File", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.No) {
                    return;
                }
            }

            using(Stream remoteStream = await PlayServicesManager.DownloadFileFromDriveAppFolderAsync(selectedFile)) {
                if(null == remoteStream) {
                    MessageBox.Show("There was an error downloading the file!", "Download Error", MessageBoxButton.OK);
                    return;
                }

                using(Stream localStream = new FileStream(filePath, FileMode.Create)) {
                    await remoteStream.CopyToAsync(localStream);
                }
            }
        }

        private async void FileListItem_Delete(object sender, RoutedEventArgs e)
        {
            Google.Apis.Drive.v2.Data.File selectedFile = _files.ElementAt(FileList.SelectedIndex);
            if(null == selectedFile) {
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Are you sure you wish to delete {selectedFile.Title}?", "Confirm Delete", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.No) {
                return;
            }

            await PlayServicesManager.DeleteFileFromDriveAppFolderAsync(selectedFile);
            _files.Remove(selectedFile);
        }

        private void FileListItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Google.Apis.Drive.v2.Data.File selectedFile = _files.ElementAt(FileList.SelectedIndex);
            if(null == selectedFile) {
                return;
            }

Logger.Debug($"TODO: double click: {selectedFile.Title}");
        }
    }
}
