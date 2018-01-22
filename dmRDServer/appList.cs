using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace dm {
    class appList {
                
        public class appListItem{
            public string section="";
            public string title="";
            public string path="";
            public string launchCode="";

            public void app() {
            }
        }

        public appList(string filename) {
            loadApplicationList(filename);
        }

        public List<appListItem> list = new List<appListItem>();

        public appListItem getApp(string launchCode) {
            foreach(appListItem appItem in list){
                if (appItem.launchCode == launchCode) return appItem;
            }

            return null;
        }
        public void loadApplicationList(string appFilename) {
            list.Clear();
            CultureInfo provider = new CultureInfo("en-US");
            string[] lines = System.IO.File.ReadAllLines(appFilename);
            appListItem newApp = null;
            
            foreach (string line in lines) {
                string nline = line.Trim();
                if (nline == "") continue;                                 //No empty lines
                if (line[0] == '[') {                                      //new section...
                    if (newApp != null) list.Add(newApp);               //if a previous secion is here... add it to the list
                    newApp = new appListItem();                           //create new item to work with.
                    newApp.section = nline.Substring(1, nline.Length - 2);
                    continue;
                }
                string[] tokens = nline.Split('=');
                if (tokens.Count() < 2) continue;            //if there are less than 2 pieces... bug off..
                switch (tokens[0].Trim().ToLower()) {
                    case "title": newApp.title = tokens[1].Trim(); break;
                    case "path": newApp.path = tokens[1].Trim().ToLower(); break;
                    case "launchcode": newApp.launchCode = tokens[1].Trim().ToLower(); break;
                }
            }
            if (newApp != null) list.Add(newApp);               //if a previous secion is here... add it to the list
            
        }//end load applist
    }
}
