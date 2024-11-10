using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Metronome.Models
{
    public class IOModel
    {
        public class Preset
        {
            private string Name { get; set; }
            private int Bpm { get; set; }
            private int Beats { get; set; }
            private int Fbs { get; set; }
            private int Bs { get; set; }
            public Preset(string _Name, int _Bpm, int _Beats, int _Fbs, int _Bs)
            {
                this.Name = _Name;
                this.Bpm = _Bpm;
                this.Beats = _Beats;
                this.Fbs = _Fbs;    
                this.Bs = _Bs;
            }
        }

        private MetronomeModel Model;
        public MetronomeModel GetMetronome() => Model;  

        public IOModel(MetronomeModel model) 
        {
            String[] lines = File.ReadAllLines("./Files/Presets.txt");
            List<Preset> presets = new List<Preset>();
            string[] parts = lines[0].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            presets.Append(new Preset(parts[0], Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]), Convert.ToInt32(parts[3]), Convert.ToInt32(parts[4])));
            Model = model;
        }    
        public void SavePreset()
        {

        }
    }
}
