#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ExtensionMethods;
using RestSharp;

#endregion

namespace ExUa_Torrents
{
    public class UpdEventArgs : EventArgs
    {
        public int Progress { get; set; }
        public int MaxProgress { get; set; }
    }

    public class ExUa
    {
        private const string GetFileName = @"<a\shref='/get/[0-9]+'\stitle='[^']+'(\srel='nofollow')?>([^<>]+)</a>";
        private const string GetSize = @"<td\salign=right\swidth=200\sclass=small><b>([0-9,]+)</b>";
        private const string LoadLink = @"<a\shref='(/load/[0-9]+)'\srel='nofollow'>";
        private const string LoadTorrentLink = @"<a\shref='(/torrent/([0-9]+))'\srel='nofollow'>";
        private const string FileId = @"<a\shref='/load/([0-9]+)'\srel='nofollow'>";

        private readonly List<ExUaFile> _files;
        private bool _downloading = false;
        private readonly bool _clearTempFolder = false;
        private readonly string _tmpFolderPath;
        private readonly string _torrentClientPath = "C:\\Program Files (x86)\\BitTorrent\\BitTorrent.exe";
        private readonly string _torrentSavePath;
        public event EventHandler<UpdEventArgs> UpdEvent = delegate { };

        public ExUa( string tmpFolderPath, string torrentClientPath, string torrentSavePath,
            bool clearTempFolder )
        {
            this._files = new List<ExUaFile>();
            this._tmpFolderPath = tmpFolderPath;
            this._torrentClientPath = torrentClientPath;
            this._torrentSavePath = torrentSavePath;
            this._clearTempFolder = clearTempFolder;
        }

        private static long CleanSize( string size )
        {
            size = size.Replace( ",", "" );
            long result = long.Parse( size );
            return result;
        }

        private static string GetTorrentClientByPath( string path )
        {
            return Path.GetFileNameWithoutExtension( path );
        }

        private void DownloadComplete( object sender, AsyncCompletedEventArgs e )
        {
            this._downloading = false;
        }

        private void DownloadFile( string link, string fileName )
        {
            this._downloading = true;
            using ( WebClient webClient = new WebClient() )
            {
                webClient.DownloadFileCompleted += this.DownloadComplete;
                webClient.DownloadFileAsync( new Uri( @"http://www.ex.ua" + link ), fileName );
                while ( this._downloading )
                {
                    Application.DoEvents();
                    Thread.Sleep( 100 );
                }
            }
        }

        #region getInfo

        private static string OpenUrl( string url )
        {
            var cookieJar = new CookieContainer();
            RestClient client = new RestClient( url );
            client.Proxy = new WebProxy();
            client.CookieContainer = cookieJar;
            client.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0";
            RestRequest request = new RestRequest( Method.GET );
            IRestResponse response = client.Execute( request );
            return response.Content;
        }

        private static List<string> GetFilesName( string content )
        {
            List<string> result = new List<string>();
            Regex regex = new Regex( GetFileName );
            MatchCollection matchs = regex.Matches( content );
            for ( int i = 0; i < matchs.Count; i++ )
            {
                result.Add( matchs[ i ].Groups[ 2 ].ToString() );
            }
            return result;
        }

        private List<long> GetFilesSize( string content )
        {
            List<long> result = new List<long>();
            Regex regex = new Regex( GetSize );
            MatchCollection matchs = regex.Matches( content );
            for ( int i = 0; i < matchs.Count; i++ )
            {
                result.Add( CleanSize( matchs[ i ].Groups[ 1 ].ToString() ) );
            }
            return result;
        }

        private List<string> GetFilesLoadLink( string content )
        {
            List<string> result = new List<string>();
            Regex regex = new Regex( LoadLink );
            MatchCollection matchs = regex.Matches( content );
            for ( int i = 0; i < matchs.Count; i++ )
            {
                result.Add( matchs[ i ].Groups[ 1 ].ToString() );
            }
            return result;
        }

        private Dictionary<string, string> GetFilesTorrentLink( string content )
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Regex regex = new Regex( LoadTorrentLink );
            MatchCollection matchs = regex.Matches( content );
            for ( int i = 0; i < matchs.Count; i++ )
            {
                result.Add( matchs[ i ].Groups[ 2 ].ToString(), matchs[ i ].Groups[ 1 ].ToString() );
            }
            return result;
        }

        private List<long> GetFilesId( string content )
        {
            List<long> result = new List<long>();
            Regex regex = new Regex( FileId );
            MatchCollection matchs = regex.Matches( content );
            for ( int i = 0; i < matchs.Count; i++ )
            {
                result.Add( long.Parse( matchs[ i ].Groups[ 1 ].ToString() ) );
            }
            return result;
        }

        #endregion

        public void GetFiles( string url )
        {
            string content = OpenUrl( url );
            List<string> filesName = GetFilesName( content );
            List<long> filesSize = this.GetFilesSize( content );
            List<string> filesLoadLink = this.GetFilesLoadLink( content );
            Dictionary<string, string> filesLoadTorrentLink = this.GetFilesTorrentLink( content );
            List<long> filesId = this.GetFilesId( content );
            for ( int i = 0; i < filesName.Count; i++ )
            {
                ExUaFile exFile = new ExUaFile();
                exFile.Name = filesName[ i ];
                exFile.Size = filesSize[ i ];
                exFile.Url = filesLoadLink[ i ];
                exFile.FileId = filesId[ i ];
                exFile.ArrIndex = i;
                if ( filesLoadTorrentLink.ContainsKey( exFile.FileId.ToString() ) )
                {
                    exFile.TorrentUrl = filesLoadTorrentLink[ exFile.FileId.ToString() ];
                }
                this._files.Add( exFile );
            }
        }

        public List<ExUaFile> GetLocalFiles( bool torrent )
        {
            List<ExUaFile> result = new List<ExUaFile>();
            foreach ( ExUaFile file in this._files )
            {
                if ( torrent )
                {
                    if ( !string.IsNullOrEmpty( file.TorrentUrl ) )
                    {
                        result.Add( file );
                    }
                }
                else
                {
                    result.Add( file );
                }
            }
            return result;
        }

        public void CheckFile( long arrIndex, bool check )
        {
            this._files[ (int)arrIndex ].Check = check;
        }

        public void ClearTmpFolder()
        {
            if ( this._clearTempFolder && Directory.Exists( this._tmpFolderPath ) )
            {
                foreach ( string file in Directory.GetFiles( this._tmpFolderPath ) )
                {
                    File.Delete( file );
                }
                foreach ( string subDirectory in Directory.GetDirectories( this._tmpFolderPath ) )
                {
                    Directory.Delete( subDirectory, true );
                }
            }
        }

        public void DownloadFile()
        {
            List<string> downFiles = new List<string>();
            UpdEventArgs args = new UpdEventArgs();
            args.Progress = 0;
            args.MaxProgress = this._files.Count;
            this.UpdEvent( this, args );
            foreach ( ExUaFile file in this._files )
            {
                if ( file.Check )
                {
                    downFiles.Add( "http://www.ex.ua" + file.Url );
                }
                args.Progress++;
                this.UpdEvent( this, args );
            }
            if ( downFiles.Count <= 0 )
            {
                return;
            }
            string fileName = this._tmpFolderPath + @"\tmp.urls";
            downFiles.SaveToFile( fileName );
            Process.Start( fileName );
        }

        private void InjectTorrent( string file, string savePath )
        {
            string arguments = string.Empty;
            string torrentClient = GetTorrentClientByPath( this._torrentClientPath );
            if ( torrentClient == "BitTorrent" || torrentClient == "uTorrent" )
            {
                arguments = @"/DIRECTORY ""{0}"" ""{1}""".f( savePath, file );
            }
            else if ( torrentClient == "BitComet" )
            {
                arguments = @"/DIRECTORY ""{0}"" - o ""{1}"" - s".f( file, savePath );
            }
            Process.Start( this._torrentClientPath, arguments );
        }

        public void DownloadTorrents()
        {
            UpdEventArgs args = new UpdEventArgs();
            args.Progress = 0;
            args.MaxProgress = this._files.Count;
            this.UpdEvent( this, args );
            foreach ( ExUaFile file in this._files )
            {
                if ( file.Check )
                {
                    string torrentUrl = file.TorrentUrl;
                    string fileName = this._tmpFolderPath + @"\" + file.Name + ".torrent";
                    this.DownloadFile( torrentUrl, fileName );
                    this.InjectTorrent( fileName, this._torrentSavePath );
                }
                args.Progress++;
                this.UpdEvent( this, args );
            }
        }

        public long GetFileSize( int index )
        {
            return this._files[ index ].Size;
        }
    }
}