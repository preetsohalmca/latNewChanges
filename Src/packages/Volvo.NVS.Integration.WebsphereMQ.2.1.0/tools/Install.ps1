param($installPath, $toolsPath, $package, $project)

# Get the common module with the help functionality
Import-Module (Join-Path $toolsPath OpenNuGetHelp.psd1)

# Run the package main help page
Open-NuGetHelp "http://teamplace.volvo.com/sites/volvoit-dotNET/SAD/NVS%20Integration%20Library.aspx"
