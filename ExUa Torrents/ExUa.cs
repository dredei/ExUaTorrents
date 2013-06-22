using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RestSharp;

namespace ExUa_Torrents
{
    public class ExUaFile
    {
        public string name = string.Empty;
        public string url = string.Empty;
        public string torrentUrl = string.Empty;
        public long size = 0;
        public long fileId = -1;
        public long arrId = -1;
    }

    public class ExUa
    {
        const string getFileName = @"<a\shref='/get/[0-9]+'\stitle='[^']+'(\srel='nofollow')?>([^<>]+)</a>";
        const string getSize = @"<td\salign=right\swidth=200\sclass=small><b>([0-9,]+)</b>";
        const string loadLink = @"<a\shref='(/load/[0-9]+)'\srel='nofollow'>";
        const string loadTorrentLink = @"<a\shref='(/torrent/([0-9]+))'\srel='nofollow'>";
        const string fileId = @"<a\shref='/load/([0-9]+)'\srel='nofollow'>";

        List<ExUaFile> files;

        public ExUa()
        {
            this.files = new List<ExUaFile>();
        }

        public long cleanSize( string size )
        {
            long result = 0;
            size = size.Replace( ",", "" );
            result = long.Parse( size );
            return result;
        }

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
                exFile.arrId = i;
                if ( filesLoadTorrentLink.ContainsKey( exFile.fileId.ToString() ) )
                {
                    exFile.torrentUrl = filesLoadTorrentLink[ exFile.fileId.ToString() ];
                }
                this.files.Add( exFile );
            }
        }
    }
}
