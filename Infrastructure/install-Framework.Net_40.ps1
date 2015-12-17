$msi_file_name = "dotNetFx40_Full_x86_x64.exe"
$download_url = "https://download.microsoft.com/download/9/5/A/95A9616B-7A37-4AF6-BC36-D6EA96C8DAAE/$msi_file_name"

(New-Object System.Net.WebClient).DownloadFile($download_url, "C:\Windows\Temp\$msi_file_name")
&"C:\Windows\Temp\$msi_file_name" /passive /norestart

