$strAppConfigFile = "$env:USERPROFILE\Documents\IISExpress\config\applicationhost.config"

If (-Not(Test-Path $strAppConfigFile)) {
	Start-Process "$env:ProgramFiles\IIS Express\iisexpress.exe" -workingdirectory "$env:ProgramFiles\IIS Express" -WindowStyle Hidden -ErrorAction SilentlyContinue
}

&"$env:ProgramFiles\IIS Express\appcmd.exe" add site /name:PackerSite /bindings:"http/*:12345:*" /physicalPath:"$env:USERPROFILE\tmp"

netsh.exe advfirewall firewall add rule name="Open Port 12345" dir=in action=allow protocol=TCP localport=12345
netsh http add urlacl url=http://*:12345/ user="Tout le monde"



