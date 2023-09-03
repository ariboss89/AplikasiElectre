using System;
using System.Collections.Generic;
using System.Linq;
using SPKElectre.Models;
using static Android.Provider.DocumentsContract;

namespace SPKElectre.Services
{
    public class ElectreService
    {
        List<kriteria> listKriteria = new List<kriteria>();
        List<penilaian> listPenilaian = new List<penilaian>();
        PenilaianService psr = new PenilaianService();
        List<normalisasi> listNormalisasi = new List<normalisasi>();
        AlternatifService asr = new AlternatifService();
        KriteriaService ksr = new KriteriaService();
        normalisasi nrm = new normalisasi();
        pembobotan pbt = new pembobotan();
        penilaian pnl = new penilaian();
        List<pembobotan> listBobot = new List<pembobotan>();
        List<alternatif> listAlternatif = new List<alternatif>();

        public List<normalisasi> ProsesNormalisasi()
        {
            listNormalisasi = new List<normalisasi>();
            listKriteria = new List<kriteria>();
            listPenilaian = new List<penilaian>();
            listPenilaian = psr.ShowDataPenilaian();
            listKriteria = ksr.ShowDataKriteria();

            double hasilNormalisasi = 0.0;
            double hitung = 0.0;

            foreach (var nama in listKriteria)
            {
                var searchByKriteria = listPenilaian.Where(x => x.id_kriteria == nama.Id).ToList();

                for (int a = 0; a < searchByKriteria.Count; a++)
                {
                    double nilaiku = Convert.ToDouble(searchByKriteria[a].nilai);

                    for (int b = 0; b < searchByKriteria.Count; b++)
                    {
                        hitung += searchByKriteria[b].nilai * searchByKriteria[b].nilai;
                    }

                    hasilNormalisasi = 0.0;

                    hasilNormalisasi = nilaiku / Math.Sqrt(hitung);

                    nrm = new normalisasi()
                    {
                        Id = searchByKriteria[a].Id,
                        alternatif = searchByKriteria[a].alternatif,
                        id_kriteria = searchByKriteria[a].id_kriteria,
                        kriteria = searchByKriteria[a].kriteria,
                        pilihan = searchByKriteria[a].pilihan,
                        nilai = searchByKriteria[a].nilai,
                        nilai_normalisasi = hasilNormalisasi
                    };

                    listNormalisasi.Add(nrm);

                    hitung = 0.0;

                }
            }

            return listNormalisasi;

        }

        public List<pembobotan> ProsesPembobotan()
        {
            listNormalisasi = new List<normalisasi>();
            listKriteria = new List<kriteria>();
            listNormalisasi = ProsesNormalisasi();
            listKriteria = ksr.ShowDataKriteria();

            double hasilPembobotan = 0.0;

            foreach (var nama in listKriteria)
            {
                var searchByKriteria = listNormalisasi.Where(x => x.id_kriteria == nama.Id).ToList();

                int nilaiBobot = nama.bobot;

                for (int a = 0; a < searchByKriteria.Count; a++)
                {
                    double nilaiku = Convert.ToDouble(searchByKriteria[a].nilai_normalisasi);

                    hasilPembobotan = nilaiku * nilaiBobot;

                    pbt = new pembobotan()
                    {
                        Id = searchByKriteria[a].Id,
                        alternatif = searchByKriteria[a].alternatif,
                        id_kriteria = searchByKriteria[a].id_kriteria,
                        kriteria = searchByKriteria[a].kriteria,
                        pilihan = searchByKriteria[a].pilihan,
                        nilai = searchByKriteria[a].nilai,
                        nilai_normalisasi = searchByKriteria[a].nilai_normalisasi,
                        nilai_pembobotan = hasilPembobotan
                    };

                    listBobot.Add(pbt);

                    hasilPembobotan = 0.0;

                }
            }

            return listBobot;

        }

        public double[,] ProsesConcordance2()
        {
            listBobot = new List<pembobotan>();
            listKriteria = new List<kriteria>();
            listAlternatif = new List<alternatif>();
            listBobot = ProsesPembobotan();
            listKriteria = ksr.ShowDataKriteria();
            listAlternatif = asr.ShowDataAlternatif();
            List<double> listSumConcordance = new List<double>();
            List<double> listConcordance2 = new List<double>();
            List<concordance> listConcordance = new List<concordance>();
            double[,] data2d = new double[listAlternatif.Count, listKriteria.Count];
            double[,] dataConcordance = new double[listAlternatif.Count, listKriteria.Count];
            double[,] dataDiscordance = new double[listAlternatif.Count, listKriteria.Count];

            

            List<double> listConc = new List<double>();
            List<double> listDisconc = new List<double>();
            List<double> listSumDiscordance = new List<double>();


            double nilaiAlt1, nilaiAlt2, nilaiDisc1, nilaiDisc2;

            for (int x = 0; x < listAlternatif.Count; x++)
            {
                string namaAlt = listAlternatif[x].nama;

                var searchByAlternatif = listBobot.Where(x => x.alternatif == namaAlt).ToList();

                string kriteria = "";
                int bobot = 0;
                int idKriteria = 0;

                List<pembobotan> searchByKriteria = new List<pembobotan>();

                for (int y = 0; y < listKriteria.Count; y++)
                {
                    idKriteria = listKriteria[y].Id;
                    kriteria = listKriteria[y].nama;
                    bobot = listKriteria[y].bobot;

                    searchByKriteria = searchByAlternatif.Where(x => x.id_kriteria == idKriteria).ToList();

                    data2d[x, y] = searchByKriteria[0].nilai_pembobotan;

                }
            }

            //membandingkan concordance
            List<int> listAcuan = new List<int>();
            List<int> listAcuanDisconc = new List<int>();
            int[] acuan = new int[data2d.GetLength(0)];

            int ari = 0;

            for (int a = 0; a < data2d.GetLength(1); a++)
            {
                ari = listAlternatif.Count;

                for (int b = 0; b < listAlternatif.Count; b++)
                {
                    acuan[b] = ari -= 1;
                }

                Array.Sort(acuan);

                for (int b = 0; b < acuan.Count(); b++)
                {
                    int nilaiAcuan = acuan[b];

                    if (a != nilaiAcuan)
                    {
                        listAcuan.Add(nilaiAcuan);
                        listAcuanDisconc.Add(nilaiAcuan);
                    }
                }

                //Membandingkan Concordance
                for (int d = 0; d < listAcuan.Count; d++)
                {
                    int dataAcuan = listAcuan[d];

                    for (int b = 0; b < data2d.GetLength(1); b++)
                    {
                        int bobot = listKriteria[b].bobot;

                        nilaiAlt1 = data2d[a, b];

                        nilaiAlt2 = data2d[dataAcuan, b];

                        var max = Math.Max(nilaiAlt1, nilaiAlt2);

                        if (nilaiAlt1 >= nilaiAlt2)
                        {
                            listSumConcordance.Add(bobot);

                            int idKriteria = listKriteria[b].Id;

                        }
                        else
                        {
                            listSumConcordance.Add(0);
                        }
                    }

                    int nilaiConc = 0;
                    string discor = "";

                    for (int e = 0; e < listSumConcordance.Count; e++)
                    {
                        nilaiConc += ((int)listSumConcordance[e]);

                    }

                    listConc.Add(nilaiConc);

                    listSumConcordance = new List<double>();

                }

                listAcuan = new List<int>();

                for (int v = 0; v < listConc.Count + 1; v++)
                {
                    if (a != v)
                    {
                        if (v == 0 ||a>v)
                        {
                            dataConcordance[a, v] = listConc[v];
                        }
                        else
                        {
                            int man = v;

                            int data = ((int)listConc[v - 1]);

                            dataConcordance[a, v] = listConc[v - 1];
                        }
                    }
                    else
                    {
                        dataConcordance[a, a] = 0;
                    }

                }
            
                listConc = new List<double>();

                //Membandingkan Discordance
                double hasilNormalisasi = 0.0;

                for (int d = 0; d < listAcuanDisconc.Count; d++)
                {
                    int dataAcuan = listAcuanDisconc[d];

                    for (int b = 0; b < data2d.GetLength(1); b++)
                    {
                        int bobot = listKriteria[b].bobot;

                        nilaiDisc1 = data2d[a, b];

                        nilaiDisc2 = data2d[dataAcuan, b];

                        hasilNormalisasi = nilaiDisc1 - nilaiDisc2;

                        var max = Math.Max(nilaiDisc1, nilaiDisc2);

                        if (nilaiDisc1 <= nilaiDisc2)
                        {
                            listSumDiscordance.Add(bobot);
                        }
                        else
                        {
                            listSumDiscordance.Add(0);
                        }
                    }

                    int nilaiDisconc = 0;

                    for (int e = 0; e < listSumDiscordance.Count; e++)
                    {
                        nilaiDisconc += ((int)listSumDiscordance[e]);
                    }

                    listDisconc.Add(nilaiDisconc);

                    listSumDiscordance = new List<double>();

                }

                listAcuanDisconc = new List<int>();

                for (int v = 0; v < listDisconc.Count + 1; v++)
                {
                    if (a != v)
                    {
                        if (v == 0 || a > v)
                        {
                            dataDiscordance[a, v] = listDisconc[v];
                        }
                        else
                        {
                            dataDiscordance[a, v] = listDisconc[v - 1];
                        }
                    }
                    else
                    {
                        dataDiscordance[a, a] = 0;
                    }

                }

                listDisconc = new List<double>();

            }

            //data concordance udah oke nich
            //ALHAMDULILLAH

            //MENCARI NILAI THRESHOLD
            int total = 0;

            int jmlRow = dataConcordance.GetLength(0);
            int jmlCol = dataConcordance.GetLength(1);
           
            for(int k = 0; k < jmlRow; k++)
            {
                for (int l = 0; l < jmlCol; l++)
                {
                    int nilai = ((int)dataConcordance[k, l]);

                    total += nilai;
                }
            }

            int n = listKriteria.Count * (listKriteria.Count-1);

            int threshold = total / n;

            return dataConcordance;
        }


        public List<alternatif> listDataAlternatif()
        {
            listAlternatif = new List<alternatif>();

            listAlternatif = asr.ShowDataAlternatif();

            return listAlternatif;
        }

        public List<perankingan> Perankingan()
        {

            List<perankingan> listRank = new List<perankingan>();

            listBobot = new List<pembobotan>();
            listKriteria = new List<kriteria>();
            listAlternatif = new List<alternatif>();
            listBobot = ProsesPembobotan();
            listKriteria = ksr.ShowDataKriteria();
            listAlternatif = asr.ShowDataAlternatif();
            List<double> listSumConcordance = new List<double>();
            List<double> listConcordance2 = new List<double>();
            List<concordance> listConcordance = new List<concordance>();
            double[,] data2d = new double[listAlternatif.Count, listKriteria.Count];
            double[,] dataConcordance = new double[listAlternatif.Count, listKriteria.Count];
            double[,] dataDiscordance = new double[listAlternatif.Count, listKriteria.Count];

            string[,] dataStrDiscordance = new string[listAlternatif.Count, listKriteria.Count];

            string[,] dataRealDiscordance = new string[listAlternatif.Count, listKriteria.Count];

            List<double> listConc = new List<double>();
            List<double> listDisconc = new List<double>();
            List<double> listSumDiscordance = new List<double>();

            List<int> listKriteriaConc = new List<int>();

            List<string> listConcStr = new List<string>();

            List<double> listSubstractDiscorn = new List<double>();
            List<double> listDataDiscorn = new List<double>();

            IDictionary<string, double> dictDiscordance = new Dictionary<string, double>();


            double nilaiAlt1, nilaiAlt2, nilaiDisc1, nilaiDisc2;

            for (int x = 0; x < listAlternatif.Count; x++)
            {
                string namaAlt = listAlternatif[x].nama;

                var searchByAlternatif = listBobot.Where(x => x.alternatif == namaAlt).ToList();

                string kriteria = "";
                int bobot = 0;
                int idKriteria = 0;

                List<pembobotan> searchByKriteria = new List<pembobotan>();

                for (int y = 0; y < listKriteria.Count; y++)
                {
                    idKriteria = listKriteria[y].Id;
                    kriteria = listKriteria[y].nama;
                    bobot = listKriteria[y].bobot;

                    searchByKriteria = searchByAlternatif.Where(x => x.id_kriteria == idKriteria).ToList();

                    data2d[x, y] = searchByKriteria[0].nilai_pembobotan;

                }
            }

            // bikinfungsi utnuk mnghasilkan angka sesuai dengan jumlah kriteria yg ada
            string paramKrit = "";

            for (int k = 0; k < listKriteria.Count; k++)
            {
                int ak = k + 1;

                if (ak == listKriteria.Count)
                {
                    paramKrit += ak;
                }
                else
                {
                    paramKrit += ak + ",";
                }
            }

            //membandingkan concordance
            List<int> listAcuan = new List<int>();
            List<int> listAcuanDisconc = new List<int>();
            int[] acuan = new int[data2d.GetLength(0)];

            int ari = 0;

            for (int a = 0; a < data2d.GetLength(1); a++)
            {
                ari = listAlternatif.Count;

                for (int b = 0; b < listAlternatif.Count; b++)
                {
                    acuan[b] = ari -= 1;
                }

                Array.Sort(acuan);

                for (int b = 0; b < acuan.Count(); b++)
                {
                    int nilaiAcuan = acuan[b];

                    if (a != nilaiAcuan)
                    {
                        listAcuan.Add(nilaiAcuan);
                        listAcuanDisconc.Add(nilaiAcuan);
                    }
                }

                //Membandingkan Concordance
                for (int d = 0; d < listAcuan.Count; d++)
                {
                    int dataAcuan = listAcuan[d];

                    for (int b = 0; b < data2d.GetLength(1); b++)
                    {
                        int bobot = listKriteria[b].bobot;

                        nilaiAlt1 = data2d[a, b];

                        nilaiAlt2 = data2d[dataAcuan, b];

                        var max = Math.Max(nilaiAlt1, nilaiAlt2);

                        if (nilaiAlt1 >= nilaiAlt2)
                        {
                            listSumConcordance.Add(bobot);

                            int idKriteria = listKriteria[b].Id;

                            listKriteriaConc.Add(idKriteria);
                        }
                        else
                        {
                            listKriteriaConc.Add(0);
                            listSumConcordance.Add(0);
                        }
                    }

                    //listKriteriaConc.Sort();

                    int nilaiConc = 0;
                    string discor = "";

                    for (int e = 0; e < listSumConcordance.Count; e++)
                    {
                        nilaiConc += ((int)listSumConcordance[e]);

                        int ee = e + 1;

                        if (ee == listSumConcordance.Count)
                        {
                            discor += listKriteriaConc[e];
                        }
                        else
                        {
                            discor += listKriteriaConc[e] + ",";
                        }
                    }

                    listConc.Add(nilaiConc);

                    listConcStr.Add(discor);

                    listSumConcordance = new List<double>();

                    listKriteriaConc = new List<int>();

                }

                listAcuan = new List<int>();

                for (int v = 0; v < listConc.Count + 1; v++)
                {
                    if (a != v)
                    {
                        if (v == 0 || a > v)
                        {
                            dataConcordance[a, v] = listConc[v];

                            dataStrDiscordance[a, v] = listConcStr[v];
                        }
                        else
                        {
                            int man = v;

                            int data = ((int)listConc[v - 1]);

                            dataConcordance[a, v] = listConc[v - 1];

                            dataStrDiscordance[a, v] = listConcStr[v - 1];
                        }
                    }
                    else
                    {
                        dataConcordance[a, a] = 0;

                        dataStrDiscordance[a, a] = "0";
                    }


                    string dataConc = dataStrDiscordance[a, v];

                    string[] strlist = dataConc.Split(",");

                    string[] paramlist = paramKrit.Split(",");

                    if (dataConc != "0")
                    {

                        for (int xx = 0; xx < paramlist.Length; xx++)
                        {
                            string data1 = paramlist[xx];
                            string data2 = strlist[xx];

                            if (data2 != data1)
                            {
                                dataRealDiscordance[a, v] += data1 + ",";
                            }

                            else
                            {
                                dataRealDiscordance[a, v] += "0,";
                            }
                        }

                        //if (dataRealDiscordance[a, v] == null)
                        //{
                        //    dataRealDiscordance[a, v] = "0,";
                        //}

                    }
                    else
                    {
                        dataRealDiscordance[a, v] = "0,";
                    }

                    dataRealDiscordance[a, v] = dataRealDiscordance[a, v].Substring(0, dataRealDiscordance[a, v].Length - 1);

                }

                listConc = new List<double>();

                listConcStr = new List<string>();

                //Membandingkan Discordance
                double hasilNormalisasi = 0.0;

                for (int d = 0; d < listAcuanDisconc.Count; d++)
                {
                    int dataAcuan = listAcuanDisconc[d];

                    for (int b = 0; b < data2d.GetLength(1); b++)
                    {
                        int bobot = listKriteria[b].bobot;

                        nilaiDisc1 = data2d[a, b];

                        nilaiDisc2 = data2d[dataAcuan, b];

                        hasilNormalisasi = nilaiDisc1 - nilaiDisc2;

                        var max = Math.Max(nilaiDisc1, nilaiDisc2);

                        if (nilaiDisc1 <= nilaiDisc2)
                        {
                            listSumDiscordance.Add(bobot);
                        }
                        else
                        {
                            listSumDiscordance.Add(0);
                        }

                        listSubstractDiscorn.Add(hasilNormalisasi);

                        string keyKu = a.ToString() + dataAcuan.ToString() + "," + b;
                        dictDiscordance.Add(keyKu, hasilNormalisasi);
                    }

                    int nilaiDisconc = 0;

                    for (int e = 0; e < listSumDiscordance.Count; e++)
                    {
                        nilaiDisconc += ((int)listSumDiscordance[e]);
                    }

                    listDisconc.Add(nilaiDisconc);

                    listSumDiscordance = new List<double>();

                }

                listAcuanDisconc = new List<int>();

                for (int v = 0; v < listDisconc.Count + 1; v++)
                {
                    if (a != v)
                    {
                        if (v == 0 || a > v)
                        {
                            dataDiscordance[a, v] = listDisconc[v];
                        }
                        else
                        {
                            dataDiscordance[a, v] = listDisconc[v - 1];
                        }
                    }
                    else
                    {
                        dataDiscordance[a, a] = 0;
                    }

                }

                listDisconc = new List<double>();

            }

            //data concordance udah oke nich
            //ALHAMDULILLAH

            //MENCARI NILAI THRESHOLD
            double total = 0;

            int jmlRow = dataConcordance.GetLength(0);
            int jmlCol = dataConcordance.GetLength(1);

            List<double> listCompareDsc = new List<double>();
            List<double> listCompareDsc2 = new List<double>();

            double[,] matriksDiscordance = new double[listAlternatif.Count, listKriteria.Count];
            double[,] matriksConcordance = new double[listAlternatif.Count, listKriteria.Count];

            double[,] matriksKeputusan = new double[listAlternatif.Count, listKriteria.Count];

            for (int k = 0; k < jmlRow; k++)
            {
                for (int l = 0; l < jmlCol; l++)
                {
                    int nilai = ((int)dataConcordance[k, l]);

                    total += nilai;

                    //ganti ini
                    //string strData = dataStrDiscordance[k, l];
                    string strData = dataRealDiscordance[k, l];

                    List<string> strlist = strData.Split(",").ToList();

                    var manise = strlist.RemoveAll(x=> x == "0");

                    if (!strData.Equals("0") && strlist.Count!=0)
                    {
                        for (int a = 0; a < strlist.Count(); a++)
                        {
                            int val = Convert.ToInt32(strlist[a])-1;

                            string key = k.ToString() + l.ToString() + "," + val;

                            var dataDsc = Math.Abs(dictDiscordance[key]);

                            listCompareDsc.Add(dataDsc);

                        }

                        for (int b = 0; b < listKriteria.Count; b++)
                        {
                            string key = k.ToString() + l.ToString() + "," + b;

                            var dataDsc = Math.Abs(dictDiscordance[key]);

                            listCompareDsc2.Add(dataDsc);
                        }


                        double max1 = listCompareDsc.Max(x => ((double)x));
                        double max2 = listCompareDsc2.Max(x => ((double)x));

                        matriksDiscordance[k, l] = max1 / max2;

                    }
                    else
                    {
                        matriksDiscordance[k, l] = 0;
                    }

                    listCompareDsc = new List<double>();
                    listCompareDsc2 = new List<double>();
                }
            }

            int n = listKriteria.Count * (listKriteria.Count - 1);

            double threshold = total / n;

            //determine the value whether 0 or 1 in discordance

            for (int k = 0; k < jmlRow; k++)
            {
                for (int l = 0; l < jmlCol; l++)
                {
                    double nilai = dataConcordance[k, l];

                    if (nilai >= threshold)
                    {
                        matriksConcordance[k, l] = 1;
                    }
                    else
                    {
                        matriksConcordance[k, l] = 0;
                    }

                }
            }

            //find threshold discordance

            double totalDisc = 0.0;
            double[,] matriksDiscordanceReal = new double[listAlternatif.Count, listKriteria.Count];

            for (int k = 0; k < jmlRow; k++)
            {
                for (int l = 0; l < jmlCol; l++)
                {
                    double nilai = matriksDiscordance[k, l];

                    totalDisc += nilai;

                }
            }


            double thresholdDisc = totalDisc / n;

            //determine the value whether 0 or 1 in discordance

            for (int k = 0; k < jmlRow; k++)
            {
                for (int l = 0; l < jmlCol; l++)
                {
                    double nilai = matriksDiscordance[k, l];

                    if (nilai >= thresholdDisc)
                    {
                        matriksDiscordanceReal[k, l] = 1;

                        //if(k == l)
                        //{
                        //    matriksDiscordanceReal[k, l] = 0;
                        //}

                    }
                    else
                    {
                        matriksDiscordanceReal[k, l] = 0;

                        //if (k == l)
                        //{
                        //    matriksDiscordanceReal[k, l] = 0;
                        //}
                    }
                }
            }


            //multiply matriks conc * matriks discor

            for (int k = 0; k < jmlRow; k++)
            {
                for (int l = 0; l < jmlCol; l++)
                {
                    double val1 = matriksConcordance[k, l];
                    double val2 = matriksDiscordanceReal[k, l];
                    double multiply = val1 * val2;

                    matriksKeputusan[k, l] = multiply;
                }
            }

            perankingan prk = new perankingan();
            int column = matriksKeputusan.GetLength(1);
            double nilaiX = 0.0;
            List<double> listHasil = new List<double>();

            for(int a = 0; a < listAlternatif.Count; a++)
            {
                for(int b=0; b<column; b++)
                {


                    nilaiX += matriksKeputusan[a, b];
                }

                listHasil.Add(nilaiX);

                prk = new perankingan()
                {
                    alternatif = listAlternatif[a].nama,
                    Hasil = nilaiX
                };

                listRank.Add(prk);

                nilaiX = 0.0;
            }

            var rankDsc = listRank.OrderByDescending(x=> x.Hasil).ToList();

            List<perankingan> listRank2 = new List<perankingan>();

            for (int a = 0; a < rankDsc.Count; a++)
            {
                
                prk = new perankingan()
                {
                    alternatif = rankDsc[a].alternatif,
                    Hasil = rankDsc[a].Hasil,
                    Keterangan = "Ranking " + (a + 1)
                };

                listRank2.Add(prk);
            }


            return listRank2;
        }

    }
}