# ZeDMD Updater v2

![image](https://github.com/user-attachments/assets/c74d5826-159f-4964-8052-d071522e7542)

## Overview

Here is the new ZeDMD Updater v2 completely rewritten from scratch and using Markus Kalkbrenner [libzedmd](https://github.com/PPUC/libzedmd).
The ZeDMD code is now compatible with both the original ESP32 and the ESP32 S3 N16R8, so this updater propose installation for both these devices.
The updater can take into account 1 WiFi ZeDMD, but it must be connected via USB too, as the used flashing tool (esptool) doesn't accept any other method.

## Connections

### Original ESP32

![ESP32](https://github.com/user-attachments/assets/76de0bd9-c888-4dba-8f51-d97536ac9b1a)

Connect your device via the microUSB plug.  
Many of these devices have an erratic behavior for the connection at flash time. If when you start flashing the device, you have such a screen, then the window closes and you have an error message:

![image](https://github.com/user-attachments/assets/9cbb9666-8c7d-4363-b078-edab730ae149)

you need to "play" with both black buttons around the USB plugs ("RST" and "BOOT") while these dots are displayed. Often you must press both buttons then release the left one then the right one and then it starts to flash:

![image](https://github.com/user-attachments/assets/851b0aac-b9a4-41ba-b701-b178e224d7cc)

If so, you'll need to do it twice as the esptool is run twice.

### ESP32 S3 N16R8

![ESP32S3](https://github.com/user-attachments/assets/b610741a-4f3d-4cd9-97c6-9d8a84fd2e77)

As shown above, the device must be connected with the left plug (known as CDC). You shouldn't have any issue when flashing, no need to play with any button.  

## Autodetecting devices

When an ESP32 has no ZeDMD firmware, it can't be recognized, above all the ESP32 S3 through the CDC plug. So all the devices listed with a "COM" in the Windows device manager, section "Ports (COM & LPT)" will be shown in the program as any of them COULD be an ESP32.  
For original ESP32, 


