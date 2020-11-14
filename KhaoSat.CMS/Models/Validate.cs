
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KhaoSat.CMS.Models
{
    public class Validate
    {
        public static bool Check(string response)
        {
            //string Response = HttpContext.Current.Request.QueryString["g-recaptcha-response"];//Getting Response String Append to Post Method
            bool Valid = false;
            //Request to Google Server
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
            ("https://www.google.com/recaptcha/api/siteverify?secret=6Ld61-AZAAAAAPX_ia709OV7orb8xIV6ZhrEY4sE" + " &response=" + response);
            try
            {
                //Google recaptcha Response
                using (WebResponse wResponse = req.GetResponse())
                {

                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();                       
                        MyObject data = JsonConvert.DeserializeObject<MyObject>(jsonResponse);// Deserialize Json

                        Valid = Convert.ToBoolean(data.success);
                    }
                }

                return Valid;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
    }
    public class MyObject
    {
        public string success { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }

    }
}
