net stop zabbixagentnet

del "%Apppath%\ZabbixAgent_old.exe"
move "%Apppath%\ZabbixAgent.exe" "%Apppath%\ZabbixAgent_old.exe"
move "%Apppath%\ZabbixAgent_new.exe" "%Apppath%\ZabbixAgent.exe"

net start zabbixagentnet