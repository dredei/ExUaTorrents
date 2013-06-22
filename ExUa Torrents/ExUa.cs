using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RestSharp;
using ExtensionMethods;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace ExUa_Torrents
{
    public class ExUaFile
    {
        public string name = string.Empty;
        public string url = string.Empty;
        public string torrentUrl = string.Empty;
        public long size = 0;
        public long fileId = -1;
        public long arrIndex = -1;
        public bool check = false;
    }

    public class ExUa
    {
        const string getFileName = @"<a\shref='/get/[0-9]+'\stitle='[^']+'(\srel='nofollow')?>([^<>]+)</a>";
        const string getSize = @"<td\salign=right\swidth=200\sclass=small><b>([0-9,]+)</b>";
        const string loadLink = @"<a\shref='(/load/[0-9]+)'\srel='nofollow'>";
        const string loadTorrentLink = @"<a\shref='(/torrent/([0-9]+))'\srel='nofollow'>";
        const string fileId = @"<a\shref='/load/([0-9]+)'\srel='nofollow'>";

        List<ExUaFile> files;
        bool downloading = false;
        bool clearTempFolder = false;
        string tmpFolderPath;
        string torrentClientPath = "C:\\Program Files (x86)\\BitTorrent\\BitTorrent.exe";
        string torrentSavePath;

        public ExUa( string tmpFolderPath, string torrentClientPath, string torrentSavePath,
            bool clearTempFolder )
        {
            this.files = new List<ExUaFile>();
            this.tmpFolderPath = tmpFolderPath;
            this.torrentClientPath = torrentClientPath;
            this.torrentSavePath = torrentSavePath;
            this.clearTempFolder = clearTempFolder;
        }

        public long cleanSize( string size )
        {
            long result = 0;
            size = size.Replace( ",", "" );
            result = long.Parse( size );
            return result;
        }

        public string getTorrentClientByPath(string path)
        {
            return Path.GetFileNameWithoutExtension( path );
        }

        public void downloadComplete( object sender, AsyncCompletedEventArgs e )
        {
            this.downloading = false;
        }

        public void downloadFile( string link, string fileName )
        {
            downloading = true;
            using ( WebClient webClient = new WebClient() )
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler( downloadComplete );
                webClient.DownloadFileAsync( new Uri( @"http://www.ex.ua" + link ), fileName );
                while ( downloading )
                {
                    Application.DoEvents();
                    Thread.Sleep( 100 );
                }
            }
        }

        #region getInfo
        public string getUrl( string url )
        {
            RestClient client = new RestClient( url );
            RestRequest request = new RestRequest( Method.GET );
            IRestResponse response = client.Execute( request );
            return response.Content;
        }

        public List<string> getFilesName( string content )
        {
            List<string> result = new List<string>();
            Regex regex = new Regex( getFileName );
            MatchCollection matchs = regex.Matches( content );
            for ( int i = 0; i < matchs.Count; i++ )
            {
                result.Add( matchs[ i ].Groups[ 2 ].ToString() );
            }
            return result;
        }

        public List<long> getFilesSize( string content )
        {
            List<long> result = new List<long>();
            Regex regex = new Regex( getSize );
            MatchCollection matchs = regex.Matches( content );
            for ( int i = 0; i < matchs.Count; i++ )
            {
                result.Add( cleanSize( matchs[ i ].Groups[ 1 ].ToString() ) );
            }
            return result;
        }

        public List<string> getFilesLoadLink( string content )
        {
            List<string> result = new List<string>();
            Regex regex = new Regex( loadLink );
            MatchCollection matchs = regex.Matches( content );
            for ( int i = 0; i < matchs.Count; i++ )
            {
                result.Add( matchs[ i ].Groups[ 1 ].ToString() );
            }
            return result;
        }

        public Dictionary<string, string> getFilesTorrentLink( string content )
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Regex regex = new Regex( loadTorrentLink );
            MatchCollection matchs = regex.Matches( content );
            for ( int i = 0; i < matchs.Count; i++ )
            {
                result.Add( matchs[ i ].Groups[ 2 ].ToString(), matchs[ i ].Groups[ 1 ].ToString() );
            }
            return result;
        }

        public List<long> getFilesId( string content )
        {
            List<long> result = new List<long>();
            Regex regex = new Regex( fileId );
            MatchCollection matchs = regex.Matches( content );
            for ( int i = 0; i < matchs.Count; i++ )
            {
                result.Add( long.Parse( matchs[ i ].Groups[ 1 ].ToString() ) );
            }
            return result;
        }
        #endregion

        public void getFiles( string url )
        {
            string content = getUrl( url );
            List<string> filesName = getFilesName( content );
            List<long> filesSize = getFilesSize( content );
            List<string> filesLoadLink = getFilesLoadLink( content );
            Dictionary<string, string> filesLoadTorrentLink = getFilesTorrentLink( content );
            List<long> filesId = getFilesId( content );
            for ( int i = 0; i < filesName.Count; i++ )
            {
                ExUaFile exFile = new ExUaFile();
                exFile.name = filesName[ i ];
                exFile.size = filesSize[ i ];
                exFile.url = filesLoadLink[ i ];
                exFile.fileId = filesId[ i ];
                exFile.arrIndex = i;
                if ( filesLoadTorrentLink.ContainsKey( exFile.fileId.ToString() ) )
                {
                    exFile.torrentUrl = filesLoadTorrentLink[ exFile.fileId.ToString() ];
                }
                this.files.Add( exFile );
            }
        }

        public List<ExUaFile> getLocalFiles( bool torrent )
        {
            List<ExUaFile> result = new List<ExUaFile>();
            for ( int i = 0; i < this.files.Count; i++ )
            {
                if ( torrent )
                {
                    if ( !string.IsNullOrEmpty( this.files[ i ].torrentUrl ) )
                    {
                        result.Add( this.files[ i ] );
                    }
                }
                else
                {
                    result.Add( this.files[ i ] );
                }
            }
            return result;
        }

        public void checkFile( long arrIndex, bool check )
        {
            this.files[ (int)arrIndex ].check = check;
        }

        public void downloadFile()
        {
            List<string> downFiles = new List<string>();
            for ( int i = 0; i < this.files.Count; i++ )
            {
                if ( this.files[ i ].check )
                {
                    downFiles.Add( "http://www.ex.ua" + this.files[ i ].url );
                }
            }
            if ( downFiles.Count > 0 )
            {
                string fileName = tmpFolderPath + @"\tmp.urls";
                downFiles.SaveToFile( fileName );
                Process.Start( fileName );
            }
        }

        public void injectTorrent( string file, string savePath )
        {
            string arguments = string.Empty;
            string torrentClient = getTorrentClientByPath( torrentClientPath );
            if ( torrentClient == "BitTorrent" || torrentClient == "uTorrent" )
            {
                arguments = @"/DIRECTORY ""{0}"" ""{1}""".f( savePath, file );
            }
            else if ( torrentClient == "BitComet" )
            {
                arguments = @"/DIRECTORY ""{0}"" - o ""{1}"" - s".f( file, savePath );
            }
            Process.Start( torrentClientPath, arguments );
        }

        public void downloadTorrents()
        {
            for ( int i = 0; i < this.files.Count; i++ )
            {
                if ( this.files[ i ].check )
                {
                    string torrentUrl = this.files[ i ].torrentUrl;
                    string fileName = tmpFolderPath + @"\" + this.files[ i ].name + ".torrent";
                    downloadFile( torrentUrl, fileName );
                    injectTorrent( fileName, torrentSavePath );
                }
            }
        }
    }
}
