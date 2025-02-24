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
The ZeDMD firmware accepts Lilygo S3 devices too.

## Autodetecting devices

![image](https://github.com/user-attachments/assets/817c186d-82aa-4dff-a602-9b7d9d63327d)

If you look at the image above, there are 3 shown devices. All the devices connected to the PC via a USB COM port are listed here. You can check the Windows device manager in "Ports (COM & LPT)" to see them too.  
In the example above, the one declared as "Stock ESP32" is an original one and the third one declared as "Unknown" on COM5 is a S3, both with no ZeDMD firmware. The first on COM1 is just another device, not an ESP32.  

As you see, the ESP32 S3 is NOT recognized as a "S3", because there is no way to know it. So to be sure that it will receive a S3 firmware when flashing, you must **double click** on the "no" of its "S3" column that will be changed to "yes".
Do the same if your device is a Lilygo.

![image](https://github.com/user-attachments/assets/3178c1bb-c3cd-4eff-9a1f-412c859a5bcf)

Above you see the result of flashing both devices with the firmware v5.1.7. Now communication with the ZeDMD is possible and all the info are up to date.  
  
Caution: Firmware **v5.1.7+** is needed for the updater to fully communicate with the device.

## Flashing the selected device

![image](https://github.com/user-attachments/assets/e3b454b1-8219-4818-b1ec-ced68459166a)

You have 2 different ways to flash your device:
- Click on the "Flash from a file" button and browse for a specific "ZeDMD.bin" firmware file you have on your computer
- Choose which released version available on the official Github and what resolution your panels are before clicking the "Download and flash" button

After flashing a ZeDMD, the device list is automatically rescanned.

## Changing the parameters of the selected device

![image](https://github.com/user-attachments/assets/195d2f8a-6832-4237-a0bd-3f7fcd29189d)

There are different parameters that can be changed if you have issues with your device. I suggest you have a look at the official ZeDMD Github in the [description table here](https://github.com/PPUC/ZeDMD/blob/main/README.md#advanced-settings).  
Especially, 2 values deserve your attention to get the smoothest animations:
- If your ZeDMD is connecting via USB, we recommend that you have a look at the "USB Package Size". This value should be the highest possible according to your hardware (ESP32/S3, cable quality, etc...), so always start from a low value, then increase as long as you have no issue.
- Whatever the connection mode, the panel "Minimum refresh rate" is important too. As written in the ZeDMD Github readme: "The default of 30Hz is very low. Try to increase it. 60Hz is a good value for ZeDMD HD, 90Hz for ZeDMD."
Once you have changed all the parameters you need, click "Set new parameters" and they'll be modified. As this takes a lot of time, the devices connected are not automatically rescanned, you can do it by yourself.
