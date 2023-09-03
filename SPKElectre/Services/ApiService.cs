using System;
using Android.App;
using SPKElectre.Helper;
using static Android.Telephony.CarrierConfigManager;

namespace SPKElectre.Services
{
    public class ApiService
	{
        AppPreferences app = new AppPreferences(Application.Context);

        public string ApiUrl()
        {
            string apiUrl = $"http://{app.getAccessKey("ip")}/api/";

            //string apiUrl = "http://10.211.55.5/api/";

            return apiUrl;
        }

        public string GetAllAlternatif()
        {
            return $"{ApiUrl()}Alternatif/GetAllAlternatif";

        }

        public string SaveAlternatif()
        {
            return $"{ApiUrl()}Alternatif/SaveAlternatif";
        }

        public string UpdateAlternatif()
        {
            return $"{ApiUrl()}Alternatif/UpdateAlternatif";
        }

        public string DeleteAlternatif(int Id)
        {
            return $"{ApiUrl()}Alternatif/DeleteAlternatif?Id=" + Id;
        }


        //KRITERIA
        public string GetAllKriteria()
        {
            return $"{ApiUrl()}Kriteria/GetAllKriteria";

        }

        public string SaveKriteria()
        {
            return $"{ApiUrl()}Kriteria/SaveKriteria";
        }

        public string UpdateKriteria()
        {
            return $"{ApiUrl()}Kriteria/UpdateKriteria";
        }

        public string DeleteKriteria(int Id)
        {
            return $"{ApiUrl()}Kriteria/DeleteKriteria?Id=" + Id;
        }

        //SUBKRITERIA
        public string GetAllSubkriteria()
        {
            return $"{ApiUrl()}Subkriteria/GetAllSubkriteria";

        }

        public string SaveSubkriteria()
        {
            return $"{ApiUrl()}Subkriteria/SaveSubkriteria";
        }

        public string UpdateSubkriteria()
        {
            return $"{ApiUrl()}Subkriteria/UpdateSubkriteria";
        }

        public string DeleteSubkriteria(int Id)
        {
            return $"{ApiUrl()}Subkriteria/DeleteSubkriteria?Id=" + Id;
        }

        //CONFIG
        public string IdKeputusan()
        {
            return $"{ApiUrl()}Config/IdKeputusan";
        }

        public string UpdateConfig()
        {
            return $"{ApiUrl()}Config/UpdateConfigValue";
        }

        //PENILAIAN
        public string GetAllPenilaian()
        {
            return $"{ApiUrl()}Penilaian/GetAllPenilaian";

        }

        public string SavePenilaian()
        {
            return $"{ApiUrl()}Penilaian/SavePenilaian";
        }

        public string DeletePenilaian(int Id)
        {
            return $"{ApiUrl()}Penilaian/DeletePenilaian?Id=" + Id;
        }

        public string CheckData(string alternatif, string kriteria)
        {
            return $"{ApiUrl()}Penilaian/CheckKriteria?alternatif="+alternatif+"&kriteria="+kriteria;
        }

        //LOGIN
        public string UserLogin()
        {
            return $"{ApiUrl()}Login/UserLogin";
        }

        public string SaveUser()
        {
            return $"{ApiUrl()}Login/SaveUser";
        }

        public string UpdateUser()
        {
            return $"{ApiUrl()}Login/UpdateUser";
        }

        public string DeleteUser(string username)
        {
            return $"{ApiUrl()}Login/DeleteUser?username=" + username;
        }

        public string GetUserRoles(string username)
        {
            return $"{ApiUrl()}Login/GetUserRoles?username=" + username;
        }

        public string GetAllDataUsers(string username)
        {
            return $"{ApiUrl()}Login/GetAllDataUser?username=" + username;
        }

        public string CheckUsername(string username)
        {
            return $"{ApiUrl()}login/CheckUsername?username=" + username;
        }

        //RIWAYAT
        public string GetAllRiwayat()
        {
            return $"{ApiUrl()}Riwayat/GetAllRiwayat";

        }

        public string GetAllDetailRiwayat(string idRiwayat)
        {
            return $"{ApiUrl()}Riwayat/GetAllDetailRiwayat?idRiwayat="+idRiwayat;

        }

        public string SaveRiwayat()
        {
            return $"{ApiUrl()}Riwayat/SaveRiwayat";
        }

        public string SaveDetailRiwayat()
        {
            return $"{ApiUrl()}Riwayat/SaveDetailRiwayat";
        }
    }
}

