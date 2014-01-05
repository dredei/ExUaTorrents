#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

#endregion

namespace ExUa_Torrents
{
    public class History
    {
        public string Path { get; set; }
        public DateTime Date { get; set; }
    }

    public class HistoryRoot
    {
        public List<History> History { get; set; }
    }

    public class SaveHistory
    {
        private readonly string _fileName;
        private HistoryRoot _history;
        private const int KeepHisoryCount = 3;

        private SaveHistory( string fileName )
        {
            this._fileName = fileName;
            this.LoadHistory();
        }

        public SaveHistory()
            : this( Application.StartupPath + @"\pathsHistory.json" )
        {
        }

        private void LoadHistory()
        {
            if ( File.Exists( this._fileName ) )
            {
                string content = File.ReadAllText( this._fileName );
                this._history = JsonConvert.DeserializeObject<HistoryRoot>( content );
                this.SortByDate();
            }
            else
            {
                this._history = new HistoryRoot();
                this._history.History = new List<History>();
            }
        }

        public void saveHistory()
        {
            string historyJSON = JsonConvert.SerializeObject( this._history );
            File.WriteAllText( this._fileName, historyJSON );
        }

        public void AddPath( string path )
        {
            History h = new History();
            h.Path = path;
            List<History> matches = this._history.History.FindAll( p => p.Path == path );
            if ( matches.Count == 0 )
            {
                if ( this._history.History.Count >= KeepHisoryCount )
                {
                    for ( int i = this._history.History.Count - 1; i > 0; i-- )
                    {
                        this._history.History[ i ].Date = this._history.History[ i - 1 ].Date;
                        this._history.History[ i ].Path = this._history.History[ i - 1 ].Path;
                    }
                    this._history.History[ 0 ].Path = path;
                    this._history.History[ 0 ].Date = DateTime.Now;
                }
                else
                {
                    this._history.History.Add( new History
                    {
                        Path = path,
                        Date = DateTime.Now
                    } );
                }
            }
            this.SortByDate();
        }

        public string[] GetPaths()
        {
            string[] paths = new string[ this._history.History.Count ];
            for ( int i = 0; i < this._history.History.Count; i++ )
            {
                paths[ i ] = this._history.History[ i ].Path;
            }
            return paths;
        }

        private void SortByDate()
        {
            this._history.History.Sort( ( p1, p2 ) => -p1.Date.CompareTo( p2.Date ) );
        }
    }
}