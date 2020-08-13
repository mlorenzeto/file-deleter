using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileDeleterService.Core.Command
{
    public class FileDeleterCommand : ICommand
    {
        private LoggerService _logger = new LoggerService("File_Deleter");
        public void Execute()
        {
            try
            {
                Configuration config = Configuration.Instance();

                string folder = config.Get("Folder");

                int maxQuantityOfDays = Convert.ToInt32(config.Get("MaxQuantityOfDays"));

                DateTime minDateToDelete = DateTime.Now.AddDays(-maxQuantityOfDays);

                DirectoryInfo directoryInfo = new DirectoryInfo(folder);

                List<FileInfo> files = directoryInfo.GetFiles().Where(x => x.CreationTime < minDateToDelete).ToList();

                if (files.Count > 0)
                {
                    _logger.Info("DELETER", $"Preparing to delete {files.Count} files");

                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                    }

                    _logger.Info("DELETER", "The files were deleted with success");
                }
                else
                {
                    _logger.Info("DELETER", "Weren't find any file to delete");
                }
                
            }
            catch (Exception ex)
            {
                _logger.Error("DELETER", "Error during the process to remove files");
            }
        }

        public string GetName()
        {
            return "FileDeleter";
        }
    }
}
