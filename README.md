# Wallpaper Swapper
Created : August 2016

# About
This was the first native application I made in C# (aside from Unity development). It was a simple program designed to swap the desktop wallpaper on my computer to a random choice from my library of pictures. 

It also can add a contextual menu option when right clicking on the desktop by adding a registry key.

# Utilizing Wallpaper Swapper
### Prerequisites
* Windows PC
* Visual Studio
* Collection of Wallpapers
### Initial Setup
The source for Wallpaper Swapper is fairly specific to my computer, however the source can easily be modified to fit yours. In the `Wallpaper Swapper/Program.cs` file, simply change the `ROOT_DIRECTORY` variable to the root directory of where your wallpapers are located. 
### Running on Start
Simply build the project using Visual Studio, then add the resultant `.exe` file to your startup applications.
