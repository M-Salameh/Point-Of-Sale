using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;

namespace Web.WebServices
{
    public class ServerAssetsDisposal: IServerAssetsDisposal
    {
        private readonly IWebHostEnvironment _environment;

        public ServerAssetsDisposal(IWebHostEnvironment environment)
        {
            this._environment = environment;
        }


        public Boolean DeleteImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new FileNotFoundException("Invalid file name");
            }

            var wwwrootPath = _environment.WebRootPath;

            var filePath = Path.Combine(wwwrootPath, fileName);

            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                throw new FieldAccessException("There Are Some Problems When Try To Delete The File { " + fileName+ " }"
                    +"Error Message { "+ ex.Message+" }");
            }
            return true;
        }
    }
}
