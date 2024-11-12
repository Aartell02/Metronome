namespace Metronome.Models
{
    public class Preset
    {
        public string name;
        public int bpm;
        public int beats;
        public int fbs;
        public int bs;
        public Preset(string _Name, int _Bpm, int _Beats, int _Fbs, int _Bs)
        {
            this.name = _Name;
            this.bpm = _Bpm;
            this.beats = _Beats;
            this.fbs = _Fbs;
            this.bs = _Bs;
        }
        public override string ToString()
        {
            return $"{name}|{bpm}|{beats}|{fbs}|{bs}";
        }
    }
    public class Presets : List<Preset> 
    {
        public Presets()
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines("./Presets.txt");
            }
            catch
            {
                File.WriteAllText("./Presets.txt","Default|120|4|800|400");
                lines = File.ReadAllLines("./Presets.txt");
            }
            foreach (string line in lines)
            {
                string[] parts = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                this.Add(new Preset(parts[0], Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]), Convert.ToInt32(parts[3]), Convert.ToInt32(parts[4])));
            }
        }
        public void SavePresets()
        {
            string[] presets = this.Select(p => p.ToString()).ToArray();
            File.WriteAllLines("./Presets.txt", presets);
        }
        public Preset? GetByName(string name) => this.FirstOrDefault(preset => preset.name == name);
        public bool AddPreset(Preset preset) {
            bool exist = false;
            foreach (string line in this.Select(p => p.name))
            {
                if(line == preset.name) exist = true;
            }
            if (!exist) this.Add(preset);
            return exist;
        }
    }
}
