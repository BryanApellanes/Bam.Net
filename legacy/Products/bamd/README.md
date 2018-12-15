# BAM Application Management Daemon

The Bam Application Management Daemon (Bamd) is a Windows service used to run and
monitor processes defined in BamDaemonProcess.json.  

## DaemonProcesses.json

The DaemonProcesses.json file defines an array of BamDaemonProcess instances
that are run by Bamd.  The file is assumed to be in the "conf" folder found in the 
**ContentRoot** folder where **ContentRoot** is defined in the app.config.  If
**ContentRoot** is not specified in the app.config the value "C:\bam" is used
and DaemonProcesses.json is assumed to be at the path "C:\bam\conf\DaemonProcesses.json".

```
[
  {
    "Name": "bambotjs",
    "FileName": "C:\\bam\\tools\\node.exe",
    "Arguments": "bambot.js",
    "WorkingDirectory": "C:\\bam\\sys\\bamjs\\bambotjs"
  }
]
```

## Processes
Each process defined in BamDaemonProcess.json should be written such that they do not 
immediately exit and are intended to be long running processes that perform service operations.

## Output
All standard output is redirected to {ContentRoot}\logs\{Process.Name}_out.txt and 
all error output is redirected to {ContentRoot}\logs\{Process.Name}_err.txt (intelligently 
concatenated using Path.Combine).

Note that some processes will output to StandardError when outputting to StandardOutput
would seem more intuitive, for example, git status messages are directed to StandardError.

