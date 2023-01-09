# fprog-wordcount

To run this program on Windows Subsystem for Linux (WSL) 2, you will need to have the .NET runtime and a C# compiler installed on your WSL 2 instance. Here are the steps you can follow to run the program on WSL 2:

## If needed

Open a terminal in WSL 2 and install the .NET runtime by running the following command:

1. update

``` bash
sudo apt-get update
```

2. install dotnet

``` bash
sudo apt-get install -y dotnet-sdk-6.0
```

3. Install a C# compiler, such as mono, by running the following command:

``` bash
sudo apt-get install mono-devel
```

## Execution (shell script)

* Clone repository and navigate to it
* Execute the "run.sh" shell script

``` bash
./run.sh
```

* Maybe execution rights need to be set for the scripot with the bash command:

``` bash
chmod +x run.sh
```

* The script will ask twice for user input
  
``` bash
Enter the desired path [../wordlists]: # ../wordlists would be the default
Enter the desired file extenstion [.txt]: # .txt would be the default
```

After collecting the user input, wordcount will be executed and the results will be printed to the terminal.

## Execution (manual)

* Clone repository and navigate to it
* Build the project:

``` bash
dotnet build
```

* Run the program by specifying the directory path and file extension as command line arguments:

``` bash
dotnet run /path/to/directory .file-extension
```

This will execute the program and print the results to the terminal.
