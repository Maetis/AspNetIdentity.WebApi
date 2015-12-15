$msi_file_name = "iisexpress_8_0_RTM_x86_fr-FR.msi"
$download_url = "https://download.microsoft.com/download/C/B/A/CBAD2E20-E2A5-40B4-9A3F-5CABFF5456D2/$msi_file_name"

(New-Object System.Net.WebClient).DownloadFile($download_url, "C:\Windows\Temp\$msi_file_name")
&msiexec /i "C:\Windows\Temp\$msi_file_name" /qb

