# CycleFC
an Experimental C# NES / Famicom emulator

![CycleFC Banner Logo](https://media.discordapp.net/attachments/1160298927879368734/1161292439970123806/Banner.png?ex=6537c4dd&is=65254fdd&hm=75a5394870a0de073ee1dc96ed34e4815fe7570a9f3c62078ce35458149a0163&=&width=1237&height=493)

## What is this
This is a fork of the original MyNES emulator created by Ala Hadid in C# from the original 2009 source, it is now has been updated and will be maintained under the name CycleFC with modern components. The application is now currently running on .NET 7, with a lot of features currently being developed to experiment with. With the original vision of the original author codebase, this project aims for accuracy.

![CycleFC running Kirby's Adventure ROM](https://media.discordapp.net/attachments/1160298927879368734/1161362869179388086/image.png?ex=65380675&is=65259175&hm=c8d0efcb1dccbece9a71b076c004fd3805731dff3776e0769b8cf68091772b9e&=&width=655&height=618)

## What's in this emulator:
- VRC6 and VRC7 Support
- Save State support under (*.esav) file format
- Battery RAM support
- Direct3D Renderer
- Near-exact accuracy cycle emulation core
- Audio recorder
- Screenshot
- Palette Editor
- Audio Channels Mixer

## To-do:
- Currently, Archive file codes are removed and expected to be added back soon.
- VRC7 FM Support
- More controller input API
- Recent files are being added back soon.
- Add more renderers including OpenGL, and Vulkan, and replace the deprecated (SlimDX with SharpDX for the current DirectX 9, and to support DirectX 11)
- Add more audio output API options to replace DirectSound (Such as NAudio, XAudio2, etc...)
- x64 support
- Emulation core improve
- Game Genie support
- Port to macOS, Linux, and possibly other devices support .NET runtime.

## LICENSE
CycleFC is using GNU General Public License v3.

CycleFC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/


## This project uses the following:
- [SlimDX (for DirectSound Wrapper)](https://github.com/SlimDX/slimdx) (Note: Is now deprecated)
- [NESCartDb for NES / Famicom Cartridge Database](https://nescartdb.com/)
- [NESDev Wiki](https://www.nesdev.org/wiki/Nesdev_Wiki)
- SevenZip

# Special Thanks
- Ala Hadid (ahdsoftware@gmail.com) - for creating the original
