$strAppConfigFile = "$env:USERPROFILE\Documents\IISExpress\config\applicationhost.config"

If (-Not(Test-Path $strAppConfigFile)) {
	Start-Process "$env:ProgramFiles\IIS Express\iisexpress.exe" -workingdirectory "$env:ProgramFiles\IIS Express" -WindowStyle Hidden -ErrorAction SilentlyContinue
}

&"$env:ProgramFiles\IIS Express\appcmd.exe" add site /name:PackerSite /bindings:"http/*:12345:localhost" /physicalPath:"$env:USERPROFILE\tmp"


