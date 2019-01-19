using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.GZip;
using System.Web;
using System.Net.Sockets;
using System.Reflection;

namespace HiveOSPacker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string savepath;
        string savedirectory;
        string savefilename;
        string minerconf = "";
       
        string minername = "";
        string algo = "";


        Dictionary<string, string> coinList =
            new Dictionary<string, string>();
        Dictionary<string, string> coinListPool =
      new Dictionary<string, string>();


        private void Form1_Load(object sender, EventArgs e)
        {
            


            comboBox3.Items.Add("energiminer solo");
            comboBox3.Items.Add("energiminer pool");
            comboBox3.Items.Add("wildrig-multi");
            comboBox3.Items.Add("sgminer kind");
            comboBox3.Items.Add("t-rex");
            comboBox3.Items.Add("cryptodredge");
            comboBox3.Items.Add("zjazz");
            comboBox3.Items.Add("teamredminer");
            comboBox3.Items.Add("bminer");
            comboBox3.Items.Add("finminer");
            comboBox3.Items.Add("xmrig-amd");
            comboBox3.Items.Add("xmrig-nvidia");
            comboBox3.Items.Add("geekminercuda");
            comboBox3.Items.Add("sgminergeek");
            comboBox3.Items.Add("other miners");


            Application.DoEvents();

            comboBox1.Text = "Select Algorithm";
            var url = "https://raw.githubusercontent.com/minershive/hive-pooltemplates/master/algos.json";
            var syncClient = new WebClient();
            var content = syncClient.DownloadString(url);
            dynamic result = JsonConvert.DeserializeObject<List<RootObject>>(content);
//


            foreach (dynamic item in result)
            {

                try
                {
                    string algo = item.name;
                    comboBox1.Items.Add(algo);
                    for (int i = 0; 0 < item.coins.Count; i++)
                    {
                        coinList.Add(item.coins[i].ToString(), algo);
                    }
                }


                catch { continue; }
            }


            textBox9.Text = "";



        }

        void Form1_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

        }




        

        public void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            savedirectory = Path.GetDirectoryName(saveFileDialog1.FileName);
            savefilename = (Path.GetFileName(saveFileDialog1.FileName));
            savepath = savedirectory + savefilename;
            textBox13.Text = savepath;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            //wildrig-multi
            if (comboBox3.SelectedItem.ToString() == "wildrig-multi")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\wildrig-multi\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\wildrig-multi\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\wildrig-multi\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\wildrig-multi\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }

                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }




            }

            //sgminer
            if (comboBox3.SelectedItem.ToString() == "sgminer kind")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\sgminer\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\sgminer\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\sgminer\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\sgminer\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }

                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }



            }

            //energiminer solo
            if (comboBox3.SelectedItem.ToString() == "energiminer solo")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\energiminer\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\energiminer\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\energiminer\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\energiminer\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }









            }
            //energiminer pool
            if (comboBox3.SelectedItem.ToString() == "energiminer pool")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\energiminer\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\energiminer\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\energiminer\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\energiminer\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }



            }

            //cryptodredge
            if (comboBox3.SelectedItem.ToString() == "cryptodredge")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\cryptodredge\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\cryptodredge\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\cryptodredge\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\cryptodredge\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }
            }
            //bminer
            if (comboBox3.SelectedItem.ToString() == "bminer")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }

                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\bminer\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\bminer\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\bminer\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\bminer\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }



            }
            //finminer
            if (comboBox3.SelectedItem.ToString() == "finminer")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\finminer\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\finminer\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\finminer\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\finminer\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }
            }

            //zjazz
            if (comboBox3.SelectedItem.ToString() == "zjazz")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*",SearchOption.AllDirectories))
                    {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                    }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*",SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }
                                


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername);
                    File.Copy(@"Resources\\zjazz\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername);
                    File.Copy(@"Resources\\zjazz\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername);
                    File.Copy(@"Resources\\zjazz\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername);
                    File.Copy(@"Resources\\zjazz\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }



            }
            //geekminercuda
            if (comboBox3.SelectedItem.ToString() == "geekminercuda")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }

                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\geekminercuda\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\geekminercuda\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\geekminercuda\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\geekminercuda\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }
            }

            //global
            if (comboBox3.SelectedItem.ToString() == "other miners")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\global\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\global\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\global\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\global\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }
            }
            //sgminergeek
            if (comboBox3.SelectedItem.ToString() == "sgminergeek")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\sgminergeek\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\sgminergeek\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\sgminergeek\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\sgminergeek\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }
            }

            //t-rex
            if (comboBox3.SelectedItem.ToString() == "t-rex")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\t-rex\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\t-rex\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\t-rex\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\t-rex\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }



            }

            //teamredminer
            if (comboBox3.SelectedItem.ToString() == "teamredminer")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\teamredminer\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\teamredminer\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\teamredminer\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\teamredminer\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }
            }

            //xmrig-amd
            if (comboBox3.SelectedItem.ToString() == "xmrig-amd")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\xmrig-amd\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\xmrig-amd\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\xmrig-amd\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\xmrig-amd\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }
            }

            //xmrig-nvidia
            if (comboBox3.SelectedItem.ToString() == "xmrig-nvidia")
            {
                if (File.Exists(@textBox10.Text + "\\" + minername))
                {
                    File.Move(@textBox10.Text + "\\" + minername, @textBox10.Text + "\\" + minername + "1");
                }


                //Now Create all of the directories


                foreach (string dirPath in Directory.GetDirectories(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"));
                }

                if (!Directory.Exists(@textBox10.Text + "\\" + minername + "\\"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@textBox10.Text, "*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(@textBox10.Text, @textBox10.Text + "\\" + minername + "\\"), true);
                }


                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-config.sh"))
                {

                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\xmrig-nvidia\\h-config.sh", @textBox10.Text + "\\" + minername + "\\h-config.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-manifest.conf"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\xmrig-nvidia\\h-manifest.conf", @textBox10.Text + "\\" + minername + "\\h-manifest.conf");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-run.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\xmrig-nvidia\\h-run.sh", @textBox10.Text + "\\" + minername + "\\h-run.sh");
                }
                if (!File.Exists(@textBox10.Text + "\\" + minername + "\\h-stats.sh"))
                {
                    Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\");
                    File.Copy(@"Resources\\xmrig-nvidia\\h-stats.sh", @textBox10.Text + "\\" + minername + "\\h-stats.sh");
                }
                foreach (string file in Directory.GetFiles(@textBox10.Text + "\\" + minername + "\\", "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        
                        string text = File.ReadAllText(file);
                        text = text.Replace("changeminername", minername);
                        File.WriteAllText(file, text);
                    }
                    catch { }
                }
            }


            minerconf = textBox9.Text;

            string sifirvarmi = minerconf.Substring(0, 1);
            if (sifirvarmi == " ")
            {
                minerconf = minerconf.Substring(1, minerconf.Length - 1);
            }



            if (!File.Exists(@textBox10.Text + "\\" + minername + "\\" + minername + ".conf"))
            {
                FileStream fs2 = new FileStream((@textBox10.Text + "\\" + minername + "\\" + minername + ".conf"), FileMode.Append);
                TextWriter sw2 = new StreamWriter(fs2);
                sw2.WriteLine(minerconf);
                sw2.Close();
            }
            else
            {
                File.Delete(@textBox10.Text + "\\" + minername + "\\" + minername + ".conf");
                FileStream fs2 = new FileStream((@textBox10.Text + "\\" + minername + "\\" + minername + ".conf"), FileMode.Append);
                TextWriter sw2 = new StreamWriter(fs2);
                sw2.WriteLine(minerconf);
                sw2.Close();
            }



            ////DirectoryInfo d = new DirectoryInfo(@textBox10.Text + "\\" + minername + "\\");//Assuming Test is your Folder
            ////FileInfo[] Files = d.GetFiles("*");


            ////Directory.CreateDirectory(@textBox10.Text + "\\" + minername + "\\" + minername);
            ////foreach (FileInfo file in Files)
            ////{

            ////    File.Move(@textBox10.Text + "\\" + minername + "\\" + file.Name, @textBox10.Text + "\\" + minername + "\\" + minername + "\\" + file.Name);
            ////}

            //DirectoryInfo d1 = new DirectoryInfo(@textBox10.Text + "\\");
            //FileInfo[] Files1 = d1.GetFiles("*");


            //foreach (FileInfo file1 in Files1)
            //{

            //    File.Move(@textBox10.Text + "\\" +file1.Name, @textBox10.Text + "\\" + minername + "\\" + minername + "\\" + file1.Name);
            //}




            if (File.Exists(@textBox10.Text + "\\" + minername + "\\" + minername + "1"))
            {
                File.Move(@textBox10.Text + "\\" + minername + "\\" + minername + "1", @textBox10.Text + "\\" + minername + "\\" + minername);

            }



            CreateTar(@textBox13.Text, @textBox10.Text + "\\" + minername + "\\", @textBox10.Text + "\\" + minername + "\\");







            DirectoryInfo d2 = new DirectoryInfo(@textBox10.Text + "\\" + minername + "\\");
            FileInfo[] Files2 = d2.GetFiles("*");



            foreach (FileInfo file in Files2)
            {
                File.Delete(@textBox10.Text + "\\" + minername + "\\" + file.Name);
            }

            Directory.Delete(@textBox10.Text + "\\" + minername + "\\", true);


            if (File.Exists(@textBox10.Text + "\\" + minername + "1"))
            {
                File.Move(@textBox10.Text + "\\" + minername + "1", @textBox10.Text + "\\" + minername);
            }


            MessageBox.Show(minername + " miner archive created succesfully at " + textBox13.Text + Environment.NewLine
                + " continue with 3rd step and upload your miner package to public server ");


            progressBar1.Value = 0;


        }



        private void CreateTar(string outputTarFilename, string sourceDirectory, string firstSourceDirectory)
        {
            using (FileStream fs = new FileStream(outputTarFilename, FileMode.Create, FileAccess.Write, FileShare.None))
            using (Stream gzipStream = new GZipOutputStream(fs))
            using (TarArchive tarArchive = TarArchive.CreateOutputTarArchive(gzipStream))
            {
                AddDirectoryFilesToTar(tarArchive, sourceDirectory, true, firstSourceDirectory);
            }
        }

        private void AddDirectoryFilesToTar(TarArchive tarArchive, string sourceDirectory, bool recurse, string firstSourceDirectory)
        {
        
            if (recurse)
            {
                string[] directories = Directory.GetDirectories(sourceDirectory);
                foreach (string directory in directories)
                    AddDirectoryFilesToTar(tarArchive, directory, recurse, firstSourceDirectory);
            }

    
            string[] filenames = Directory.GetFiles(sourceDirectory);
            foreach (string filename in filenames)
            {

                string path1 = @firstSourceDirectory;
                string fullPath2 = @filename;
                string fullPath1 = Path.GetFullPath(path1);

      

                if (fullPath2.StartsWith(fullPath1, StringComparison.CurrentCultureIgnoreCase))
                {

                    string result = fullPath2.Substring(fullPath1.Length).TrimStart(Path.DirectorySeparatorChar);
                    
                    TarEntry tarEntry = TarEntry.CreateEntryFromFile(filename);

                    //tarEntry.Name = minername + "/" + Path.GetFileName(filename);

                    result = result.Replace('\\', '/');
                    if (result.EndsWith("/"))
                        result = result.Remove(result.Length - 1);


                    tarEntry.Name = minername + "/" + result;

                    tarArchive.WriteEntry(tarEntry, true);




                }


          

              
            }
        }


        public class RootObject
        {
            public string name { get; set; }
            public List<object> coins { get; set; }
            public string coin { get; set; }
        }

       


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox2.Items.Clear();
            string algoritma = comboBox1.SelectedText;

            foreach (KeyValuePair<string, string> kvp in coinList)
            {
                if (kvp.Value == algoritma)
                {
                    comboBox2.Items.Add(kvp.Key);

                }
            }

        }
   

        




        private void button2_Click(object sender, EventArgs e)
        {


        }

       
        private void button2_Click_1(object sender, EventArgs e)
        {


            folderBrowserDialog1.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox10.Text = folderBrowserDialog1.SelectedPath;

                saveFileDialog1.FileName = minername + ".tar.gz";
                Environment.SpecialFolder root = folderBrowserDialog1.RootFolder;
            }



        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = textBox10.Text;
            file.RestoreDirectory = true;
            file.Title = "Select Miner Execute File";
            file.Multiselect = false;

            if (file.ShowDialog() == DialogResult.OK)
            {
                textBox19.Text = file.FileName;
                minername = file.SafeFileName;
       
            }


        }



        private void button3_Click(object sender, EventArgs e)
        {
           
            saveFileDialog1.FileName = minername + ".tar.gz";
            saveFileDialog1.DefaultExt = ".tar.gz";
            saveFileDialog1.ShowDialog();

        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            string walletaddress = textBox2.Text;
            string pass = textBox15.Text;
            string pooladdress = textBox1.Text;
            string customconfig = textBox3.Text;
            

           
                if (comboBox1.Text != "")
            {
                algo = comboBox1.Text;
            }
            else
            {
                algo = comboBox1.SelectedItem.ToString();

            }



            comboBox3.Items.Add("other miners");
            if (comboBox3.SelectedItem.ToString() == "wildrig-multi")
            {

                textBox9.Text = "--url " + pooladdress + " --user " + walletaddress + " --algo " + algo + " --pass " + pass + " " + customconfig;
                if (textBox9.Text != "")
                {
                    Clipboard.SetText(textBox9.Text);
                }
            }
            if (comboBox3.SelectedItem.ToString() == "sgminer kind")
            {
             
                textBox9.Text = "-o " + pooladdress + " -u " + walletaddress + " -k " + algo + " -p " + pass + " " + customconfig;
                if (textBox9.Text != "")
                {
                    Clipboard.SetText(textBox9.Text);
                }
            }
            if (comboBox3.SelectedItem.ToString() == "energiminer solo")
            {

               
                textBox9.Text = "--coinbase-addr " + "\"" + walletaddress + "\""  + "--protocol " + "\"" + "http://" + pass + "@" + pooladdress + "\"" + " " + customconfig;
                if (textBox9.Text != "")
                {
                    Clipboard.SetText(textBox9.Text);
                }
            }
            if (comboBox3.SelectedItem.ToString() == "energiminer pool")
            {

             
                textBox9.Text = "stratum://" + walletaddress + "@" + pooladdress + " " + customconfig;
                if (textBox9.Text != "")
                {
                    Clipboard.SetText(textBox9.Text);
                }
            }

            if (comboBox3.SelectedItem.ToString() == "geekminercuda")
            {

                textBox3.Text = "-i 21";
                textBox9.Text = "-o " + pooladdress + " -u " + walletaddress + " -a " + algo + " -p " + pass + " " + customconfig;

                if (textBox9.Text != "")
                {
                    Clipboard.SetText(textBox9.Text);
                }
            }

            if (comboBox3.SelectedItem.ToString() == "sgminergeek")
            {

                textBox9.Text = "-o " + pooladdress + " -u " + walletaddress + " -a " + algo + " -p " + pass + " " + customconfig;

                if (textBox9.Text != "")
                {
                    Clipboard.SetText(textBox9.Text);
                }
            }


            if (comboBox3.SelectedItem.ToString() == "bminer" | comboBox3.SelectedItem.ToString() == "finminer" | comboBox3.SelectedItem.ToString() == "xmrig-amd" | comboBox3.SelectedItem.ToString() == "xmrig-nvidia")
            {

                textBox9.Text = customconfig;

                if (textBox9.Text != "")
                {
                    Clipboard.SetText(textBox9.Text);
                }
            }




            if (comboBox3.SelectedItem.ToString() == "cryptodredge" | comboBox3.SelectedItem.ToString() == "t-rex" | comboBox3.SelectedItem.ToString() == "zjazz" | comboBox3.SelectedItem.ToString() == "teamredminer")
            {
                textBox9.Text = "-o " + pooladdress + " -u " + walletaddress + " -a " + algo + " -p " + pass + " " + customconfig;

                if (textBox9.Text != "")
                {
                    Clipboard.SetText(textBox9.Text);
                }
            }







            if (comboBox3.SelectedItem.ToString() == "other miners)")
            {

                textBox9.Text = customconfig;
                if (textBox9.Text != "")
                {
                    Clipboard.SetText(textBox9.Text);
                }
            }

            



        }

  

        private void saveFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {
            savedirectory = Path.GetDirectoryName(saveFileDialog1.FileName);
            savefilename = (Path.GetFileName(saveFileDialog1.FileName));
            savepath = savedirectory +"\\"+  savefilename;
            textBox13.Text = savepath;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
       
     

        private void button6_Click(object sender, EventArgs e)
        {
 
         Clipboard.SetText(textBox16.Text);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() == "energiminer solo")
            {

                MessageBox.Show("You need to enter your local wallet user:password to pass box!!, then you need to specify your gpu related settings to custom config box!! ex: For OPENGL -G, FOR CUDA-U vb.");

                comboBox1.Visible = false;

                textBox1.Text = "yourwalletip:9796";
                textBox2.Text = "walletaddress";
                textBox15.Text = "user:pass";
                textBox3.Text = "For AMD: -G --response-timeout 10 --cl-local-work 256 // For NVIDIA: -U --response-timeout 10 --cuda-parallel-hash 8 --cuda-block-size 256";


            }
            if (comboBox3.SelectedItem.ToString() == "energiminer pool")
            {

                MessageBox.Show("You need to enter your gpu related settings to custom config box!! ex: For OPENGL -G, FOR CUDA-U vb.");
                comboBox1.Visible = false;

                textBox1.Text = "stratum.nrg-bonus.minecrypto.pro:9999";
                textBox2.Text = "user:pass";
                textBox15.Text = "";
                textBox3.Text = "For AMD: -G --response-timeout 10 --cl-local-work 256 // For NVIDIA: -U --response-timeout 10 --cuda-parallel-hash 8 --cuda-block-size 256";


            }

            if (comboBox3.SelectedItem.ToString() != "energiminer pool" & (comboBox3.SelectedItem.ToString() != "energiminer solo"))
            {
                comboBox1.Visible = true;
            }

            if (comboBox3.SelectedItem.ToString() == "bminer" | comboBox3.SelectedItem.ToString() == "finminer" | comboBox3.SelectedItem.ToString() == "xmrig-amd" | comboBox3.SelectedItem.ToString() == "xmrig-nvidia")
            {

                
                comboBox1.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox15.Visible = false;

                textBox3.Text = "write your config here";

            }
            if (comboBox3.SelectedItem.ToString() == "cryptodredge" | comboBox3.SelectedItem.ToString() == "t-rex" | comboBox3.SelectedItem.ToString() == "zjazz" | comboBox3.SelectedItem.ToString() == "teamredminer" | comboBox3.SelectedItem.ToString() == "wildrig-multi" | comboBox3.SelectedItem.ToString() == "sgminer kind" | comboBox3.SelectedItem.ToString() == "sgminergeek" | comboBox3.SelectedItem.ToString() == "sgminercuda")
            {
                textBox1.Text = "stratum+tcp://<POOL>";
                textBox2.Text = "<WALLET_ADDRESS>";
                textBox15.Text = "<OPTIONS>";
                textBox3.Text = "-i 20";
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {


            string filelocationtoupload = textBox13.Text;

            progressBar1.Value = 10;


            string value = filelocationtoupload;
            var lastModified = File.GetLastWriteTime(value);

            string value1 = minername;
           
            Random rnd = new Random();

            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string userName1 = userName;
            userName1 = new string((from c in userName1
                                    where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                                    select c
               ).ToArray());

            string time = DateTime.Now.ToString("yyyy-M-d-HH-mm-ss");
            int bas = rnd.Next(5555, 77777777);
            value1 = value1+"-"+bas;
            progressBar1.Value = 30;

            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential("ftpaccount", "ftppass");

                client.UploadFile("ftpsite" + value1 + "-" + time + ".tar.gz", WebRequestMethods.Ftp.UploadFile, value);

            }

            textBox16.Text= "ftpsite" + value1 + "-" + time+".tar.gz";
            progressBar1.Value = 100;

            MessageBox.Show(minername + " miner archive created succesfully at " + textBox13.Text + Environment.NewLine
                + "Miner Name: " + minername + Environment.NewLine
                + "Installation URL: " + "ftpsite" + value1 + "-" + time + ".tar.gz" + Environment.NewLine
                + "Hash Algorithm: " +algo + Environment.NewLine
                + "Wallet and Worker Template: " + textBox2.Text + Environment.NewLine
                + "Pool Url: " + textBox1.Text + Environment.NewLine
                + "Pass: " + textBox15.Text + Environment.NewLine
                + "Extra Config Arguments: " + textBox3.Text + Environment.NewLine);
           



        }
    }




}
