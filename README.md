# GetJavaInfo
This library allows you to get jdk information, for example to get latest installed version or path, to get list of all installed versions and their paths.

You can also query if specified version that given as a paremeter installed. If it was installed, returns path otherwise it will return null.

## Methods
All methods that this library contains listed below:

####CurrentJavaVersion()
This method returns current installed jdk version lastly as string.

```
string currentJdkVersion = GetJavaInfo.General.CurrentJavaVersion();
```

####CurrentJavaPath()
This method returns current installed jdk path (where located on pc) lastly as string.

```
string currentJdkPath = GetJavaInfo.General.CurrentJavaPath();
```

####WasItInstalled(string version)
This method returns specified version that given as parameter on the computer. If specified version exists, returns path of it, otherwise will return null.

```
string path17 = GetJavaInfo.General.WasItInstalled("1.7");
```

####GetAllVersion()
This method returns all installed jdk versions as a string array.

```
string[] jdkVersionList = GetJavaInfo.General.GetAllVersion();
```

####GetAllPath()
This method returns all installed jdk paths as a string array.

```
string[] jdkPathList = GetJavaInfo.General.GetAllPath();
```
