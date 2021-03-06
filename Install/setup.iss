; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{A1B86A6C-07E4-4CE2-AD35-EEF6440FC6CC}
AppName=ExUaTorrents
AppVersion=2.1.0
;AppVerName=Pages Searcher 1.0
AppPublisherURL=http://www.softez.pp.ua/
AppSupportURL=http://www.softez.pp.ua/
AppUpdatesURL=http://www.softez.pp.ua/
DefaultDirName={pf}\ExUaTorrents
DefaultGroupName=ExUaTorrents
AllowNoIcons=yes
LicenseFile=D:\Progs\license_freeware.txt
OutputDir=bin\
OutputBaseFilename=exuatorrents_install
Compression=lzma2/ultra64
SolidCompression=true
InternalCompressLevel=ultra

[Languages]
Name: russian; MessagesFile: compiler:Languages\Russian.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked
Name: quicklaunchicon; Description: {cm:CreateQuickLaunchIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: Release\*; DestDir: {app}; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: {group}\ExUa Torrents; Filename: {app}\ExUa Torrents.exe
;Name: {group}\Assistant WebMaster Help; Filename: {app}\AssistantWebMasterHelp.exe
Name: "{group}\{cm:ProgramOnTheWeb,ExUa Torrents}"; Filename: "http://softez.pp.ua/"
Name: {group}\{cm:ProgramOnTheWeb,ExUa Torrents}; Filename: http://www.softez.pp.ua/
Name: {group}\{cm:UninstallProgram,ExUa Torrents}; Filename: {uninstallexe}
Name: {commondesktop}\ExUa Torrents; Filename: {app}\ExUa Torrents.exe; Tasks: desktopicon
Name: {userappdata}\Microsoft\Internet Explorer\Quick Launch\ExUa Torrents; Filename: {app}\ExUa Torrents.exe; Tasks: quicklaunchicon

[Run]
Filename: {app}\ExUa Torrents.exe; Description: {cm:LaunchProgram,ExUaTorrents}; Flags: nowait postinstall skipifsilent
Filename: "http://www.softez.pp.ua/"; Flags: shellexec

[Messages]
BeveledLabel=dredei, http://www.softez.pp.ua/

