using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.ServiceProxy;
using System.Linq;
using System.Linq.Expressions;

namespace Bam.Net.Server
{
    /// <summary>
    /// Modified from http://stackoverflow.com/questions/8466703/httplistener-and-file-upload
    /// </summary>
    public class HttpPostedFile
    {
        public HttpPostedFile(Encoding encoding, string boundary, Stream input)
        {
            Encoding = encoding;
            Boundary = boundary;
            InputStream = input;
            QueryString = new Dictionary<string, string>();
        }

        public static HttpPostedFile FromRequest(IRequest request)
        {
            HttpPostedFile file = new HttpPostedFile(request.ContentEncoding, GetBoundary(request), request.InputStream);
            foreach(string key in request.QueryString.Keys)
            {
                file.QueryString.Add(key, request.QueryString[key]);
            }
            return file;
        }

        private static string GetBoundary(IRequest request)
        {
            return "--" + request.ContentType.Split(';')[1].Split('=')[1];
        }

        public Dictionary<string, string> QueryString
        {
            get;
            private set;
        }
        
        public string TempPath
        {
            get;
            private set;
        }
        public string FullPath
        {
            get;
            internal set;
        }
        public string FileName
        {
            get; set;
        }
        public string FormInputName
        {
            get; set;
        }
        public string ContentType
        {
            get; set;
        }

        public void ReadMeta()
        {
            MemoryStream copy = CopyInputStream();
            string dispo = string.Empty;
            using (StreamReader sr = new StreamReader(copy))
            {
                string discard = sr.ReadLine();
                dispo = sr.ReadLine();
                ContentType = sr.ReadLine().DelimitSplit(":")[1];
            }
            string[] meta = dispo.DelimitSplit(":")[1].DelimitSplit(";");
            FormInputName = meta[1].DelimitSplit("=")[1].TruncateFront(1).Truncate(1);
            FileName = meta[2].DelimitSplit("=")[1].TruncateFront(1).Truncate(1);
        }

        protected internal void Save(string path)
        {
            if(string.IsNullOrEmpty(TempPath))
            {
                TempPath = path;
                Encoding enc = Encoding;
                string boundary = Boundary;                
                InputStream = CopyInputStream();
                Stream input = InputStream;
                byte[] boundaryBytes = enc.GetBytes(boundary);
                int boundaryLen = boundaryBytes.Length;

                using (FileStream output = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[1024];
                    int len = input.Read(buffer, 0, 1024);
                    int startPos = -1;

                    // Find start boundary
                    while (true)
                    {
                        if (len == 0)
                        {
                            throw new InvalidOperationException("Start Boundaray Not Found");
                        }

                        startPos = IndexOf(buffer, len, boundaryBytes);
                        if (startPos >= 0)
                        {
                            break;
                        }
                        else
                        {
                            Array.Copy(buffer, len - boundaryLen, buffer, 0, boundaryLen);
                            len = input.Read(buffer, boundaryLen, 1024 - boundaryLen);
                        }
                    }

                    // Skip four lines (Boundary, Content-Disposition, Content-Type, and a blank)
                    for (int i = 0; i < 4; i++)
                    {
                        while (true)
                        {
                            if (len == 0)
                            {
                                throw new InvalidOperationException("Preamble not Found.");
                            }

                            startPos = Array.IndexOf(buffer, enc.GetBytes("\n")[0], startPos);
                            if (startPos >= 0)
                            {
                                startPos++;
                                break;
                            }
                            else
                            {
                                len = input.Read(buffer, 0, 1024);
                            }
                        }
                    }

                    Array.Copy(buffer, startPos, buffer, 0, len - startPos);
                    len = len - startPos;

                    while (true)
                    {
                        int endPos = IndexOf(buffer, len, boundaryBytes);
                        if (endPos >= 0)
                        {
                            if (endPos > 0) output.Write(buffer, 0, endPos);
                            break;
                        }
                        else if (len <= boundaryLen)
                        {
                            throw new InvalidOperationException("End Boundaray Not Found");
                        }
                        else
                        {
                            output.Write(buffer, 0, len - boundaryLen);
                            Array.Copy(buffer, len - boundaryLen, buffer, 0, boundaryLen);
                            len = input.Read(buffer, boundaryLen, 1024 - boundaryLen) + boundaryLen;
                        }
                    }
                }
                ReadMeta();
            }
        }

        private MemoryStream CopyInputStream()
        {
            if (InputStream.CanSeek)
            {
                InputStream.Seek(0, SeekOrigin.Begin);
            }
            MemoryStream copy = new MemoryStream();
            InputStream.CopyTo(copy);
            copy.Seek(0, SeekOrigin.Begin);
            return copy;
        }

        protected Encoding Encoding { get; private set; }
        protected string Boundary { get; private set; }
        protected Stream InputStream { get; private set; }

        private static int IndexOf(byte[] buffer, int len, byte[] boundaryBytes)
        {
            for (int i = 0; i <= len - boundaryBytes.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < boundaryBytes.Length && match; j++)
                {
                    match = buffer[i + j] == boundaryBytes[j];
                }

                if (match)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
