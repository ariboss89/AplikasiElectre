using System;
using Android.Content;
using Android.Preferences;

namespace SPKElectre.Helper
{
	public class AppPreferences
	{
        private ISharedPreferences nameSharedPrefs;
        private ISharedPreferencesEditor namePrefsEditor; //Declare Context,Prefrences name and Editor name  
        private Context mContext;

        [Obsolete]
        public AppPreferences(Context context)
        {
            this.mContext = context;
            nameSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            namePrefsEditor = nameSharedPrefs.Edit();
        }

        public void saveIP(string ipAddress) // Save data Values  
        {
            namePrefsEditor.PutString("ip", ipAddress);
            namePrefsEditor.Commit();
        }

        public void saveRole(string role) // Save data Values  
        {
            namePrefsEditor.PutString("role", role);
            namePrefsEditor.Commit();
        }

        public string getAccessKey(string key) // Return Get the Value  
        {
            return nameSharedPrefs.GetString(key, "");
        }

        public void deleteAccessKey() // Save data Values  
        {
            namePrefsEditor.Clear();
            namePrefsEditor.Commit();
        }
    }
}

