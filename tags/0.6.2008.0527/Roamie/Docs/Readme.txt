=======================
Roamie readme
© 2006, Virtuoso
tomasdeml@msn.com
ICQ 177 147 220
GMT+1h
=======================

=== GENERAL INFO ===

I wanted to demonstrate how easy and fast is to write a managed plugin for Miranda using my first plugin, 
Hyphen (formerly Midas, http://forums.miranda-im.org/showthread.php?p=51301).

I've created Roamie, a plugin that can sync your local database with a remote one. 
It compresses and encrypts a database for you and uploads it to a provided ftp location. 
Next time, on another computer, it can download it for you, decrypt, decompress and load so you can have all your history, 
contacts & settings with you.

Let's say you have a PC at home and notebook with, let's say a wifi access. 
At home, you can tell Roamie to upload your db to your ftp location when Miranda exits. On your notebook, 
you can tell Roamie to download the database, load it and when you're finished, it's uploaded back.

Roamie relies on Hyphen, so you need .NET FX 2.0 installed but I hope it's not such a complication once installed...


=== HOW IT WORKS ===
Roamie works as a proxy between Miranda and your db plugin, it transparently provides the plugin with your database file.


=== HOW TO INSTALL ===
Just be sure you have .NET FX 2.0 installed (get it from MS website: http://www.microsoft.com/downloads/d...displaylang=en), 
then unpack the archive into your miranda directory.

If you have multiple db plugins installed, then rename your favourite one (for example dbx_3x.dll) to *.dbx 
(for example dbx_3x.dbx). This is not neccessary if you have only one plugin installed, Roamie will pick it herself.

There are 3 files:
- hyphen.dll (.net - miranda glue)
- dbx_roamie.dll (.net - miranda special glue)
- dbx_roamie.master.dll (roamie plugin itself)

=== LOGGING ===
To enable logging, insert these elements under the configuration tag in miranda32.exe.config:

<system.diagnostics>
   <switches>
      <add name="RoamieTracing" value="Verbose" />
   </switches>
</system.diagnostics>

Instead of Verbose, you can set Info, Warning and Error levels.

=== PRIVACY ===
- Your db is always encrypted with a database password you specify.
- All the information stored on your PC by Roamie (ftp user name, password & db password) are encrypted using the DPAPI 
for your Windows account so only your account can decrypt these information. This is done automatically for your convenience.

=== DISCLAIMER ===
It's just a beta, so don't forget to backup your db!

Bugs, thoughts, questions? Just mail me...