# 🦴 X-Ray Explorer VR - Unity project

<p align="right">
  Developed by: <a href="https://github.com/AlexAzumi">Alejandro Suárez 🪐</a>
</p>

X-Ray Explorer VR is a virtual reality application which can be used as a learning tool about the human skeleton.

![Screenshot](screenshot.png)
<p align='center'>
  <i>Screenshot of the application in Unity Engine</i>
</p>

This project implements the OpenXR runtime, which means that can be executed in almost every VR headset, including exporting it as a standalone application for Meta Quest devices.

The user can interact directly with the human skeleton, taking in apart and scan the bones to get useful information.

## Features

* Compatibility with most VR headsets
* Easy to use interface
* 206 named bones for reference (tagged with floating tooltips)
* Human skeleton that can be disassembled in 11 parts
  * Cranium, spinal cord, rib cage, both hands, both upper limbs, both lower limbs and foot
* Voice recognition with Vosk to ask questions
* Uses Gemini Flash and Deepseek to respond the user questions
* Connects to a spectator web page that shows more useful information
