@echo off
rem @ suppresses echo command from being echoed, and then disables echoing in this script.

rem Unturned will run as a dedicated server if +InternetServer/ServerId or +LanServer/ServerId are found on the command-line.

rem ServerId differentiates multiple servers running from the same installation, and a directory for each server's savedata
rem and configuration will be created in the Servers/ServerId directory. Startup commands can be specified in the
rem ServerId/Server/Commands.dat file, or on the command-line with -CommandName/Arg0/Arg1/Arg# before +InternetServer/LanServer.

rem Note for the server to be visible on the in-game internet server list you will need to set a Game Server Login Token (GSLT):
rem https://github.com/SmartlyDressedGames/U3-Docs/blob/master/GameServerLoginTokens.md

rem Running multiple servers simultaneously requires specifying different ports using the Port command. Each server uses two
rem consecutive ports. The first is for server list queries, and the second for in-game traffic. Recommended port values are
rem 27015 for the first server, 27017 for the second server, 27019 for the third server, so on and so forth.
rem These can be set as a line in Commands.dat e.g. "Port 27017" (without quotes) or on the command-line "-Port/27019" (without quotes).

rem Other common startup commands to use in Commands.dat are:
rem Name XYZ - Advertise server as "XYZ", or "Unturned" if left unset.
rem Owner SteamId - Allows player to invoke admin commands in chat e.g. "/give 132" to spawn a Dragonfang.
rem Password XYZ - Requires password "XYZ" to join server. Note that password is only SHA1 hashed, so don't use the same password anywhere else.
rem Perspective Both - Can be set to "First", "Third", "Both", or "Vehicle" to change camera options.
rem Cheats - Allows admins to invoke cheat commands like spawning items or vehicles from the chat.

rem Useful commands to use from chat or console while server is running are:
rem Help XYZ - List all commands, or describe XYZ command.
rem Shutdown - Save and cleanly exit server process.

rem For more information read the official server hosting documentation here:
rem https://github.com/SmartlyDressedGames/U3-Docs/blob/master/ServerHosting.md

rem %~dp0 expands to the path to this script's directory, allowing it to be called from a different working directory.
rem ServerHelper.bat acts as an intermediary for any future required Unity arguments.
rem Custom scripts can pass additional arguments to ServerHelper.bat e.g. "-Port/27017 +InternetServer/My Server" (without quotes).
rem By default this script will launch a LAN server and save to the Servers/Example directory.
start "" "%~dp0ServerHelper.bat" +LanServer/MundoRP
exit
