$msi_file_name = "iisexpress_x86_fr-FR.msi"
$download_url = "https://download.microsoft.com/download/C/E/8/CE8D18F5-D4C0-45B5-B531-ADECD637A1AA/Dev14%20Update%201%20MSIs/$msi_file_name"

(New-Object System.Net.WebClient).DownloadFile($download_url, "C:\Windows\Temp\$msi_file_name")
&msiexec /i "C:\Windows\Temp\$msi_file_name" /qb

