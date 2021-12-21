
# https://stackoverflow.com/a/44234056
function Get-UrlStatusCode([string] $Url)
{
    try
    {
        $req = Invoke-WebRequest -Uri $Url -UseBasicParsing -DisableKeepAlive
        $req.StatusCode
    }
    catch
    {
        0
    }
}

function PollServer()
{
    Get-UrlStatusCode "https://localhost:7213/material/1"
}

while (PollServer -ne 401)
{
    Start-Sleep -Seconds 5
}

Start-Process "https://localhost:7213"


