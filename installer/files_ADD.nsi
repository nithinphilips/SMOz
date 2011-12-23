
SetOutPath "$INSTDIR"
SetOverwrite ifnewer
File "..\build\bin\SMOz-0.8.0\COPYING"
File "..\build\bin\SMOz-0.8.0\MagicLocalLibrary.dll"
File "..\build\bin\SMOz-0.8.0\README"
File "..\build\bin\SMOz-0.8.0\SMOz.exe"
File "..\build\bin\SMOz-0.8.0\Template.ini"
File "..\build\bin\SMOz-0.8.0\XPTable.dll"

SetOutPath "$INSTDIR\de"
SetOverwrite ifnewer
File "..\build\bin\SMOz-0.8.0\de\SMOz.resources.dll"

SetOutPath "$INSTDIR\de-de"
SetOverwrite ifnewer
File "..\build\bin\SMOz-0.8.0\de-de\SMOz.resources.dll"

SetOutPath "$INSTDIR\ml-IN"
SetOverwrite ifnewer
File "..\build\bin\SMOz-0.8.0\ml-IN\SMOz.resources.dll"
