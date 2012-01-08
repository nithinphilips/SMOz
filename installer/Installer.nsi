# Notes:
#   There are two optional variables that affect the version
#   PRODUCT_VERSION and BUILD_VERSION
#    PRODUCT_VERSION: three numbers separated by a dot (.) eg: 0.0.0
#    BUILD_VERSION:   a single number denoting the build number. eg: 0
#

!define PRODUCT_NAME "SMOz"
!define PRODUCT_PUBLISHER "Nithin Philips"
!define PRODUCT_WEB_SITE "http://smoz.sourceforge.net/"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\${PRODUCT_NAME}.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
!define PRODUCT_STARTMENU_REGVAL "NSIS:StartMenuDir"
!define PRODUCT_EXECUTABLE "SMOz.WinForms.exe"                                     ; Name of IT executable file (case sensitive!)

!addplugindir "Plugins"

!ifndef PRODUCT_VERSION 
    !define PRODUCT_VERSION "0.0.0"
!endif

!ifndef BUILD_VERSION 
    !define BUILD_VERSION "0"
!endif

!ifndef OUT_FILE
    !define OUT_FILE "${PRODUCT_NAME}-${PRODUCT_VERSION}-setup.exe"
!endif

SetCompressor lzma
RequestExecutionLevel admin

;#######################################################################
;# FileInfos                                                           #
;#######################################################################

; These fields add properties to the generated installer exe.
VIProductVersion                "${PRODUCT_VERSION}.${BUILD_VERSION}"
VIAddVersionKey Comments        "${PRODUCT_NAME} Installer Built on ${__TIMESTAMP__}"
VIAddVersionKey SpecialBuild    "No"
VIAddVersionKey PrivateBuild    "No"
VIAddVersionKey ProductName     "${PRODUCT_NAME}"
VIAddVersionKey ProductVersion  "${PRODUCT_VERSION}.${BUILD_VERSION}"
VIAddVersionKey CompanyName     "${PRODUCT_PUBLISHER}"
VIAddVersionKey LegalCopyright  "(C) 2012 ${PRODUCT_PUBLISHER}"
VIAddVersionKey FileVersion     "${PRODUCT_VERSION}.${BUILD_VERSION}"
VIAddVersionKey LegalTrademarks "All Rights Reserved"
VIAddVersionKey FileDescription "Installer for ${PRODUCT_NAME}"

!include "Include\General.nsh"                                          ; CreateInternetShortcut and Dumplog macros
!include "Include\UAC.nsh"
!include "MUI.nsh"

!define DOT_MAJOR 3
!define DOT_MINOR 5
!include "include\IsDotNetInstalled.nsh"

; MUI 1.67 compatible ------
!include "MUI.nsh"

;-----------------------------------------------------------------------
; Theme (Orange Vista)
;-----------------------------------------------------------------------
; Sets the theme path
!define OMUI_THEME_PATH "Contrib\MUI Orange Vista Theme\Clean"
; Icons
!define MUI_ICON "${OMUI_THEME_PATH}\installer-nopng.ico"
!define MUI_UNICON "${OMUI_THEME_PATH}\uninstaller-nopng.ico"
; MUI Settings / Header
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_RIGHT
!define MUI_HEADERIMAGE_BITMAP "${OMUI_THEME_PATH}\header-r.bmp"
!define MUI_HEADERIMAGE_UNBITMAP "${OMUI_THEME_PATH}\header-r-un.bmp"
; MUI Settings / Wizard
!define MUI_WELCOMEFINISHPAGE_BITMAP "${OMUI_THEME_PATH}\wizard.bmp"
!define MUI_UNWELCOMEFINISHPAGE_BITMAP "${OMUI_THEME_PATH}\wizard-un.bmp"

;-----------------------------------------------------------------------
; MUI Settings
;-----------------------------------------------------------------------
!define MUI_ABORTWARNING
; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE "gpl.txt"
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Start menu page
var ICONS_GROUP
!define MUI_STARTMENUPAGE_NODISABLE
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "${PRODUCT_NAME}"
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "${PRODUCT_STARTMENU_REGVAL}"
!insertmacro MUI_PAGE_STARTMENU Application $ICONS_GROUP
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\${PRODUCT_EXECUTABLE}"
!define MUI_FINISHPAGE_RUN_TEXT "Run ${PRODUCT_NAME}"
; !define MUI_FINISHPAGE_RUN_FUNCTION RunAutoConf
!define MUI_FINISHPAGE_LINK "Visit Home Page"
!define MUI_FINISHPAGE_LINK_LOCATION "${PRODUCT_WEB_SITE}"
!define MUI_FINISHPAGE_NOREBOOTSUPPORT
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"

; Reserve files
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS

; MUI end ------


;#######################################################################
;# Installer Info                                                      #
;#######################################################################
Name "${PRODUCT_NAME}"
OutFile "${OUT_FILE}"
InstallDir "$PROGRAMFILES\${PRODUCT_NAME}"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show
BrandingText "Installer for ${PRODUCT_NAME} v${PRODUCT_VERSION}"

Section "Application" SEC01

  SetShellVarContext all

  !include "files_ADD.nsi"

  CreateDirectory "$SMPROGRAMS\$ICONS_GROUP"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\${PRODUCT_NAME} (Command Line).lnk" "$INSTDIR\SMOz.exe"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\${PRODUCT_NAME} Documentation.lnk" "$INSTDIR\SMOzdoc.chm"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\${PRODUCT_NAME} Documentation (pdf).lnk" "$INSTDIR\SMOz.pdf"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\${PRODUCT_NAME} Programmer's Documentation.lnk" "$INSTDIR\libSmoz.chm"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\${PRODUCT_NAME}.lnk" "$INSTDIR\${PRODUCT_EXECUTABLE}"
  ; TODO: Website icon is actually created for the user not the system, like everything else
  !insertmacro CreateInternetShortcut "$SMPROGRAMS\$ICONS_GROUP\Visit ${PRODUCT_NAME} Website" "${PRODUCT_WEB_SITE}" "" ""
SectionEnd

Section Uninstall

  SetShellVarContext all

  ReadRegStr $ICONS_GROUP ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "${PRODUCT_STARTMENU_REGVAL}"

  Delete "$INSTDIR\uninst.exe"
  !include "files_REM.nsi"

  Delete "$SMPROGRAMS\$ICONS_GROUP\${PRODUCT_NAME} (Command Line).lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\${PRODUCT_NAME} Documentation.lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\${PRODUCT_NAME} Documentation (pdf).lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\${PRODUCT_NAME} Programmer's Documentation.lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\${PRODUCT_NAME}.lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\Visit ${PRODUCT_NAME} Website.url"
  Delete "$SMPROGRAMS\$ICONS_GROUP\Uninstall ${PRODUCT_NAME}.lnk"
  RMDir  "$SMPROGRAMS\$ICONS_GROUP"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd

Function .onInit
  Call IsDotNETInstalled
FunctionEnd

Section -AdditionalIcons
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\Uninstall ${PRODUCT_NAME}.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\${PRODUCT_EXECUTABLE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\SMOz.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "${PRODUCT_STARTMENU_REGVAL}" "$ICONS_GROUP"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd
