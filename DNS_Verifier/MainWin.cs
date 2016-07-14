using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace DNS_Verifier
{
    public partial class MainWin : Form
    {
        private string[] LocalDnsServer = new string[2];
        private List<string> GoogleDns = new List<string>{ "8.8.8.8", "8.8.4.4" }; //No blocking of any sites, but uses DNSSEC
        private List<string> FreeDNSDns = new List<string> { "37.235.1.174", "37.235.1.177" }; //http://freedns.zone/en/why/   <-- Claims to not do any dns redirects, and does no logging
        private List<string> OpenDNSDns = new List<string> { "208.67.222.222", "208.67.220.220" }; //http://http://208.69.38.170/ or https://www.opendns.com/home-internet-security/ <-- Popular free public dns
        private List<string> level3Dns = new List<string> { "209.244.0.3", "209.244.0.4", "4.2.2.1", "4.2.2.2", "4.2.2.3", "4.2.2.4" }; //No known blocking of any site, used by many ISPs themselves
        private List<string> ListOfSites = new List<string>();
        private List<List<string>> defaultRList = new List<List<string>>(), googleRList = new List<List<string>>(), freeDnsRList = new List<List<string>>(),
            level3RList = new List<List<string>>(), openDnsRList = new List<List<string>>(), customRList = new List<List<string>>();
        private string[] resultsArray = new string[6];
        private bool doneGoogle = false, doneDefault = false, doneFreeDns = false, doneOpenDns = false, doneLevel3 = false, doneCustom = false, DebugOn = false;
        private int GoogleIndex = 0, FreeDnsIndex = 0, OpenDnsIndex = 0, Level3Index = 0;

        public MainWin()
        {
            InitializeComponent();
        }

        public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {//http://stackoverflow.com/questions/3669970/compare-two-listt-objects-for-equality-ignoring-order
            //This compares two lists at once against each other
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }

        private string CompileResults()
        {
            Checked options = Checked.None;
            List<bool> TestResults = new List<bool>();
            string returnString = "DNS Test Results:" + Environment.NewLine + Environment.NewLine;

            if (level3CB.Checked) options = options | Checked.Level3;
            if (freeDnsCB.Checked) options = options | Checked.FreeDns;
            if (openDnsCB.Checked) options = options | Checked.OpenDns;
            if (customCB.Checked) options = options | Checked.Custom;

            //cLookup[options].DynamicInvoke();

            for (int ListIndex = 0; ListIndex < defaultRList.Count; ListIndex++)
            {
                switch (options)
                {
                    case Checked.Level3:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], level3RList[ListIndex]));
                        break;

                    case Checked.OpenDns:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], openDnsRList[ListIndex]));
                        break;

                    case Checked.FreeDns:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], freeDnsRList[ListIndex]));
                        break;

                    case Checked.Custom:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], customRList[ListIndex]));
                        break;

                    case Checked.L_O:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], level3RList[ListIndex], openDnsRList[ListIndex]));
                        break;

                    case Checked.L_F:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], level3RList[ListIndex], freeDnsRList[ListIndex]));
                        break;

                    case Checked.L_C:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], level3RList[ListIndex], customRList[ListIndex]));
                        break;

                    case Checked.F_O:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], freeDnsRList[ListIndex], openDnsRList[ListIndex]));
                        break;

                    case Checked.F_C:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], freeDnsRList[ListIndex], customRList[ListIndex]));
                        break;

                    case Checked.C_O:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], customRList[ListIndex], openDnsRList[ListIndex]));
                        break;

                    case Checked.L_O_C:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], level3RList[ListIndex], openDnsRList[ListIndex], customRList[ListIndex]));
                        break;

                    case Checked.L_F_C:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], level3RList[ListIndex], freeDnsRList[ListIndex], customRList[ListIndex]));
                        break;

                    case Checked.L_F_O:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], level3RList[ListIndex], freeDnsRList[ListIndex], openDnsRList[ListIndex]));
                        break;

                    case Checked.F_C_O:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], freeDnsRList[ListIndex], customRList[ListIndex], openDnsRList[ListIndex]));
                        break;

                    case Checked.All:
                        TestResults.Add(CompareResults(defaultRList[ListIndex], googleRList[ListIndex], freeDnsRList[ListIndex], customRList[ListIndex], openDnsRList[ListIndex], level3RList[ListIndex]));
                        break;
                }
            }

            for (int SiteIndex = 0; SiteIndex < ListOfSites.Count; SiteIndex++)
            {
                string startText = "For the site, \"" + ListOfSites[SiteIndex] + "\", there is ";
                startText += (TestResults[SiteIndex]) ? "no" : "an";
                startText += " anomaly with the DNS test results. Their responses ";
                startText += (TestResults[SiteIndex]) ? "match." : "do not match.";
                startText += Environment.NewLine;

                returnString += startText;
            }

            return returnString;
        }

        private bool CompareResults(List<string> defaultResult, List<string> result2, List<string> result3, List<string> result4, List<string> result5, List<string> result6)
        {
            bool DefaultN2 = ScrambledEquals<string>(defaultResult, result2);
            bool DefaultN3 = ScrambledEquals<string>(defaultResult, result3);
            bool DefaultN4 = ScrambledEquals<string>(defaultResult, result4);
            bool DefaultN5 = ScrambledEquals<string>(defaultResult, result5);
            bool DefaultN6 = ScrambledEquals<string>(defaultResult, result6);

            bool ReturnCheck = (DefaultN2 && DefaultN3 && DefaultN4 && DefaultN5 && DefaultN6) ? true : false;

            return ReturnCheck;
        }

        private bool CompareResults(List<string> defaultResult, List<string> result2, List<string> result3, List<string> result4, List<string> result5)
        {
            bool DefaultN2 = ScrambledEquals<string>(defaultResult, result2);
            bool DefaultN3 = ScrambledEquals<string>(defaultResult, result3);
            bool DefaultN4 = ScrambledEquals<string>(defaultResult, result4);
            bool DefaultN5 = ScrambledEquals<string>(defaultResult, result5);

            bool ReturnCheck = (DefaultN2 && DefaultN3 && DefaultN4 && DefaultN5) ? true : false;

            return ReturnCheck;
        }

        private bool CompareResults(List<string> defaultResult, List<string> result2, List<string> result3, List<string> result4)
        {
            bool DefaultN2 = ScrambledEquals<string>(defaultResult, result2);
            bool DefaultN3 = ScrambledEquals<string>(defaultResult, result3);
            bool DefaultN4 = ScrambledEquals<string>(defaultResult, result4);

            bool ReturnCheck = (DefaultN2 && DefaultN3 && DefaultN4) ? true : false;

            return ReturnCheck;
        }

        private bool CompareResults(List<string> defaultResult, List<string> result2, List<string> result3)
        {
            bool DefaultN2 = ScrambledEquals<string>(defaultResult, result2);
            bool DefaultN3 = ScrambledEquals<string>(defaultResult, result3);

            bool ReturnCheck = (DefaultN2 && DefaultN3) ? true : false;

            return ReturnCheck;
        }

        private List<string> ExtractIPv4(string sInput)
        {
            List<string> tempStore = new List<string>();
            string tempString = sInput;
            string tempProcess = "";
            int intNewLine = 0;
            while (tempString.Contains("\r\n") || tempString.Contains("\t") || IsValidIP(tempString))
            {
                if (tempString.Contains("Addresses: ") || tempString.Contains("Address: "))
                {

                    int IndexOfAddresses = (tempString.Contains("Addresses: ")) ? tempString.IndexOf("Addresses: ") : tempString.IndexOf("Address: ");

                    tempString = (tempString.Contains("Addresses: ")) ? tempString.Substring(IndexOfAddresses + "Addresses: ".Length) : tempString.Substring(IndexOfAddresses + "Address: ".Length);
                }
                if (tempString.Contains("\r\n"))
                    intNewLine = tempString.IndexOf("\r\n");
                else if (tempString.Contains("\t"))
                    intNewLine = tempString.IndexOf("\t") + 3;
                else
                    intNewLine = tempString.Length;

                tempProcess = (tempString.Substring(0, intNewLine)).Trim();
                if (IsValidIP(tempProcess))
                {
                    tempStore.Add(tempProcess);
                    tempString = tempString.Substring(intNewLine);
                }
                else if (tempProcess == Environment.NewLine || tempProcess == String.Empty || tempProcess == "\t  " || tempProcess == " ")
                { tempString = tempString.Substring(intNewLine); }
                else
                    return tempStore;
                tempString = tempString.Replace(Environment.NewLine, String.Empty);
            }

            return tempStore;
        }

        private string RemoveIPv6(string sInput)
        {
            //string pattern = @"(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))";
            //That is one looooong regex! From: http://stackoverflow.com/a/17871737/3472690

            string pattern = @"(?:^|(?<=\s))(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))(?=\s|$)";
            //Provided by vks, on stackoverflow: http://stackoverflow.com/a/32368136/3472690

            //if (IsCompressedIPv6(sInput))
              //  sInput = UncompressIPv6(sInput);
            string output = Regex.Replace(sInput, pattern, "");
            if (output.Contains("Addresses"))
                output = output.Substring(0, "Addresses: ".Length);

            return output;
        }

        private bool IsValidIP(string addr)
        {
            //create our match pattern
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
            bool valid = false;
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            return valid;
        }

        private bool IsCompressedIPv6(string sInput)
        {
            return sInput.Contains("::");
        }

        private string UncompressIPv6(string sInput)
        {
            int IndexOfCompressed = sInput.IndexOf("::");
            string tempString2 = sInput.Substring(IndexOfCompressed + 1);
            string tempString1 = sInput.Substring(0, IndexOfCompressed + 1);

            return tempString1 + "0" + tempString2;
        }

        private string RemoveAllSpaces(string inputHere)
        {
            return string.Join("", inputHere.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)); //kernowcode at
            //http://stackoverflow.com/questions/6219454/efficient-way-to-remove-all-whitespace-from-string claims it to be faster over
            //return Regex.Replace(inputHere, @"\s+", "");
        }

        private string ChangeCharacter(string inputHere, string charIn, string RemoveThis)
        {
            return Regex.Replace(inputHere, @"\" + RemoveThis + "+", charIn);
        }

        private string LookupUrl(string urlHere, string dnsServer, string iDentifier)
        {
            //string caller = iDentifier;  <--- This function was mainly used for debugging, to check which backgroundworker was accessing this.
            //                                  Turns out, they are all random and almost all over the place
            return LookupUrl(urlHere, dnsServer);
        }

        private string LookupUrl(string urlHere, string dnsServer)
        {
            int LinesRead = 1;
            Process nslookup = new Process()
            {
                StartInfo = new ProcessStartInfo("nslookup")
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            nslookup.Start();
            nslookup.StandardInput.WriteLine(urlHere + " " + dnsServer);
            nslookup.StandardInput.WriteLine("exit");
            string output = "";
            string garbagecan = "";
            string tempRead = "";
            string lastRead = "";
            bool StartRead = false;
            using (StreamReader reader = nslookup.StandardOutput)
            {
                while (reader.Peek() != -1)
                {
                    tempRead = reader.ReadLine();
                    if (tempRead.Contains("Addresses") && !StartRead)
                        StartRead = true;

                    if (LinesRead > 3)
                    {
                        
                        tempRead = RemoveIPv6(tempRead);

                        if (tempRead.Contains("Addresses"))
                            output += tempRead;
                        else if (lastRead.Contains("Addresses") && tempRead != "\t  ")
                            output += tempRead.Trim() + Environment.NewLine;
                        else if (tempRead == Environment.NewLine || tempRead == "> " || tempRead == String.Empty || tempRead == "\t  ")
                        { }
                        else
                            output += tempRead + Environment.NewLine;
                        if (!(tempRead == Environment.NewLine || tempRead == "> " || tempRead == String.Empty || tempRead == "\t  "))
                            lastRead = tempRead;
                    }
                    else
                        garbagecan = reader.ReadLine();
                    LinesRead++;
                }
            }
            return output;
        }

        private string LookupUrl(string urlHere)
        {
            int LinesRead = 1;
            Process nslookup = new Process()
            {
                StartInfo = new ProcessStartInfo("nslookup")
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            nslookup.Start();
            nslookup.StandardInput.WriteLine(urlHere);
            nslookup.StandardInput.WriteLine("exit");
            string output = "";
            string garbagecan = "";
            string tempRead = "";
            string lastRead = "";
            using (StreamReader reader = nslookup.StandardOutput)
            {
                while (reader.Peek() != -1)
                {
                    if (LinesRead > 6)
                    {
                        tempRead = reader.ReadLine();
                        tempRead = RemoveIPv6(tempRead);

                        if (tempRead.Contains("Addresses"))
                            output += tempRead;
                        else if (lastRead.Contains("Addresses") && tempRead != "\t  ")
                            output += tempRead.Trim() + Environment.NewLine;
                        else if (tempRead == Environment.NewLine || tempRead == "> " || tempRead == String.Empty || tempRead == "\t  ")
                        { }
                        else
                            output += tempRead + Environment.NewLine;
                        if (!(tempRead == Environment.NewLine || tempRead == "> " || tempRead == String.Empty || tempRead == "\t  "))
                            lastRead = tempRead;
                    }
                    else
                        garbagecan = reader.ReadLine();
                    LinesRead++;
                }
            }
            return output;
        }

        private string RemoveNewLines(string inputHere)
        {
            return Regex.Replace(inputHere, @"\t|\n|\r", "");
        }

        private string[] ReturnIPAddress(string sInput)
        {
            string rPattern = @"\[(\d+\.\d+\.\d+\.\d+)\]";
            //MatchCollection tempMC = Regex.Matches(sInput, rPattern);
            return Regex.Matches(sInput, rPattern).OfType<Match>().Select(m => m.Groups[0].Value).ToArray();
        }

        private void ipTextBox_Leave(object sender, EventArgs e)
        {
            string tempCheck = RemoveAllSpaces(ipTextBox.Text);
            if (!IsValidIP(tempCheck))
            {
                ipTextBox.Clear();
                MessageBox.Show("The DNS address you provided, " + tempCheck + ", is not in a valid IPv4 format!", "Invalid Format");
            }
        }

        [Flags]
        enum Checked
        {
            None = 0,

            Level3 = 1,
            FreeDns = 2,
            OpenDns = 4,
            Custom = 8,

            L_F = Level3 | FreeDns,
            L_O = Level3 | OpenDns,
            L_C = Level3 | Custom,
            F_O = FreeDns | OpenDns,
            F_C = FreeDns | Custom,
            C_O = Custom | OpenDns,

            L_F_C = Level3 | FreeDns | Custom,
            L_F_O = Level3 | FreeDns | OpenDns,
            L_O_C = Level3 | OpenDns | Custom,
            F_C_O = FreeDns | Custom | OpenDns,

            All = Level3 | FreeDns | OpenDns | Custom
        }

        private string GetCurrentDNS()
        {

            Process nslookup = new Process()
            {
                StartInfo = new ProcessStartInfo("nslookup")
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            nslookup.Start();
            nslookup.StandardInput.WriteLine("exit");
            StreamReader reader = nslookup.StandardOutput;
            

            string output = reader.ReadToEnd();

            string FirstOutput = RemoveNewLines(output);
            int temp1 = "Default Server:".Length;
            int temp2 = FirstOutput.IndexOf("Address:") - temp1;
            FirstOutput = FirstOutput.Substring(temp1 , temp2);// - "Address:".Length);
            LocalDnsServer[0] = FirstOutput = RemoveAllSpaces(FirstOutput);

            string FinalOutput = output.Substring(output.IndexOf("Address: ") + "Address:".Length);
            FinalOutput = ChangeCharacter(FinalOutput, "", "\r\n\r\n>");
            LocalDnsServer[1] = FinalOutput = RemoveAllSpaces(FinalOutput);

            //FinalOutput = ChangeCharacter(FinalOutput, "_", "/");
            return FirstOutput + Environment.NewLine + FinalOutput;
        }

        private void customCB_CheckedChanged(object sender, EventArgs e)
        {
            OneAtLeast(sender);
            ipTextBox.Visible = customCB.Checked;
        }

        private void getDnsButton_Click(object sender, EventArgs e)
        {
            dnsTextBox.Text = GetCurrentDNS();
            //DisplayDnsAddresses(); Not working as intended
        }

        private void MainWin_Load(object sender, EventArgs e)
        {
            dnsTextBox.Text = GetCurrentDNS();
            ReadIndexesForDnsLists();
            LoadPopularSites();
        }

        private void ReadIndexesForDnsLists()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\" + "conf.ini"))
                {
                    while (reader.Peek() != -1)
                    {
                        string tempRead = reader.ReadLine();
                        if (tempRead.Contains("GoogleDnsIndex="))
                        {
                            int testParse = int.Parse(tempRead.Substring("GoogleDnsIndex=".Length));

                            if (testParse > 1)
                            {
                                while (testParse > 1)
                                {
                                    testParse -= 2;
                                }
                            }

                            GoogleIndex = testParse;
                        }
                        else if (tempRead.Contains("FreeDnsIndex="))
                        {
                            int testParse = int.Parse(tempRead.Substring("FreeDnsIndex=".Length));

                            if (testParse > 1)
                            {
                                while (testParse > 1)
                                {
                                    testParse -= 2;
                                }
                            }

                            FreeDnsIndex = testParse;
                        }
                        else if (tempRead.Contains("OpenDnsIndex="))
                        {
                            int testParse = int.Parse(tempRead.Substring("OpenDnsIndex=".Length));

                            if (testParse > 1)
                            {
                                while (testParse > 1)
                                {
                                    testParse -= 2;
                                }
                            }

                            OpenDnsIndex = testParse;
                        }
                        else if (tempRead.Contains("Level3DnsIndex="))
                        {
                            int testParse = int.Parse(tempRead.Substring("Level3DnsIndex=".Length));

                            if (testParse > 5)
                            {
                                while (testParse > 5 || testParse< 0)
                                {
                                    testParse -= 6;
                                }
                            }

                            Level3Index = testParse;
                        }
                        else if (tempRead.Contains("Debugging="))
                        {
                            DebugOn = bool.Parse(tempRead.Substring("Debugging=".Length));
                        }
                    }
                }
            }
            catch
            {
                GoogleIndex = FreeDnsIndex = Level3Index = OpenDnsIndex = 0;
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (LoadPopularSites() == "failed")
                MessageBox.Show("The program is unable to find a list of popular sites to test with, under the filename \"popularsites.pst\", with each site being on a separate line.", "Error loading list");
        }

        private string LoadPopularSites()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\" + "freedns.psl"))
                {
                    FreeDNSDns.Clear();
                    while (reader.Peek() != -1)
                    {
                        FreeDNSDns.Add(reader.ReadLine());
                    }
                }
            }
            catch
            { }

            try
            {
                using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\" + "level3dns.psl"))
                {
                    level3Dns.Clear();
                    while (reader.Peek() != -1)
                    {
                        level3Dns.Add(reader.ReadLine());
                    }
                }
            }
            catch
            { }

            try
            {
                using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\" + "opendns.psl"))
                {
                    OpenDNSDns.Clear();
                    while (reader.Peek() != -1)
                    {
                        OpenDNSDns.Add(reader.ReadLine());
                    }
                }
            }
            catch
            { }

            try
            {
                string target = Directory.GetCurrentDirectory() + @"\" + "popularsites.psl";

                using (StreamReader reader = new StreamReader(target))
                {
                    while (reader.Peek() != -1)
                    {
                        ListOfSites.Add(reader.ReadLine());
                    }

                    statusLabel.BackColor = Color.LightGreen;
                    statusLabel.ForeColor = Color.Gray;
                    statusLabel.Text = "Loaded";
                    loadButton.Enabled = !(startButton.Enabled = true);
                    return "success";
                }
            }
            catch 
            {
                statusLabel.BackColor = Color.IndianRed;
                statusLabel.ForeColor = Color.LightGray;
                statusLabel.Text = "Unloaded";
                loadButton.Enabled = !(startButton.Enabled = false);
                return "failed";
            }
        }

        private void dnsCB_CheckedChanged(object sender, EventArgs e)
        {
            OneAtLeast(sender);
        }

        private void OneAtLeast(object originalSender)
        {
            CheckBox tempCB = (CheckBox)originalSender;
            if (!level3CB.Checked && !openDnsCB.Checked && !customCB.Checked && !freeDnsCB.Checked)
            {
                tempCB.Checked = true;
                MessageBox.Show("You must select at least one option!", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<string> ExtractSubList(List<List<string>> ListToExtractFrom, int listIndex)
        {
            return ListToExtractFrom[listIndex];
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            resultTextBox.Clear();

            defaultRList.Clear();
            googleRList.Clear();
            freeDnsRList.Clear();
            level3RList.Clear();
            openDnsRList.Clear();
            customRList.Clear();

            bgWorkerDefault.RunWorkerAsync();
            bgWorkerGoogle.RunWorkerAsync();

            doneDefault = doneCustom = false;
            if (freeDnsCB.Checked)
            {
                doneFreeDns = false;
                bgWorkerFreeDns.RunWorkerAsync();
            }
            else
                doneFreeDns = true;

            if (level3CB.Checked)
            {
                doneOpenDns = false;
                bgWorkerOpenDns.RunWorkerAsync();
            }
            else
                doneOpenDns = true;

            if (openDnsCB.Checked)
            {
                doneLevel3 = false;
                bgWorkerLevel3.RunWorkerAsync();
            }
            else
                doneLevel3 = true;

            if (customCB.Checked)
            {
                doneCustom = false;
                bgWorkerCustom.RunWorkerAsync();
            }
            else
                doneCustom = true;

            resultTimer.Start();

            this.Text += " - Running";
            getDnsButton.Enabled = startButton.Enabled = false;

            foreach (Control c in dnsGroupBox.Controls)
            {
                c.Enabled = false;
            }
        }

        private int GetNumberOfCheckboxesChecked()
        {
            int NumberofCheckBoxesChecked = 0;

            foreach (Control c in dnsGroupBox.Controls)
            {
                if ((c is CheckBox) && ((CheckBox)c).Checked)
                    NumberofCheckBoxesChecked++;
            }

            return NumberofCheckBoxesChecked;
        }

        private string[] GetNamesOfCheckboxesChecked()
        {
            List<string> NamesofCheckboxesChecked = new List<string>();

            foreach (Control c in dnsGroupBox.Controls)
            {
                if ((c is CheckBox) && ((CheckBox)c).Checked)
                    NamesofCheckboxesChecked.Add(c.Name);
            }

            return NamesofCheckboxesChecked.ToArray();
        }

        private void bgWorkerDefault_DoWork(object sender, DoWorkEventArgs e)
        {
            //We first lookup the sites using the default dns settings
            //string result = LookupUrl("abc.xyz", LocalDnsServer[1]);

            string result = "";
            string StoreResult = "";

            var sublist = ListOfSites;
            List<string> subIPList = new List<string>();
            foreach (var value in sublist)
            {
                result = /*value + ":" + Environment.NewLine + "--------------------" + Environment.NewLine + */LookupUrl(value, LocalDnsServer[1], "default") + Environment.NewLine;
                StoreResult += result;
                subIPList = ExtractIPv4(result);
                //subIPList.Add("--------------------");
                if (subIPList.Count != 0)
                    defaultRList.Add(subIPList);
            }


            resultsArray[0] = "Default DNS:" + Environment.NewLine + "--------------------" + Environment.NewLine + StoreResult;
        }

        private void bgWorkerGoogle_DoWork(object sender, DoWorkEventArgs e)
        {
            //Here we start looking up the sites using the google dns
            //string result = LookupUrl("abc.xyz", GoogleDns[0]);

            string result = "";
            string StoreResult = "";

            var sublist = ListOfSites;
            List<string> subIPList = new List<string>();
            foreach (var value in sublist)
            {
                result = /*value + ":" + Environment.NewLine + "--------------------" + Environment.NewLine + */LookupUrl(value, GoogleDns[GoogleIndex], "google") + Environment.NewLine;
                StoreResult += result;
                subIPList = ExtractIPv4(result);
                //subIPList.Add("--------------------");
                googleRList.Add(subIPList);
            }

            resultsArray[1] = "Google DNS:" + Environment.NewLine + "--------------------" + Environment.NewLine + StoreResult;    
        }

        private void resultTimer_Tick(object sender, EventArgs e)
        {
            if (doneDefault && doneGoogle && doneFreeDns && doneOpenDns && doneLevel3 && doneCustom)
            {

                resultTextBox.Text = CompileResults() + Environment.NewLine + "Individual responses for the DNS test:" + Environment.NewLine + "-------------------------------------" + Environment.NewLine;

                resultTimer.Stop();

                foreach (string value in resultsArray)
                {
                    if (value != String.Empty || value != null)
                    {
                        resultTextBox.Text += value + Environment.NewLine;
                    }
                }

                
                //Used to test if the extracting function is working properly
                int intListIndex = 0;
                string msgOut = "";
                if (DebugOn)
                {
                    List<List<string>> tempListArray = new List<List<string>>();
                    bool ContinueRead = false;

                    while (intListIndex < 5)
                    {
                        tempListArray.Clear();
                        ContinueRead = false;

                        if (intListIndex == 0)
                        {
                            tempListArray = defaultRList;
                            ContinueRead = true;
                        }
                        else if (intListIndex == 1)
                        {
                            tempListArray = googleRList;
                            ContinueRead = true;
                        }
                        else if (intListIndex == 2 && freeDnsCB.Checked)
                        {
                            tempListArray = freeDnsRList;
                            ContinueRead = true;
                        }
                        else if (intListIndex == 3 && level3CB.Checked)
                        {
                            tempListArray = level3RList;
                            ContinueRead = true;
                        }
                        else if (intListIndex == 4 && openDnsCB.Checked)
                        {
                            tempListArray = openDnsRList;
                            ContinueRead = true;
                        }
                        else if (intListIndex == 5 && customCB.Checked)
                        {
                            tempListArray = customRList;
                            ContinueRead = true;
                        }

                        if (ContinueRead)
                        {
                            foreach (var sublist in tempListArray)
                            {
                                foreach (var value in sublist)
                                {
                                    if (value != String.Empty || value != null)
                                    {
                                        msgOut += value + Environment.NewLine;
                                    }
                                }
                                msgOut += "----------------------------------" + Environment.NewLine;
                            }
                        }
                        intListIndex++;
                    }

                }

                this.Text = "DNS Verifier";
                getDnsButton.Enabled = startButton.Enabled = true;

                foreach (Control c in dnsGroupBox.Controls)
                {
                    c.Enabled = true;
                }

                if (DebugOn)
                    IPList.Show(msgOut, "Debug - Extracted IPv4 Addresses");
            }
        }

        private void bgWorkerDefault_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            doneDefault = true;
        }

        private void bgWorkerGoogle_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            doneGoogle = true;
        }

        private void bgWorkerFreeDns_DoWork(object sender, DoWorkEventArgs e)
        {
            //Here we start looking up the sites using the google dns
            //string result = LookupUrl("abc.xyz", FreeDNSDns[0]);

            string result = "";
            string StoreResult = "";

            var sublist = ListOfSites;
            List<string> subIPList = new List<string>();
            foreach (var value in sublist)
            {
                result = /*value + ":" + Environment.NewLine + "--------------------" + Environment.NewLine + */LookupUrl(value, FreeDNSDns[FreeDnsIndex], "freedns") + Environment.NewLine;
                StoreResult += result;
                subIPList = ExtractIPv4(result);
                //subIPList.Add("--------------------");
                freeDnsRList.Add(subIPList);
            }

            resultsArray[2] = "FreeDNS:" + Environment.NewLine + "--------------------" + Environment.NewLine + StoreResult;
        }

        private void bgWorkerFreeDns_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            doneFreeDns = true;
        }

        private void bgWorkerCustom_DoWork(object sender, DoWorkEventArgs e)
        {
            //Here we start looking up the sites using the google dns
            //string result = LookupUrl("abc.xyz", ipTextBox.Text);

            string result = "";
            string StoreResult = "";

            var sublist = ListOfSites;
            List<string> subIPList = new List<string>();
            foreach (var value in sublist)
            {
                result = /*value + ":" + Environment.NewLine + "--------------------" + Environment.NewLine + */LookupUrl(value, ipTextBox.Text, "custom") + Environment.NewLine;
                StoreResult += result;
                subIPList = ExtractIPv4(result);
                //subIPList.Add("--------------------");
                customRList.Add(subIPList);
            }
            resultsArray[5] = "Custom DNS:" + Environment.NewLine + "--------------------" + Environment.NewLine + StoreResult;
        }

        private void bgWorkerCustom_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            doneCustom = true;
        }

        private void bgWorkerOpenDns_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            doneOpenDns = true;
        }

        private void bgWorkerLevel3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            doneLevel3 = true;
        }

        private void bgWorkerOpenDns_DoWork(object sender, DoWorkEventArgs e)
        {
            //Here we start looking up the sites using the google dns
            //string result = LookupUrl("abc.xyz", level3Dns[0]);

            string result = "";
            string StoreResult = "";

            var sublist = ListOfSites;
            List<string> subIPList = new List<string>();
            foreach (var value in sublist)
            {
                result = /*value + ":" + Environment.NewLine + "--------------------" + Environment.NewLine + */LookupUrl(value, level3Dns[Level3Index], "level3") + Environment.NewLine;
                StoreResult += result;
                subIPList = ExtractIPv4(result);
                //subIPList.Add("--------------------");
                level3RList.Add(subIPList);
            }

            resultsArray[3] = "Level 3 DNS:" + Environment.NewLine + "--------------------" + Environment.NewLine + StoreResult;
        }

        private void bgWorkerLevel3_DoWork(object sender, DoWorkEventArgs e)
        {
            //Here we start looking up the sites using the google dns
            //string result = LookupUrl("abc.xyz", OpenDNSDns[0]);

            string result = "";
            string StoreResult = "";

            var sublist = ListOfSites;
            List<string> subIPList = new List<string>();
            foreach (var value in sublist)
            {
                result = /*value + ":" + Environment.NewLine + "--------------------" + Environment.NewLine + */LookupUrl(value, OpenDNSDns[OpenDnsIndex], "OpenDNS") + Environment.NewLine;
                StoreResult += result;
                subIPList = ExtractIPv4(result);
                //subIPList.Add("--------------------");
                openDnsRList.Add(subIPList);
            }

            resultsArray[4] = "OpenDNS:" + Environment.NewLine + "--------------------" + Environment.NewLine + StoreResult;
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program is created by: Katie Delaney" + Environment.NewLine + Environment.NewLine + "Version: 1.0", "About This Program", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void generateConfigFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateGeneralConfig();
            GeneratePopularSites();
            GenerateFreeDnsList();
            GenerateLevel3DnsList();
            GenerateOpenDnsList();
        }

        private void GenerateLevel3DnsList()
        {
            try
            {
                string target = Directory.GetCurrentDirectory() + "\\level3dns.psl";

                using (StreamWriter s_w = new StreamWriter(target))
                {
                    s_w.WriteLine("209.244.0.3");
                    s_w.WriteLine("209.244.0.4");
                    s_w.WriteLine("4.2.2.1");
                    s_w.WriteLine("4.2.2.2");
                    s_w.WriteLine("4.2.2.3");
                    s_w.WriteLine("4.2.2.4");
                }
            }
            catch { }
        }

        private void GenerateOpenDnsList()
        {
            try
            {
                string target = Directory.GetCurrentDirectory() + "\\opendns.psl";

                using (StreamWriter s_w = new StreamWriter(target))
                {
                    s_w.WriteLine("208.67.222.222");
                    s_w.WriteLine("208.67.220.220");
                }
            }
            catch { }
        }

        private void GenerateFreeDnsList()
        {
            try
            {
                string target = Directory.GetCurrentDirectory() + "\\freedns.psl";

                using (StreamWriter s_w = new StreamWriter(target))
                {
                    s_w.WriteLine("37.235.1.174");
                    s_w.WriteLine("37.235.1.177");
                }
            }
            catch { }
        }

        private void GeneratePopularSites()
        {
            try
            {
                string target = Directory.GetCurrentDirectory() + "\\popularsites.psl";

                using (StreamWriter s_w = new StreamWriter(target))
                {
                    s_w.WriteLine("msn.com");
                    s_w.WriteLine("yahoo.com");
                    s_w.WriteLine("bbc.co.uk");
                }
            }
            catch { }
        }

        private void GenerateGeneralConfig()
        {
            try
            {
                string target = Directory.GetCurrentDirectory() + "\\conf.ini";

                using (StreamWriter s_w = new StreamWriter(target))
                {
                    //s_w.WriteLine("Firstname \t Lastname \t Age");
                    s_w.WriteLine("# You can set this value to either 1 or 0");
                    s_w.WriteLine("# Google DNS by default has these two dns servers:");
                    s_w.WriteLine("# 8.8.8.8             =   0");
                    s_w.WriteLine("# 8.8.4.4             =   1");
                    s_w.WriteLine("GoogleDnsIndex=0");
                    s_w.WriteLine("");
                    s_w.WriteLine("# You can set this value to either 1 or 0");
                    s_w.WriteLine("# FreeDNS by default has these two dns servers");
                    s_w.WriteLine("# 37.235.1.174    =   0");
                    s_w.WriteLine("# 37.235.1.177    =   1");
                    s_w.WriteLine("FreeDnsIndex=0");
                    s_w.WriteLine("");
                    s_w.WriteLine("# You can set this value to either 1 or 0");
                    s_w.WriteLine("# OpenDNS by default has these two dns servers");
                    s_w.WriteLine("# 208.67.222.222   =  0");
                    s_w.WriteLine("# 208.67.220.220   =  1");
                    s_w.WriteLine("OpenDnsIndex=0");
                    s_w.WriteLine("");
                    s_w.WriteLine("# You can set this value to from 5 to 0");
                    s_w.WriteLine("# Level3 DNS by default has these six dns servers");
                    s_w.WriteLine("# 209.244.0.3         =  0");
                    s_w.WriteLine("# 209.244.0.4         =  1");
                    s_w.WriteLine("# 4.2.2.1                 =  2");
                    s_w.WriteLine("# 4.2.2.2                 =  3");
                    s_w.WriteLine("# 4.2.2.3                 =  4");
                    s_w.WriteLine("# 4.2.2.4                 =  5");
                    s_w.WriteLine("Level3DnsIndex=0");
                    s_w.WriteLine("");
                    s_w.WriteLine("# Set to either true to enable basic debugging, or false to disable basic debugging");
                    s_w.WriteLine("Debugging=false");
                }
            }
            catch { }
        }

        private void generatePopularSitesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneratePopularSites();
        }
    }
}
