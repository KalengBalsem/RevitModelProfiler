; -- Inno Setup Script for Revit Model Profiler --

[Setup]
AppName=Revit Model Profiler
AppVersion=1.0
; Install to a dedicated folder in Program Files
DefaultDirName={userappdata}\Autodesk\Revit\Addins\
DisableDirPage=yes
DefaultGroupName=Revit Model Profiler
UninstallDisplayIcon={app}\RevitModelProfiler.dll
OutputBaseFilename=RevitModelProfiler_Setup_v1.0
Compression=lzma2
SolidCompression=yes

[Types]
Name: "custom"; Description: "Full Installation"; Flags: iscustom

[Components]
Name: "Revit2025"; Description: "Install RevitModelProfiler for Revit 2025"; Types: custom
Name: "Revit2026"; Description: "Install RevitModelProfiler for Revit 2026"; Types: custom

[Files]
; ================= REVIT 2025 =================
; 1. The Manifest (Goes to Main Folder)
Source: ".\RevitModelProfiler.addin"; DestDir: "{userappdata}\Autodesk\Revit\Addins\2025"; Components: Revit2025; Flags: ignoreversion

; 2. The DLL (Goes to Subfolder)
; NOTE: Ensure this source path points to your 2025-compiled DLL
Source: "..\..\bin\Release\net8.0-windows\RevitModelProfiler.dll"; DestDir: "{userappdata}\Autodesk\Revit\Addins\2025\RevitModelProfiler"; Components: Revit2025; Flags: ignoreversion

; ================= REVIT 2026 =================
; 1. The Manifest (Goes to Main Folder)
Source: ".\RevitModelProfiler.addin"; DestDir: "{userappdata}\Autodesk\Revit\Addins\2026"; Components: Revit2026; Flags: ignoreversion

; 2. The DLL (Goes to Subfolder)
; NOTE: Ensure this source path points to your 2026-compiled DLL
Source: "..\..\bin\Release\net8.0-windows\RevitModelProfiler.dll"; DestDir: "{userappdata}\Autodesk\Revit\Addins\2026\RevitModelProfiler"; Components: Revit2026; Flags: ignoreversion

[Icons]
Name: "{group}\Revit Model Profiler Help"; Filename: "https://github.com/KalengBalsem/RevitModelProfiler"
Name: "{group}\Uninstall Revit Model Profiler"; Filename: "{uninstallexe}"