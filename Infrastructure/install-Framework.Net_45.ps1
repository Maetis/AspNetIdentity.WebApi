$msi_file_name = "dotNetFx45_Full_setup.exe"
$download_url = "https://download.microsoft.com/download/B/A/4/BA4A7E71-2906-4B2D-A0E1-80CF16844F5F/$msi_file_name"

(New-Object System.Net.WebClient).DownloadFile($download_url, "C:\Windows\Temp\$msi_file_name")
&"C:\Windows\Temp\$msi_file_name" /passive /norestart

