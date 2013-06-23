using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

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
        string fileName;
        HistoryRoot history;
        int keepHisoryCount = 3;

        public SaveHistory( string fileName )
        {
            this.fileName = fileName;
            loadHistory();
        }

        public SaveHistory()
            : this( Application.StartupPath + @"\pathsHistory.json" )
        { }

        public void loadHistory()
        {
            if ( File.Exists( this.fileName ) )
            {

                string content = File.ReadAllText( this.fileName );
                this.history = JsonConvert.DeserializeObject<HistoryRoot>( content );
                sortByDate();
            }
            else
            {
                this.history = new HistoryRoot();
                this.history.History = new List<History>();
            }
        }

        public void saveHistory()
        {
            string historyJSON = JsonConvert.SerializeObject( this.history );
            File.WriteAllText( fileName, historyJSON );
        }

        public void addPath( string path )
        {
            History h = new History();
            h.Path = path;
            List<History> matches = this.history.History.FindAll( p => p.Path == path );
            if ( matches.Count == 0 )
            {
                if ( this.history.History.Count >= keepHisoryCount )
                {
                    for ( int i = this.history.History.Count - 1; i > 0; i-- )
                    {
                        this.history.History[ i ].Date = this.history.History[ i - 1 ].Date;
                        this.history.History[ i ].Path = this.history.History[ i - 1 ].Path;
                    }
                    this.history.History[ 0 ].Path = path;
                    this.history.History[ 0 ].Date = DateTime.Now;
                }
                else
                {
                    this.history.History.Add( new History
                    {
                        Path = path,
                        Date = DateTime.Now
                    } );                    
                }
            }
            this.sortByDate();
        }

        public string[] getPaths()
        {
            string[] paths = new string[ this.history.History.Count ];
            for ( int i = 0; i < this.history.History.Count; i++ )
            {
                paths[ i ] = this.history.History[ i ].Path;
            }
            return paths;
        }

        public void sortByDate()
        {
            this.history.History.Sort( ( p1, p2 ) => -p1.Date.CompareTo( p2.Date ) );
        }
    }
}
