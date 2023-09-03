using System;
namespace SPKElectre.Models
{
	public class concordance
	{
        public int Id { get; set; }
        public string alternatif { get; set; }
        public int id_kriteria { get; set; }
        public string kriteria { get; set; }
        public string pilihan { get; set; }
        public int nilai { get; set; }
        public double nilai_normalisasi { get; set; }
        public double nilai_pembobotan { get; set; }
        public double nilai_discordance { get; set; }
    }
}

