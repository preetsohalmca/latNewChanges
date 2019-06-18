
# Opens a new windows in Visual Studio navigating into a given help url
function Open-NuGetHelp
{
    Param(
        [string]$url
    )

    try
    {
        # Read the current window caption
        [string]$caption = $DTE.ActiveWindow.Caption

        # Process only if installed from the UI and not from the powershell commandline console
        if ($DTE.ActiveWindow.Caption -ne "Package Manager Console")
        {
            $DTE.ItemOperations.Navigate($url) | Out-Null
        }

    }
    catch
    {
        # Ignore all the errors. If not possible to open a help window just do not care
    }
}

Export-ModuleMember -function Open-NuGetHelp
