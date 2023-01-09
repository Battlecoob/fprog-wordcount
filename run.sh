#!/bin/bash
# program commands
cd wordcount || exit

echo "Building Project..."
dotnet build

path_text="Enter the desired path [../wordlists]:"
read -r -p "$path_text" path

if [ -z "$path" ]
then
    path="../wordlists"
fi

extension_text="Enter the desired file extenstion [.txt]:"
read -r -p "$extension_text" extension

if [ -z "$extension" ]
then
    extension=".txt"
fi

echo "Executing wordcount for '$extension' files in '$path'"

read -r -p "Press enter to start"

dotnet run "$path" "$extension"

cd ..