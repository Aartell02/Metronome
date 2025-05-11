using NAudio.SoundFont;

namespace Metronome.Models
{
    public class Preset
    {
        public string Name { get; set; }
        public int Bpm { get; set; }
        public int Beats { get; set; }
        public int Fbs { get; set; }
        public int Bs { get; set; }
        public Preset(string name, int bpm, int beats, int fbs, int bs)
        {
            this.Name = name;
            this.Bpm = bpm;
            this.Beats = beats;
            this.Fbs = fbs;
            this.Bs = bs;
        }
        public override string ToString()
        {
            return $"{Name}|{Bpm}|{Beats}|{Fbs}|{Bs}";
        }
    }
    public class PresetRepository : List<Preset> 
    {
        public PresetRepository()
        {
            LoadPresets();
        }
        public void LoadPresets()
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines("./Presets.txt");
            }
            catch
            {
                File.WriteAllText("./Presets.txt", "Default|120|4|800|400");
                lines = File.ReadAllLines("./Presets.txt");
            }
            foreach (string line in lines)
            {
                string[] parts = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                this.Add(new Preset(parts[0], Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]), Convert.ToInt32(parts[3]), Convert.ToInt32(parts[4])));
            }
        }
        public void SavePresets() => File.WriteAllLines("./Presets.txt", this.Select(p => p.ToString()).ToArray());
        public Preset? GetByName(string name) => this.FirstOrDefault(preset => preset.Name == name);
        public bool AddPreset(Preset preset)
        {
            if (this.Any(p => p.Name == preset.Name)) return false;
            base.Add(preset);
            return true;
        }
    }
}
