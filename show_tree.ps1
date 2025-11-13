param(
    [string]$Path = "."
)

function Show-Tree {
    param(
        [string]$Path,
        [string]$Prefix = ""
    )

    # Get directories first
    $dirs = Get-ChildItem -Path $Path -Directory | Sort-Object Name
    foreach ($dir in $dirs) {
        Write-Host "$Prefix+--$($dir.Name)"
        Show-Tree -Path $dir.FullName -Prefix ("$Prefix|   ")
    }

    # Get files, ignore .meta
    $files = Get-ChildItem -Path $Path -File | Where-Object { $_.Extension -ne ".meta" } | Sort-Object Name
    foreach ($file in $files) {
        Write-Host "$Prefix|   $($file.Name)"
    }
}

Write-Host "$Path"
Show-Tree -Path $Path
