using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Sis.Ges.Doc.Frontend
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        public class PreparedUpload
        {
            public string TempFilePath { get; set; }
            public string FinalFilePath { get; set; }
            public string HashedFileName { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];
                string nombreArchivoHash = SaveUploadedFile(file, context.Server);

                context.Response.ContentType = "application/json";
                context.Response.Write("{\"mensaje\": \"Archivo subido exitosamente.\", \"archivo\": \"" + HttpUtility.JavaScriptStringEncode(nombreArchivoHash) + "\"}");
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Write("No se seleccionó ningún archivo.");
            }
        }

        public static string SaveUploadedFile(HttpPostedFile file, HttpServerUtility server)
        {
            if (file == null || file.ContentLength == 0)
                throw new ArgumentException("A file must be provided.", nameof(file));

            if (server == null)
                throw new ArgumentNullException(nameof(server));

            string uploadsFolder = server.MapPath("~/Uploads/");
            Directory.CreateDirectory(uploadsFolder);

            string originalFileName = Path.GetFileName(file.FileName);
            string baseName = Path.GetFileNameWithoutExtension(originalFileName);
            string extension = Path.GetExtension(originalFileName);
            string tempFileName = Guid.NewGuid().ToString("N") + extension;
            string tempFilePath = Path.Combine(uploadsFolder, tempFileName);

            file.SaveAs(tempFilePath);

            string sha256Hash = MetadataGeneration(tempFilePath);
            string nombreArchivoHash = string.Format("{0}_{1}{2}", baseName, sha256Hash, extension);
            string finalFilePath = Path.Combine(uploadsFolder, nombreArchivoHash);

            if (File.Exists(finalFilePath))
                File.Delete(finalFilePath);

            File.Move(tempFilePath, finalFilePath);

            return nombreArchivoHash;
        }

        public static PreparedUpload PrepareUploadedFileForRegistration(HttpPostedFile file, HttpServerUtility server)
        {
            if (file == null || file.ContentLength == 0)
                throw new ArgumentException("A file must be provided.", nameof(file));

            if (server == null)
                throw new ArgumentNullException(nameof(server));

            string uploadsFolder = server.MapPath("~/Uploads/");
            string tempFolder = Path.Combine(uploadsFolder, "Temp", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);
            Directory.CreateDirectory(uploadsFolder);

            string originalFileName = Path.GetFileName(file.FileName);
            string tempFilePath = Path.Combine(tempFolder, originalFileName);

            file.SaveAs(tempFilePath);

            string sha256Hash = MetadataGeneration(tempFilePath);
            string baseName = Path.GetFileNameWithoutExtension(originalFileName);
            string extension = Path.GetExtension(originalFileName);
            string nombreArchivoHash = string.Format("{0}_{1}{2}", baseName, sha256Hash, extension);
            string finalFilePath = Path.Combine(uploadsFolder, nombreArchivoHash);

            return new PreparedUpload
            {
                TempFilePath = tempFilePath,
                FinalFilePath = finalFilePath,
                HashedFileName = nombreArchivoHash
            };
        }

        public static void CompletePreparedUpload(PreparedUpload upload)
        {
            if (upload == null)
                throw new ArgumentNullException(nameof(upload));

            if (File.Exists(upload.FinalFilePath))
                File.Delete(upload.FinalFilePath);

            File.Move(upload.TempFilePath, upload.FinalFilePath);

            string tempFolder = Path.GetDirectoryName(upload.TempFilePath);
            if (!string.IsNullOrEmpty(tempFolder) && Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }

        public static void DeletePreparedUpload(PreparedUpload upload)
        {
            if (upload == null)
                return;

            string tempFolder = Path.GetDirectoryName(upload.TempFilePath);
            if (!string.IsNullOrEmpty(tempFolder) && Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }

        public static string MetadataGeneration(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("filePath must be provided.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found.", filePath);

            using (SHA256 sha256 = SHA256.Create())
            using (FileStream stream = File.OpenRead(filePath))
            {
                byte[] hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
