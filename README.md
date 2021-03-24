# CANFinder
A tool for debugging SLKCAN log files.

The example code for the SLKCAN board provides a means to stream CANBUS data from the serial port.
This data can be monitored in the Arduino IDE serial terminal window and then copied into a logfile.

This application is designed to work with that log file in order to help decoding canbus messages observed during the log. 

- Provides a list of all observed CAN ids. 
- When selecting an ID all messages observed are populated.
- Data is displayed as hex and decimal or just decimal to make it easy to copy to a spreadsheet program.
- CAN messages in which data changed during the log will have columns highlighted for the data bytes that changed. 

This app was quickly put together and does not contain much in the way of error checking.

