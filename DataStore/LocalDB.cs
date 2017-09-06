using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;

namespace voltaire.DataStore
{
    public static class LocalDB
    {

        public static void SaveFile(string key,byte[] file)
        {
             BlobCache.LocalMachine.Insert(key, file);
        }

        public static async Task<byte[]> GetFile(string key)
        {
            try
            {
                var file = await BlobCache.LocalMachine.Get(key);
                return file;
            }
            catch(KeyNotFoundException)
            {
                return null;
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
