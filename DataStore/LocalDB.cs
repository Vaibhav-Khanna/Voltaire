using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;

using JsonRpc.CoreCLR.Client;
using OdooRpc.CoreCLR.Client.Models.Parameters;

namespace voltaire.DataStore
{
    public class LocalDB
    {

        const string Url_EndPoint = "https://ltd.integration.groupevoltaire.com/jsonrpc";
        const string UserName = "matisya";
        const string Password = "matsiya";

        public static async Task LoginToOdoo()
        {
            try
            {
                var OdooRpcC = new JsonRpcWebClient(new Uri(Url_EndPoint));

                var odooVersion = await OdooRpcC.InvokeAsync<dynamic>("login", new Dictionary<string, string>()
                {
                    { "username", UserName },
                    { "password", Password },
                { "db","v10_mdb"}

                });

                if(odooVersion.Result!=null)
                {
                    
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

     
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static void ShutDown()
        {
            BlobCache.Shutdown().Wait();
        }




    }
}
