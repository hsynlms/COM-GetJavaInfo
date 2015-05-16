using System;
using Microsoft.Win32;
using System.Linq;
using System.Collections.Generic;

namespace GetJavaInfo
{
    public class Methods
    {
        public static string GetJavaInformation(Types.DataType type, string version = null)
        {
            try
            {
                string myKey = string.Empty;
                string myPathX86 = @"SOFTWARE\Wow6432Node\JavaSoft\Java Development Kit";
                string myPathX64 = @"SOFTWARE\JavaSoft\Java Development Kit";

                if (Environment.Is64BitOperatingSystem)
                {
                    using (RegistryKey reg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(myPathX64))
                    {
                        if (reg == null)
                        {
                            using (RegistryKey reg2 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(myPathX86))
                            {
                                if (reg2 == null)
                                {
                                    myKey = string.Empty;
                                }
                                else
                                {
                                    switch (type)
                                    {
                                        case Types.DataType.Path:
                                            var subkey = string.Empty;

                                            if (version == null)
                                                subkey = (string)reg2.GetValue("CurrentVersion");
                                            else
                                                subkey = version;

                                            using (var reg3 = reg2.OpenSubKey(subkey))
                                            {
                                                if (reg2 == null)
                                                {
                                                    myKey = string.Empty;
                                                }
                                                else
                                                {
                                                    myKey = (string)reg3.GetValue("JavaHome");
                                                }
                                            }

                                            break;
                                        case Types.DataType.Version:
                                            myKey = (string)reg2.GetValue("CurrentVersion");

                                            break;
                                        default:
                                            myKey = string.Empty;

                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            switch (type)
                            {
                                case Types.DataType.Path:
                                    var subkey = string.Empty;

                                    if (version == null)
                                        subkey = (string)reg.GetValue("CurrentVersion");
                                    else
                                        subkey = version;

                                    using (var reg2 = reg.OpenSubKey(subkey))
                                    {
                                        if (reg2 == null)
                                        {
                                            myKey = string.Empty;
                                        }
                                        else
                                        {
                                            myKey = (string)reg2.GetValue("JavaHome");
                                        }
                                    }

                                    break;
                                case Types.DataType.Version:
                                    myKey = (string)reg.GetValue("CurrentVersion");

                                    break;
                                default:
                                    myKey = string.Empty;

                                    break;
                            }
                        }
                    }
                }
                else
                {
                    using (RegistryKey reg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(myPathX86))
                    {
                        if (reg == null)
                        {
                            myKey = string.Empty;
                        }
                        else
                        {
                            switch (type)
                            {
                                case Types.DataType.Path:
                                    var subkey = string.Empty;

                                    if (version == null)
                                        subkey = (string)reg.GetValue("CurrentVersion");
                                    else
                                        subkey = version;

                                    using (var reg2 = reg.OpenSubKey(subkey))
                                    {
                                        if (reg2 == null)
                                        {
                                            myKey = string.Empty;
                                        }
                                        else
                                        {
                                            myKey = (string)reg2.GetValue("JavaHome");
                                        }
                                    }

                                    break;
                                case Types.DataType.Version:
                                    myKey = (string)reg.GetValue("CurrentVersion");

                                    break;
                                default:
                                    myKey = string.Empty;

                                    break;
                            }
                        }
                    }
                }

                if (myKey == string.Empty)
                    return null;
                else
                    return myKey;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string[] GetAllJavaList(Types.DataType type)
        {
            try
            {
                string[] myKey = new string[] { };
                string myPathX86 = @"SOFTWARE\Wow6432Node\JavaSoft\Java Development Kit";
                string myPathX64 = @"SOFTWARE\JavaSoft\Java Development Kit";

                if (Environment.Is64BitOperatingSystem)
                {
                    using (RegistryKey reg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(myPathX64))
                    {
                        //64bit registry inceleniyor
                        if (reg == null)
                        {
                            //eger 64bit registry null donerse, 32bit registry deneniyor
                            using (RegistryKey reg2 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(myPathX86))
                            {
                                if (reg2 == null)
                                {
                                    myKey = null;
                                }
                                else
                                {
                                    //32bit registry incelemesi basarili
                                    switch (type)
                                    {
                                        case Types.DataType.Path:
                                            List<string> list = new List<string>();

                                            foreach (var item in reg2.GetSubKeyNames().Where(r => r.LastIndexOf('.') == 1))
                                            {
                                                using (var reg3 = reg2.OpenSubKey(item))
                                                {
                                                    if (reg3 != null)
                                                    {
                                                        list.Add((string)reg3.GetValue("JavaHome"));
                                                    }
                                                }
                                            }

                                            myKey = list.ToArray();

                                            break;
                                        case Types.DataType.Version:
                                            myKey = reg2.GetSubKeyNames().Where(r => r.LastIndexOf('.') == 1).ToArray();

                                            break;
                                        default:
                                            myKey = null;

                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //64 bit registry incelemesi basarili
                            switch (type)
                            {
                                case Types.DataType.Path:
                                    List<string> list = new List<string>();

                                    foreach (var item in reg.GetSubKeyNames().Where(r => r.LastIndexOf('.') == 1))
                                    {
                                        using (var reg3 = reg.OpenSubKey(item))
                                        {
                                            if (reg3 != null)
                                            {
                                                list.Add((string)reg3.GetValue("JavaHome"));
                                            }
                                        }
                                    }

                                    myKey = list.ToArray();

                                    break;
                                case Types.DataType.Version:
                                    myKey = reg.GetSubKeyNames().Where(r => r.LastIndexOf('.') == 1).ToArray();

                                    break;
                                default:
                                    myKey = null;

                                    break;
                            }
                        }
                    }
                }
                else
                {
                    using (RegistryKey reg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(myPathX86))
                    {
                        if (reg == null)
                        {
                            myKey = null;
                        }
                        else
                        {
                            //32 bit registry incelemesi basarili
                            switch (type)
                            {
                                case Types.DataType.Path:
                                    List<string> list = new List<string>();

                                    foreach (var item in reg.GetSubKeyNames().Where(r => r.LastIndexOf('.') == 1))
                                    {
                                        using (var reg3 = reg.OpenSubKey(item))
                                        {
                                            if (reg3 != null)
                                            {
                                                list.Add((string)reg3.GetValue("JavaHome"));
                                            }
                                        }
                                    }

                                    myKey = list.ToArray();

                                    break;
                                case Types.DataType.Version:
                                    myKey = reg.GetSubKeyNames().Where(r => r.LastIndexOf('.') == 1).ToArray();

                                    break;
                                default:
                                    myKey = null;

                                    break;

                            }
                        }
                    }
                }

                return myKey;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
