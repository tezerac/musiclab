using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Storage.FileProperties;

namespace MusicLibrary
{
    public static class MediaHelper
    {
        public static async Task <string > GetMediaFileAsync(string filename)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var mediaFile = await localFolder.GetFileAsync(filename);
            var mediaStream = await mediaFile.OpenReadAsync();
            var mediaReader = new DataReader(mediaStream );
            var mediaLength = mediaStream.Size;
            await mediaReader.LoadAsync((uint)mediaLength );
            return mediaReader.ReadString((uint)mediaLength);
        }
        public static async void WriteMediaFileAsync(string filename,string content)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var mediaFile = await localFolder.CreateFileAsync(filename,CreationCollisionOption.OpenIfExists);
            var mediaStream = await mediaFile.OpenAsync(FileAccessMode.ReadWrite);
            var mediaWriter = new DataWriter(mediaStream);
            mediaWriter.WriteString(content);
            await mediaWriter.StoreAsync();
            
        }
    }
}
