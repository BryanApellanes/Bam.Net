/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Configuration;

namespace Bam.Net
{
    /// <summary>
    /// A file based
    /// IPC mechanism that uses a single file
    /// with a binary formatted copy of an instance of T 
    /// in the directory RootDirectory with the name specified.
    /// </summary>
    public class IpcMessage: Loggable
    {
        internal IpcMessage(string name, Type messageType)
        {
            this.Name = name;           
            this.LockTimeout = 75;
            this.MessageType = messageType;
        }

        internal IpcMessage(string name, Type messageType, string rootDir)
            : this(name, messageType)
        {
            this.RootDirectory = rootDir ?? RuntimeSettings.AppDataFolder;
        }
        
        public static IpcMessage Get<T>(string name, string rootDirectory = null)
        {
            return Get(name, typeof(T), rootDirectory);
        }

        /// <summary>
        /// Gets a message with the specified name creating it 
        /// if necessary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="messageType"></param>
        /// <param name="rootDirectory"></param>
        /// <returns></returns>
        public static IpcMessage Get(string name, Type messageType, string rootDirectory = null)
        {
            if (!Exists(name, messageType, rootDirectory, out IpcMessage result))
            {
                result = Create(name, messageType, rootDirectory);
            }

            return result;
        }

        public static IpcMessage Create<T>(string name)
        {
            return Create(name, typeof(T));
        }

        public static IpcMessage Create(string name, Type type)
        {
            return Create(name, type, null);
        }

        public static IpcMessage Create(string name, Type type, string rootDirectory = null)
        {
            IpcMessage result = new IpcMessage(name, type, rootDirectory);

            if (File.Exists(result.MessageFile))
            {
                throw new InvalidOperationException("The specified {0}.Name is already in use"._Format(typeof(IpcMessage).Name));
            }

            return result;
        }

        public static bool Exists<T>(string name)
        {
            return (Exists<T>(name, out IpcMessage ignore));
        }

        public static bool Exists<T>(string name, out IpcMessage result)
        {
            result = new IpcMessage(name, typeof(T));
            return File.Exists(result.MessageFile);
        }

        public static bool Exists(string name, Type messageType, out IpcMessage result)
        {
            result = new IpcMessage(name, messageType);
            return File.Exists(result.MessageFile);
        }

        public static bool Exists(string name, Type messageType, string rootDirectory)
        {
            return Exists(name, messageType, rootDirectory, out IpcMessage ignore);
        }

        public static bool Exists(string name, Type messageType, string rootDirectory, out IpcMessage result)
        {
            result = new IpcMessage(name, messageType, rootDirectory);
            return File.Exists(result.MessageFile);
        }

        public static void Delete(string name, Type type, string rootDir = null)
        {
            IpcMessage toDelete = new IpcMessage(name, type, rootDir);
            if (Directory.Exists(toDelete.RootDirectory))
            {
                Directory.Delete(toDelete.RootDirectory, true);
            }
        }

        public string Name { get; set; }

        public virtual bool Write(object data)
        {
            if(AcquireLock(LockTimeout))
            {
                // if the message file doesn't exist write to it
                string writeTo = MessageFile;
                if (File.Exists(MessageFile))
                {
                    //  else write to the WriteFile
                    writeTo = WriteFile;
                }

                data.ToBinaryFile(writeTo);

                // if WriteFile exists move it on top of MessageFile
                if (File.Exists(WriteFile))
                {
                    File.Delete(MessageFile);
                    File.Move(WriteFile, MessageFile);
                }

                // copy MessageFile to ReadFile
                File.Copy(MessageFile, ReadFile, true);
                File.Move(LockFile, TempLockFile);
                File.Delete(TempLockFile);
                return true;
            }

            return false;
        }
        
        public T Read<T>()
        {            
            if (File.Exists(ReadFile))
            {
                return ReadFile.FromBinaryFile<T>();
            }

            return default(T);
        }

        /// <summary>
        /// The number of milliseconds to wait to 
        /// try and acquire a lock
        /// </summary>
        public int LockTimeout
        {
            get;
            set;
        }

        public Type MessageType
        {
            get;
            set;
        }

        string _rootDirectory;
        object _rootDirectoryLock = new object();
        protected internal string RootDirectory
        {
            get
            {
                return _rootDirectoryLock.DoubleCheckLock(ref _rootDirectory, () =>
                {
					return Path.Combine(RuntimeSettings.AppDataFolder, MessageType.Name);
                });
            }
            set
            {
				_rootDirectory = Path.Combine(value, MessageType.Name);
            }
        }

        [Verbosity(VerbosityLevel.Warning, MessageFormat="{Name}:Unable to acquire lock:{LastExceptionMessage}")]
        public event EventHandler AcquireLockException;
      
        protected void OnAcquireLockException(Exception ex)
        {
            if (AcquireLockException != null)
            {
                LastExceptionMessage = "PID={0}:{1}"._Format(Process.GetCurrentProcess().Id, ex.Message);
                AcquireLockException(this, new EventArgs());
            }
        }

		[Verbosity(VerbosityLevel.Warning, MessageFormat = "PID {CurrentLockerId} has lock on {Name}")]
        public event EventHandler WaitingForLock;

        protected void OnWaitingForLock()
        {
            WaitingForLock?.Invoke(this, new EventArgs());
        }

        public string LastExceptionMessage { get; set; }

        /// <summary>
        /// Gets the process id of the process who has 
        /// the lock
        /// </summary>
        public string CurrentLockerId { get; set; }

        public string CurrentLockerMachineName { get; set; }

        protected string LockFile
        {
            get
            {
                return Path.Combine(RootDirectory, "{0}.lock"._Format(Name));
            }
        }

        protected string TempLockFile
        {
            get
            {
                return $"{LockFile}.tmp";
            }
        }

        protected internal string WriteFile
        {
            get
            {
                return Path.Combine(RootDirectory, "{0}.write"._Format(Name));
            }
        }

        protected internal string ReadFile
        {
            get
            {
                return Path.Combine(RootDirectory, "{0}.read"._Format(Name));
            }
        }

        protected internal string MessageFile
        {
            get
            {
                return Path.Combine(RootDirectory, Name);
            }
        }

        private void EnsureRoot()
        {
            if (!Directory.Exists(RootDirectory))
            {
                Directory.CreateDirectory(RootDirectory);
            }
        }

        static object _lock = new object();
        private bool AcquireLock(int timeoutInMilliseconds)
        {
            try
            {
                lock (_lock)
                {
                    EnsureRoot();
                    IpcMessageLockInfo lockInfo = new IpcMessageLockInfo();
                    bool timeoutExpired = Exec.TakesTooLong(() =>
                    {
                        bool logged = false;
                        while (File.Exists(LockFile))
                        {
                            if (!logged)
                            {
                                logged = true;
                                IpcMessageLockInfo currentLockInfo = LockFile.FromBinaryFile<IpcMessageLockInfo>();
                                CurrentLockerId = currentLockInfo?.ProcessId.ToString();
                                CurrentLockerMachineName = currentLockInfo?.MachineName;
                                OnWaitingForLock();
                            }

                            Thread.Sleep(100);
                        }
                        return LockFile;
                    }, (lockFile) =>
                    {
                        lockInfo.ToBinaryFile(lockFile);
                        return lockFile;
                    }, TimeSpan.FromMilliseconds(timeoutInMilliseconds));

                    return !timeoutExpired;
                }
            }
            catch (Exception ex)
            {
                OnAcquireLockException(ex);
                return false;
            }
        }
        
    }
}
