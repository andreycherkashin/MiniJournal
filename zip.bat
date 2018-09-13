rd server /s /q
del server.zip /q
xcopy WcfService server\WcfService /i /e
xcopy WinService server\WinService /i /e
powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory('server', 'server.zip'); }"
rd server /s /q

rd client /s /q
del client.zip /q
xcopy WcfService client\CmdLet /i /e
xcopy WinService client\WpfClient /i /e
powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory('client', 'client.zip'); }"
rd client /s /q