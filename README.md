# fprog-wordcount

To run this program on Windows Subsystem for Linux (WSL) 2, you will need to have the .NET runtime and a C# compiler installed on your WSL 2 instance. Here are the steps you can follow to run the program on WSL 2:

---
## If needed
---

Open a terminal in WSL 2 and install the .NET runtime by running the following command:

1. sudo apt-get update
2. sudo apt-get install dotnet-runtime-3.1
3. Install a C# compiler, such as mono, by running the following command:

``` bash
sudo apt-get install mono-devel
```

---
## Execution
---

* Clone repository and navigate to it
* Build the project:

``` bash
dotnet build
```

* Run the program by specifying the directory path and file extension as command line arguments:

``` bash
dotnet run /path/to/directory .cpp
```

This will execute the program and print the results to the terminal.
