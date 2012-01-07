;--------------------------------
;Include Modern UI

  !include "MUI.nsh"

;--------------------------------
;General

  ;Name and file
  Name "Orange UI Test"
  OutFile "example.exe"

  ;Default installation folder
  InstallDir "$PROGRAMFILES\Orange UI Test"

  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\Orange UI Test" ""

;--------------------------------
;Interface Settings

  !define MUI_ABORTWARNING
 
  !define OMUI_THEME "CD-Clean"
  
; MUI Settings / Icons

; In the moment of writing this, NSIS don't support well Vista icons with PNG compression.
; We provide both, compressed and uncompressed (-nopng) icons.

	!define MUI_ICON "${NSISDIR}\Contrib\MUI Orange Vista Theme\${OMUI_THEME}\installer-nopng.ico"
	!define MUI_UNICON "${NSISDIR}\Contrib\MUI Orange Vista Theme\${OMUI_THEME}\uninstaller-nopng.ico"
 
; MUI Settings / Header
	!define MUI_HEADERIMAGE
	!define MUI_HEADERIMAGE_RIGHT
	!define MUI_HEADERIMAGE_BITMAP "${NSISDIR}\Contrib\MUI Orange Vista Theme\${OMUI_THEME}\header-r.bmp"
	!define MUI_HEADERIMAGE_UNBITMAP "${NSISDIR}\Contrib\MUI Orange Vista Theme\${OMUI_THEME}\header-r-un.bmp"
 
; MUI Settings / Wizard		
	!define MUI_WELCOMEFINISHPAGE_BITMAP "${NSISDIR}\Contrib\MUI Orange Vista Theme\${OMUI_THEME}\wizard.bmp"
	!define MUI_UNWELCOMEFINISHPAGE_BITMAP "${NSISDIR}\Contrib\MUI Orange Vista Theme\${OMUI_THEME}\wizard-un.bmp"  

;--------------------------------
;Pages

  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "License.txt"
  !insertmacro MUI_PAGE_COMPONENTS
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  !insertmacro MUI_PAGE_FINISH

  !insertmacro MUI_UNPAGE_WELCOME
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  !insertmacro MUI_UNPAGE_FINISH

;--------------------------------
;Languages

  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "Dummy Section" SecDummy

  SetOutPath "$INSTDIR"

  ;ADD YOUR OWN FILES HERE...

  ;Store installation folder
  WriteRegStr HKCU "Software\Orange UI Test" "" $INSTDIR

  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecDummy ${LANG_ENGLISH} "A test section."

  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;Uninstaller Section

Section "Uninstall"

  ;ADD YOUR OWN FILES HERE...

  Delete "$INSTDIR\Uninstall.exe"

  RMDir "$INSTDIR"

  DeleteRegKey /ifempty HKCU "Software\Modern UI Test"

SectionEnd
